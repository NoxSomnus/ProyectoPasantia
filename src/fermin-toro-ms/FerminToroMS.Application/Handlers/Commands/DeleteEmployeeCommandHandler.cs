using FerminToroMS.Application.Commands;
using FerminToroMS.Application.Exceptions;
using FerminToroMS.Core.Database;
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
    /// <summary>
    /// Manejador de comando que elimina empleados del sistema.
    /// </summary>
    internal class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand, bool>
    {
        private readonly IFerminToroDbContext _dbContext;
        private readonly ILogger<DeleteEmployeeCommandHandler> _logger;

        /// <summary>
        /// Constructor de la clase DeleteEmployeeCommandHandler.
        /// </summary>
        /// <param name="dbContext">El contexto de la base de datos que se utilizará para buscar el usuario.</param>
        /// <param name="logger">El objeto ILogger que se utilizará para registrar la actividad del manejador del comando.</param>
        public DeleteEmployeeCommandHandler(IFerminToroDbContext dbContext, ILogger<DeleteEmployeeCommandHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        /// <summary>
        /// Manejador del comando que elimina un empleado del sistema.
        /// </summary>
        /// <param name="request">El comando DeleteEmployeeCommand que especifica el id del empleado a eliminar.</param>
        /// <param name="cancellationToken">El token de cancelación que puede detener la operación en cualquier momento.</param>
        /// <returns>Un objeto bool que contiene información si la operacion fue exitosa o no.</returns>
        public Task<bool> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
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
        /// Método asincronico que maneja la eliminacion empleado.
        /// </summary>
        /// <param name="request">El comando DeleteEmployeeCommand que especifica el Id del empleado a eliminar.</param>
        /// <returns>Un objeto bool que contiene información de si la operacion de registro fue exitosa o no.</returns>
        private async Task<bool> HandleAsync(DeleteEmployeeCommand request)
        {
            var transaction = _dbContext.BeginTransaction();
            try
            {
                _logger.LogInformation("EmployeeByIdQueryHandler.HandleAsync");
                var employee = await _dbContext.Empleados.FirstOrDefaultAsync(c => c.Id == request.EmployeeId);
                if (employee == null)
                {
                    throw new UserIdNotFoundException("No se encontro el usuario");
                }
                _dbContext.Empleados.Remove(employee);
                await _dbContext.SaveEfContextChanges("APP");
                transaction.Commit();
                return true;
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
