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
    /// <summary>
    /// Manejador de la consulta que verifica si un empleado tiene el permiso a checkear.
    /// </summary>
    internal class CheckPermissionQueryHandler : IRequestHandler<CheckPermissionQuery, GeneralResponse>
    {
        private readonly IFerminToroDbContext _dbContext;
        private readonly ILogger<CheckPermissionQueryHandler> _logger;

        /// <summary>
        /// Constructor de la clase LoginQueryHandler.
        /// </summary>
        /// <param name="dbContext">El contexto de la base de datos que se utilizará para buscar el usuario.</param>
        /// <param name="logger">El objeto ILogger que se utilizará para registrar la actividad del manejador de consulta.</param>
        public CheckPermissionQueryHandler(IFerminToroDbContext dbContext, ILogger<CheckPermissionQueryHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        /// <summary>
        /// Manejador de la consulta que verifica si un usuario tiene el permiso asignado.
        /// </summary>
        /// <param name="request">La consulta CheckPermissionQuery que especifica el nombre del permiso y el Id del usuario.</param>
        /// <param name="cancellationToken">El token de cancelación que puede detener la operación en cualquier momento.</param>
        /// <returns>Un bool es true si el usuario tiene ese permiso o false en caso contrario.</returns>
        public Task<GeneralResponse> Handle(CheckPermissionQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (request == null)
                {
                    _logger.LogWarning("CheckPermissionQueryHandler.Handle: Request null.");

                    throw new ArgumentNullException(nameof(request));

                }
                else
                {
                    return HandleAsync(request);
                }
            }
            catch
            {
                _logger.LogWarning("CheckPermissionQueryHandler.Handle: ArgumentNullException");
                throw;
            }
            throw new NotImplementedException();
        }
        /// <summary>
        /// Método asincronico que maneja la consulta de si el permiso esta asignado al usuario.
        /// </summary>
        /// <param name="request">La consulta CheckPermissionQuery que especifica el id del usuario y el nombre del permiso.</param>
        /// <returns>Un bool que dice si tiene el permiso asignado o no.</returns>
        private async Task<GeneralResponse> HandleAsync(CheckPermissionQuery request)
        {
            try
            {
                _logger.LogInformation("CheckPermissionQueryHandler.HandleAsync");
                var permission = await _dbContext.Permisos.FirstOrDefaultAsync(c => c.NombrePermiso == request.Permission);
                if (permission == null) 
                {
                    throw new DataNotFoundException("El permiso no existe");
                }
                var user = await _dbContext.Empleados.FirstOrDefaultAsync(c => c.Id == request.UserId);
                if (user == null)
                {
                    throw new UserIdNotFoundException("El usuario no existe");
                }
                var permissionassigned = await _dbContext.Permisos_Empleados.
                    AnyAsync(c => c.EmpleadoId == request.UserId && c.PermisoId == permission.Id);
                if (!permissionassigned) 
                {
                    return new GeneralResponse { Success = false, Message = "Usted no tiene permiso para acceder a esta opcion" };
                }
                return new GeneralResponse { Success = true, Message = "Permiso Concedido" }; ;
            }
            catch (UserIdNotFoundException ex)
            {
                _logger.LogError(ex, "Error CheckPermissionQueryHandler.HandleAsync. {Usuario no encontrado}", ex.Message);
                throw;
            }
            catch (DataNotFoundException ex)
            {
                _logger.LogError(ex, "Error CheckPermissionQueryHandler.HandleAsync. {Permiso no encontrado}", ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error CheckPermissionQueryHandler.HandleAsync. {Los datos ingresados no son validos}", ex.Message);
                throw;
            }
        }
    }
}
