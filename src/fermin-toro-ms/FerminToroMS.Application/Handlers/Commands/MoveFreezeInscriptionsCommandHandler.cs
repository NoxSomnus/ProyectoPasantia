using FerminToroMS.Application.Commands;
using FerminToroMS.Application.Exceptions;
using FerminToroMS.Application.Mappers;
using FerminToroMS.Core.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerminToroMS.Application.Handlers.Commands
{
    internal class MoveFreezeInscriptionsCommandHandler : IRequestHandler<MoveFreezeInscriptionsCommand, bool>
    {
        private readonly IFerminToroDbContext _dbContext;
        private readonly ILogger<MoveFreezeInscriptionsCommandHandler> _logger;

        /// <summary>
        /// Constructor de la clase MoveFreezeInscriptionsCommandHandler.
        /// </summary>
        /// <param name="dbContext">El contexto de la base de datos que se utilizará para buscar el usuario.</param>
        /// <param name="logger">El objeto ILogger que se utilizará para registrar la actividad del manejador del comando.</param>
        public MoveFreezeInscriptionsCommandHandler(IFerminToroDbContext dbContext, ILogger<MoveFreezeInscriptionsCommandHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        /// <summary>
        /// Manejador del comando que mueve las inscripciones congeladas a un nuevo curso abierto en un cronograma posterior.
        /// </summary>
        /// <param name="request">El comando MoveFreezeInscriptionsCommand que contiene las inscripciones a ser movidas o mantener congeladas</param>
        /// <param name="cancellationToken">El token de cancelación que puede detener la operación en cualquier momento.</param>
        /// <returns>Un bool que contiene información si la operacion fue exitosa o no.</returns>
        public Task<bool> Handle(MoveFreezeInscriptionsCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request == null)
                {
                    _logger.LogWarning("MoveFreezeInscriptionsCommandHandler.Handle: Request null.");

                    throw new ArgumentNullException(nameof(request));

                }
                else
                {
                    return HandleAsync(request);
                }
            }
            catch
            {
                _logger.LogWarning("MoveFreezeInscriptionsCommandHandler.Handle: ArgumentNullException");
                throw;
            }
            throw new NotImplementedException();
        }

        /// <summary>
        /// Método asincronico que mueve las inscripciones congeladas a un nuevo curso abierto en un cronograma posterior.
        /// </summary>
        /// <param name="request">El comando CreateScheduleCommand que especifica los datos del nuevo cronograma.</param>
        /// <returns>Un bool que contiene información de si la operacion de registro fue exitosa o no.</returns>
        private async Task<bool> HandleAsync(MoveFreezeInscriptionsCommand request)
        {
            var transaction = _dbContext.BeginTransaction();
            try
            {
                for (int i = 0; i < request._request.SchedulesId.Count; i++)
                    {
                    if (request._request.SchedulesId[i] != null) 
                    {
                        var inscription = await _dbContext.Inscripciones
                            .FirstOrDefaultAsync(c=>c.Id == request._request.InscriptionsIds[i]);
                        if (inscription == null)
                        {
                            throw new IdNotFoundException("No se encontro la inscripcion");
                        }
                        inscription.CronogramaId = (Guid)request._request.SchedulesId[i];
                        var freezeinscription = _dbContext.InscripcionesCongeladas
                            .FirstOrDefault(c=>c.InscripcionId == inscription.Id);
                        if(freezeinscription == null) throw new IdNotFoundException("No se encontro la inscripcion congelada");
                        _dbContext.Inscripciones.Update(inscription); //mueve la inscripcion
                        _dbContext.InscripcionesCongeladas.Remove(freezeinscription); //quita la referencia de que es una inscripcion congelada
                    }
                }
                await _dbContext.SaveEfContextChanges("APP");
                transaction.Commit();
                return true;
            }
            catch (IdNotFoundException ex)
            {
                _logger.LogError(ex, "Error SetScheduleCodeCommandHandler.HandleAsync. {Mensaje}", ex.Message);
                transaction.Rollback();
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error SetScheduleCodeCommandHandler.HandleAsync. {Mensaje}", ex.Message);
                transaction.Rollback();
                throw;
            }
        }

    }
}
