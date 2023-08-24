using FerminToroMS.Application.Commands;
using FerminToroMS.Application.Exceptions;
using FerminToroMS.Application.Mappers;
using FerminToroMS.Core.Database;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerminToroMS.Application.Handlers.Commands
{
    public class CreateScheduleCommandHandler : IRequestHandler<CreateScheduleCommand, bool>
    {
        private readonly IFerminToroDbContext _dbContext;
        private readonly ILogger<CreateScheduleCommandHandler> _logger;

        /// <summary>
        /// Constructor de la clase CreateScheduleCommandHandler.
        /// </summary>
        /// <param name="dbContext">El contexto de la base de datos que se utilizará para buscar el usuario.</param>
        /// <param name="logger">El objeto ILogger que se utilizará para registrar la actividad del manejador del comando.</param>
        public CreateScheduleCommandHandler(IFerminToroDbContext dbContext, ILogger<CreateScheduleCommandHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        /// <summary>
        /// Manejador del comando que registra los registros de un nuevo cronograma.
        /// </summary>
        /// <param name="request">El comando CreateScheduleCommand que especifica los datos a registrar del nuevo cronograma.</param>
        /// <param name="cancellationToken">El token de cancelación que puede detener la operación en cualquier momento.</param>
        /// <returns>Un bool que contiene información si la operacion fue exitosa o no.</returns>
        public Task<bool> Handle(CreateScheduleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request == null)
                {
                    _logger.LogWarning("CreateScheduleCommandHandler.Handle: Request null.");

                    throw new ArgumentNullException(nameof(request));

                }
                else
                {
                    return HandleAsync(request);
                }
            }
            catch
            {
                _logger.LogWarning("CreateScheduleCommandHandler.Handle: ArgumentNullException");
                throw;
            }
            throw new NotImplementedException();
        }

        /// <summary>
        /// Método asincronico que maneja el registro de un nuevo cronograma.
        /// </summary>
        /// <param name="request">El comando CreateScheduleCommand que especifica los datos del nuevo cronograma.</param>
        /// <returns>Un bool que contiene información de si la operacion de registro fue exitosa o no.</returns>
        private async Task<bool> HandleAsync(CreateScheduleCommand request)
        {
            var transaction = _dbContext.BeginTransaction();

            try
            {
                _logger.LogInformation("CreateScheduleCommandHandler.HandleAsync");

                var period = _dbContext.Periodos.FirstOrDefault(c => c.Id == request._request.PeriodId);
                if (period == null) throw new IdNotFoundException("El periodo no fue encontrado");
                foreach (var schedule in request._request.Schedules)
                {
                    var program = _dbContext.Cursos.FirstOrDefault(c => c.Nombre == schedule.Programa);
                    if (program == null)
                    {
                        throw new DataNotFoundException("Programa/Curso no encontrado: " + schedule.Programa);
                    }
                    var modulId = _dbContext.Modulos.FirstOrDefault(c => c.Nombre == schedule.Modulo
                                    && c.CursoId == program.Id);
                    if (modulId == null)
                    {
                        throw new DataNotFoundException("Modulo no encontrado: " + schedule.Modulo + " del curso: " + schedule.Programa);
                    }
                    _dbContext.Cronogramas.Add(CronogramasMapper.MapRequestToEntitySchedule(schedule, modulId.Id, period.Id));
                }
                await _dbContext.SaveEfContextChanges("APP");
                transaction.Commit();
                _logger.LogInformation("CreateScheduleCommandHandler.HandleAsync {Response}", true);
                return true;
            }
            catch (DataNotFoundException ex)
            {
                _logger.LogError(ex, "Error CreateScheduleCommandHandler.HandleAsync. {Mensaje}", ex.Message);
                transaction.Rollback();
                throw;
            }
            catch (IdNotFoundException ex) 
            {
                _logger.LogError(ex, "Error CreateScheduleCommandHandler.HandleAsync. {Mensaje}", ex.Message);
                transaction.Rollback();
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error CreateScheduleCommandHandler.HandleAsync. {Mensaje}", ex.Message);
                transaction.Rollback();
                throw;
            }
        }
    }
}
