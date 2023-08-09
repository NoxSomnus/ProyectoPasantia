using FerminToroMS.Application.Queries;
using FerminToroMS.Application.Responses;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FerminToroMS.Core.Database;
using FerminToroMS.Core.Entities;
using FerminToroMS.Application.Exceptions;

namespace FerminToroMS.Application.Handlers.Queries
{
    /// <summary>
    /// Manejador de la consulta que busca un usuario por su nombre de usuario y contraseña.
    /// </summary>
    internal class LoginQueryHandler : IRequestHandler<LoginQuery, LoginResponse>
    {
        private readonly IFerminToroDbContext _dbContext;
        private readonly ILogger<LoginQueryHandler> _logger;

        /// <summary>
        /// Constructor de la clase LoginQueryHandler.
        /// </summary>
        /// <param name="dbContext">El contexto de la base de datos que se utilizará para buscar el usuario.</param>
        /// <param name="logger">El objeto ILogger que se utilizará para registrar la actividad del manejador de consulta.</param>
        public LoginQueryHandler(IFerminToroDbContext dbContext, ILogger<LoginQueryHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        /// <summary>
        /// Manejador de la consulta que busca un usuario por su nombre de usuario y contraseña.
        /// </summary>
        /// <param name="request">La consulta LoginQuery que especifica el usuario y contraseña del usuario que se busca.</param>
        /// <param name="cancellationToken">El token de cancelación que puede detener la operación en cualquier momento.</param>
        /// <returns>Un objeto LoginResponse que contiene información detallada del usuario encontrado.</returns>
        public Task<LoginResponse> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (request == null)
                {
                    _logger.LogWarning("LoginQuery.Handle: Request null.");

                    throw new ArgumentNullException(nameof(request));

                }
                else
                {
                    return HandleAsync(request);
                }
            }
            catch
            {
                _logger.LogWarning("LoginQuery.Handle: ArgumentNullException");
                throw;
            }
            throw new NotImplementedException();
        }

        /// <summary>
        /// Método asincronico que maneja la búsqueda de un usuario por su nombre de usuario y contraseña.
        /// </summary>
        /// <param name="request">La consulta LoginQuery que especifica el usuario y contraseña del usuario que se busca.</param>
        /// <returns>Un objeto UserLoginResponse que contiene información detallada del usuario encontrado.</returns>
        private async Task<LoginResponse> HandleAsync(LoginQuery request)
        {
            try
            {
                _logger.LogInformation("LoginQueryHandler.HandleAsync");

                var userExists = await _dbContext.Empleados.AnyAsync(c => c.Username == request._request.UserName);
                if (!userExists)
                {
                    throw new UserNotFoundException(request._request.UserName);
                }

                var user = _dbContext.Empleados.FirstOrDefault(c => c.Username == request._request.UserName
                && c.Password == request._request.Password);
                if (user == null) 
                {
                    throw new Exceptions.InvalidPasswordException("La contraseña es incorrecta, intente nuevamente");
                }

                if (user.esAdmin) 
                {
                    return new LoginResponse
                    {
                        Id = user.Id,
                        Success = true,
                        IsDirector = user.esDirector
                    };
                }
                return new LoginResponse
                {
                    Id = user.Id,
                    Success = true,
                    IsDirector = false
                };

            }
            catch (UserNotFoundException ex)
            {
                _logger.LogError(ex, "Error LoginQueryHandler.HandleAsync. {Usuario no encontrado}", ex.Message);
                throw;
            }
            catch (InvalidPasswordException ex)
            {
                _logger.LogError(ex, "Error LoginQueryHandler.HandleAsync. {Contraseña invalida}", ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error LoginQueryHandler.HandleAsync. {Los datos ingresados no son validos}", ex.Message);
                throw;
            }
        }
    }
}
