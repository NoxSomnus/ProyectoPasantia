using FerminToroMS.Application.Commands;
using FerminToroMS.Core.Database;
using FerminToroMS.Core.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerminToroMS.Application.Handlers.Commands
{
    /// <summary>
    /// Manejador de comando que registra nuevos estudiantes en el sistema.
    /// </summary>
    internal class AddStudentsByCSVCommandHandler : IRequestHandler<AddStudentsByCSVCommand, bool>
    {
        private readonly IFerminToroDbContext _dbContext;
        private readonly ILogger<AddStudentsByCSVCommandHandler> _logger;

        /// <summary>
        /// Constructor de la clase AddStudentsByCSVCommandHandler.
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
        /// <param name="request">El comando AddStudentsByCSVCommand que especifica el nombre del nuevo permiso.</param>
        /// <param name="cancellationToken">El token de cancelación que puede detener la operación en cualquier momento.</param>
        /// <returns>Un tipo bool que contiene información si la operacion fue exitosa o no.</returns>
        public Task<bool> Handle(AddStudentsByCSVCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request == null)
                {
                    _logger.LogWarning("AddStudentsByCSVCommandHandler.Handle: Request null.");

                    throw new ArgumentNullException(nameof(request));

                }
                else
                {
                    return HandleAsync(request);
                }
            }
            catch
            {
                _logger.LogWarning("AddStudentsByCSVCommandHandler.Handle: ArgumentNullException");
                throw;
            }
            throw new NotImplementedException();
        }
        /// <summary>
        /// Método asincronico que maneja el registro de los estudiantes.
        /// </summary>
        /// <param name="request">El comando AddStudentsByCSVCommand que especifica los datos de los estudiantes a guardar.</param>
        /// <returns>Un tipo bool que contiene información de si la operacion de registro fue exitosa o no.</returns>
        private async Task<bool> HandleAsync(AddStudentsByCSVCommand request)
        {
            var transaction = _dbContext.BeginTransaction();

            try
            {
                string yearsRange = "";
                int? age = null;
                DateTime birthDate;
                DateTime actualDate = DateTime.Now; TimeSpan difference;
                _logger.LogInformation("AddStudentsByCSVCommandHandler.HandleAsync");
                foreach (var studentrequest in request._request.Students)
                {
                    birthDate = DateTime.Now.Date;
                    if (studentrequest.Edad == null && (studentrequest.Fecha_Nac == "" || studentrequest.Fecha_Nac == string.Empty)) 
                    {
                        yearsRange = "";
                        age = null;
                    }
                    if (studentrequest.Edad != null) 
                    {
                        if (studentrequest.Edad < 18) { yearsRange = "Entre 14 - 18 años"; }
                        if (studentrequest.Edad >= 19 && studentrequest.Edad <= 26) yearsRange = "Entre 19 - 26 años";
                        if (studentrequest.Edad >= 27 && studentrequest.Edad <= 59) yearsRange = "Entre 27 - 59 años";
                        if (studentrequest.Edad >= 60) yearsRange = "Mayor a 60 años";
                        age = studentrequest.Edad;
                    }
                    if (studentrequest.Edad == null && (studentrequest.Fecha_Nac != "" || studentrequest.Fecha_Nac != string.Empty)) 
                    {
                        DateTime.TryParseExact(studentrequest.Fecha_Nac, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out birthDate);
                        difference = actualDate.Subtract(birthDate);
                        double edad = difference.TotalDays / 365.25;
                        age = (int)Math.Floor(edad);
                    }
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
                            CorreoSecundario = studentrequest.CorreoSecundario,
                            Direccion_Hab = studentrequest.Direccion,
                            Edad = age,
                            Telefono = studentrequest.Telefono,
                            Rango_Edad = yearsRange,
                            Es_Regular = false,                            
                        };
                        if (birthDate != DateTime.Now.Date) 
                        {
                            estudiante.Fecha_Nac = birthDate;
                        }
                        _dbContext.Estudiantes.Add(estudiante);
                    }
                    else 
                    {
                        student.Es_Regular = true;
                        student.Edad = age;
                        student.Rango_Edad = yearsRange;
                        student.Telefono = student.Telefono;
                        _dbContext.Estudiantes.Update(student);
                    }
                    await _dbContext.SaveEfContextChanges("APP");
                }

                transaction.Commit();

                _logger.LogInformation("AddStudentsByCSVCommandHandler.HandleAsync {Response}", true);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error AddStudentsByCSVCommandHandler.HandleAsync. {Mensaje}", ex.Message);
                transaction.Rollback();
                throw;
            }
        }
    }
}
