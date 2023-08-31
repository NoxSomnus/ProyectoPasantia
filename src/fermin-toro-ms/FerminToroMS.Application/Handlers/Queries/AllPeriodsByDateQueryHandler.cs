using FerminToroMS.Application.Mappers;
using FerminToroMS.Application.Queries;
using FerminToroMS.Application.Responses;
using FerminToroMS.Core.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerminToroMS.Application.Handlers.Queries
{
    internal class AllPeriodsByDateQueryHandler : IRequestHandler<AllPeriodsByDateQuery, List<PeriodResponse>>
    {
        private readonly IFerminToroDbContext _dbContext;
        private readonly ILogger<AllPeriodsByDateQueryHandler> _logger;

        /// <summary>
        /// Constructor de la clase AllPeriodByDateQueryHandler.
        /// </summary>
        /// <param name="dbContext">El contexto de la base de datos que se utilizará para buscar el usuario.</param>
        /// <param name="logger">El objeto ILogger que se utilizará para registrar la actividad del manejador de consulta.</param>
        public AllPeriodsByDateQueryHandler(IFerminToroDbContext dbContext, ILogger<AllPeriodsByDateQueryHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        /// <summary>
        /// Manejador de la consulta que busca todos los periodos de un año registrados.
        /// </summary>
        /// <param name="request">La consulta AllPeriodByDateQuery que especifica la busqueda de todos los periodos en un rango de fecha.</param>
        /// <param name="cancellationToken">El token de cancelación que puede detener la operación en cualquier momento.</param>
        /// <returns>Una lista de todos los periodos del sistema.</returns>
        public Task<List<PeriodResponse>> Handle(AllPeriodsByDateQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (request == null)
                {
                    _logger.LogWarning("AllPeriodByDateQueryHandler.Handle: Request null.");

                    throw new ArgumentNullException(nameof(request));

                }
                else
                {
                    return HandleAsync(request);
                }
            }
            catch
            {
                _logger.LogWarning("AllPeriodByDateQueryHandler.Handle: ArgumentNullException");
                throw;
            }
            throw new NotImplementedException();
        }
        /// <summary>
        /// Método asincronico que maneja la consulta de todos los periodos registrados de un año.
        /// </summary>
        /// <param name="request">La consulta AllPeriodsByYearQuery que especifica la busqueda de todos los periodos de un año.</param>
        /// <returns>Una lista de periodos del sistema.</returns>
        private async Task<List<PeriodResponse>> HandleAsync(AllPeriodsByDateQuery request)
        {
            try
            {
                CultureInfo culture = new CultureInfo("es-ES");
                DateTime fechaInicio = DateTime.ParseExact(request._request.StartDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime fechaFin = DateTime.ParseExact(request._request.EndDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                
                _logger.LogInformation("AllPeriodsQueryHandler.HandleAsync");
                var periods = await _dbContext.Periodos.OrderByDescending(c => c.CreatedAt).ToListAsync();
                var response = new List<PeriodResponse>();
                foreach (var _period in periods) 
                {
                    string fechainicio = "01/" + _period.MesInicio + "/" + _period.Año;
                    string formato = "dd/MMMM/yyyy";
                    DateTime StartDate = DateTime.ParseExact(fechainicio, formato, culture);
                    string fechafin = "30/" + _period.MesFin + "/" + _period.Año;
                    if (_period.MesFin == "febrero")
                    {
                        fechafin = "28/" + _period.MesFin + "/" + _period.Año;
                    }
                    DateTime EndDate = DateTime.ParseExact(fechafin, formato, culture);
                    if (StartDate >= fechaInicio && EndDate <= fechaFin) 
                    {
                        response.Add(PeriodosMapper.MapEntityToResponse(_period));
                    }
                }
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error AllPeriodsQueryHandler.HandleAsync. {Los datos ingresados no son validos}", ex.Message);
                throw;
            }
        }

    }
}
