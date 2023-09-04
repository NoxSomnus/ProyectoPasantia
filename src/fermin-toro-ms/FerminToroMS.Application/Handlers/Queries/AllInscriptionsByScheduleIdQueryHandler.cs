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
    internal class AllInscriptionsByScheduleIdQueryHandler : IRequestHandler<AllInscriptionsByScheduleIdQuery, AllInscriptionsResponse>
    {
        private readonly IFerminToroDbContext _dbContext;
        private readonly ILogger<AllInscriptionsByScheduleIdQueryHandler> _logger;

        /// <summary>
        /// Constructor de la clase AllInscriptionsByScheduleIdQueryHandler.
        /// </summary>
        /// <param name="dbContext">El contexto de la base de datos que se utilizará para buscar el usuario.</param>
        /// <param name="logger">El objeto ILogger que se utilizará para registrar la actividad del manejador de consulta.</param>
        public AllInscriptionsByScheduleIdQueryHandler(IFerminToroDbContext dbContext, ILogger<AllInscriptionsByScheduleIdQueryHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        /// <summary>
        /// Manejador de la consulta que busca todos las inscripciones segun un cronograma.
        /// </summary>
        /// <param name="request">La consulta AllInscriptionsByScheduleIdQuery que especifica la busqueda de todos las inscripciones segun un cronograma.</param>
        /// <param name="cancellationToken">El token de cancelación que puede detener la operación en cualquier momento.</param>
        /// <returns>Una lista de todos las inscripciones de un cronograma.</returns>
        public Task<AllInscriptionsResponse> Handle(AllInscriptionsByScheduleIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (request == null)
                {
                    _logger.LogWarning("AllInscriptionsByScheduleIdQueryHandler.Handle: Request null.");

                    throw new ArgumentNullException(nameof(request));

                }
                else
                {
                    return HandleAsync(request);
                }
            }
            catch
            {
                _logger.LogWarning("AllInscriptionsByScheduleIdQueryHandler.Handle: ArgumentNullException");
                throw;
            }
            throw new NotImplementedException();
        }
        /// <summary>
        /// Método asincronico que maneja la consulta de todos las inscripciones segun un cronograma.
        /// </summary>
        /// <param name="request">La consulta AllInscriptionsByScheduleIdQuery que especifica la busqueda de todos las inscripciones segun un cronogramas.</param>
        /// <returns>Una lista de inscripciones segun un cronograma.</returns>
        private async Task<AllInscriptionsResponse> HandleAsync(AllInscriptionsByScheduleIdQuery request)
        {
            try
            {
                _logger.LogInformation("AllInscriptionsByScheduleIdQueryHandler.HandleAsync");
                var schedule = _dbContext.Cronogramas.FirstOrDefault(c => c.Id == request.ScheduleId);
                if (schedule == null)
                {
                    throw new IdNotFoundException("El cronograma seleccionado no existe");
                }
                var response = await _dbContext.Cronogramas
                            .Include(c => c.Modulo)
                            .Include(c => c.Instructor)
                            .Where(c => c.Id == request.ScheduleId)
                            .Select(c => new AllInscriptionsResponse
                            {
                                ModulCompleteName = c.Modulo.NombreCompleto,
                                CourseCompleteName = c.Modulo.Curso.NombreCompleto,
                                ModulName = c.Modulo.Nombre,
                                Code = c.Codigo,
                                StartDate = c.FechaInicio.ToString("dd/MM/yyyy"),
                                EndDate = c.FechaFin.HasValue ? c.FechaFin.Value.ToString("dd/MM/yyyy") : string.Empty,
                                Horario = c.Horario_Dias,
                                Modalidad = c.Modalidad.ToString(),
                                Regularidad = c.Regularidad.ToString(),
                                Turno = c.Turno.ToString(),
                                Instructor = c.Instructor != null ? c.Instructor.Nombre+" "+c.Instructor.Apellido: "No Asignado"
                            })
                            .FirstOrDefaultAsync();
                if (response == null)
                {
                    throw new DataNotFoundException("Ocurrio un error al consultar las inscripciones del cronograma");
                }
                var inscriptions = await _dbContext.Inscripciones.Where(c => c.CronogramaId == schedule.Id).OrderBy(c => c.NroInscripcion)
                    .Select(c => new StudentRegiteredOnInscriptionResponse()
                    {
                        InscriptionId = c.Id,
                        Cedula = c.Estudiante.Cedula,
                        Name = c.Estudiante.Nombre,
                        LastName = c.Estudiante.Apellido,
                        CellPhone = c.Estudiante.Telefono,
                        Email = c.Estudiante.Correo,
                        NroInscription = c.NroInscripcion
                    }).ToListAsync();
                response.Students = inscriptions;
                return response;
            }
            catch (DataNotFoundException ex)
            {
                _logger.LogError(ex, "Error AllInscriptionsByScheduleIdQueryHandler.HandleAsync. {Los datos ingresados no son validos}", ex.Message);
                throw;
            }
            catch (IdNotFoundException ex) 
            {
                _logger.LogError(ex, "Error AllInscriptionsByScheduleIdQueryHandler.HandleAsync. {Los datos ingresados no son validos}", ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error AllInscriptionsByScheduleIdQueryHandler.HandleAsync. {Los datos ingresados no son validos}", ex.Message);
                throw;
            }
        }
    }
}
