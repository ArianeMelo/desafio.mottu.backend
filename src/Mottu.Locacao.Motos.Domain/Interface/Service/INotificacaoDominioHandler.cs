namespace Mottu.Locacao.Motos.Domain.Interface.Service
{
    public interface INotificacaoDominioHandler
    {
        public void AdicionarNotificacao(string key, string message);
        public bool ExisteNotificacao();
        public string RecuperarNotificacoes();
        public IEnumerable<string> RecuperarListaNotificacoes();
    }
}
