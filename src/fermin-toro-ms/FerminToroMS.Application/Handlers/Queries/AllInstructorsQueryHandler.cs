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
    public class AllInstructorsQueryHandler : IRequestHandler<AllInstructorsQuery, List<AllInstructorsResponse>>
    {
        private readonly IFerminToroDbContext _dbContext;
        private readonly ILogger<AllInstructorsQueryHandler> _logger;

        /// <summary>
        /// Constructor de la clase AllInstructorsQueryHandler.
        /// </summary>
        /// <param name="dbContext">El contexto de la base de datos que se utilizará para buscar el usuario.</param>
        /// <param name="logger">El objeto ILogger que se utilizará para registrar la actividad del manejador de consulta.</param>
        public AllInstructorsQueryHandler(IFerminToroDbContext dbContext, ILogger<AllInstructorsQueryHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        /// <summary>
        /// Manejador de la consulta que busca todos los instructores registrados.
        /// </summary>
        /// <param name="request">La consulta AllInstructorsQuery que especifica la busqueda de todos los instructores.</param>
        /// <param name="cancellationToken">El token de cancelación que puede detener la operación en cualquier momento.</param>
        /// <returns>Una lista de todos los instructores del sistema.</returns>
        public Task<List<AllInstructorsResponse>> Handle(AllInstructorsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (request == null)
                {
                    _logger.LogWarning("AllInstructorsQueryHandler.Handle: Request null.");

                    throw new ArgumentNullException(nameof(request));

                }
                else
                {
                    return HandleAsync(request);
                }
            }
            catch
            {
                _logger.LogWarning("AllInstructorsQueryHandler.Handle: ArgumentNullException");
                throw;
            }
            throw new NotImplementedException();
        }
        /// <summary>
        /// Método asincronico que maneja la consulta de todos los instructores registrados.
        /// </summary>
        /// <param name="request">La consulta AllInstructorsQuery que especifica la busqueda de todos los instructores.</param>
        /// <returns>Una lista de instructores del sistema.</returns>
        private async Task<List<AllInstructorsResponse>> HandleAsync(AllInstructorsQuery request)
        {
            try
            {
                _logger.LogInformation("AllInstructorsQueryHandler.HandleAsync");
                var employees = await _dbContext.Empleados.OrderBy(c => c.Apellido).Where(c=> c.esInstructor == true)
                    .Select(c => new AllInstructorsResponse()
                    {
                        InstructorId = c.Id,
                        InstructorName = c.Nombre + " " + c.Apellido                       
                    }).ToListAsync();
                return employees;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error AllInstructorsQueryHandler.HandleAsync. {Los datos ingresados no son validos}", ex.Message);
                throw;
            }
        }
    }
}
