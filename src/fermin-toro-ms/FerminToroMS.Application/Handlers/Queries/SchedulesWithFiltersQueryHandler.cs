using FerminToroMS.Application.Mappers;
using FerminToroMS.Application.Queries;
using FerminToroMS.Application.Requests;
using FerminToroMS.Application.Responses;
using FerminToroMS.Core.Database;
using FerminToroMS.Core.Entities;
using FerminToroMS.Core.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace FerminToroMS.Application.Handlers.Queries
{
    internal class SchedulesWithFiltersQueryHandler : IRequestHandler<SchedulesWithFiltersQuery, List<ScheduleResponse>>
    {
        private readonly IFerminToroDbContext _dbContext;
        private readonly ILogger<SchedulesWithFiltersQueryHandler> _logger;

        /// <summary>
        /// Constructor de la clase SchedulesWithFiltersQueryHandler.
        /// </summary>
        /// <param name="dbContext">El contexto de la base de datos que se utilizará para buscar el usuario.</param>
        /// <param name="logger">El objeto ILogger que se utilizará para registrar la actividad del manejador de consulta.</param>
        public SchedulesWithFiltersQueryHandler(IFerminToroDbContext dbContext, ILogger<SchedulesWithFiltersQueryHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        /// <summary>
        /// Manejador de la consulta que busca todos los cronogramas segun un periodo y algunos filtros.
        /// </summary>
        /// <param name="request">La consulta SchedulesWithFiltersQuery que especifica la busqueda de todos los cronogramas segun un periodo y algunos filtros.</param>
        /// <param name="cancellationToken">El token de cancelación que puede detener la operación en cualquier momento.</param>
        /// <returns>Una lista de todos los cronogramas de un periodo y algunos filtros.</returns>
        public Task<List<ScheduleResponse>> Handle(SchedulesWithFiltersQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (request == null)
                {
                    _logger.LogWarning("SchedulesWithFiltersQueryHandler.Handle: Request null.");

                    throw new ArgumentNullException(nameof(request));

                }
                else
                {
                    return HandleAsync(request);
                }
            }
            catch
            {
                _logger.LogWarning("SchedulesWithFiltersQueryHandler.Handle: ArgumentNullException");
                throw;
            }
            throw new NotImplementedException();
        }
        /// <summary>
        /// Método asincronico que maneja la consulta de todos los cronogramas segun un periodo.
        /// </summary>
        /// <param name="request">La consulta SchedulesWithFiltersQuery que especifica la busqueda de todos los cronogramas segun un periodo y algunos filtros.</param>
        /// <returns>Una lista de cronogramas segun un periodo y algunos filtros.</returns>
        private async Task<List<ScheduleResponse>> HandleAsync(SchedulesWithFiltersQuery request)
        {
            try
            {
                _logger.LogInformation("SchedulesWithFiltersQueryHandler.HandleAsync");
                var schedules = _dbContext.Cronogramas
                    .Include(c => c.Periodo)
                    .Include(c => c.Modulo)
                        .ThenInclude(m => m.Curso)
                    .Where(c => c.PeriodoId == request._request.PeriodId);
                var response = ApplyFilters(schedules,request._request);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error SchedulesWithFiltersQueryHandler.HandleAsync. {Los datos ingresados no son validos}", ex.Message);
                throw;
            }
        }

        private List<ScheduleResponse> ApplyFilters(IQueryable<CronogramaEntity> schedules, SchedulesWithFiltersRequest filter) 
        {
            //Primer filtro por modalidad
            if (filter.ByModalidad != null && filter.ByModalidad.Any())
            {
                var modalidad = filter.ByModalidad.Cast<ModalidadEnum>();
                schedules = schedules.Where(c => modalidad.Contains((ModalidadEnum)c.Modalidad));
            }
            //Segundo filtro por turno
            if (filter.ByTurno != null && filter.ByTurno.Any())
            {
                var turnos = filter.ByTurno.Cast<TurnosEnum>();
                schedules = schedules.Where(c => turnos.Contains((TurnosEnum)c.Turno));
            }
            //Tercer filtro por regularidad
            if (filter.ByRegularidad != null && filter.ByRegularidad.Any())
            {
                var regularidad = filter.ByRegularidad.Cast<RegularidadEnum>();
                schedules = schedules.Where(c => regularidad.Contains((RegularidadEnum)c.Regularidad));
            }
            //Cuarto filtro por modulo
            if (filter.ByModulo != null && filter.ByModulo.Any())
            {
                schedules = schedules.Where(c => filter.ByModulo.Contains(c.Modulo.Nombre));
            }
            //Quinto filtro por curso
            if (filter.ByCurso != null && filter.ByCurso.Any())
            {
                schedules = schedules.Where(c => filter.ByCurso.Contains(c.Modulo.Curso.Nombre));
            }
            try
            {
                var list = schedules.ToList();
                var response = CronogramasMapper.MapListEntityToResponse(list);
                return response;
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex, "Error SchedulesWithFiltersQueryHandler.HandleAsync. {Los datos ingresados no son validos}", ex.Message);
                throw;

            }
        }

    }
}
