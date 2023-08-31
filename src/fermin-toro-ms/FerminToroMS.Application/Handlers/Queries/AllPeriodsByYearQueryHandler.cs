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
    internal class AllPeriodsByYearQueryHandler : IRequestHandler<AllPeriodsByYearQuery, List<PeriodResponse>>
    {
        private readonly IFerminToroDbContext _dbContext;
        private readonly ILogger<AllPeriodsByYearQueryHandler> _logger;

        /// <summary>
        /// Constructor de la clase AllPeriodsByYearQueryHandler.
        /// </summary>
        /// <param name="dbContext">El contexto de la base de datos que se utilizará para buscar el usuario.</param>
        /// <param name="logger">El objeto ILogger que se utilizará para registrar la actividad del manejador de consulta.</param>
        public AllPeriodsByYearQueryHandler(IFerminToroDbContext dbContext, ILogger<AllPeriodsByYearQueryHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        /// <summary>
        /// Manejador de la consulta que busca todos los periodos de un año registrados.
        /// </summary>
        /// <param name="request">La consulta AllPeriodsByYearQuery que especifica la busqueda de todos los periodos de un año.</param>
        /// <param name="cancellationToken">El token de cancelación que puede detener la operación en cualquier momento.</param>
        /// <returns>Una lista de todos los periodos del sistema.</returns>
        public Task<List<PeriodResponse>> Handle(AllPeriodsByYearQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (request == null)
                {
                    _logger.LogWarning("AllPeriodsByYearQueryHandler.Handle: Request null.");

                    throw new ArgumentNullException(nameof(request));

                }
                else
                {
                    return HandleAsync(request);
                }
            }
            catch
            {
                _logger.LogWarning("AllPeriodsByYearQueryHandler.Handle: ArgumentNullException");
                throw;
            }
            throw new NotImplementedException();
        }
        /// <summary>
        /// Método asincronico que maneja la consulta de todos los periodos registrados de un año.
        /// </summary>
        /// <param name="request">La consulta AllPeriodsByYearQuery que especifica la busqueda de todos los periodos de un año.</param>
        /// <returns>Una lista de periodos del sistema.</returns>
        private async Task<List<PeriodResponse>> HandleAsync(AllPeriodsByYearQuery request)
        {
            try
            {
                _logger.LogInformation("AllPeriodsQueryHandler.HandleAsync");
                var periods = await _dbContext.Periodos.OrderByDescending(c => c.CreatedAt).Where(c=>c.Año == request.Año)
                    .Select(c => new PeriodResponse()
                    {
                        PeriodId = c.Id,
                        PeriodName = c.NombrePeriodo,
                        Año = c.Año,
                        MesInicio = c.MesInicio,
                        MesFin = c.MesFin
                    }).ToListAsync();
                return periods;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error AllPeriodsQueryHandler.HandleAsync. {Los datos ingresados no son validos}", ex.Message);
                throw;
            }
        }

    }
}
