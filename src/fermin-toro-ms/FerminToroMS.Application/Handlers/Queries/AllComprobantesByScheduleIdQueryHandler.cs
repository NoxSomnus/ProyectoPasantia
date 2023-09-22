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
    internal class AllComprobantesByScheduleIdQueryHandler : IRequestHandler<AllComprobantesByScheduleIdQuery, AllComprobantesByScheduleIdResponse>
    {
        private readonly IFerminToroDbContext _dbContext;
        private readonly ILogger<AllComprobantesByScheduleIdQueryHandler> _logger;

        /// <summary>
        /// Constructor de la clase AllComprobantesByScheduleIdQueryHandler.
        /// </summary>
        /// <param name="dbContext">El contexto de la base de datos que se utilizará para buscar el usuario.</param>
        /// <param name="logger">El objeto ILogger que se utilizará para registrar la actividad del manejador de consulta.</param>
        public AllComprobantesByScheduleIdQueryHandler(IFerminToroDbContext dbContext, ILogger<AllComprobantesByScheduleIdQueryHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        /// <summary>
        /// Manejador de la consulta que busca todos los pagos que tengan comprobante.
        /// </summary>
        /// <param name="request">La consulta AllComprobantesByScheduleIdQuery que especifica la busqueda de todos los pagos con comprobates.</param>
        /// <param name="cancellationToken">El token de cancelación que puede detener la operación en cualquier momento.</param>
        /// <returns>Una lista de todos los pagos con comprobantes del sistema.</returns>
        public Task<AllComprobantesByScheduleIdResponse> Handle(AllComprobantesByScheduleIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (request == null)
                {
                    _logger.LogWarning("AllComprobantesByScheduleIdQueryHandler.Handle: Request null.");

                    throw new ArgumentNullException(nameof(request));

                }
                else
                {
                    return HandleAsync(request);
                }
            }
            catch
            {
                _logger.LogWarning("AllComprobantesByScheduleIdQueryHandler.Handle: ArgumentNullException");
                throw;
            }
            throw new NotImplementedException();
        }
        /// <summary>
        /// Método asincronico que maneja la consulta de todos los pagos con comprobantes registrados.
        /// </summary>
        /// <param name="request">La consulta AllComprobantesByScheduleIdQuery que especifica la busqueda de todos los pagos con comprobantes.</param>
        /// <returns>Una lista de pagos con comprobantes del sistema.</returns>
        private async Task<AllComprobantesByScheduleIdResponse> HandleAsync(AllComprobantesByScheduleIdQuery request)
        {
            try
            {
                _logger.LogInformation("AllEmployeesQueryHandler.HandleAsync");
                var schedule = _dbContext.Cronogramas.FirstOrDefault(c => c.Id == request.ScheduleId);
                if (schedule == null)
                {
                    throw new IdNotFoundException("El cronograma seleccionado no existe");
                }
                var response = await _dbContext.Cronogramas
                            .Include(c => c.Modulo)
                            .Include(c => c.Instructor)
                            .Where(c => c.Id == request.ScheduleId)
                            .Select(c => new AllComprobantesByScheduleIdResponse
                            {
                                ModulCompleteName = c.Modulo.NombreCompleto,
                                CourseCompleteName = c.Modulo.Curso.NombreCompleto,
                                ModulId = c.ModuloId,
                                ModulName = c.Modulo.Nombre,
                                Code = c.Codigo,
                                StartDate = c.FechaInicio.ToString("dd/MM/yyyy"),
                                EndDate = c.FechaFin.HasValue ? c.FechaFin.Value.ToString("dd/MM/yyyy") : string.Empty,
                                Horario = c.Horario_Dias,
                                Modalidad = c.Modalidad.ToString(),
                                Regularidad = c.Regularidad.ToString(),
                                Turno = c.Turno.ToString(),
                                Instructor = c.Instructor != null ? c.Instructor.Nombre + " " + c.Instructor.Apellido : "No Asignado"
                            })
                            .FirstOrDefaultAsync();
                if (response == null)
                {
                    throw new DataNotFoundException("Ocurrio un error al consultar las inscripciones del cronograma");
                }
                var MountByCuota = _dbContext.Precios
                    .FirstOrDefault(c=>c.ModuloId == response.ModulId 
                    && c.PorCuotas == true && c.Modalidad == schedule.Modalidad && c.Turno == schedule.Turno);
                var MountByComplete = _dbContext.Precios
                    .FirstOrDefault(c => c.ModuloId == response.ModulId
                    && c.PorCuotas == false && c.Modalidad == schedule.Modalidad && c.Turno == schedule.Turno);
                double MontoPorCuota = MountByCuota != null ? MountByCuota.Precio : 0;
                double MontoCompleto = MountByComplete != null ? MountByComplete.Precio : 0;
                var payments = await _dbContext.Pagos
                    .Include(p => p.Inscripcion)
                    .ThenInclude(i => i.Cronograma)
                    .Include(p => p.Inscripcion)
                    .ThenInclude(i => i.Estudiante)
                    .Include(p=>p.MetodoPago)
                    .Where(p => p.Inscripcion.CronogramaId == request.ScheduleId
                    && p.URLComprobante != null && p.Estado != "Aprobado")
                    .OrderBy(p=>p.Fecha)
                    .Select(p => new AllComprobantes
                    {
                        PaymentId = p.Id,
                        Monto = p.Monto,
                        NroInscripcion = p.Inscripcion.NroInscripcion,
                        StudentName = p.Inscripcion.Estudiante.Nombre + " " + p.Inscripcion.Estudiante.Apellido,
                        StudentEmail = p.Inscripcion.Estudiante.Correo,
                        StudentPhone = p.Inscripcion.Estudiante.Telefono,
                        UrlComprobante = p.URLComprobante != null ? p.URLComprobante : "",
                        MetodoPago = p.MetodoPago.NombreMetodo,
                        Cuota = p.PorCuotas,
                        Monto_A_Pagar = p.PorCuotas ? MontoPorCuota : MontoCompleto,
                        EstadoActual = p.Estado
                    }
                    )
                    .ToListAsync();
                response.Comprobantes = payments;
                return response;
            }
            catch (IdNotFoundException ex)
            {
                _logger.LogError(ex, "Error AllEmployeesQueryHandler.HandleAsync. {Los datos ingresados no son validos}", ex.Message);
                throw;
            }
            catch (DataNotFoundException ex)
            {
                _logger.LogError(ex, "Error AllEmployeesQueryHandler.HandleAsync. {Los datos ingresados no son validos}", ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error AllEmployeesQueryHandler.HandleAsync. {Los datos ingresados no son validos}", ex.Message);
                throw;
            }
        }
    }
}
