﻿using FerminToroMS.Application.Exceptions;
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
                                ScheduleId = schedule.Id,
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
                var inscripciones = new List<StudentRegiteredOnInscriptionResponse>();
                var pagos = new List<InscriptionsPaymentsResponse>();
                var inscriptions = await _dbContext.Inscripciones
                    .Include(p=>p.Pagos)
                    .ThenInclude(p=>p.MetodoPago)
                    .Include(e=>e.Estudiante)
                    .Where(c => c.CronogramaId == schedule.Id).OrderBy(c => c.NroInscripcion).ToListAsync();
                foreach (var inscription in inscriptions) 
                {
                    var studentRegistered = new StudentRegiteredOnInscriptionResponse
                    {
                        InscriptionId = inscription.Id,
                        StudentId = inscription.EstudianteId,
                        Cedula = inscription.Estudiante.Cedula,
                        Name = inscription.Estudiante.Nombre,
                        LastName = inscription.Estudiante.Apellido,
                        CellPhone = inscription.Estudiante.Telefono,
                        Email = inscription.Estudiante.Correo,
                        NroInscription = inscription.NroInscripcion
                    };
                    if (inscription.Pagos != null && inscription.Pagos.Count() != 0)
                    {
                        foreach (var payment in inscription.Pagos)
                        {
                            studentRegistered.TotalPaid = studentRegistered.TotalPaid + payment.Monto;
                            var pago = new InscriptionsPaymentsResponse
                            {
                                InscripcionId = inscription.Id,
                                MetodoPagoId = payment.MetodoPagoId,
                                Estado = inscription.EstadoSolvencia != null ? inscription.EstadoSolvencia : " ",
                                Comentario = payment.Comentarios != null ? payment.Comentarios : "",
                                Cuotas = payment.PorCuotas,
                                Divisa = payment.EnDivisa,
                                Fecha = payment.Fecha.ToString("dd/MM/yyyy"),
                                MetodoPago = payment.MetodoPago.NombreMetodo,
                                Monto = payment.Monto,
                                NroFactura = payment.NroFactura,
                                NroRecibo = payment.NroRecibo,
                                NroInscripcion = inscription.NroInscripcion,
                                UrlComprobante = payment.URLComprobante,
                                StudentName = inscription.Estudiante.Nombre + " " + inscription.Estudiante.Apellido
                            };
                            pagos.Add(pago);
                        }
                        studentRegistered.ByCuota = inscription.Pagos.First().PorCuotas;
                        studentRegistered.EsJuridico = inscription.Pagos.First().EsJuridico;
                        studentRegistered.HasPayment = true;
                    }
                    else 
                    {
                        studentRegistered.HasPayment = false;
                    }
                    inscripciones.Add(studentRegistered);
                }

                response.Students = inscripciones;
                response.Payments = pagos;
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
