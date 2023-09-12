using FerminToroMS.Application.Exceptions;
using FerminToroMS.Application.Queries;
using FerminToroMS.Application.Responses;
using FerminToroMS.Core.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerminToroMS.Application.Handlers.Queries
{
    internal class PaymentDetailsQueryHandler : IRequestHandler<PaymentDetailsQuery, PaymentDetails>
    {
        private readonly IFerminToroDbContext _dbContext;
        private readonly ILogger<PaymentDetailsQueryHandler> _logger;

        /// <summary>
        /// Constructor de la clase PaymentDetailsQueryHandler.
        /// </summary>
        /// <param name="dbContext">El contexto de la base de datos que se utilizará para buscar el usuario.</param>
        /// <param name="logger">El objeto ILogger que se utilizará para registrar la actividad del manejador de consulta.</param>
        public PaymentDetailsQueryHandler(IFerminToroDbContext dbContext, ILogger<PaymentDetailsQueryHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        /// <summary>
        /// Manejador de la consulta que busca todos los pagos de una inscripcion.
        /// </summary>
        /// <param name="request">La consulta PaymentDetailsQuery que especifica el id de la inscripcion donde se buscara sus pagos.</param>
        /// <param name="cancellationToken">El token de cancelación que puede detener la operación en cualquier momento.</param>
        /// <returns>Una lista de todos los pagos de una inscripcion.</returns>
        public Task<PaymentDetails> Handle(PaymentDetailsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (request == null)
                {
                    _logger.LogWarning("PaymentDetailsQueryHandler.Handle: Request null.");

                    throw new ArgumentNullException(nameof(request));

                }
                else
                {
                    return HandleAsync(request);
                }
            }
            catch
            {
                _logger.LogWarning("AllInscriptionsByScheduleIdQueryHandler.Handle: ArgumentNullException");
                throw;
            }
            throw new NotImplementedException();
        }
        /// <summary>
        /// Método asincronico que maneja la consulta de todos los pagos de una inscripcion.
        /// </summary>
        /// <param name="request">La consulta PaymentDetailsQuery que especifica la busqueda de todos los pagos de una inscripcion.</param>
        /// <returns>Una lista de pagos segun una inscripcion.</returns>
        private async Task<PaymentDetails> HandleAsync(PaymentDetailsQuery request)
        {
            try
            {
                _logger.LogInformation("PaymentDetailsQueryHandler.HandleAsync");
               
                var payments = await _dbContext.Pagos
                            .Include(c => c.Inscripcion)
                            .Include(c => c.MetodoPago)
                            .Where(c => c.InscripcionId == request.InscriptionId)
                            .OrderBy(c => c.Fecha)
                            .Select(c => new PaymentsDetailsResponse
                            {
                                Mount = c.Monto,
                                NroFactura = c.NroFactura,
                                NroRecibo = c.NroRecibo,
                                PaymentMethod = c.MetodoPago.NombreMetodo,
                                PaymentDate = c.Fecha.ToString("dd/MM/yyyy"),
                                CuotaPayment = c.PorCuotas,
                                EsJuridico = c.EsJuridico,
                                EnDivisa = c.EnDivisa,
                                Comprobante = c.URLComprobante,
                                EmpresaJuridica = c.EmpresaJuridicaId,
                                Comments = c.Comentarios == null ? " " : c.Comentarios,
                            })
                            .ToListAsync();
                var response = new PaymentDetails
                {
                    ByCuota = false,
                    ModulPrice = 0,
                    Payments = payments
                };
                var inscription = _dbContext.Inscripciones
                    .Include(c => c.Cronograma)
                    .FirstOrDefault(c=>c.Id == request.InscriptionId);
                if (inscription == null) 
                {
                    throw new IdNotFoundException("Inscripcion no encontrada");
                }
                var price = _dbContext.Precios
                    .FirstOrDefault(c=>c.ModuloId == inscription.Cronograma.ModuloId
                    && c.Modalidad == inscription.Cronograma.Modalidad
                    && c.Turno == inscription.Cronograma.Turno
                    && c.Regularidad == inscription.Cronograma.Regularidad
                    && c.PorCuotas == payments.First().CuotaPayment);
                if (price == null) 
                {
                    return response;    
                }
                response.ModulPrice = price.Precio;
                response.ByCuota = price.PorCuotas;
                return response;
            }
            catch (IdNotFoundException ex) 
            {
                _logger.LogError(ex, "Error PaymentDetailsQueryHandler.HandleAsync. {Los datos ingresados no son validos}", ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error PaymentDetailsQueryHandler.HandleAsync. {Los datos ingresados no son validos}", ex.Message);
                throw;
            }
        }
    }
}
