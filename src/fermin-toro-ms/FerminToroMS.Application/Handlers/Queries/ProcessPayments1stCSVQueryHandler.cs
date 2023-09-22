using FerminToroMS.Application.Commands;
using FerminToroMS.Application.Exceptions;
using FerminToroMS.Application.Queries;
using FerminToroMS.Application.Requests;
using FerminToroMS.Application.Responses;
using FerminToroMS.Core.Enums;
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
    internal class ProcessPayments1stCSVQueryHandler : IRequestHandler<ProcessPayments1stCSVQuery, AddPayments1stCSVResponse>
    {
        private readonly ILogger<ProcessPayments1stCSVQueryHandler> _logger;
        private readonly IMediator _mediator;
        private readonly IRabbitProducerCSV _rabbit;
        private readonly IRabbitConsumerCSV _consumer;
        private readonly IGoogleDriveService _googleDriveService;

        /// <summary>
        /// Constructor de la clase ProcessPayments1stCSVQueryHandler.
        /// </summary>
        /// <param name="logger">El objeto ILogger que se utilizará para registrar la actividad del manejador de consulta.</param>
        /// <param name="mediator">El objeto IMediator que se utilizará para enviar el comando AddDebtToServiceCommand.</param>
        /// <param name="rabbit">El objeto IRabbitProducer que se utilizará para enviar el contenido del archivo csv a través de RabbitMQ.</param>
        /// <param name="consumer">El objeto IRabbitConsumer que se utilizará para iniciar el consumo de mensajes de RabbitMQ.</param>

        public ProcessPayments1stCSVQueryHandler(ILogger<ProcessPayments1stCSVQueryHandler> logger,
            IMediator mediator, IRabbitProducerCSV rabbit, IRabbitConsumerCSV consumer, IGoogleDriveService googleDriveService)
        {
            _logger = logger;
            _mediator = mediator;
            _rabbit = rabbit;
            _consumer = consumer;
            _googleDriveService = googleDriveService;
        }

        /// <summary>
        /// Maneja la consulta ProcessInscriptionsCSVFileQuery y devuelve un boolean para indicar el resultado de la operacion
        /// </summary>
        /// <param name="request">La consulta que se va a procesar.</param>
        /// <param name="cancellationToken">El token de cancelación que se puede utilizar para cancelar la operación de manera asincrónica.</param>
        /// <returns>Un objeto AddInscriptionsResponse para indicar el resultado de la operacion.</returns>

        public Task<AddPayments1stCSVResponse> Handle(ProcessPayments1stCSVQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (request == null)
                {
                    _logger.LogWarning("ProcessPayments1stCSVQueryHandler.Handle: Request null.");

                    throw new ArgumentNullException(nameof(request));

                }
                else
                {
                    return HandleAsync(request);
                }
            }
            catch
            {
                _logger.LogWarning("ProcessPayments1stCSVQueryHandler.Handle: ArgumentNullException");
                throw;
            }
            throw new NotImplementedException();
        }
        /// <summary>
        /// Procesa la consulta ProcessDebtFileQuery y envía el contenido del archivo de deudas a través de RabbitMQ para su procesamiento posterior.
        /// </summary>
        /// <param name="request">La consulta que se va a procesar.</param>
        /// <returns>Un objeto AddInscriptionsResponse.</returns>

        public async Task<AddPayments1stCSVResponse> HandleAsync(ProcessPayments1stCSVQuery request)
        {

            try
            {
                _logger.LogInformation("ProcessPayments1stCSVQueryHandler.HandleAsync");
                // Descarga el archivo desde el ID de archivo especificado
                byte[] fileBytes = _googleDriveService.DownloadFile(request._request.DriveFileId);
                List<AddPayments1stCSVRequest> payments = new List<AddPayments1stCSVRequest>(); //CAMBIAR ESTO
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
                            try
                            {
                                // Crear un objeto AddCourseRequest y asignar los valores de las columnas
                                AddPayments1stCSVRequest payment = new AddPayments1stCSVRequest
                                {
                                    NroInscripcion = int.Parse(columns[0]),
                                    FechaPago = DateTime.ParseExact(columns[1],"dd/MM/yyyy", null),
                                    Cuota = bool.Parse(columns[2]),
                                    Divisa = bool.Parse(columns[3]),
                                    MetodoPago = columns[4],
                                    URLComprobante = columns[5],
                                    Juridico = bool.Parse(columns[6]),
                                    UrlRif = columns[7]
                                };
                                payments.Add(payment);
                            }
                            catch (BadCSVRequest)
                            {
                                throw new BadCSVRequest("El formato del csv no es correcto. " +
                               "Asegurese de poner los nombres de los cursos como estan registrados en el sistema");
                            }
                        }
                    }

                    _rabbit.SendProductMessage(payments);
                    var response = await _mediator.Send(new AddPayments1stCSVCommand(payments));
                    _consumer.StartConsuming();
                    return response;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error ProcessPayments1stCSVQueryHandler.HandleAsync. {Los datos ingresados no son validos}", ex.Message);
                throw;
            }
        }
    }
}
