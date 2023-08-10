using MediatR;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FerminToroMS.Core.Interfaces;

namespace FerminToroMS.Application.Rabbit
{
    public class RabbitConsumerCSV : IRabbitConsumerCSV
    {
        /// <summary>
        /// Clase que implementa la interfaz IRabbitConsumer y se encarga de consumir mensajes de la cola "csv" de RabbitMQ.
        /// </summary>
        private IConnection _connection;
        private IModel _channel;
        /// <summary>
        /// Constructor de la clase RabbitConsumerCSV
        /// </summary>
        

        public RabbitConsumerCSV()
        {
        }
        /// <summary>
        /// Inicia el consumo de mensajes de la cola "csv" de RabbitMQ.
        /// </summary>
        public void StartConsuming()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            try
            {
                _channel.QueueDeclare(queue: "csv",
                                      durable: false,
                                      exclusive: false,
                                      autoDelete: true,
                                      arguments: null);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al declarar la cola: {0}", ex.Message);
                return;
            }

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine("Received message: {0}", message);
            };

            _channel.BasicConsume(queue: "csv",
                                  autoAck: true,
                                  consumer: consumer);
        }

        /// <summary>
        /// Detiene el consumo de mensajes de la cola "csv" de RabbitMQ.
        /// </summary>
        public void StopConsuming()
        {
            _channel.Close();
            _connection.Close();
        }
    }

}

