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
    public LocacaoService Setup()
    {
        LocacaoRepositoryMock = new Mock<ILocacaoRepository>();
        EntregadorRepositoryMock = new Mock<IEntregadorRepository>();
        NotificacaoHandlerMock = new Mock<INotificacaoDominioHandler>();

        return new LocacaoService(
            LocacaoRepositoryMock.Object,
            EntregadorRepositoryMock.Object,
            NotificacaoHandlerMock.Object
        );
    }

    public LocacaoRequestDto CriarLocacaoRequestDto()
    {
        return new LocacaoRequestDto
        {
            EntregadorId = "Entregador123",
            MotoId = "Moto123",
            PlanoLocacao = 1,        
            DataInicio = DateTime.Today
        };
    }

    public Entregador CriarEntregadorValido()
        => new Entregador
        {
            Identificador = "Entregador123",
            TipoCnh = 1, // Categoria Cnh A
        };

    public LocacaoEntity CriarLocacaoEntity()
        => new LocacaoEntity
        {
            EntregadorId = "Entregador123",
            MotoId = "moto123",
            DataInicio = DateTime.Today,
            PlanoLocacao = 7 //Numero de dias referente ao plano de locação
        };

    public DevolucaoDto CriarDevolucaoDto(DateTime time)
        => new DevolucaoDto
        {
            DataDevolucao = time.Date
        };
}
