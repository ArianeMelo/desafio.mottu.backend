using Microsoft.AspNetCore.Http;
using Mottu.Locacao.Motos.Application.Extensions;
using Mottu.Locacao.Motos.Domain.Dtos;
using Mottu.Locacao.Motos.Domain.Interface.Repository;
using Mottu.Locacao.Motos.Domain.Interface.Service;

namespace Mottu.Locacao.Motos.Application.Service
{
    public class EntregadorService : IEntregadorService
    {
        private readonly string _cnhFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "CNH");

        private readonly IEntregadorRepository _entregadorRepository;
        private readonly INotificacaoDominioHandler _notificationHandler;
        public EntregadorService(
            IEntregadorRepository entregadorRepository,
            INotificacaoDominioHandler notificationHandler)
        {
            if (!Directory.Exists(_cnhFolder))
                Directory.CreateDirectory(_cnhFolder);

            _entregadorRepository = entregadorRepository;
            _notificationHandler = notificationHandler;
        }

        public async Task Inserir(EntregadorDto entregadorDto, CancellationToken cancellationToken)
        {
            var cnpj = await _entregadorRepository.ObterPorCnpj(entregadorDto.Cnpj, cancellationToken);

            if (cnpj is not null)
            {
                _notificationHandler.AdicionarNotificacao("EntregadorService Inserir", string.Format("Já existe um registro para cnpj informado {0}", entregadorDto.Cnpj));
                return;
            }

            var numeroCnh = await _entregadorRepository.ObterEntregadorPorId(entregadorDto.Identificador, cancellationToken);
            if (numeroCnh is not null)
            {
                _notificationHandler.AdicionarNotificacao("EntregadorService Inserir", string.Format("Já existe registro de entregador {0} para a CNH informada {1}", entregadorDto.Identificador, numeroCnh.NumeroCnh));
                return;
            }
            
            await _entregadorRepository.Inserir(entregadorDto?.ParaDominio(), cancellationToken);

            var salvouMensagem = await EntregadorExtensions.SalvarImagemCnh(entregadorDto.ImagemCnh, entregadorDto.Cnpj, _cnhFolder);
        }
    }
}
