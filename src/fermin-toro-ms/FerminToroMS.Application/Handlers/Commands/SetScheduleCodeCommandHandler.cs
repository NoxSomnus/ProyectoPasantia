using FerminToroMS.Application.Commands;
using FerminToroMS.Application.Mappers;
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
    internal class SetScheduleCodeCommandHandler : IRequestHandler<SetScheduleCodeCommand, bool>
    {
        private readonly IFerminToroDbContext _dbContext;
        private readonly ILogger<SetScheduleCodeCommandHandler> _logger;

        /// <summary>
        /// Constructor de la clase SetScheduleCodeCommandHandler.
        /// </summary>
        /// <param name="dbContext">El contexto de la base de datos que se utilizará para buscar el usuario.</param>
        /// <param name="logger">El objeto ILogger que se utilizará para registrar la actividad del manejador del comando.</param>
        public SetScheduleCodeCommandHandler(IFerminToroDbContext dbContext, ILogger<SetScheduleCodeCommandHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        /// <summary>
        /// Manejador del comando que setea los valores del codigo del programa en un cronograma.
        /// </summary>
        /// <param name="request">El comando SetScheduleCodeCommand que especifica el set de los valores del codigo de cronograma.</param>
        /// <param name="cancellationToken">El token de cancelación que puede detener la operación en cualquier momento.</param>
        /// <returns>Un bool que contiene información si la operacion fue exitosa o no.</returns>
        public Task<bool> Handle(SetScheduleCodeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request == null)
                {
                    _logger.LogWarning("SetScheduleCodeCommandHandler.Handle: Request null.");

                    throw new ArgumentNullException(nameof(request));

                }
                else
                {
                    return HandleAsync(request);
                }
            }
            catch
            {
                _logger.LogWarning("SetScheduleCodeCommandHandler.Handle: ArgumentNullException");
                throw;
            }
            throw new NotImplementedException();
        }

        /// <summary>
        /// Método asincronico que maneja el registro de un nuevo cronograma.
        /// </summary>
        /// <param name="request">El comando CreateScheduleCommand que especifica los datos del nuevo cronograma.</param>
        /// <returns>Un bool que contiene información de si la operacion de registro fue exitosa o no.</returns>
        private async Task<bool> HandleAsync(SetScheduleCodeCommand request)
        {
            var transaction = _dbContext.BeginTransaction();
            try
            {
                var schedules = _dbContext.Cronogramas.ToList();
                foreach (var schedule in schedules)
                {
                    var period = _dbContext.Periodos.FirstOrDefault(c => c.Id == schedule.PeriodoId);
                    var modul = _dbContext.Modulos.FirstOrDefault(c => c.Id == schedule.ModuloId);
                    schedule.Codigo = CronogramasMapper.ExtractCode(schedule, period.NombrePeriodo, modul.Diminutivo);
                    _dbContext.Cronogramas.Update(schedule);
                }
                await _dbContext.SaveEfContextChanges("APP");
                transaction.Commit();
                return true;
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex, "Error SetScheduleCodeCommandHandler.HandleAsync. {Mensaje}", ex.Message);
                transaction.Rollback();
                throw;
            }
        }

        
    }
}
