using Mottu.Locacao.Motos.Domain.Interface.Application;
using Mottu.Locacao.Motos.Domain.Interface.Repository;
using Mottu.Locacao.Motos.Domain.Interface.Service;

namespace Mottu.Locacao.Motos.Api.Consumer
{
    public class MotoCadastradaConsumer : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        public MotoCadastradaConsumer(IServiceProvider serviceProvider)
            => _serviceProvider = serviceProvider;          
        protected override async Task ExecuteAsync(CancellationToken cancellation)
        {
            while (!cancellation.IsCancellationRequested)
            {
                using var scope = _serviceProvider.CreateScope();

                // Aqui você resolve serviço Scoped (Declaracação IRabbitService) 
                var rabbitService = scope.ServiceProvider.GetRequiredService<IRabbitService>();
                var repository = scope.ServiceProvider.GetRequiredService<IMotoService>();

                await rabbitService.ConsumirMensagem((string placa, int ano) 
                    => repository.InserirAnoEspecifico(placa, ano, cancellation));
                
                await Task.Delay(TimeSpan.FromSeconds(10));
            }
        }
    }
}
