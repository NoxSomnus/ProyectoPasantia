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
    internal class SchedulesByPeriodIdQueryHandler : IRequestHandler<SchedulesByPeriodIdQuery, List<ScheduleResponse>>
    {
        private readonly IFerminToroDbContext _dbContext;
        private readonly ILogger<SchedulesByPeriodIdQueryHandler> _logger;

        /// <summary>
        /// Constructor de la clase SchedulesByPeriodIdQueryHandler.
        /// </summary>
        /// <param name="dbContext">El contexto de la base de datos que se utilizará para buscar el usuario.</param>
        /// <param name="logger">El objeto ILogger que se utilizará para registrar la actividad del manejador de consulta.</param>
        public SchedulesByPeriodIdQueryHandler(IFerminToroDbContext dbContext, ILogger<SchedulesByPeriodIdQueryHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        /// <summary>
        /// Manejador de la consulta que busca todos los cronogramas segun un periodo.
        /// </summary>
        /// <param name="request">La consulta ScheduleByPeriodIdQuery que especifica la busqueda de todos los cronogramas segun un periodo.</param>
        /// <param name="cancellationToken">El token de cancelación que puede detener la operación en cualquier momento.</param>
        /// <returns>Una lista de todos los cronogramas de un periodo.</returns>
        public Task<List<ScheduleResponse>> Handle(SchedulesByPeriodIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (request == null)
                {
                    _logger.LogWarning("SchedulesByPeriodIdQueryHandler.Handle: Request null.");

                    throw new ArgumentNullException(nameof(request));

                }
                else
                {
                    return HandleAsync(request);
                }
            }
            catch
            {
                _logger.LogWarning("SchedulesByPeriodIdQueryHandler.Handle: ArgumentNullException");
                throw;
            }
            throw new NotImplementedException();
        }
        /// <summary>
        /// Método asincronico que maneja la consulta de todos los cronogramas segun un periodo.
        /// </summary>
        /// <param name="request">La consulta ScheduleByPeriodIdQueryHandler que especifica la busqueda de todos los cronogramas segun un periodo.</param>
        /// <returns>Una lista de cronogramas segun un periodo.</returns>
        private async Task<List<ScheduleResponse>> HandleAsync(SchedulesByPeriodIdQuery request)
        {
            try
            {
                _logger.LogInformation("AllPeriodsQueryHandler.HandleAsync");
                var schedules = await _dbContext.Cronogramas.Where(c => c.PeriodoId == request.PeriodId).OrderBy(c => c.FechaInicio)
                    .Select(c => new ScheduleResponse()
                    {
                        ScheduleId = c.Id,
                        PeriodId = c.PeriodoId,
                        PeriodName = c.Periodo.NombrePeriodo,
                        CourseName = c.Modulo.Curso.Nombre,
                        ModulName = c.Modulo.Nombre,
                        Fecha_Inicio = c.FechaInicio.ToString("dd/MM/yyyy"),
                        Fecha_Fin = c.FechaFin.HasValue ? c.FechaFin.Value.ToString("dd/MM/yyyy") : string.Empty,
                        Horario = c.Horario_Dias,
                        Horas = 80,
                        Modalidad = c.Modalidad.ToString(),
                        Turno = c.Turno.ToString(),
                        Regularidad = c.Regularidad.ToString(),
                        Duracion = c.Duracion_Semanas,
                        NroVacantes = c.NroVacantes,
                        InstructorAsignado = c.InstructorId != null && c.Instructor != null ? c.Instructor.Nombre + " " + c.Instructor.Apellido : null ,
                        InstructorId = c.InstructorId != null ? c.InstructorId : null
                    }).ToListAsync();
                return schedules;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error AllPeriodsQueryHandler.HandleAsync. {Los datos ingresados no son validos}", ex.Message);
                throw;
            }
        }

    }
}
