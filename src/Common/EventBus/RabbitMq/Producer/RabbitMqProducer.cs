using Newtonsoft.Json;
using RabbitMq.Events;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMq.Producer
{
    public class RabbitMqProducer
    {
        private readonly IRabbitMqConnection _connection;

        public RabbitMqProducer(IRabbitMqConnection connection)
        {
            _connection = connection;
        }

        public void PublishBasketCheckout(string queueName, BasketCheckoutEvent publishModel)
        {
            using (var channel = _connection.CreateModel())
            {
                // Handle Message
                // durable : false means the queue has been stored in memory not a physical storage
                // exclusive: false means we do not want to use this queue with other connections.
                channel.QueueDeclare(queue: queueName, durable: false,
                    exclusive: false, autoDelete: false, arguments: null);
                var message = JsonConvert.SerializeObject(publishModel);
                var body = Encoding.UTF8.GetBytes(message);

                // Set Properties
                IBasicProperties properties = channel.CreateBasicProperties();
                properties.Persistent = true;
                properties.DeliveryMode = 2;

                // Send Event To Queue
                channel.ConfirmSelect();
                channel.BasicPublish(exchange: "", routingKey: queueName,
                    mandatory: true, basicProperties: properties, body: body);
                channel.WaitForConfirmsOrDie();

                // Send Acknowledgement
                channel.BasicAcks += (sender, eventArgs) =>
                {
                    Console.WriteLine("Send RabitMq");
                };
                channel.ConfirmSelect();
            }
        }
    }
}
