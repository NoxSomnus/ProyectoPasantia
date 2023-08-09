using FerminToroMS.Core.Interfaces;
using Microsoft.Azure.Amqp;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerminToroMS.Application.Rabbit
{
    /// <summary>
    /// Clase que implementa la interfaz IRabbitProducer y se encarga de subir mensajes de la cola "csv" de RabbitMQ.
    /// </summary>
    public class RabbitProducerCSV : IRabbitProducerCSV
    {
        public void SendProductMessage<T>(T message)
        {
            //Here we specify the Rabbit MQ Server. we use rabbitmq docker image and use it
            var factory = new ConnectionFactory
            {
                HostName = "localhost"
            };

            //Create the RabbitMQ connection using connection factory details as i mentioned above
            var connection = factory.CreateConnection();

            //Here we create channel with session and model
            using var channel = connection.CreateModel();

            //declare the queue after mentioning name and a few property related to that
            channel.QueueDeclare("csv", exclusive: false);

            //Serialize the message
            var json = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(json);

            //put the data on to the product queue
            channel.BasicPublish(exchange: "", routingKey: "csv", body: body);
        }


    }
}
