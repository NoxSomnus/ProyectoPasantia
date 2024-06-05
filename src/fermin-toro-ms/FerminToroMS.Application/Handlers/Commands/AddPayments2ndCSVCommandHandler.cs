using FerminToroMS.Application.Commands;
using FerminToroMS.Application.Requests;
using FerminToroMS.Application.Responses;
using FerminToroMS.Core.Database;
using FerminToroMS.Core.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerminToroMS.Application.Handlers.Commands
{
    internal class AddPayments2ndCSVCommandHandler : IRequestHandler<AddPayments2ndCSVCommand, AddPayments2ndCSVResponse>
    {
        private readonly IFerminToroDbContext _dbContext;
        private readonly ILogger<AddPayments2ndCSVCommandHandler> _logger;

        /// <summary>
        /// Constructor de la clase AddPayments2ndCSVCommandHandler.
        /// </summary>
        /// <param name="dbContext">El contexto de la base de datos que se utilizará para buscar el usuario.</param>
        /// <param name="logger">El objeto ILogger que se utilizará para registrar la actividad del manejador del comando.</param>
        public AddPayments2ndCSVCommandHandler(IFerminToroDbContext dbContext, ILogger<AddPayments2ndCSVCommandHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        /// <summary>
        /// Manejador del comando que registra los pagados cargados de un csv.
        /// </summary>
        /// <param name="request">El comando AddPayments2ndCSVCommand que especifica los datos de los pagos.</param>
        /// <param name="cancellationToken">El token de cancelación que puede detener la operación en cualquier momento.</param>
        /// <returns>Un bool que contiene información si la operacion fue exitosa o no.</returns>
        public Task<AddPayments2ndCSVResponse> Handle(AddPayments2ndCSVCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request == null)
                {
                    _logger.LogWarning("AddPayments2ndCSVCommandHandler.Handle: Request null.");

                    throw new ArgumentNullException(nameof(request));

                }
                else
                {
                    return HandleAsync(request);
                }
            }
            catch
            {
                _logger.LogWarning("AddPayments2ndCSVCommandHandler.Handle: ArgumentNullException");
                throw;
            }
            throw new NotImplementedException();
        }

        /// <summary>
        /// Método asincronico que maneja el registro de los pagos cargados de un csv.
        /// </summary>
        /// <param name="request">El comando AddPayments2ndCSVCommand que especifica los datos de los pagos.</param>
        /// <returns>Un objeto GeneralResponse que contiene información de si la operacion de registro fue exitosa o no.</returns>
        private async Task<AddPayments2ndCSVResponse> HandleAsync(AddPayments2ndCSVCommand request)
        {
            var transaction = _dbContext.BeginTransaction();
            int linea = 1;
            try
            {
                var response = new AddPayments2ndCSVResponse
                {
                    NroInscripcion_Cedula = new List<string>()                    
                };
                _logger.LogInformation("AddPayments1stCSVCommandHandler.HandleAsync");
                foreach (var payment in request.payments)
                {

                    var inscription = _dbContext.Inscripciones.Include(i=>i.Estudiante).FirstOrDefault(i => i.NroInscripcion == payment.NroInscripcion);
                    if (inscription != null)
                    {
                        if (inscription.Estudiante.Cedula != payment.Cedula) 
                        {
                            Console.WriteLine(linea);
                            response.NroInscripcion_Cedula.Add("CSV: " + payment.Cedula + " - " + payment.NroInscripcion
                                + " - Registrado: " + inscription.Estudiante.Cedula+"-"+inscription.NroInscripcion);
                            Console.WriteLine(response.NroInscripcion_Cedula);
                        }
                        else
                        {
                            try 
                            { 
                                ReadString(payment, inscription);
                                linea++;
                            }

                            catch(Exception ex)
                            { 
                                Console.WriteLine(payment);
                                Console.WriteLine(linea);
                            }
                            
                        }
                    }

                }
                await _dbContext.SaveEfContextChanges("APP");
                transaction.Commit();
                _logger.LogInformation("AddPayments1stCSVCommandHandler.HandleAsync {Response}", true);
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(linea);
                _logger.LogError(ex, "Error AddPayments1stCSVCommandHandler.HandleAsync. {Mensaje}", ex.Message);
                transaction.Rollback();
                throw;
            }
        }

        private async void ReadString(AddPayments2ndCSVRequest _payment, InscripcionEntity inscription) 
        {
            if (_payment.ReciboFacturaMonto == "") 
            {
                return;
            }
            var payment = _dbContext.Pagos.Include(p=>p.MetodoPago).FirstOrDefault(p=>p.InscripcionId == inscription.Id);
            if (payment == null)
            {
                Console.WriteLine(_payment);
                return;
            }
            else
            {
                var isFirst = true;
                var first = true;
                string[] pagos = _payment.ReciboFacturaMonto.Split('@');
                foreach (var pago in pagos)
                {
                    if (first) 
                    {
                        first = false;
                        continue;
                    }
                    double monto = 0;
                    string[] partes = pago.Split('/');
                    string parte = partes[0];
                    char Recb_o_Fact = parte[0];
                    if (Recb_o_Fact == '#')
                    {
                        if (isFirst)
                        {
                            isFirst = false;
                            payment.NroRecibo = int.Parse(partes[0].Substring(1));
                            if (partes[1] != "") 
                            {
                                payment.Monto = double.Parse(partes[1]);
                            }
                                         
                            _dbContext.Pagos.Update(payment);                            
                        }
                        else 
                        {
                            if (partes[1] != "") 
                            {
                                monto = double.Parse(partes[1]);
                            }
                            var newPayment = new PagoEntity
                            {
                                Id = Guid.NewGuid(),
                                Fecha = payment.Fecha,
                                PorCuotas = _payment.Cuota,
                                EnDivisa = payment.EnDivisa,
                                EsJuridico = payment.EsJuridico,
                                NroRecibo = int.Parse(partes[0].Substring(1)),
                                PrimeraCuotaId = payment.Id,
                                EmpresaJuridicaId = payment.EmpresaJuridicaId != null ? payment.EmpresaJuridicaId : null,
                                MetodoPagoId = payment.MetodoPagoId,
                                Monto = monto,
                                Comentarios = _payment.Comentario,
                                InscripcionId = payment.InscripcionId
                            };                            
                            _dbContext.Pagos.Add(newPayment);                            
                        }
                    }
                    if (Recb_o_Fact == '%') 
                    {
                        if (isFirst)
                        {
                            payment.NroFactura = int.Parse(partes[0].Substring(1));
                            if (partes[1] != "")
                            {
                                monto = double.Parse(partes[1]);
                            }
                            payment.Monto = monto;
                            isFirst = false;                            
                            _dbContext.Pagos.Update(payment);
                        }
                        else 
                        {
                            if (partes[1] != "")
                            {
                                monto = double.Parse(partes[1]);
                            }
                            var newPayment = new PagoEntity
                            {
                                Id = Guid.NewGuid(),
                                Fecha = payment.Fecha,
                                PorCuotas = _payment.Cuota,
                                EnDivisa = payment.EnDivisa,
                                EsJuridico = payment.EsJuridico,
                                NroFactura = int.Parse(partes[0].Substring(1)),
                                PrimeraCuotaId = payment.Id,
                                EmpresaJuridicaId = payment.EmpresaJuridicaId != null ? payment.EmpresaJuridicaId : null,
                                MetodoPagoId = payment.MetodoPagoId,
                                Monto = monto,
                                Comentarios = _payment.Comentario,
                                InscripcionId = payment.InscripcionId
                            };
                                                        
                            _dbContext.Pagos.Add(newPayment);
                        }
                    }
                }
            }
        }
    }
}
