using FerminToroMS.Application.Commands;
using FerminToroMS.Application.Exceptions;
using FerminToroMS.Application.Queries;
using FerminToroMS.Application.Requests;
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
using static MassTransit.Util.ChartTable;

namespace FerminToroMS.Application.Handlers.Queries
{
    internal class ProcessPricesCSVFileQueryHandler : IRequestHandler<ProcessPricesCSVFileQuery, bool>
    {
        private readonly ILogger<ProcessPricesCSVFileQueryHandler> _logger;
        private readonly IMediator _mediator;
        private readonly IRabbitProducerCSV _rabbit;
        private readonly IRabbitConsumerCSV _consumer;
        private readonly IGoogleDriveService _googleDriveService;

        /// <summary>
        /// Constructor de la clase ProcessCoursesCSVFileQueryHandler.
        /// </summary>
        /// <param name="logger">El objeto ILogger que se utilizará para registrar la actividad del manejador de consulta.</param>
        /// <param name="mediator">El objeto IMediator que se utilizará para enviar el comando AddDebtToServiceCommand.</param>
        /// <param name="rabbit">El objeto IRabbitProducer que se utilizará para enviar el contenido del archivo csv a través de RabbitMQ.</param>
        /// <param name="consumer">El objeto IRabbitConsumer que se utilizará para iniciar el consumo de mensajes de RabbitMQ.</param>

        public ProcessPricesCSVFileQueryHandler(ILogger<ProcessPricesCSVFileQueryHandler> logger,
            IMediator mediator, IRabbitProducerCSV rabbit, IRabbitConsumerCSV consumer, IGoogleDriveService googleDriveService)
        {
            _logger = logger;
            _mediator = mediator;
            _rabbit = rabbit;
            _consumer = consumer;
            _googleDriveService = googleDriveService;
        }

        /// <summary>
        /// Maneja la consulta ProcessCoursesCSVFileQuery y devuelve un boolean para indicar el resultado de la operacion
        /// </summary>
        /// <param name="request">La consulta que se va a procesar.</param>
        /// <param name="cancellationToken">El token de cancelación que se puede utilizar para cancelar la operación de manera asincrónica.</param>
        /// <returns>Un boolean para indicar el resultado de la operacion.</returns>

        public Task<bool> Handle(ProcessPricesCSVFileQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (request == null)
                {
                    _logger.LogWarning("ProcessPricesCSVFileQueryHandler.Handle: Request null.");

                    throw new ArgumentNullException(nameof(request));

                }
                else
                {
                    return HandleAsync(request);
                }
            }
            catch
            {
                _logger.LogWarning("ProcessPricesCSVFileQueryHandler.Handle: ArgumentNullException");
                throw;
            }
            throw new NotImplementedException();
        }
        /// <summary>
        /// Procesa la consulta ProcessDebtFileQuery y envía el contenido del archivo de deudas a través de RabbitMQ para su procesamiento posterior.
        /// </summary>
        /// <param name="request">La consulta que se va a procesar.</param>
        /// <returns>Un identificador único de tipo Guid.</returns>

        public async Task<bool> HandleAsync(ProcessPricesCSVFileQuery request)
        {

            try
            {
                _logger.LogInformation("ProcessPricesCSVFileQueryHandler.HandleAsync");
                // Descarga el archivo desde el ID de archivo especificado
                byte[] fileBytes = _googleDriveService.DownloadFile(request._request.DriveFileId);
                List<PricesRequest> prices = new List<PricesRequest>(); 
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
                            string[] columns = line.Split(',');
                            try
                            {
                                PricesRequest priceRequest = new PricesRequest
                                {
                                    CourseName = columns[0],
                                    ModulName = columns[1],
                                    Modalidad = (ModalidadEnum)Enum.Parse(typeof(ModalidadEnum), columns[2]),
                                    Turno = (TurnosEnum)Enum.Parse(typeof(TurnosEnum), columns[3]),
                                    Regularidad = (RegularidadEnum)Enum.Parse(typeof(RegularidadEnum), columns[4]),
                                    Precio = double.Parse(columns[5]),
                                    Cuotas = bool.Parse(columns[6])
                                };
                                prices.Add(priceRequest);
                            }
                            catch (BadCSVRequest)
                            {
                                throw new BadCSVRequest("El formato del csv no es correcto. " +
                                       "Asegurese de poner los nombres de los cursos, modulos, turnos, modalidad y regularidad como estan registrados en el sistema");
                            }
                        }
                    }
                    var filerequest = new AddPricesToCourseByCSVRequest //CAMBIAR ESTO
                    {
                        Prices = prices
                    };

                    _rabbit.SendProductMessage(filerequest);
                    await _mediator.Send(new AddPricesToCourseByCSVCommand(filerequest));
                    _consumer.StartConsuming();
                    return true;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error ProcessPricesCSVFileQueryHandler.HandleAsync. {Los datos ingresados no son validos}", ex.Message);
                throw;
            }
        }

    }
}
