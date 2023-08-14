using FerminToroMS.Application.Commands;
using FerminToroMS.Application.Exceptions;
using FerminToroMS.Application.Requests;
using FerminToroMS.Application.Responses;
using FerminToroMS.Core.Database;
using FerminToroMS.Core.Entities;
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
    internal class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, bool>
    {
        private readonly IFerminToroDbContext _dbContext;
        private readonly ILogger<UpdateEmployeeCommandHandler> _logger;

        /// <summary>
        /// Constructor de la clase UpdateEmployeeCommandHandler.
        /// </summary>
        /// <param name="dbContext">El contexto de la base de datos que se utilizará para buscar el usuario.</param>
        /// <param name="logger">El objeto ILogger que se utilizará para registrar la actividad del manejador del comando.</param>
        public UpdateEmployeeCommandHandler(IFerminToroDbContext dbContext, ILogger<UpdateEmployeeCommandHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        /// <summary>
        /// Manejador del comando que actualiza un empleado.
        /// </summary>
        /// <param name="request">El comando UpdateEmployeeCommand que especifica los datos a actualizar del nuevo empleado.</param>
        /// <param name="cancellationToken">El token de cancelación que puede detener la operación en cualquier momento.</param>
        /// <returns>Un objeto bool que contiene información si la operacion fue exitosa o no.</returns>
        public Task<bool> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request == null)
                {
                    _logger.LogWarning("UpdateEmployeeCommandHandler.Handle: Request null.");

                    throw new ArgumentNullException(nameof(request));

                }
                else
                {
                    return HandleAsync(request);
                }
            }
            catch
            {
                _logger.LogWarning("UpdateEmployeeCommandHandler.Handle: ArgumentNullException");
                throw;
            }
            throw new NotImplementedException();
        }

        /// <summary>
        /// Método asincronico que maneja la actualizacion de informacion de un empleado.
        /// </summary>
        /// <param name="request">El comando UpdateEmployeeCommand que especifica los datos a actualizar empleado.</param>
        /// <returns>Un objeto bool que contiene información de si la operacion de registro fue exitosa o no.</returns>

        private async Task<bool> HandleAsync(UpdateEmployeeCommand request)
        {
            var transaction = _dbContext.BeginTransaction();
            try 
            {
                var employee = await _dbContext.Empleados.FirstOrDefaultAsync(c=>c.Id == request._request.Id);
                if (employee == null) 
                {
                    throw new UserIdNotFoundException("No se encontró el usuario a actualizar");
                }
                employee = UpdateRequestToEntity(employee,request._request);
                UpdatePermissions(employee,request._request);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error UpdateEmployeeCommandHandler.HandleAsync. {Mensaje}", ex.Message);
                transaction.Rollback();
                throw;
            }
        }

        private EmpleadoEntity UpdateRequestToEntity(EmpleadoEntity employee,UpdateEmployeeRequest request) 
        {
            employee.Cedula = request.Cedula;
            employee.Nombre = request.Nombre;
            employee.Apellido = request.Apellido;
            employee.Username = request.Username;
            employee.Correo = request.Correo;
            employee.esDirector = request.esDirector;
            employee.esAdmin = request.esAdmin;
            employee.esInstructor = request.esInstructor;
            return employee;
        }

        private void UpdatePermissions(EmpleadoEntity employee, UpdateEmployeeRequest request) 
        {
            var Deleted_Permissions = request.Permisos_Asignados.Except(request.Permisos_Nuevos).ToList();
            var Final_Permissions = request.Permisos_Asignados.Union(request.Permisos_Nuevos).ToList();
            Final_Permissions.RemoveAll(p=> Deleted_Permissions.Contains(p));
            //Elimino todos los permisos originales de ese empleado
            var empleadosPermisosAEliminar = _dbContext.Permisos_Empleados.Where(ep => ep.EmpleadoId == request.Id);
            _dbContext.Permisos_Empleados.RemoveRange(empleadosPermisosAEliminar);

            //Para luego volverlo a agregar segun el update
            foreach (var permissions in Final_Permissions) 
            {
                var permiso = new Empleado_PermisoEntity
                {
                    EmpleadoId = request.Id,
                    PermisoId = permissions.PermisoId
                };
                _dbContext.Permisos_Empleados.Add(permiso);
            }           
        }
    }
}
