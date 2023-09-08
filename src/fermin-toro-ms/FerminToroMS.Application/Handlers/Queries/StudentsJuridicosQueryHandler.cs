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
    internal class StudentsJuridicosQueryHandler : IRequestHandler<StudentsJuridicosQuery, List<AllStudentsResponse>>
    {
        private readonly IFerminToroDbContext _dbContext;
        private readonly ILogger<StudentsJuridicosQueryHandler> _logger;

        /// <summary>
        /// Constructor de la clase StudentsJuridicosQueryHandler.
        /// </summary>
        /// <param name="dbContext">El contexto de la base de datos que se utilizará para buscar el usuario.</param>
        /// <param name="logger">El objeto ILogger que se utilizará para registrar la actividad del manejador de consulta.</param>
        public StudentsJuridicosQueryHandler(IFerminToroDbContext dbContext, ILogger<StudentsJuridicosQueryHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        /// <summary>
        /// Manejador de la consulta que busca todos los estudiantes registrados.
        /// </summary>
        /// <param name="request">La consulta StudentsJuridicosQuery que especifica la busqueda de todos los estudiantes juridicos.</param>
        /// <param name="cancellationToken">El token de cancelación que puede detener la operación en cualquier momento.</param>
        /// <returns>Una lista de todos los estudiantes juridicos del sistema.</returns>
        public Task<List<AllStudentsResponse>> Handle(StudentsJuridicosQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (request == null)
                {
                    _logger.LogWarning("StudentsJuridicosQueryHandler.Handle: Request null.");

                    throw new ArgumentNullException(nameof(request));

                }
                else
                {
                    return HandleAsync();
                }
            }
            catch
            {
                _logger.LogWarning("StudentsJuridicosQueryHandler.Handle: ArgumentNullException");
                throw;
            }
            throw new NotImplementedException();
        }
        /// <summary>
        /// Método asincronico que maneja la consulta de todos los estudiantes registrados.
        /// </summary>
        /// <param name="request">La consulta StudentsJuridicosQuery que especifica la busqueda de todos los estudiantes juridicos.</param>
        /// <returns>Una lista de estudiantes juridicos del sistema.</returns>
        private async Task<List<AllStudentsResponse>> HandleAsync()
        {
            try
            {
                _logger.LogInformation("StudentsJuridicosQueryHandler.HandleAsync");
                var students = await _dbContext.Estudiantes.Where(c=>c.EsJuridico == true).OrderBy(c => c.Apellido)
                    .Select(c => new AllStudentsResponse()
                    {
                        StudentId = c.Id,
                        Cedula = c.Cedula,
                        StudentName = c.Nombre,
                        StudentLastName = c.Apellido,
                        StudentCellPhone = c.Telefono,
                        StudentAge = c.Edad == null ? null : c.Edad.ToString(),
                        StudentEmail = c.Correo
                    }).ToListAsync();
                return students;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error StudentsJuridicosQueryHandler.HandleAsync. {Los datos ingresados no son validos}", ex.Message);
                throw;
            }
        }

    }
}
