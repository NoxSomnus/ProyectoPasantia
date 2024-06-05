using FerminToroMS.Application.Commands;
using FerminToroMS.Core.Database;
using FerminToroMS.Core.Entities;
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
    internal class SetJuridicoStudentsCommandHandler : IRequestHandler<SetJuridicoStudentsCommand, bool>
    {
        private readonly IFerminToroDbContext _dbContext;
        private readonly ILogger<SetJuridicoStudentsCommandHandler> _logger;

        /// <summary>
        /// Constructor de la clase SetJuridicoStudentsCommandHandler.
        /// </summary>
        /// <param name="dbContext">El contexto de la base de datos que se utilizará para buscar el usuario.</param>
        /// <param name="logger">El objeto ILogger que se utilizará para registrar la actividad del manejador del comando.</param>
        public SetJuridicoStudentsCommandHandler(IFerminToroDbContext dbContext, ILogger<SetJuridicoStudentsCommandHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        /// <summary>
        /// Manejador del comando que setea los valores de estado del pago.
        /// </summary>
        /// <param name="request">El comando SetApprovedPaymentsCommand que especifica el set de los valores del codigo de cronograma.</param>
        /// <param name="cancellationToken">El token de cancelación que puede detener la operación en cualquier momento.</param>
        /// <returns>Un bool que contiene información si la operacion fue exitosa o no.</returns>
        public Task<bool> Handle(SetJuridicoStudentsCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request == null)
                {
                    _logger.LogWarning("SetJuridicoStudentsCommandHandler.Handle: Request null.");

                    throw new ArgumentNullException(nameof(request));

                }
                else
                {
                    return HandleAsync(request);
                }
            }
            catch
            {
                _logger.LogWarning("SetJuridicoStudentsCommandHandler.Handle: ArgumentNullException");
                throw;
            }
            throw new NotImplementedException();
        }

        /// <summary>
        /// Método asincronico que maneja el registro de un nuevo cronograma.
        /// </summary>
        /// <param name="request">El comando CreateScheduleCommand que especifica los datos del nuevo cronograma.</param>
        /// <returns>Un bool que contiene información de si la operacion de registro fue exitosa o no.</returns>
        private async Task<bool> HandleAsync(SetJuridicoStudentsCommand request)
        {
            var transaction = _dbContext.BeginTransaction();
            try
            {
                var students = _dbContext.Estudiantes.Include(e => e.Inscripciones).ThenInclude(i=>i.Pagos).ToList();
                foreach (var student in students)
                {
                    if(student.Inscripciones != null && student.Inscripciones.Count>0)
                        foreach (var inscription in student.Inscripciones) 
                        {
                            if(inscription.Pagos != null && inscription.Pagos.Count>0)
                                foreach (var payment in inscription.Pagos) 
                                {
                                    if (payment.EsJuridico) 
                                    {
                                        student.EsJuridico = true;
                                    }  
                                }
                        }
                    _dbContext.Estudiantes.Update(student);
                }
                await _dbContext.SaveEfContextChanges("APP");
                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error SetJuridicoStudentsCommandHandler.HandleAsync. {Mensaje}", ex.Message);
                transaction.Rollback();
                throw;
            }
        }        
    }
}
