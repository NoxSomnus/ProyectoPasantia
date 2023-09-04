using FerminToroMS.Application.Exceptions;
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
    internal class PeriodSummaryQueryHandler : IRequestHandler<PeriodSummaryQuery, PeriodSummaryResponse>
    {
        private readonly IFerminToroDbContext _dbContext;
        private readonly ILogger<PeriodSummaryQueryHandler> _logger;

        /// <summary>
        /// Constructor de la clase CalculateAllPaymentsByPeriodIdQueryHandler.
        /// </summary>
        /// <param name="dbContext">El contexto de la base de datos que se utilizará para buscar el usuario.</param>
        /// <param name="logger">El objeto ILogger que se utilizará para registrar la actividad del manejador de consulta.</param>
        public PeriodSummaryQueryHandler(IFerminToroDbContext dbContext, ILogger<PeriodSummaryQueryHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        /// <summary>
        /// Manejador de la consulta que busca todos las inscripciones segun un cronograma.
        /// </summary>
        /// <param name="request">La consulta CalculateAllPaymentsByPeriodIdQuery que especifica la busqueda de todos los pagos de un periodo y los calcula.</param>
        /// <param name="cancellationToken">El token de cancelación que puede detener la operación en cualquier momento.</param>
        /// <returns>Un double que dice el total de ganancias.</returns>
        public Task<PeriodSummaryResponse> Handle(PeriodSummaryQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (request == null)
                {
                    _logger.LogWarning("CalculateAllPaymentsByPeriodIdQueryHandler.Handle: Request null.");

                    throw new ArgumentNullException(nameof(request));

                }
                else
                {
                    return HandleAsync(request);
                }
            }
            catch
            {
                _logger.LogWarning("CalculateAllPaymentsByPeriodIdQueryHandler.Handle: ArgumentNullException");
                throw;
            }
            throw new NotImplementedException();
        }
        /// <summary>
        /// Método asincronico que maneja la consulta de todos los pagos de un periodo y los totaliza.
        /// </summary>
        /// <param name="request">La consulta CalculateAllPaymentsByPeriodIdQuery que especifica la busqueda de todos los pagos de un periodo y los totaliza.</param>
        /// <returns>Un double con el total de los pagos.</returns>
        private async Task<PeriodSummaryResponse> HandleAsync(PeriodSummaryQuery request)
        {
            try
            {
                _logger.LogInformation("CalculateAllPaymentsByPeriodIdQueryHandler.HandleAsync");
                var response = new PeriodSummaryResponse
                {
                    TotalOnline = 0,
                    TotalOnPeriod = 0,
                    TotalPresencial = 0,
                    TotalOnModuls = new List<TotalOnModuls>()
                };
                var period = await _dbContext.Periodos.FirstOrDefaultAsync(c => c.Id == request.PeriodId);
                if (period == null) 
                {
                    throw new IdNotFoundException("El periodo no fue encontrado");
                }
                var schedules = _dbContext.Cronogramas.Include(c=> c.Modulo)
                    .ThenInclude(m => m.Curso).Where(c=>c.PeriodoId == period.Id && c.Habilitado == true).ToList();
                foreach (var schedule in schedules) 
                {
                    var totalonmodul = new TotalOnModuls
                    {
                        ProgramName = schedule.Modulo.Curso.Nombre,
                        ModulName = schedule.Modulo.Nombre,
                        Turno = schedule.Turno.ToString(),
                        Total = 0,
                        Modalidad = schedule.Modalidad.ToString(),
                        Regularidad = schedule.Regularidad.ToString()
                    };
                    var inscriptions = _dbContext.Inscripciones.Where(c=>c.CronogramaId == schedule.Id).ToList();
                    foreach (var inscription in inscriptions) 
                    {
                        var payments = _dbContext.Pagos.Where(c=>c.InscripcionId == inscription.Id).ToList();
                        foreach (var payment in payments) 
                        {
                            if (schedule.Modalidad == 0)
                            {
                                response.TotalPresencial = response.TotalPresencial + payment.Monto;
                            }
                            else 
                            {
                                response.TotalOnline = response.TotalOnline + payment.Monto;
                            }
                            totalonmodul.Total = totalonmodul.Total + payment.Monto;
                            response.TotalOnPeriod = response.TotalOnPeriod + payment.Monto;
                        }
                    }
                    response.TotalOnModuls.Add(totalonmodul);
                }
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error CalculateAllPaymentsByPeriodIdQueryHandler.HandleAsync. {Los datos ingresados no son validos}", ex.Message);
                throw;
            }
        }
    }
}
