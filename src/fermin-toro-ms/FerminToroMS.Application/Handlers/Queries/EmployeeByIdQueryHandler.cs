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
    internal class EmployeeByIdQueryHandler : IRequestHandler<EmployeeByIdQuery, EmployeeResponse>
    {
        private readonly IFerminToroDbContext _dbContext;
        private readonly ILogger<EmployeeByIdQueryHandler> _logger;

        /// <summary>
        /// Constructor de la clase EmployeeByIdQueryHandler.
        /// </summary>
        /// <param name="dbContext">El contexto de la base de datos que se utilizará para buscar el usuario.</param>
        /// <param name="logger">El objeto ILogger que se utilizará para registrar la actividad del manejador de consulta.</param>
        public EmployeeByIdQueryHandler(IFerminToroDbContext dbContext, ILogger<EmployeeByIdQueryHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        /// <summary>
        /// Manejador de la consulta que busca un empleado por Id.
        /// </summary>
        /// <param name="request">La consulta EmployeeByIdQuery que especifica la busqueda del empleado por Id.</param>
        /// <param name="cancellationToken">El token de cancelación que puede detener la operación en cualquier momento.</param>
        /// <returns>Un objeto JSON que contiene toda la informacion del empleado.</returns>
        public Task<EmployeeResponse> Handle(EmployeeByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (request == null)
                {
                    _logger.LogWarning("EmployeeByIdQueryHandler.Handle: Request null.");

                    throw new ArgumentNullException(nameof(request));

                }
                else
                {
                    return HandleAsync(request);
                }
            }
            catch
            {
                _logger.LogWarning("EmployeeByIdQueryHandler.Handle: ArgumentNullException");
                throw;
            }
            throw new NotImplementedException();
        }
        /// <summary>
        /// Método asincronico que maneja la consulta de todos los empleados registrados.
        /// </summary>
        /// <param name="request">La consulta EmployeeByIdQuery que especifica la busqueda de un empleado por Id.</param>
        /// <returns>Un objeto JSON que contiene toda la informacion del empleado.</returns>
        private async Task<EmployeeResponse> HandleAsync(EmployeeByIdQuery request)
        {
            try
            {
                _logger.LogInformation("EmployeeByIdQueryHandler.HandleAsync");
                var employee = await _dbContext.Empleados.FirstOrDefaultAsync(c => c.Id == request.EmployeeId);
                if (employee == null)
                {
                    throw new UserIdNotFoundException("No se encontro el usuario");
                }
                var permissions = await _dbContext.Permisos_Empleados
                    .Where(pe => pe.EmpleadoId == request.EmployeeId)
                    .Join(_dbContext.Permisos,
                        pe => pe.PermisoId,
                        p => p.Id,
                        (pe, p) => new PermissionResponse
                        {
                            IdPermiso = pe.PermisoId,
                            NombrePermiso = p.NombrePermiso
                        })
                    .ToListAsync();
                var response = new EmployeeResponse
                {
                    Id = employee.Id,
                    Cedula = employee.Cedula,
                    Nombre = employee.Nombre,
                    Apellido = employee.Apellido,
                    Correo = employee.Correo,
                    Username = employee.Username,
                    esAdmin = employee.esAdmin,
                    esDirector = employee.esDirector,
                    esInstructor = employee.esInstructor,
                    permisos_asignados = permissions
                };
                return response;
            }
            catch (UserIdNotFoundException ex) 
            {
                _logger.LogError(ex, "Error EmployeeByIdQueryHandler.HandleAsync.", ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error EmployeeByIdQueryHandler.HandleAsync. {Los datos ingresados no son validos}", ex.Message);
                throw;
            }
        }
    }
}
