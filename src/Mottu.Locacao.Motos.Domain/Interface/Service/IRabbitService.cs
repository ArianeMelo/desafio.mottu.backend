namespace Mottu.Locacao.Motos.Domain.Interface.Service
{
    public interface IRabbitService 
    {
        Task PostarMenssagem<T>(T message);
        Task ConsumirMensagem(Action<string, int> consume);
    }
}
