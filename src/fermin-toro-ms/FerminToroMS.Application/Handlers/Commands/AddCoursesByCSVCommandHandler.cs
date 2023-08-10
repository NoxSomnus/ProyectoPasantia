using FerminToroMS.Application.Commands;
using FerminToroMS.Application.Exceptions;
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
    /// Manejador de comando que registra nuevos cursos mediante lectura csv.
    /// </summary>
    internal class AddCoursesByCSVCommandHandler : IRequestHandler<AddCoursesByCSVCommand, bool>
    {
        private readonly IFerminToroDbContext _dbContext;
        private readonly ILogger<AddCoursesByCSVCommandHandler> _logger;

        /// <summary>
        /// Constructor de la clase AddPermissionCommandHandler.
        /// </summary>
        /// <param name="dbContext">El contexto de la base de datos que se utilizará para buscar el usuario.</param>
        /// <param name="logger">El objeto ILogger que se utilizará para registrar la actividad del manejador del comando.</param>
        public AddCoursesByCSVCommandHandler(IFerminToroDbContext dbContext, ILogger<AddCoursesByCSVCommandHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        /// <summary>
        /// Manejador del comando que registra un nuevo permiso al sistema.
        /// </summary>
        /// <param name="request">El comando AddPermissionCommand que especifica el nombre del nuevo permiso.</param>
        /// <param name="cancellationToken">El token de cancelación que puede detener la operación en cualquier momento.</param>
        /// <returns>Un objeto GeneralResponse que contiene información si la operacion fue exitosa o no.</returns>
        public Task<bool> Handle(AddCoursesByCSVCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request == null)
                {
                    _logger.LogWarning("AddPermissionCommandHandler.Handle: Request null.");

                    throw new ArgumentNullException(nameof(request));

                }
                else
                {
                    return HandleAsync(request);
                }
            }
            catch
            {
                _logger.LogWarning("AddPermissionCommandHandler.Handle: ArgumentNullException");
                throw;
            }
            throw new NotImplementedException();
        }

        /// <summary>
        /// Método asincronico que maneja el registro del nuevo empleado.
        /// </summary>
        /// <param name="request">El comando EmployeeSignUpCommand que especifica los datos del nuevo empleado.</param>
        /// <returns>Un objeto GeneralResponse que contiene información de si la operacion de registro fue exitosa o no.</returns>
        private async Task<bool> HandleAsync(AddCoursesByCSVCommand request)
        {
            var transaction = _dbContext.BeginTransaction();

            try
            {
                _logger.LogInformation("AddPermissionCommandHandler.HandleAsync");
                //borra el primero
                request._request.Courses.Remove(request._request.Courses.First());
                foreach (var courserequest in request._request.Courses)
                {
                    var courseId = Guid.NewGuid();
                    var course = _dbContext.Cursos.FirstOrDefault(c => c.Nombre == courserequest.CourseName);
                    if (course == null)
                    {
                        var curso = new CursoEntity
                        {
                            Id = courseId,
                            Nombre = courserequest.CourseName,
                        };
                        _dbContext.Cursos.Add(curso);
                        course = curso;
                    }
                    var modul = _dbContext.Modulos.FirstOrDefault(c => c.Nombre == courserequest.ModulName
                    && c.CursoId == course.Id);
                    if (modul == null)
                    {
                        modul = new ModuloEntity
                        {
                            CursoId = course.Id,
                            Nombre = courserequest.ModulName
                        };
                        _dbContext.Modulos.Add(modul);
                    }
                    await _dbContext.SaveEfContextChanges("APP");
                }

                transaction.Commit();

                _logger.LogInformation("AddPermissionCommandHandler.HandleAsync {Response}", true);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error AddPermissionCommandHandler.HandleAsync. {Mensaje}", ex.Message);
                transaction.Rollback();
                throw;
            }
        }

    }
}
