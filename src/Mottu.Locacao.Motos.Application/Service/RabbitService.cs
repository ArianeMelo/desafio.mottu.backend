using Microsoft.Extensions.Options;
using Mottu.Locacao.Motos.Application.Configuration;
using Mottu.Locacao.Motos.Application.Evento;
using Mottu.Locacao.Motos.Domain.Interface.Service;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Mottu.Locacao.Motos.Application.Service
{
    public class RabbitService : IRabbitService
    {
        private readonly RabbitMQSettings _rabbitmqSettings;
        public RabbitService(IOptions<RabbitMQSettings> rabbitmqSettings)
            => _rabbitmqSettings = rabbitmqSettings.Value;        
        public async Task PostarMenssagem<T>(T message)
        {
            var channel = await CriarConexao();            

            var messageString = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(messageString);

            await channel.QueueDeclareAsync(queue: _rabbitmqSettings.QueueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
            await channel.BasicPublishAsync(exchange: "", routingKey: _rabbitmqSettings.QueueName, body: body);

        }
        public async Task ConsumirMensagem(Action<string, int> consume)
        {           
            var mensagem = string.Empty;
            var channel = await CriarConexao();

            await channel.QueueDeclareAsync(queue: _rabbitmqSettings.QueueName, durable: true, exclusive: false, autoDelete: false, arguments: null);

            var consumer = new AsyncEventingBasicConsumer(channel);

            consumer.ReceivedAsync += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                mensagem = Encoding.UTF8.GetString(body);

                if (!string.IsNullOrWhiteSpace(mensagem))
                {
                    var evento = JsonConvert.DeserializeObject<MotoCadastradaEvent>(mensagem);
                    Console.WriteLine(mensagem);
                    consume(evento.Placa, evento.Ano);
                }
            };

            await channel.BasicConsumeAsync(queue: _rabbitmqSettings.QueueName, autoAck: true, consumer: consumer);
        }
        private async Task<IChannel> CriarConexao()
        {
            var factory = new ConnectionFactory()
            {
                UserName = _rabbitmqSettings.Username,
                Password = _rabbitmqSettings.Password,
                HostName = _rabbitmqSettings.Host,
            };

            var conn = await factory.CreateConnectionAsync();

            return await conn.CreateChannelAsync();
        }
    }
}

