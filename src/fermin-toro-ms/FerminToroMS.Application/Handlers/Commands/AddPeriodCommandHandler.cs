using FerminToroMS.Application.Commands;
using FerminToroMS.Application.Exceptions;
using FerminToroMS.Application.Mappers;
using FerminToroMS.Application.Responses;
using FerminToroMS.Core.Database;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerminToroMS.Application.Handlers.Commands
{
    internal class AddPeriodCommandHandler : IRequestHandler<AddPeriodCommand, bool>
    {
        private readonly IFerminToroDbContext _dbContext;
        private readonly ILogger<AddPeriodCommandHandler> _logger;

        /// <summary>
        /// Constructor de la clase AddPeriodCommandHandler.
        /// </summary>
        /// <param name="dbContext">El contexto de la base de datos que se utilizará para buscar el usuario.</param>
        /// <param name="logger">El objeto ILogger que se utilizará para registrar la actividad del manejador del comando.</param>
        public AddPeriodCommandHandler(IFerminToroDbContext dbContext, ILogger<AddPeriodCommandHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        /// <summary>
        /// Manejador del comando que registra un nuevo periodo.
        /// </summary>
        /// <param name="request">El comando AddPeriodCommand que especifica los datos a registrar del nuevo periodo.</param>
        /// <param name="cancellationToken">El token de cancelación que puede detener la operación en cualquier momento.</param>
        /// <returns>Un bool que contiene información si la operacion fue exitosa o no.</returns>
        public Task<bool> Handle(AddPeriodCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request == null)
                {
                    _logger.LogWarning("AddPeriodCommandHandler.Handle: Request null.");

                    throw new ArgumentNullException(nameof(request));

                }
                else
                {
                    return HandleAsync(request);
                }
            }
            catch
            {
                _logger.LogWarning("AddPeriodCommandHandler.Handle: ArgumentNullException");
                throw;
            }
            throw new NotImplementedException();
        }

        /// <summary>
        /// Método asincronico que maneja el registro de un nuevo periodo.
        /// </summary>
        /// <param name="request">El comando AddPeriodCommand que especifica los datos del nuevo periodo.</param>
        /// <returns>Un bool que contiene información de si la operacion de registro fue exitosa o no.</returns>
        private async Task<bool> HandleAsync(AddPeriodCommand request)
        {
            var transaction = _dbContext.BeginTransaction();

            try
            {
                _logger.LogInformation("AddPeriodCommandHandler.HandleAsync");

                _dbContext.Periodos.Add(PeriodosMapper.MapRequestToEntity(request._request));
                await _dbContext.SaveEfContextChanges("APP");
                transaction.Commit();
                _logger.LogInformation("AddPeriodCommandHandler.HandleAsync {Response}", true);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error AddPeriodCommandHandler.HandleAsync. {Mensaje}", ex.Message);
                transaction.Rollback();
                throw;
            }
        }

    }
}
