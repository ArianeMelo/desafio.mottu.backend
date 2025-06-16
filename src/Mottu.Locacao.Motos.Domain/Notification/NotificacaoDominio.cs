namespace Mottu.Locacao.Motos.Domain.Notification
{
    public class NotificacaoDominio
    {
        public string Chave { get; }
        public string Mensagem { get; }

        public NotificacaoDominio(string chave, string mensagem)
        {
            Chave = chave;
            Mensagem = mensagem;
        }
    }
}
