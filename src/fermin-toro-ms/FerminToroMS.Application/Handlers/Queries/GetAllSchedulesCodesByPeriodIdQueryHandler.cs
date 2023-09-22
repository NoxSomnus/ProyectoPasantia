using FerminToroMS.Application.CustomClasses;
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
    internal class GetAllSchedulesCodesByPeriodIdQueryHandler : IRequestHandler<GetAllSchedulesCodesByPeriodIdQuery, List<ScheduleCodes>>
    {
        private readonly IFerminToroDbContext _dbContext;
        private readonly ILogger<GetAllSchedulesCodesByPeriodIdQueryHandler> _logger;

        /// <summary>
        /// Constructor de la clase GetAllSchedulesByPeriodIdQueryHandler.
        /// </summary>
        /// <param name="dbContext">El contexto de la base de datos que se utilizará para buscar el usuario.</param>
        /// <param name="logger">El objeto ILogger que se utilizará para registrar la actividad del manejador de consulta.</param>
        public GetAllSchedulesCodesByPeriodIdQueryHandler(IFerminToroDbContext dbContext, ILogger<GetAllSchedulesCodesByPeriodIdQueryHandler> logger)
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
        public Task<List<ScheduleCodes>> Handle(GetAllSchedulesCodesByPeriodIdQuery request, CancellationToken cancellationToken)
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
        private async Task<List<ScheduleCodes>> HandleAsync(GetAllSchedulesCodesByPeriodIdQuery request)
        {
            try
            {
                _logger.LogInformation("AllPeriodsQueryHandler.HandleAsync");
                var schedules = await _dbContext.Cronogramas.Where(c => c.PeriodoId == request.PeriodId).OrderByDescending(c => c.Habilitado).ThenBy(c => c.FechaInicio)
                    .Select(c => new ScheduleCodes()
                    {
                        ScheduleId = c.Id,
                        ScheduleCode = c.Codigo
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
