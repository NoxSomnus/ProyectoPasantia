using FerminToroMS.Application.Commands;
using FerminToroMS.Application.Responses;
using FerminToroMS.Core.Database;
using FerminToroMS.Core.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerminToroMS.Application.Handlers.Commands
{
    internal class AddPayments1stCSVCommandHandler : IRequestHandler<AddPayments1stCSVCommand, AddPayments1stCSVResponse>
    {
        private readonly IFerminToroDbContext _dbContext;
        private readonly ILogger<AddPayments1stCSVCommandHandler> _logger;

        /// <summary>
        /// Constructor de la clase AddPermissionCommandHandler.
        /// </summary>
        /// <param name="dbContext">El contexto de la base de datos que se utilizará para buscar el usuario.</param>
        /// <param name="logger">El objeto ILogger que se utilizará para registrar la actividad del manejador del comando.</param>
        public AddPayments1stCSVCommandHandler(IFerminToroDbContext dbContext, ILogger<AddPayments1stCSVCommandHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        /// <summary>
        /// Manejador del comando que registra los pagados cargados de un csv.
        /// </summary>
        /// <param name="request">El comando AddPayments1stCSVCommand que especifica los datos de los pagos.</param>
        /// <param name="cancellationToken">El token de cancelación que puede detener la operación en cualquier momento.</param>
        /// <returns>Un bool que contiene información si la operacion fue exitosa o no.</returns>
        public Task<AddPayments1stCSVResponse> Handle(AddPayments1stCSVCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request == null)
                {
                    _logger.LogWarning("AddPayments1stCSVCommandHandler.Handle: Request null.");

                    throw new ArgumentNullException(nameof(request));

                }
                else
                {
                    return HandleAsync(request);
                }
            }
            catch
            {
                _logger.LogWarning("AddPayments1stCSVCommandHandler.Handle: ArgumentNullException");
                throw;
            }
            throw new NotImplementedException();
        }

        /// <summary>
        /// Método asincronico que maneja el registro de los pagos cargados de un csv.
        /// </summary>
        /// <param name="request">El comando AddPayments1stCSVCommand que especifica los datos de los pagos.</param>
        /// <returns>Un objeto GeneralResponse que contiene información de si la operacion de registro fue exitosa o no.</returns>
        private async Task<AddPayments1stCSVResponse> HandleAsync(AddPayments1stCSVCommand request)
        {
            var transaction = _dbContext.BeginTransaction();

            try
            {
                var response = new AddPayments1stCSVResponse
                {
                    NroInscripcionNoRegistrados = new List<int>()
                };
                _logger.LogInformation("AddPayments1stCSVCommandHandler.HandleAsync");          
                foreach (var payment in request.payments)
                {

                    var inscription = _dbContext.Inscripciones.FirstOrDefault(i => i.NroInscripcion == payment.NroInscripcion);
                    if (inscription != null)
                    {
                        var method = _dbContext.Metodos_Pago.FirstOrDefault(m => m.NombreMetodo == payment.MetodoPago);
                        if (method != null)
                        {
                            var NewPayment = new PagoEntity
                            {
                                InscripcionId = inscription.Id,
                                Fecha = payment.FechaPago,
                                PorCuotas = payment.Cuota,
                                EnDivisa = payment.Divisa,
                                MetodoPagoId = method.Id,
                                URLComprobante = payment.URLComprobante,
                                EsJuridico = payment.Juridico,
                            };
                            if (payment.UrlRif != "")
                            {
                                var EmpresaJuridica = new DatoEmpresaJuridicaEntity
                                {
                                    URL_Rif = payment.UrlRif,
                                    Id = Guid.NewGuid()
                                };
                                NewPayment.EmpresaJuridicaId = EmpresaJuridica.Id;
                                _dbContext.Datos_Empresa_Juridica.Add(EmpresaJuridica);
                            }
                            _dbContext.Pagos.Add(NewPayment);
                        }
                    }
                    else 
                    {
                        response.NroInscripcionNoRegistrados.Add(payment.NroInscripcion);
                    }
                }
                await _dbContext.SaveEfContextChanges("APP");
                transaction.Commit();
                _logger.LogInformation("AddPayments1stCSVCommandHandler.HandleAsync {Response}", true);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error AddPayments1stCSVCommandHandler.HandleAsync. {Mensaje}", ex.Message);
                transaction.Rollback();
                throw;
            }
        }
    }
}
