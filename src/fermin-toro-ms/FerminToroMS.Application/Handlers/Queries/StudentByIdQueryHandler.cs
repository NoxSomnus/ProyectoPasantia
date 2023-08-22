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
    public class StudentByIdQueryHandler : IRequestHandler<StudentByIdQuery, StudentResponse>
    {
        private readonly IFerminToroDbContext _dbContext;
        private readonly ILogger<StudentByIdQueryHandler> _logger;

        /// <summary>
        /// Constructor de la clase StudentByIdQueryHandler.
        /// </summary>
        /// <param name="dbContext">El contexto de la base de datos que se utilizará para buscar el usuario.</param>
        /// <param name="logger">El objeto ILogger que se utilizará para registrar la actividad del manejador de consulta.</param>
        public StudentByIdQueryHandler(IFerminToroDbContext dbContext, ILogger<StudentByIdQueryHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        /// <summary>
        /// Manejador de la consulta que busca un estudiante por Id.
        /// </summary>
        /// <param name="request">La consulta StudentByIdQuery que especifica la busqueda del estudiante por Id.</param>
        /// <param name="cancellationToken">El token de cancelación que puede detener la operación en cualquier momento.</param>
        /// <returns>Un objeto JSON que contiene toda la informacion del estudiante.</returns>
        public Task<StudentResponse> Handle(StudentByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (request == null)
                {
                    _logger.LogWarning("StudentByIdQueryHandler.Handle: Request null.");

                    throw new ArgumentNullException(nameof(request));

                }
                else
                {
                    return HandleAsync(request);
                }
            }
            catch
            {
                _logger.LogWarning("StudentByIdQueryHandler.Handle: ArgumentNullException");
                throw;
            }
            throw new NotImplementedException();
        }
        /// <summary>
        /// Método asincronico que maneja la consulta de todos los empleados registrados.
        /// </summary>
        /// <param name="request">La consulta EmployeeByIdQuery que especifica la busqueda de un empleado por Id.</param>
        /// <returns>Un objeto JSON que contiene toda la informacion del empleado.</returns>
        private async Task<StudentResponse> HandleAsync(StudentByIdQuery request)
        {
            try
            {
                _logger.LogInformation("EmployeeByIdQueryHandler.HandleAsync");
                var student = await _dbContext.Estudiantes.FirstOrDefaultAsync(c => c.Id == request.Id);
                if (student == null)
                {
                    throw new UserIdNotFoundException("No se encontro el estudiante");
                }
                var response = new StudentResponse
                {
                    StudentId = student.Id,
                    Cedula = student.Cedula,
                    StudentName = student.Nombre,
                    StudentLastName = student.Apellido,
                    StudentEmail = student.Correo,
                    StudentCellPhone = student.Telefono,
                    StudentAge = student.Edad == null ? null : student.Edad.ToString(),
                    StudentDirection = student.Direccion_Hab,
                    YearRange = student.Rango_Edad == null ? null : student.Rango_Edad,
                    Beca = student.Porcentaje_Beca
                };
                return response;
            }
            catch (UserIdNotFoundException ex)
            {
                _logger.LogError(ex, "Error StudentByIdQueryHandler.HandleAsync.", ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error StudentByIdQueryHandler.HandleAsync. {Los datos ingresados no son validos}", ex.Message);
                throw;
            }
        }

    }
}
