using Moq;
using Mottu.Locacao.Motos.Application.Service;
using Mottu.Locacao.Motos.Domain.Dtos;
using Mottu.Locacao.Motos.Domain.Entities;
using Mottu.Locacao.Motos.Domain.Interface.Repository;
using Mottu.Locacao.Motos.Domain.Interface.Service;

namespace Mottu.Locacao.Motos.Tests.Fixture;

public class LocacaoServiceFixture
{
    public Mock<ILocacaoRepository> LocacaoRepositoryMock { get; private set; }
    public Mock<IEntregadorRepository> EntregadorRepositoryMock { get; private set; }
    public Mock<INotificacaoDominioHandler> NotificacaoHandlerMock { get; private set; }
    public LocacaoService Service { get; private set; }

    public LocacaoServiceFixture()
    {
        LocacaoRepositoryMock = new Mock<ILocacaoRepository>();
        EntregadorRepositoryMock = new Mock<IEntregadorRepository>();
        NotificacaoHandlerMock = new Mock<INotificacaoDominioHandler>();

        Service = new LocacaoService(
            LocacaoRepositoryMock.Object,
            EntregadorRepositoryMock.Object,
            NotificacaoHandlerMock.Object
        );
    }

    public LocacaoRequestDto CriarLocacaoRequest()
    {
        return new LocacaoRequestDto
        {
            EntregadorId = Guid.NewGuid().ToString(),
            MotoId = Guid.NewGuid().ToString(),
            PlanoLocacao = 1
        };
    }

    public Entregador CriarEntregadorValido()
    {
        return new Entregador
        {
            Identificador = "EntregadorId",
            TipoCnh = 1, // Categoria Cnh A
        };
    }

    public LocacaoEntity CriarLocacaoDominio()
    {
        return new LocacaoEntity
        {
            EntregadorId = "Entregador123",
            MotoId = "moto123",
            DataInicio = DateTime.Today,
            PlanoLocacao = 1
        };
    }
}
