using FerminToroMS.Application.Commands;
using FerminToroMS.Application.Exceptions;
using FerminToroMS.Application.Responses;
using FerminToroMS.Core.Database;
using FerminToroMS.Core.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerminToroMS.Application.Handlers.Commands
{
    internal class AddInscriptionsByCSVCommandHandler : IRequestHandler<AddInscriptionsByCSVCommand, AddInscriptionsResponse>
    {
        private readonly IFerminToroDbContext _dbContext;
        private readonly ILogger<AddInscriptionsByCSVCommandHandler> _logger;

        /// <summary>
        /// Constructor de la clase AddInscriptionsByCSVCommandHandler.
        /// </summary>
        /// <param name="dbContext">El contexto de la base de datos que se utilizará para buscar el usuario.</param>
        /// <param name="logger">El objeto ILogger que se utilizará para registrar la actividad del manejador del comando.</param>
        public AddInscriptionsByCSVCommandHandler(IFerminToroDbContext dbContext, ILogger<AddInscriptionsByCSVCommandHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        /// <summary>
        /// Manejador del comando que registra las inscripciones.
        /// </summary>
        /// <param name="request">El comando AddInscriptionsByCSVCommand que especifica las inscripciones a agregar.</param>
        /// <param name="cancellationToken">El token de cancelación que puede detener la operación en cualquier momento.</param>
        /// <returns>Un boll que contiene información si la operacion fue exitosa o no.</returns>
        public Task<AddInscriptionsResponse> Handle(AddInscriptionsByCSVCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request == null)
                {
                    _logger.LogWarning("AddInscriptionsByCSVCommandHandler.Handle: Request null.");

                    throw new ArgumentNullException(nameof(request));

                }
                else
                {
                    return HandleAsync(request);
                }
            }
            catch
            {
                _logger.LogWarning("AddInscriptionsByCSVCommandHandler.Handle: ArgumentNullException");
                throw;
            }
            throw new NotImplementedException();
        }

        /// <summary>
        /// Método asincronico que maneja el registro de las inscripciones.
        /// </summary>
        /// <param name="request">El comando AddInscriptionsByCSVCommand que especifica los datos de las inscripciones a agregar.</param>
        /// <returns>Un bool que contiene información de si la operacion de registro fue exitosa o no.</returns>
        private async Task<AddInscriptionsResponse> HandleAsync(AddInscriptionsByCSVCommand request)
        {
            var transaction = _dbContext.BeginTransaction();
            var cedulasNoRegistradas = new List<string>();
            try
            {
                _logger.LogInformation("AddPermissionCommandHandler.HandleAsync");
                foreach (var inscripcionrequest in request._request.Inscriptions)
                {
                    var period = _dbContext.Periodos.FirstOrDefault(c => c.NombrePeriodo == inscripcionrequest.PeriodName
                        && c.Año == inscripcionrequest.PeriodYear);
                    if (period == null)
                    {
                        throw new DataNotFoundException("El periodo no ha sido encontrado");
                    }
                    var course = _dbContext.Cursos.FirstOrDefault(c => c.Nombre == inscripcionrequest.CourseName);
                    if (course == null)
                    {
                        throw new DataNotFoundException("El curso no ha sido encontrado");
                    }
                    var modul = _dbContext.Modulos.FirstOrDefault(c => c.Nombre == inscripcionrequest.ModulName
                        && c.CursoId == course.Id);
                    if (modul == null)
                    {
                        throw new DataNotFoundException("El modulo no ha sido encontrado");
                    }
                    var schedule = _dbContext.Cronogramas.FirstOrDefault(c => c.PeriodoId == period.Id
                        && c.ModuloId == modul.Id && c.Regularidad == inscripcionrequest.Regularidad
                        && c.Turno == inscripcionrequest.Turno && c.Modalidad == inscripcionrequest.Modalidad);
                    if (schedule == null)
                    {
                        throw new DataNotFoundException("El registro del cronograma no se encontro");
                    }
                    var student = _dbContext.Estudiantes.FirstOrDefault(c => c.Cedula == inscripcionrequest.Cedula);
                    if (student == null)
                    {
                        cedulasNoRegistradas.Add(inscripcionrequest.Cedula);
                        continue;
                    }
                    var inscription = _dbContext.Inscripciones.FirstOrDefault(c=> c.EstudianteId == student.Id
                        && c.CronogramaId == schedule.Id);
                    if (inscription != null)
                    {
                        inscription.PagoEnCuotas = inscripcionrequest.PagoPorCuotas;
                        inscription.Cantidad_A_Pagar = GetModulPrice(modul.Id,schedule, inscripcionrequest.PagoPorCuotas);
                        _dbContext.Inscripciones.Update(inscription);
                    }
                    else
                    {
                        inscription = new InscripcionEntity
                        {
                            CronogramaId = schedule.Id,
                            EstudianteId = student.Id,
                            NroInscripcion = inscripcionrequest.NroInscription,
                            FueraVenezuela = schedule.Modalidad == 0 ? false : true,
                            EstadoSolvencia = "Solvente",
                            FechaInscripcion = DateTime.ParseExact(inscripcionrequest.InscriptionDate, "dd/MM/yyyy", null),
                            PagoEnCuotas = inscripcionrequest.PagoPorCuotas,
                            Cantidad_A_Pagar = GetModulPrice(modul.Id,schedule, inscripcionrequest.PagoPorCuotas)
                        };
                        _dbContext.Inscripciones.Add(inscription);
                    }
                }
                await _dbContext.SaveEfContextChanges("APP");
                transaction.Commit();
                _logger.LogInformation("AddPermissionCommandHandler.HandleAsync {Response}", true);
                var response = new AddInscriptionsResponse
                {
                    Success = true,
                    Message = "Inscripciones realizadas con exito",
                    Cedulas = cedulasNoRegistradas
                };
                return response;
            }
            catch (DataNotFoundException ex) 
            {
                _logger.LogError(ex, "Error AddPermissionCommandHandler.HandleAsync. {Mensaje}", ex.Message);
                transaction.Rollback();
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error AddPermissionCommandHandler.HandleAsync. {Mensaje}", ex.Message);
                transaction.Rollback();
                throw;
            }
        }

        private double GetModulPrice(Guid modulId, CronogramaEntity schedule, bool Cuotas) 
        {
            double price = 0;
            var modulprice = _dbContext.Precios
                .FirstOrDefault(p=>p.ModuloId == modulId && p.PorCuotas == Cuotas 
                && p.Regularidad == schedule.Regularidad && p.Turno == schedule.Turno
                && p.Modalidad == schedule.Modalidad);
            if (modulprice != null) 
            {
                if(modulprice.PorCuotas)price = modulprice.Precio*2;
                else price = modulprice.Precio;
            }
            return price;
        }
    }
}
