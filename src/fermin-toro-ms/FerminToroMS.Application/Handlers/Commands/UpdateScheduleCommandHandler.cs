using FerminToroMS.Application.Commands;
using FerminToroMS.Application.Exceptions;
using FerminToroMS.Application.Mappers;
using FerminToroMS.Application.Requests;
using FerminToroMS.Core.Database;
using FerminToroMS.Core.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FerminToroMS.Application.Handlers.Commands
{
    internal class UpdateScheduleCommandHandler : IRequestHandler<UpdateScheduleCommand, bool>
    {
        private readonly IFerminToroDbContext _dbContext;
        private readonly ILogger<UpdateScheduleCommandHandler> _logger;

        /// <summary>
        /// Constructor de la clase UpdateScheduleCommandHandler.
        /// </summary>
        /// <param name="dbContext">El contexto de la base de datos que se utilizará para buscar el usuario.</param>
        /// <param name="logger">El objeto ILogger que se utilizará para registrar la actividad del manejador del comando.</param>
        public UpdateScheduleCommandHandler(IFerminToroDbContext dbContext, ILogger<UpdateScheduleCommandHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        /// <summary>
        /// Manejador del comando que actualiza un cronograma.
        /// </summary>
        /// <param name="request">El comando UpdateScheduleCommand que especifica los datos a actualizar del cronograma.</param>
        /// <param name="cancellationToken">El token de cancelación que puede detener la operación en cualquier momento.</param>
        /// <returns>Un objeto bool que contiene información si la operacion fue exitosa o no.</returns>
        public Task<bool> Handle(UpdateScheduleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request == null)
                {
                    _logger.LogWarning("UpdateScheduleCommandHandler.Handle: Request null.");

                    throw new ArgumentNullException(nameof(request));

                }
                else
                {
                    return HandleAsync(request);
                }
            }
            catch
            {
                _logger.LogWarning("UpdateScheduleCommandHandler.Handle: ArgumentNullException");
                throw;
            }
            throw new NotImplementedException();
        }

        /// <summary>
        /// Método asincronico que maneja la actualizacion de informacion de un cronograma.
        /// </summary>
        /// <param name="request">El comando UpdateScheduleCommand que especifica los datos a actualizar del cronograma.</param>
        /// <returns>Un objeto bool que contiene información de si la operacion de registro fue exitosa o no.</returns>
        private async Task<bool> HandleAsync(UpdateScheduleCommand request)
        {
            var transaction = _dbContext.BeginTransaction();
            try
            {
                var period = _dbContext.Periodos.FirstOrDefault(c => c.Id == request._request.PeriodId);
                if (period == null)
                {
                    throw new IdNotFoundException("No se encontró el periodo");
                }
                foreach (var schedule in request._request.schedules)
                {
                    var course = _dbContext.Cursos.FirstOrDefault(c => c.Nombre == schedule.Programa);
                    if (course == null) { throw new DataNotFoundException("El curso: " + schedule.Programa + " no se encontro"); }
                    var modul = _dbContext.Modulos.FirstOrDefault(c => c.Nombre == schedule.Modulo && c.CursoId == course.Id);
                    if (modul == null) { throw new DataNotFoundException("El modulo: " + schedule.Modulo + " no pertenece al programa seleccionado"); }
                    if (schedule.InstructorId == "No Asignado") { schedule.InstructorId = null; }
                    if (schedule.ScheduleId != null) //es una actualizacion
                    {
                        var cronograma = _dbContext.Cronogramas.FirstOrDefault(c => c.Id == schedule.ScheduleId);
                        if (cronograma == null)
                        {
                            throw new IdNotFoundException("No se encontró el cronograma a actualizar");
                        }
                        cronograma = UpdateRequestToEntity(cronograma, schedule, modul.Id);
                        cronograma.Codigo = CronogramasMapper.ExtractCode(cronograma, period.NombrePeriodo, modul.Diminutivo);
                        _dbContext.Cronogramas.Update(cronograma);
                    }
                    else //es una adicion al cronograma 
                    {
                        var cronograma = CreateNewCronograma(schedule, modul.Id, period.Id);
                        cronograma.Codigo = CronogramasMapper.ExtractCode(cronograma, period.NombrePeriodo, modul.Diminutivo);
                        _dbContext.Cronogramas.Add(cronograma);
                    }
                }
                await _dbContext.SaveEfContextChanges("APP");
                transaction.Commit();
                return true;
            }
            catch (IdNotFoundException ex) 
            {
                _logger.LogError(ex, "Error UpdateScheduleCommandHandler.HandleAsync. {Mensaje}", ex.Message);
                transaction.Rollback();
                throw;
            }
            catch (DataNotFoundException ex)
            {
                _logger.LogError(ex, "Error UpdateScheduleCommandHandler.HandleAsync. {Mensaje}", ex.Message);
                transaction.Rollback();
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error UpdateScheduleCommandHandler.HandleAsync. {Mensaje}", ex.Message);
                transaction.Rollback();
                throw;
            }
        }
        private CronogramaEntity UpdateRequestToEntity(CronogramaEntity schedule,
            ScheduleDataToUpdate request, Guid ModulId)
        {
            schedule.ModuloId = ModulId;
            schedule.InstructorId = request.InstructorId == null ? null : new Guid(request.InstructorId);
            schedule.FechaInicio = DateOnly.ParseExact(request.FechaInicio, "yyyy-MM-dd", null);
            schedule.FechaFin = DateOnly.ParseExact(request.FechaFin, "yyyy-MM-dd", null);
            schedule.Regularidad = request.Regularidad;
            schedule.Turno = request.Turno;
            schedule.Horario_Dias = request.Horario;
            schedule.Modalidad = request.Modalidad;
            schedule.Duracion_Semanas = request.Duracion;
            schedule.NroVacantes = request.Vacantes;
            schedule.Habilitado = request.Habilitado;
            if (!schedule.Habilitado) 
            {
                var inscriptions = _dbContext.Inscripciones.Where(c=>c.CronogramaId == schedule.Id).ToList();
                if (inscriptions.Count != 0 || inscriptions != null) 
                {
                    foreach (var inscription in inscriptions)
                    {
                        var inscripcionYaCongelada = _dbContext.InscripcionesCongeladas
                            .FirstOrDefault(c=>c.InscripcionId == inscription.Id);
                        if (inscripcionYaCongelada == null)
                        {
                            var congelada = new InscripcionesCongeladasEntity
                            {
                                Id = Guid.NewGuid(),
                                InscripcionId = inscription.Id,
                                PlanificacionCerrada = true
                            };
                            _dbContext.InscripcionesCongeladas.Add(congelada);
                        }                                                                    
                    }
                }
            }
            return schedule;
        }

        private CronogramaEntity CreateNewCronograma(ScheduleDataToUpdate request, Guid ModulId, Guid PeriodId) 
        {
            var schedule = new CronogramaEntity
            {
                Id = Guid.NewGuid(),
                ModuloId = ModulId,
                InstructorId = request.InstructorId == null ? null : new Guid(request.InstructorId),
                FechaInicio = DateOnly.ParseExact(request.FechaInicio, "yyyy-MM-dd", null),
                FechaFin = DateOnly.ParseExact(request.FechaFin, "yyyy-MM-dd", null),
                Regularidad = request.Regularidad,
                Turno = request.Turno,
                Horario_Dias = request.Horario,
                Modalidad = request.Modalidad,
                Duracion_Semanas = request.Duracion,
                NroVacantes = request.Vacantes,
                Habilitado = true,
                NroHoras = 80,
                PeriodoId = PeriodId
            };
            return schedule;
        }
    }
}
