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
    /// Manejador de comando que registra los precios de los cursos segun modalidad y turno en el sistema.
    /// </summary>
    public class AddPricesToCourseByCSVCommandHandler : IRequestHandler<AddPricesToCourseByCSVCommand, bool>
    {
        private readonly IFerminToroDbContext _dbContext;
        private readonly ILogger<AddPricesToCourseByCSVCommandHandler> _logger;

        /// <summary>
        /// Constructor de la clase AddPricesToCourseCommandHandler.
        /// </summary>
        /// <param name="dbContext">El contexto de la base de datos que se utilizará para buscar el usuario.</param>
        /// <param name="logger">El objeto ILogger que se utilizará para registrar la actividad del manejador del comando.</param>
        public AddPricesToCourseByCSVCommandHandler(IFerminToroDbContext dbContext, ILogger<AddPricesToCourseByCSVCommandHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        /// <summary>
        /// Manejador del comando que registra los precios de los cursos en el sistema.
        /// </summary>
        /// <param name="request">El comando AddPricesToCourseCommand que especifica los datos para almacenar los precios.</param>
        /// <param name="cancellationToken">El token de cancelación que puede detener la operación en cualquier momento.</param>
        /// <returns>Un bool que retorna true si la operacion fue exitosa.</returns>
        public Task<bool> Handle(AddPricesToCourseByCSVCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request == null)
                {
                    _logger.LogWarning("AddPricesToCourseCommand.Handle: Request null.");

                    throw new ArgumentNullException(nameof(request));

                }
                else
                {
                    return HandleAsync(request);
                }
            }
            catch
            {
                _logger.LogWarning("AddPricesToCourseCommand.Handle: ArgumentNullException");
                throw;
            }
            throw new NotImplementedException();
        }
        /// <summary>
        /// Método asincronico que maneja la adicion de los precios de los cursos.
        /// </summary>
        /// <param name="request">El comando AddPricesToCourseCommand que especifica los datos necesarios para guardar los precios.</param>
        /// <returns>Un bool que especifica si la operacion fue exitosa o no.</returns>
        private async Task<bool> HandleAsync(AddPricesToCourseByCSVCommand request)
        {
            var transaction = _dbContext.BeginTransaction();

            try
            {
                _logger.LogInformation("AddPricesToCourseCommand.HandleAsync");
                foreach (var pricerequest in request._request.Prices) 
                {
                    var course = _dbContext.Cursos.FirstOrDefault(c => c.Nombre == pricerequest.CourseName);
                    if (course == null) 
                    {
                        throw new BadCSVRequest("El formato del csv no es correcto. " +
                            "Asegurese de poner los nombres de los cursos como estan registrados en el sistema");
                    }
                    var precio = new Precio_Mod_TurnoEntity
                    {
                        ModuloId = course.Id,
                        Modalidad = pricerequest.Modalidad,
                        Turno = pricerequest.Turno,
                        Precio = pricerequest.Precio
                    };
                    _dbContext.Precios.Add(precio);
                }
                await _dbContext.SaveEfContextChanges("APP");
                transaction.Commit();
                
                _logger.LogInformation("AddPricesToCourseCommand.HandleAsync {Response}", true);
                return true;
            }
            catch (BadCSVRequest ex)
            {
                _logger.LogError(ex, "Error AddPricesToCourseCommand.HandleAsync. {Mensaje}", ex.Message);
                transaction.Rollback();
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error AddPricesToCourseCommand.HandleAsync. {Mensaje}", ex.Message);
                transaction.Rollback();
                throw;
            }
        }

    }
}
