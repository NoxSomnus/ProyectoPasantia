using FerminToroMS.Application.Commands;
using FerminToroMS.Application.Exceptions;
using FerminToroMS.Application.Requests;
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
    internal class UpdatePeriodCommandHandler : IRequestHandler<UpdatePeriodCommand, bool>
    {
        private readonly IFerminToroDbContext _dbContext;
        private readonly ILogger<UpdatePeriodCommandHandler> _logger;

        /// <summary>
        /// Constructor de la clase UpdatePeriodCommandHandler.
        /// </summary>
        /// <param name="dbContext">El contexto de la base de datos que se utilizará para buscar el usuario.</param>
        /// <param name="logger">El objeto ILogger que se utilizará para registrar la actividad del manejador del comando.</param>
        public UpdatePeriodCommandHandler(IFerminToroDbContext dbContext, ILogger<UpdatePeriodCommandHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        /// <summary>
        /// Manejador del comando que actualiza un periodo.
        /// </summary>
        /// <param name="request">El comando UpdatePeriodCommand que especifica los datos a actualizar del periodo.</param>
        /// <param name="cancellationToken">El token de cancelación que puede detener la operación en cualquier momento.</param>
        /// <returns>Un objeto bool que contiene información si la operacion fue exitosa o no.</returns>
        public Task<bool> Handle(UpdatePeriodCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request == null)
                {
                    _logger.LogWarning("UpdatePeriodCommandHandler.Handle: Request null.");

                    throw new ArgumentNullException(nameof(request));

                }
                else
                {
                    return HandleAsync(request);
                }
            }
            catch
            {
                _logger.LogWarning("UpdatePeriodCommandHandler.Handle: ArgumentNullException");
                throw;
            }
            throw new NotImplementedException();
        }

        /// <summary>
        /// Método asincronico que maneja la actualizacion de informacion de un periodo.
        /// </summary>
        /// <param name="request">El comando UpdatePeriodCommand que especifica los datos a actualizar del periodo.</param>
        /// <returns>Un objeto bool que contiene información de si la operacion de registro fue exitosa o no.</returns>
        private async Task<bool> HandleAsync(UpdatePeriodCommand request)
        {
            var transaction = _dbContext.BeginTransaction();
            try
            {
                var period = await _dbContext.Periodos.FirstOrDefaultAsync(c => c.Id == request._request.PeriodId);
                if (period == null)
                {
                    throw new IdNotFoundException("No se encontró el periodo a actualizar");
                }
                period = UpdateRequestToEntity(period, request._request);
                _dbContext.Periodos.Update(period);
                await _dbContext.SaveEfContextChanges("APP");
                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error UpdateEmployeeCommandHandler.HandleAsync. {Mensaje}", ex.Message);
                transaction.Rollback();
                throw;
            }
        }
        private PeriodoEntity UpdateRequestToEntity(PeriodoEntity period, UpdatePeriodRequest request) 
        {
            period.NombrePeriodo = request.PeriodName;
            period.Año = request.Year;
            period.MesFin = request.EndMonth;
            period.MesInicio = request.StartMonth;
            return period;
        }
    }
}
