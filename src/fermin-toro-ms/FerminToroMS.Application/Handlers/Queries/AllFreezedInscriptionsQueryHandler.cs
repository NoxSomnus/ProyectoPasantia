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
    internal class AllFreezedInscriptionsQueryHandler : IRequestHandler<AllFreezedInscriptionsQuery, List<FreezedInscriptionResponse>>
    {
        private readonly IFerminToroDbContext _dbContext;
        private readonly ILogger<AllFreezedInscriptionsQueryHandler> _logger;

        /// <summary>
        /// Constructor de la clase AllFreezedInscriptionsQueryHandler.
        /// </summary>
        /// <param name="dbContext">El contexto de la base de datos que se utilizará para buscar el usuario.</param>
        /// <param name="logger">El objeto ILogger que se utilizará para registrar la actividad del manejador de consulta.</param>
        public AllFreezedInscriptionsQueryHandler(IFerminToroDbContext dbContext, ILogger<AllFreezedInscriptionsQueryHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        /// <summary>
        /// Manejador de la consulta que busca todos las inscripciones congeladas.
        /// </summary>
        /// <param name="request">La consulta AllFreezedInscriptionsQuery que especifica la busqueda de todos las inscripciones segun un cronograma.</param>
        /// <param name="cancellationToken">El token de cancelación que puede detener la operación en cualquier momento.</param>
        /// <returns>Una lista de todos las inscripciones congeladas.</returns>
        public Task<List<FreezedInscriptionResponse>> Handle(AllFreezedInscriptionsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (request == null)
                {
                    _logger.LogWarning("AllFreezedInscriptionsQueryHandler.Handle: Request null.");

                    throw new ArgumentNullException(nameof(request));

                }
                else
                {
                    return HandleAsync(request);
                }
            }
            catch
            {
                _logger.LogWarning("AllFreezedInscriptionsQueryHandler.Handle: ArgumentNullException");
                throw;
            }
            throw new NotImplementedException();
        }
        /// <summary>
        /// Método asincronico que maneja la consulta de todos las inscripciones congeladas.
        /// </summary>
        /// <param name="request">La consulta AllFreezedInscriptionsQuery que especifica la busqueda de todos las inscripciones segun un cronogramas.</param>
        /// <returns>Una lista de inscripciones congeladas.</returns>
        /// 

        private async Task<List<FreezedInscriptionResponse>> HandleAsync(AllFreezedInscriptionsQuery request)
        {
            try
            {
                _logger.LogInformation("AllFreezedInscriptionsQueryHandler.HandleAsync");
                var freezedInscriptions = await _dbContext.InscripcionesCongeladas
                    .Include(ic => ic.Inscripcion)
                    .ThenInclude(i => i.Estudiante)
                    .Include(ic => ic.Inscripcion)
                    .ThenInclude(i => i.Cronograma)
                    .Select(c => new FreezedInscriptionResponse
                    {
                        InscriptionId = c.Inscripcion.Id,
                        FreezeInscriptionId = c.Id,
                        ScheduleCode = c.Inscripcion.Cronograma.Codigo,
                        StudentName = c.Inscripcion.Estudiante.Nombre + " " + c.Inscripcion.Estudiante.Apellido,
                        NroInscripcion = c.Inscripcion.NroInscripcion,
                        ScheduleId = c.Inscripcion.CronogramaId,
                        PlanificacionCerrada = c.PlanificacionCerrada,
                        ModulId = c.Inscripcion.Cronograma.ModuloId
                    })
                    .ToListAsync();
                return freezedInscriptions;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error AllFreezedInscriptionsQueryHandler.HandleAsync. {Los datos ingresados no son validos}", ex.Message);
                throw;
            }
        }
    }
}
