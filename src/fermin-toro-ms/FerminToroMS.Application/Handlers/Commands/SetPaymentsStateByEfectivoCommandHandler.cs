using FerminToroMS.Application.Commands;
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
    internal class SetPaymentsStateByEfectivoCommandHandler : IRequestHandler<SetPaymentsStateByEfectivoCommand, bool>
    {
        private readonly IFerminToroDbContext _dbContext;
        private readonly ILogger<SetPaymentsStateByEfectivoCommandHandler> _logger;

        /// <summary>
        /// Constructor de la clase SetApprovedPaymentsCommandHandler.
        /// </summary>
        /// <param name="dbContext">El contexto de la base de datos que se utilizará para buscar el usuario.</param>
        /// <param name="logger">El objeto ILogger que se utilizará para registrar la actividad del manejador del comando.</param>
        public SetPaymentsStateByEfectivoCommandHandler(IFerminToroDbContext dbContext, ILogger<SetPaymentsStateByEfectivoCommandHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        /// <summary>
        /// Manejador del comando que setea los valores de estado del pago.
        /// </summary>
        /// <param name="request">El comando SetApprovedPaymentsCommand que especifica el set de los valores del codigo de cronograma.</param>
        /// <param name="cancellationToken">El token de cancelación que puede detener la operación en cualquier momento.</param>
        /// <returns>Un bool que contiene información si la operacion fue exitosa o no.</returns>
        public Task<bool> Handle(SetPaymentsStateByEfectivoCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request == null)
                {
                    _logger.LogWarning("SetPaymentsStateByEfectivoCommandHandler.Handle: Request null.");

                    throw new ArgumentNullException(nameof(request));

                }
                else
                {
                    return HandleAsync(request);
                }
            }
            catch
            {
                _logger.LogWarning("SetPaymentsStateByEfectivoCommandHandler.Handle: ArgumentNullException");
                throw;
            }
            throw new NotImplementedException();
        }

        /// <summary>
        /// Método asincronico que maneja el registro de un nuevo cronograma.
        /// </summary>
        /// <param name="request">El comando CreateScheduleCommand que especifica los datos del nuevo cronograma.</param>
        /// <returns>Un bool que contiene información de si la operacion de registro fue exitosa o no.</returns>
        private async Task<bool> HandleAsync(SetPaymentsStateByEfectivoCommand request)
        {
            var transaction = _dbContext.BeginTransaction();
            try
            {
                var payments = _dbContext.Pagos.Include(p => p.MetodoPago).ToList();
                foreach (var payment in payments)
                {
                    if (payment.MetodoPago.NombreMetodo == "Efectivo" && payment.Monto > 0) 
                    {
                        var approved = new PagosAprobadosEntity
                        {
                            PagoId = payment.Id,
                            FechaTransaccion = payment.Fecha,
                            FechaConciliacion = DateTime.Now,
                            Nombre_Empleado = "dsuarez"
                        };
                        if (payment.NroRecibo != null) 
                        {
                            approved.NroTransaccion = payment.NroRecibo.ToString();
                        }
                        else
                        {
                            if (payment.NroFactura != null)
                            {
                                approved.NroTransaccion = payment.NroFactura.ToString();
                            }
                            else 
                            {
                                approved.NroTransaccion = "Efectivo";
                            }                            
                        }
                        payment.Estado = "Aprobado";
                        _dbContext.Pagos.Update(payment);
                        _dbContext.Pagos_Aprobados.Add(approved);
                    }
                }
                await _dbContext.SaveEfContextChanges("APP");
                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error SetPaymentsStateByEfectivoCommandHandler.HandleAsync. {Mensaje}", ex.Message);
                transaction.Rollback();
                throw;
            }
        }
    }
}
