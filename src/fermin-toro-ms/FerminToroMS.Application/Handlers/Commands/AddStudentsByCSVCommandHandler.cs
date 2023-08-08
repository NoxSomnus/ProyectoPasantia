﻿using FerminToroMS.Application.Commands;
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
    internal class AddStudentsByCSVCommandHandler : IRequestHandler<AddStudentsByCSVCommand, bool>
    {
        private readonly IFerminToroDbContext _dbContext;
        private readonly ILogger<AddStudentsByCSVCommandHandler> _logger;

        /// <summary>
        /// Constructor de la clase AddPermissionCommandHandler.
        /// </summary>
        /// <param name="dbContext">El contexto de la base de datos que se utilizará para buscar el usuario.</param>
        /// <param name="logger">El objeto ILogger que se utilizará para registrar la actividad del manejador del comando.</param>
        public AddStudentsByCSVCommandHandler(IFerminToroDbContext dbContext, ILogger<AddStudentsByCSVCommandHandler> logger)
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
        public Task<bool> Handle(AddStudentsByCSVCommand request, CancellationToken cancellationToken)
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
        private async Task<bool> HandleAsync(AddStudentsByCSVCommand request)
        {
            var transaction = _dbContext.BeginTransaction();

            try
            {
                _logger.LogInformation("AddPermissionCommandHandler.HandleAsync");
                foreach (var studentrequest in request._request.Students)
                {
                    var studentId = Guid.NewGuid();
                    var student = _dbContext.Estudiantes.FirstOrDefault(c => c.Cedula == studentrequest.Cedula);
                    if (student == null)
                    {
                        var estudiante = new EstudianteEntity
                        {
                            Id = studentId,
                            Nombre = studentrequest.Nombre,
                            Cedula = studentrequest.Cedula,
                            Apellido = studentrequest.Apellido,
                            Correo = studentrequest.Correo,
                            Direccion_Hab = studentrequest.Direccion,
                            Edad = studentrequest.Edad,
                            Telefono = studentrequest.Telefono,
                            Rango_Edad = studentrequest.Rango_Edad,
                            CreatedAt = studentrequest.Fecha_Creacion,
                            

                        };
                        _dbContext.Estudiantes.Add(estudiante);
                    }
                    else 
                    {
                        student.Es_Regular = studentrequest.Es_Regular;
                        student.Edad = studentrequest.Edad;
                        student.Rango_Edad = student.Rango_Edad;
                        _dbContext.Estudiantes.Update(student);
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