using FerminToroMS.Application.Commands;
using FerminToroMS.Application.Exceptions;
using FerminToroMS.Application.Queries;
using FerminToroMS.Application.Requests;
using FerminToroMS.Core.Interfaces;
using FerminToroMS.Infrastructure.Services;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerminToroMS.Application.Handlers.Queries
{
    internal class ProcessStudentsCSVFileQueryHandler : IRequestHandler<ProcessStudentsCSVFileQuery, bool>
    {
        private readonly ILogger<ProcessStudentsCSVFileQueryHandler> _logger;
        private readonly IMediator _mediator;
        private readonly IRabbitProducerCSV _rabbit;
        private readonly IRabbitConsumerCSV _consumer;
        private readonly IGoogleDriveService _googleDriveService;

        /// <summary>
        /// Constructor de la clase ProcessStudentsCSVFileQueryHandler.
        /// </summary>
        /// <param name="logger">El objeto ILogger que se utilizará para registrar la actividad del manejador de consulta.</param>
        /// <param name="mediator">El objeto IMediator que se utilizará para enviar el comando AddDebtToServiceCommand.</param>
        /// <param name="rabbit">El objeto IRabbitProducer que se utilizará para enviar el contenido del archivo csv a través de RabbitMQ.</param>
        /// <param name="consumer">El objeto IRabbitConsumer que se utilizará para iniciar el consumo de mensajes de RabbitMQ.</param>

        public ProcessStudentsCSVFileQueryHandler(ILogger<ProcessStudentsCSVFileQueryHandler> logger,
            IMediator mediator, IRabbitProducerCSV rabbit, IRabbitConsumerCSV consumer, IGoogleDriveService googleDriveService)
        {
            _logger = logger;
            _mediator = mediator;
            _rabbit = rabbit;
            _consumer = consumer;
            _googleDriveService = googleDriveService;
        }

        /// <summary>
        /// Maneja la consulta ProcessStudentsCSVFileQuery y devuelve un boolean para indicar el resultado de la operacion
        /// </summary>
        /// <param name="request">La consulta que se va a procesar.</param>
        /// <param name="cancellationToken">El token de cancelación que se puede utilizar para cancelar la operación de manera asincrónica.</param>
        /// <returns>Un boolean para indicar el resultado de la operacion.</returns>

        public Task<bool> Handle(ProcessStudentsCSVFileQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (request == null)
                {
                    _logger.LogWarning("ProcessStudentsCSVFileQueryHandler.Handle: Request null.");

                    throw new ArgumentNullException(nameof(request));

                }
                else
                {
                    return HandleAsync(request);
                }
            }
            catch
            {
                _logger.LogWarning("ProcessStudentsCSVFileQueryHandler.Handle: ArgumentNullException");
                throw;
            }
            throw new NotImplementedException();
        }

        /// <summary>
        /// Procesa la consulta ProcessStudentsCSVFileQuery y envía el contenido del archivo de deudas a través de RabbitMQ para su procesamiento posterior.
        /// </summary>
        /// <param name="request">La consulta que se va a procesar.</param>
        /// <returns>Un boolean para indicar el resultado de la operacion.</returns>
        /// 
        public async Task<bool> HandleAsync(ProcessStudentsCSVFileQuery request)
        {

            try
            {
                _logger.LogInformation("ProcessCoursesCSVFileQueryHandler.HandleAsync");
                // Descarga el archivo desde el ID de archivo especificado
                byte[] fileBytes = _googleDriveService.DownloadFile(request._request.DriveFileId);
                List<AddStudentRequest> students = new List<AddStudentRequest>();
                bool isFirstLine = true;
                // Lee el contenido del archivo en un string
                using (var fileStream = new MemoryStream(fileBytes))
                {
                    // Leer el contenido del archivo en un string
                    using (StreamReader reader = new StreamReader(fileStream))
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            if (isFirstLine)
                            {
                                isFirstLine = false;
                                continue;  // Saltar a la siguiente iteración sin agregar la línea a la lista
                            }
                            // Dividir la línea en sus columnas utilizando el carácter de coma (',') como separador
                            string[] columns = line.Split(',');
                            Console.WriteLine(columns);
                            try
                            {
                                // Crear un objeto AddCourseRequest y asignar los valores de las columnas
                                AddStudentRequest student = new AddStudentRequest
                                {
                                    Cedula = columns[0],
                                    Nombre = columns[1],
                                    Apellido = columns[2],
                                    Telefono = columns[3],
                                    Correo = columns[4],
                                    CorreoSecundario = columns[5],
                                    Direccion = columns[6],
                                    Edad = null,
                                    Fecha_Nac = columns[8],
                                };
                                if (columns[7] != string.Empty) 
                                {
                                    try
                                    {
                                        student.Edad = int.Parse(columns[7]);
                                    }
                                    catch (Exception ex)
                                    { 
                                        Console.WriteLine(columns); }
                                }
                                students.Add(student);
                            }
                            catch (BadCSVRequest)
                            {
                                throw new BadCSVRequest("El formato del csv no es correcto. " +
                               "Asegurese de poner los nombres de los cursos como estan registrados en el sistema");
                            }
                        }
                    }

                    var filerequest = new AddStudentsByCSVRequest
                    {
                        Students = students
                    };

                    _rabbit.SendProductMessage(filerequest);
                    await _mediator.Send(new AddStudentsByCSVCommand(filerequest));
                    _consumer.StartConsuming();
                    return true;
                }
            }
            catch (BadCSVRequest ex)
            {
                _logger.LogError(ex, "Error ProcessCoursesCSVFileQueryHandler.HandleAsync. {Los datos ingresados no son validos}", ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error ProcessCoursesCSVFileQueryHandler.HandleAsync. {Los datos ingresados no son validos}", ex.Message);
                throw;
            }
        }

    }
}
