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
    /// Manejador de comando que registra un nuevo empleado.
    /// </summary>
    internal class EmployeeSignUpCommandHandler : IRequestHandler<EmployeeSignUpCommand, GeneralResponse>
    {
        private readonly IFerminToroDbContext _dbContext;
        private readonly ILogger<EmployeeSignUpCommandHandler> _logger;

        /// <summary>
        /// Constructor de la clase EmployeeSignUpCommandHandler.
        /// </summary>
        /// <param name="dbContext">El contexto de la base de datos que se utilizará para buscar el usuario.</param>
        /// <param name="logger">El objeto ILogger que se utilizará para registrar la actividad del manejador del comando.</param>
        public EmployeeSignUpCommandHandler(IFerminToroDbContext dbContext, ILogger<EmployeeSignUpCommandHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        /// <summary>
        /// Manejador del comando que registra un nuevo empleado.
        /// </summary>
        /// <param name="request">El comando EmployeeSignUpCommand que especifica los datos a registrar del nuevo empleado.</param>
        /// <param name="cancellationToken">El token de cancelación que puede detener la operación en cualquier momento.</param>
        /// <returns>Un objeto GeneralResponse que contiene información si la operacion fue exitosa o no.</returns>
        public Task<GeneralResponse> Handle(EmployeeSignUpCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request == null)
                {
                    _logger.LogWarning("EmployeeSignUpCommand.Handle: Request null.");

                    throw new ArgumentNullException(nameof(request));

                }
                else
                {
                    return HandleAsync(request);
                }
            }
            catch
            {
                _logger.LogWarning("EmployeeSignUpCommand.Handle: ArgumentNullException");
                throw;
            }
            throw new NotImplementedException();
        }

        /// <summary>
        /// Método asincronico que maneja el registro del nuevo empleado.
        /// </summary>
        /// <param name="request">El comando EmployeeSignUpCommand que especifica los datos del nuevo empleado.</param>
        /// <returns>Un objeto GeneralResponse que contiene información de si la operacion de registro fue exitosa o no.</returns>
        private async Task<GeneralResponse> HandleAsync(EmployeeSignUpCommand request)
        {
            var transaction = _dbContext.BeginTransaction();

            try
            {
                _logger.LogInformation("EmployeeSignUpCommandHandler.HandleAsync");
                var usernameExists = _dbContext.Empleados.Count(c => c.Username == request._request.Username);
                if (usernameExists > 0)
                {
                    throw new DataAlreadyExistException("Error: El usuario ya existe");
                }
                var cedulaExists = _dbContext.Empleados.Count(c => c.Cedula == request._request.Cedula);
                if (cedulaExists > 0)
                {
                    throw new DataAlreadyExistException("Error: La cedula ya fue registrada");
                }
                var UserNewId = Guid.NewGuid();
                var permissions = new List<Empleado_PermisoEntity>();

                if (request._request.Permisos != null) {
                    foreach (var permisos in request._request.Permisos)
                    {
                        var permiso_registrado = _dbContext.Permisos.FirstOrDefault(c => c.Id == permisos.PermisoId);
                        if (permiso_registrado != null)
                        {
                            var permiso_empleado = new Empleado_PermisoEntity();
                            permiso_empleado.EmpleadoId = UserNewId;
                            permiso_empleado.PermisoId = permiso_registrado.Id;
                            _dbContext.Permisos_Empleados.Add(permiso_empleado);
                            permissions.Add(permiso_empleado);
                        }
                    }
                }
                _dbContext.Empleados.Add(EmpleadosMapper.MapRequestToEntity(request._request,permissions,UserNewId));

                await _dbContext.SaveEfContextChanges("APP");
                transaction.Commit();
                var response = new GeneralResponse
                {
                    Message = "Registro Exitoso",
                    Success = true
                };
                _logger.LogInformation("EmployeeSignUpCommandHandler.HandleAsync {Response}", response);
                return response;
            }
            catch (DataAlreadyExistException ex)
            {
                _logger.LogError(ex, "Error EmployeeSignUpCommandHandler.HandleAsync El empleado ya existe", ex.Message);
                transaction.Rollback();
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error EmployeeSignUpCommandHandler.HandleAsync. {Mensaje}", ex.Message);
                transaction.Rollback();
                throw;
            }
        }
    }
}
