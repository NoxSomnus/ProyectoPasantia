using FerminToroMS.Application.Commands;
using FerminToroMS.Application.Exceptions;
using FerminToroMS.Application.Queries;
using FerminToroMS.Application.Requests;
using FerminToroMS.Application.Responses;
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
    internal class ProcessPayments2ndCSVQueryHandler : IRequestHandler<ProcessPayments2ndCSVQuery, AddPayments2ndCSVResponse>
    {
        private readonly ILogger<ProcessPayments2ndCSVQueryHandler> _logger;
        private readonly IMediator _mediator;
        private readonly IRabbitProducerCSV _rabbit;
        private readonly IRabbitConsumerCSV _consumer;
        private readonly IGoogleDriveService _googleDriveService;

        /// <summary>
        /// Constructor de la clase ProcessPayments2ndCSVQueryHandler.
        /// </summary>
        /// <param name="logger">El objeto ILogger que se utilizará para registrar la actividad del manejador de consulta.</param>
        /// <param name="mediator">El objeto IMediator que se utilizará para enviar el comando AddDebtToServiceCommand.</param>
        /// <param name="rabbit">El objeto IRabbitProducer que se utilizará para enviar el contenido del archivo csv a través de RabbitMQ.</param>
        /// <param name="consumer">El objeto IRabbitConsumer que se utilizará para iniciar el consumo de mensajes de RabbitMQ.</param>

        public ProcessPayments2ndCSVQueryHandler(ILogger<ProcessPayments2ndCSVQueryHandler> logger,
            IMediator mediator, IRabbitProducerCSV rabbit, IRabbitConsumerCSV consumer, IGoogleDriveService googleDriveService)
        {
            _logger = logger;
            _mediator = mediator;
            _rabbit = rabbit;
            _consumer = consumer;
            _googleDriveService = googleDriveService;
        }

        /// <summary>
        /// Maneja la consulta ProcessPayments2ndCSVQuery y devuelve un AddPayments2ndCSVResponse para indicar el resultado de la operacion
        /// </summary>
        /// <param name="request">La consulta que se va a procesar.</param>
        /// <param name="cancellationToken">El token de cancelación que se puede utilizar para cancelar la operación de manera asincrónica.</param>
        /// <returns>Un objeto AddPayments2ndCSVResponse para indicar el resultado de la operacion.</returns>

        public Task<AddPayments2ndCSVResponse> Handle(ProcessPayments2ndCSVQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (request == null)
                {
                    _logger.LogWarning("ProcessPayments2ndCSVQueryHandler.Handle: Request null.");

                    throw new ArgumentNullException(nameof(request));

                }
                else
                {
                    return HandleAsync(request);
                }
            }
            catch
            {
                _logger.LogWarning("ProcessPayments2ndCSVQueryHandler.Handle: ArgumentNullException");
                throw;
            }
            throw new NotImplementedException();
        }
        /// <summary>
        /// Procesa la consulta ProcessPayments2ndCSVQuery y envía el contenido del archivo de pagos a través de RabbitMQ para su procesamiento posterior.
        /// </summary>
        /// <param name="request">La consulta que se va a procesar.</param>
        /// <returns>Un objeto AddPayments2ndCSVResponse.</returns>

        public async Task<AddPayments2ndCSVResponse> HandleAsync(ProcessPayments2ndCSVQuery request)
        {
            int linea = 1;
            string[] columns = { };
            try
            {
                _logger.LogInformation("ProcessPayments2ndCSVQueryHandler.HandleAsync");
                // Descarga el archivo desde el ID de archivo especificado
                byte[] fileBytes = _googleDriveService.DownloadFile(request._request.DriveFileId);
                List<AddPayments2ndCSVRequest> payments = new List<AddPayments2ndCSVRequest>();
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
                            columns = line.Split(',');
                            Console.WriteLine(columns);
                            linea++;
                            try
                            {
                                // Crear un objeto AddCourseRequest y asignar los valores de las columnas
                                AddPayments2ndCSVRequest payment = new AddPayments2ndCSVRequest
                                {
                                    Cedula = columns[0],
                                    NroInscripcion = int.Parse(columns[1]),
                                    Cuota = bool.Parse(columns[2]),
                                    ReciboFacturaMonto = columns[3],
                                    Comentario = columns[4]
                                };
                                payments.Add(payment);
                            }
                            catch (BadCSVRequest)
                            {
                                throw new BadCSVRequest("El formato del csv no es correcto. " +
                               "Asegurese de cumplir con el formato del csv establecido");
                            }
                        }
                    }

                    _rabbit.SendProductMessage(payments);
                    var response = await _mediator.Send(new AddPayments2ndCSVCommand(payments));
                    _consumer.StartConsuming();
                    return response;
                }
            }
            catch (Exception ex)
            {              
                _logger.LogError(ex, "Error ProcessPayments2ndCSVQueryHandler.HandleAsync. {Los datos ingresados no son validos}", ex.Message);
                Console.WriteLine(linea);
                Console.WriteLine(columns);
                throw;
            }
        }
    }
}
