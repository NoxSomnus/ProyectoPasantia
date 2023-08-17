using FerminToroMS.Application.Commands;
using FerminToroMS.Application.Exceptions;
using FerminToroMS.Application.RefactoredMethods;
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
    /// <summary>
    /// Manejador de comando que registra nuevos cronogramas en el sistema.
    /// </summary>
    internal class AddScheduleByCSVCommandHandler : IRequestHandler<AddScheduleByCSVCommand, bool>
    {
        private readonly IFerminToroDbContext _dbContext;
        private readonly ILogger<AddScheduleByCSVCommandHandler> _logger;

        /// <summary>
        /// Constructor de la clase AddScheduleByCSVCommandHandler.
        /// </summary>
        /// <param name="dbContext">El contexto de la base de datos que se utilizará para buscar el usuario.</param>
        /// <param name="logger">El objeto ILogger que se utilizará para registrar la actividad del manejador del comando.</param>
        public AddScheduleByCSVCommandHandler(IFerminToroDbContext dbContext, ILogger<AddScheduleByCSVCommandHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        /// <summary>
        /// Manejador del comando que registra los cronogramas de la migracion por medio de .csv.
        /// </summary>
        /// <param name="request">El comando AddScheduleByCSVCommand que especifica los cronogramas a añadir.</param>
        /// <param name="cancellationToken">El token de cancelación que puede detener la operación en cualquier momento.</param>
        /// <returns>Un tipo bool que contiene información si la operacion fue exitosa o no.</returns>
        public Task<bool> Handle(AddScheduleByCSVCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request == null)
                {
                    _logger.LogWarning("AddScheduleByCSVCommandHandler.Handle: Request null.");

                    throw new ArgumentNullException(nameof(request));

                }
                else
                {
                    return HandleAsync(request);
                }
            }
            catch
            {
                _logger.LogWarning("AddScheduleByCSVCommandHandler.Handle: ArgumentNullException");
                throw;
            }
            throw new NotImplementedException();
        }

        /// <summary>
        /// Método asincronico que maneja el registro de los cronogramas.
        /// </summary>
        /// <param name="request">El comando AddScheduleByCSVCommand que especifica los datos de los cronogramas a guardar.</param>
        /// <returns>Un tipo bool que contiene información de si la operacion de registro fue exitosa o no.</returns>
        private async Task<bool> HandleAsync(AddScheduleByCSVCommand request)
        {
            var transaction = _dbContext.BeginTransaction();
            try
            {
                var enumcheck = new EnumChecks();
                _logger.LogInformation("AddScheduleByCSVCommandHandler.HandleAsync");
                foreach (var schedulerequest in request._request.Schedules)
                {
                    var periodId = Guid.NewGuid();
                    var period = _dbContext.Periodos.FirstOrDefault(c => c.NombrePeriodo == schedulerequest.NombrePerido
                    && c.Año == schedulerequest.Año);
                    if (period == null)
                    {
                        period = new PeriodoEntity
                        {
                            Id = periodId,
                            Año = schedulerequest.Año,
                            MesInicio = schedulerequest.Meses,
                            NombrePeriodo = schedulerequest.NombrePerido,
                            MesFin = schedulerequest.Meses
                        };
                        _dbContext.Periodos.Add(period);
                    }
                    else
                    {
                        if (period.MesInicio != schedulerequest.Meses)
                        {
                            period.MesFin = schedulerequest.Meses;
                            _dbContext.Periodos.Update(period);
                        }
                    }
                    var course = _dbContext.Cursos.FirstOrDefault(c => c.Nombre == schedulerequest.Curso);
                    if (course == null)
                    {
                        throw new BadCSVRequest("El archivo .csv no cumple con el formato esperado, ingrese el nombre" +
                            "de los cursos como esta registrado en el sistema");
                    }
                    var modul = _dbContext.Modulos.FirstOrDefault(c => c.Nombre == schedulerequest.Modulo
                    && c.CursoId == course.Id);
                    if (modul == null)
                    {
                        throw new BadCSVRequest("El archivo .csv no cumple con el formato esperado, ingrese el nombre" +
                            "de los modulos como esta registrado en el sistema");
                    }
                    var fechainicio = "";
                    var schedule = _dbContext.Cronogramas.FirstOrDefault(c => c.ModuloId == modul.Id 
                    && c.PeriodoId == period.Id && c.Regularidad == schedulerequest.Regularidad 
                    && c.Turno == schedulerequest.Turno && c.Modalidad == schedulerequest.Modalidad);
                    fechainicio = schedulerequest.FechaInicio + "/" + schedulerequest.Año;
                    if (schedule == null)
                    {
                        schedule = new CronogramaEntity
                        {
                            FechaInicio = DateOnly.ParseExact(fechainicio, "dd/MM/yyyy", null),
                            Horario_Dias = schedulerequest.Horario,
                            Regularidad = schedulerequest.Regularidad,
                            Modalidad = schedulerequest.Modalidad,
                            Turno = schedulerequest.Turno,
                            Duracion_Semanas = enumcheck.RegularidadCheck(schedulerequest.Regularidad),
                            NroHoras = 80,
                            ModuloId = modul.Id,
                            PeriodoId = period.Id
                        };
                        if (schedulerequest.FechaFin != "")
                        {
                            schedule.FechaFin = DateOnly.ParseExact(schedulerequest.FechaFin, "dd/MM/yyyy", null);
                        }
                        _dbContext.Cronogramas.Add(schedule);
                    }
                    else 
                    {
                        schedule.FechaInicio = DateOnly.ParseExact(fechainicio, "dd/MM/yyyy", null);
                        _dbContext.Cronogramas.Update(schedule);
                    }
                    await _dbContext.SaveEfContextChanges("APP");
                }
                transaction.Commit();
                _logger.LogInformation("AddScheduleByCSVCommandHandler.HandleAsync {Response}", true);
                return true;
            }
            catch (BadCSVRequest ex) 
            {
                _logger.LogError(ex, "Error AddScheduleByCSVCommandHandler.HandleAsync. {Mensaje}", ex.Message);
                transaction.Rollback();
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error AddScheduleByCSVCommandHandler.HandleAsync. {Mensaje}", ex.Message);
                transaction.Rollback();
                throw;
            }
        }


    }
}
