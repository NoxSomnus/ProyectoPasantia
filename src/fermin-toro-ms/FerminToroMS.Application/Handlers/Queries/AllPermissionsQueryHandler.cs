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
    internal class AllPermissionsQueryHandler : IRequestHandler<AllPermissionsQuery, List<PermissionResponse>>
    {
        private readonly IFerminToroDbContext _dbContext;
        private readonly ILogger<AllPermissionsQueryHandler> _logger;

        /// <summary>
        /// Constructor de la clase AllPermissionsQueryHandler.
        /// </summary>
        /// <param name="dbContext">El contexto de la base de datos que se utilizará para buscar el usuario.</param>
        /// <param name="logger">El objeto ILogger que se utilizará para registrar la actividad del manejador de consulta.</param>
        public AllPermissionsQueryHandler(IFerminToroDbContext dbContext, ILogger<AllPermissionsQueryHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        /// <summary>
        /// Manejador de la consulta que busca todos los permisos disponibles.
        /// </summary>
        /// <param name="request">La consulta AllPermissionsQuery que especifica la busqueda de todos los permisos.</param>
        /// <param name="cancellationToken">El token de cancelación que puede detener la operación en cualquier momento.</param>
        /// <returns>Una lista de todos los permisos del sistema.</returns>
        public Task<List<PermissionResponse>> Handle(AllPermissionsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (request == null)
                {
                    _logger.LogWarning("AllPermissionsQueryHandler.Handle: Request null.");

                    throw new ArgumentNullException(nameof(request));

                }
                else
                {
                    return HandleAsync(request);
                }
            }
            catch
            {
                _logger.LogWarning("AllPermissionsQueryHandler.Handle: ArgumentNullException");
                throw;
            }
            throw new NotImplementedException();
        }
        /// <summary>
        /// Método asincronico que maneja la consulta de todos los permisos.
        /// </summary>
        /// <param name="request">La consulta AllPermissionsQuery que especifica la busqueda de todos los permisos.</param>
        /// <returns>Una lista de permisos del sistema.</returns>
        private async Task<List<PermissionResponse>> HandleAsync(AllPermissionsQuery request)
        {
            try
            {
                _logger.LogInformation("AllPermissionsQueryHandler.HandleAsync");
                var permissions = await _dbContext.Permisos.Select(c => new PermissionResponse()
                {
                    IdPermiso = c.Id,
                    NombrePermiso = c.NombrePermiso
                }).ToListAsync();
                return permissions;                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error AllPermissionsQueryHandler.HandleAsync. {Los datos ingresados no son validos}", ex.Message);
                throw;
            }
        }
    }
}
