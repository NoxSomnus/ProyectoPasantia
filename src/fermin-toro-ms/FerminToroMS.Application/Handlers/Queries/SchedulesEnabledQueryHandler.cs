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
    internal class SchedulesEnabledQueryHandler : IRequestHandler<SchedulesEnabledQuery, List<SchedulesEnabledResponse>>
    {
        private readonly IFerminToroDbContext _dbContext;
        private readonly ILogger<SchedulesEnabledQueryHandler> _logger;

        /// <summary>
        /// Constructor de la clase SchedulesEnabledQueryHandler.
        /// </summary>
        /// <param name="dbContext">El contexto de la base de datos que se utilizará para buscar el usuario.</param>
        /// <param name="logger">El objeto ILogger que se utilizará para registrar la actividad del manejador de consulta.</param>
        public SchedulesEnabledQueryHandler(IFerminToroDbContext dbContext, ILogger<SchedulesEnabledQueryHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        /// <summary>
        /// Manejador de la consulta que busca todos los cronogramas de un periodo futuro.
        /// </summary>
        /// <param name="request">La consulta ScheduleByPeriodIdQuery que especifica la busqueda de todos los cronogramas disponibles.</param>
        /// <param name="cancellationToken">El token de cancelación que puede detener la operación en cualquier momento.</param>
        /// <returns>Una lista de todos los cronogramas disponibles.</returns>
        public Task<List<SchedulesEnabledResponse>> Handle(SchedulesEnabledQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (request == null)
                {
                    _logger.LogWarning("SchedulesEnabledQueryHandler.Handle: Request null.");

                    throw new ArgumentNullException(nameof(request));

                }
                else
                {
                    return HandleAsync(request);
                }
            }
            catch
            {
                _logger.LogWarning("SchedulesEnabledQueryHandler.Handle: ArgumentNullException");
                throw;
            }
            throw new NotImplementedException();
        }
        /// <summary>
        /// Método asincronico que maneja la consulta de todos los cronogramas disponibles.
        /// </summary>
        /// <param name="request">La consulta SchedulesEnabledQuery que especifica la busqueda de todos los cronogramas disponibles.</param>
        /// <returns>Una lista de cronogramas disponibles.</returns>
        private async Task<List<SchedulesEnabledResponse>> HandleAsync(SchedulesEnabledQuery request)
        {
            try
            {
                _logger.LogInformation("SchedulesEnabledQuery.HandleAsync");
                DateOnly fechaActual = DateOnly.FromDateTime(DateTime.Now);
                var schedules = await _dbContext.Cronogramas
                        .Where(c => (c.FechaInicio >= fechaActual || (c.FechaFin.HasValue && c.FechaFin.Value >= fechaActual))
                        && c.Habilitado)
                        .Select(c => new SchedulesEnabledResponse()
                        {
                            ScheduleCode = c.Codigo,
                            ScheduleId = c.Id,
                            ModulId = c.ModuloId
                        }).ToListAsync();
                return schedules;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error SchedulesEnabledQuery.HandleAsync. {Los datos ingresados no son validos}", ex.Message);
                throw;
            }
        }
    }
}
