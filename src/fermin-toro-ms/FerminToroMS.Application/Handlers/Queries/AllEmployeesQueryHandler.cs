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
    internal class AllEmployeesQueryHandler : IRequestHandler<AllEmployeesQuery, List<AllEmployeesResponse>>
    {
        private readonly IFerminToroDbContext _dbContext;
        private readonly ILogger<AllEmployeesQueryHandler> _logger;

        /// <summary>
        /// Constructor de la clase AllEmployeesQueryHandler.
        /// </summary>
        /// <param name="dbContext">El contexto de la base de datos que se utilizará para buscar el usuario.</param>
        /// <param name="logger">El objeto ILogger que se utilizará para registrar la actividad del manejador de consulta.</param>
        public AllEmployeesQueryHandler(IFerminToroDbContext dbContext, ILogger<AllEmployeesQueryHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        /// <summary>
        /// Manejador de la consulta que busca todos los empleados registrados.
        /// </summary>
        /// <param name="request">La consulta AllEmployeesQuery que especifica la busqueda de todos los empleados.</param>
        /// <param name="cancellationToken">El token de cancelación que puede detener la operación en cualquier momento.</param>
        /// <returns>Una lista de todos los empleados del sistema.</returns>
        public Task<List<AllEmployeesResponse>> Handle(AllEmployeesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (request == null)
                {
                    _logger.LogWarning("AllEmployeesQueryHandler.Handle: Request null.");

                    throw new ArgumentNullException(nameof(request));

                }
                else
                {
                    return HandleAsync(request);
                }
            }
            catch
            {
                _logger.LogWarning("AllEmployeesQueryHandler.Handle: ArgumentNullException");
                throw;
            }
            throw new NotImplementedException();
        }
        /// <summary>
        /// Método asincronico que maneja la consulta de todos los empleados registrados.
        /// </summary>
        /// <param name="request">La consulta AllEmployeesQuery que especifica la busqueda de todos los empleados.</param>
        /// <returns>Una lista de empleados del sistema.</returns>
        private async Task<List<AllEmployeesResponse>> HandleAsync(AllEmployeesQuery request)
        {
            try
            {
                _logger.LogInformation("AllEmployeesQueryHandler.HandleAsync");
                var employees = await _dbContext.Empleados.OrderBy(c => c.Cedula)
                    .Select(c => new AllEmployeesResponse()
                    {
                        Id = c.Id,
                        Cedula = c.Cedula,
                        Nombre = c.Nombre + " " + c.Apellido,
                        Username = c.Username,
                    }).ToListAsync();
                return employees;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error AllEmployeesQueryHandler.HandleAsync. {Los datos ingresados no son validos}", ex.Message);
                throw;
            }
        }

    }
}
