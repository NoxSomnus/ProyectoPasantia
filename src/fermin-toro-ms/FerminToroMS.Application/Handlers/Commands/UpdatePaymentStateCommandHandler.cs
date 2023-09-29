using Automatonymous;
using FerminToroMS.Application.Commands;
using FerminToroMS.Application.Exceptions;
using FerminToroMS.Application.Mappers;
using FerminToroMS.Core.Database;
using FerminToroMS.Core.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerminToroMS.Application.Handlers.Commands
{
    internal class UpdatePaymentStateCommandHandler : IRequestHandler<UpdatePaymentStateCommand, bool>
    {
        private readonly IFerminToroDbContext _dbContext;
        private readonly ILogger<UpdatePaymentStateCommandHandler> _logger;

        /// <summary>
        /// Constructor de la clase UpdatePaymentStateCommandHandler.
        /// </summary>
        /// <param name="dbContext">El contexto de la base de datos que se utilizará para buscar el usuario.</param>
        /// <param name="logger">El objeto ILogger que se utilizará para registrar la actividad del manejador del comando.</param>
        public UpdatePaymentStateCommandHandler(IFerminToroDbContext dbContext, ILogger<UpdatePaymentStateCommandHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        /// <summary>
        /// Manejador del comando que actualiza un los estados de varios pagos con comprobante.
        /// </summary>
        /// <param name="request">El comando UpdatePaymentStateCommand que especifica los datos a actualizar de los pagos.</param>
        /// <param name="cancellationToken">El token de cancelación que puede detener la operación en cualquier momento.</param>
        /// <returns>Un objeto bool que contiene información si la operacion fue exitosa o no.</returns>
        public Task<bool> Handle(UpdatePaymentStateCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request == null)
                {
                    _logger.LogWarning("UpdatePaymentStateCommandHandler.Handle: Request null.");

                    throw new ArgumentNullException(nameof(request));

                }
                else
                {
                    return HandleAsync(request);
                }
            }
            catch
            {
                _logger.LogWarning("UpdatePaymentStateCommandHandler.Handle: ArgumentNullException");
                throw;
            }
            throw new NotImplementedException();
        }

        /// <summary>
        /// Método asincronico que maneja la actualizacion de los estados de los pagos con comprobante.
        /// </summary>
        /// <param name="request">El comando UpdatePaymentStateCommand que especifica los datos a actualizar de los pagos.</param>
        /// <returns>Un objeto bool que contiene información de si la operacion de registro fue exitosa o no.</returns>
        private async Task<bool> HandleAsync(UpdatePaymentStateCommand request)
        {
            var transaction = _dbContext.BeginTransaction();
            try
            {
                var employee = _dbContext.Empleados.FirstOrDefault(c=>c.Id == request._request.Empleado);
                foreach (var PaymentToUpdate in request._request.PaymentsToUpdate)
                {
                    var payment = _dbContext.Pagos.FirstOrDefault(p=>p.Id == PaymentToUpdate.PaymentId);
                    if (payment != null) 
                    {
                        payment.Monto = PaymentToUpdate.Monto;
                        payment.Estado = PaymentToUpdate.State;
                        if (payment.Estado == "Aprobado") 
                        {
                            _dbContext.Pagos_Aprobados.Add(
                                new PagosAprobadosEntity
                                {
                                    PagoId = payment.Id,
                                    Nombre_Empleado = employee.Nombre + " " + employee.Apellido,
                                    FechaConciliacion = DateTime.Now,
                                    ComprobanteIVA = PaymentToUpdate.ComprobanteIVA,
                                    FechaTransaccion =  PaymentToUpdate.TransactionDate != null ? DateTime.ParseExact(PaymentToUpdate.TransactionDate, "yyyy-MM-dd", null) : DateTime.Now,
                                    NroTransaccion = PaymentToUpdate.TransactionNumber,
                                    Correo = PaymentToUpdate.Email,
                                    TasaBCV = PaymentToUpdate.TasaBCV
                                }
                            );
                        }
                    }
                }    
                await _dbContext.SaveEfContextChanges("APP");
                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error UpdatePaymentStateCommandHandler.HandleAsync. {Mensaje}", ex.Message);
                transaction.Rollback();
                throw;
            }
        }
    }
}
