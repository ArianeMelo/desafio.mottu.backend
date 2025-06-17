using Mottu.Locacao.Motos.Domain.Interface.Service;
using Newtonsoft.Json;

namespace Mottu.Locacao.Motos.Domain.Notification
{
    public class NotificacaoDominioHandler : Interface.Service.INotificacaoDominioHandler
    {
        private readonly List<NotificacaoDominio> _notifications;
        public NotificacaoDominioHandler()
            => _notifications = new List<NotificacaoDominio>();

        public void AdicionarNotificacao(string key, string message)
            => _notifications.Add(new NotificacaoDominio(key, message));

        public bool ExisteNotificacao()
            => _notifications.Any();             

        public string RecuperarNotificacoes()
            =>  JsonConvert.SerializeObject(_notifications!.ToDictionary(n => n.Chave, n => n.Mensagem));        

        public IEnumerable<string> RecuperarListaNotificacoes()
            => _notifications.Select(n => n.Mensagem);
    }
}
