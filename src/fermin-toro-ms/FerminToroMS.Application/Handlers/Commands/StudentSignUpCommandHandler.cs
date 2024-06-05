using FerminToroMS.Application.Commands;
using FerminToroMS.Application.Exceptions;
using FerminToroMS.Application.Mappers;
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
    /// <summary>
    /// Manejador de comando que registra un nuevo estudiante.
    /// </summary>
    internal class StudentSignUpCommandHandler : IRequestHandler<StudentSignUpCommand, GeneralResponse>
    {
        private readonly IFerminToroDbContext _dbContext;
        private readonly ILogger<StudentSignUpCommandHandler> _logger;

        /// <summary>
        /// Constructor de la clase StudentSignUpCommandHandler.
        /// </summary>
        /// <param name="dbContext">El contexto de la base de datos que se utilizará para buscar el usuario.</param>
        /// <param name="logger">El objeto ILogger que se utilizará para registrar la actividad del manejador del comando.</param>
        public StudentSignUpCommandHandler(IFerminToroDbContext dbContext, ILogger<StudentSignUpCommandHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        /// <summary>
        /// Manejador del comando que registra un nuevo estudiante.
        /// </summary>
        /// <param name="request">El comando StudentSignUpCommand que especifica los datos a registrar del nuevo estudiante.</param>
        /// <param name="cancellationToken">El token de cancelación que puede detener la operación en cualquier momento.</param>
        /// <returns>Un objeto GeneralResponse que contiene información si la operacion fue exitosa o no.</returns>
        public Task<GeneralResponse> Handle(StudentSignUpCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request == null)
                {
                    _logger.LogWarning("StudentSignUpCommandHandler.Handle: Request null.");

                    throw new ArgumentNullException(nameof(request));

                }
                else
                {
                    return HandleAsync(request);
                }
            }
            catch
            {
                _logger.LogWarning("StudentSignUpCommandHandler.Handle: ArgumentNullException");
                throw;
            }
            throw new NotImplementedException();
        }

        /// <summary>
        /// Método asincronico que maneja el registro del nuevo estudiante.
        /// </summary>
        /// <param name="request">El comando StudentSignUpCommand que especifica los datos del nuevo estudiante.</param>
        /// <returns>Un objeto GeneralResponse que contiene información de si la operacion de registro fue exitosa o no.</returns>
        private async Task<GeneralResponse> HandleAsync(StudentSignUpCommand request)
        {
            var transaction = _dbContext.BeginTransaction();

            try
            {
                _logger.LogInformation("EmployeeSignUpCommandHandler.HandleAsync");
                
                var cedulaExists = _dbContext.Empleados.Count(c => c.Cedula == request._request.Cedula);
                if (cedulaExists > 0)
                {
                    throw new DataAlreadyExistException("Error: La cedula ya fue registrada");
                }
                var emailExists = _dbContext.Estudiantes.Count(c => c.Correo == request._request.Correo);
                if (emailExists > 0)
                {
                    throw new DataAlreadyExistException("Error: El correo introducido ya esta en uso");
                }               
                _dbContext.Estudiantes.Add(EstudiantesMapper.MapRequestToEntity(request._request));
                await _dbContext.SaveEfContextChanges("APP");
                transaction.Commit();
                var response = new GeneralResponse
                {
                    Message = "Registro Exitoso",
                    Success = true
                };
                _logger.LogInformation("StudentSignUpCommandHandler.HandleAsync {Response}", response);
                return response;
            }
            catch (DataAlreadyExistException ex)
            {
                _logger.LogError(ex, "Error StudentSignUpCommandHandler.HandleAsync El estudiante ya existe", ex.Message);
                transaction.Rollback();
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error StudentSignUpCommandHandler.HandleAsync. {Mensaje}", ex.Message);
                transaction.Rollback();
                throw;
            }
        }
    }
}
