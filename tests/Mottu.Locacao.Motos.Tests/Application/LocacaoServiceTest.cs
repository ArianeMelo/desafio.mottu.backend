using Moq;
using Mottu.Locacao.Motos.Domain.Entities;
using Mottu.Locacao.Motos.Tests.Collections;
using Mottu.Locacao.Motos.Tests.Fixture;
using Xunit;

namespace Mottu.Locacao.Motos.Tests.Services;

[Collection(nameof(LocacaoCollections))]
public class LocacaoServiceTests 
{
    private readonly LocacaoServiceFixture _fixture;

    public LocacaoServiceTests(LocacaoServiceFixture fixture)
    {
        _fixture = fixture;
    }


    [Fact(DisplayName = "LocacaoService ")]
    [Trait("Application", "LocacaoService")]
    public async Task Inserir_DeveInserirLocacao_QuandoEntregadorEhValido()
    {
        // Arrange
        var locacaoDto = _fixture.CriarLocacaoRequest();
        var entregador = _fixture.CriarEntregadorValido();

        _fixture.LocacaoRepositoryMock
            .Setup(x => x.ObterPorId(locacaoDto!.EntregadorId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(_fixture.CriarLocacaoDominio());

        _fixture.EntregadorRepositoryMock
            .Setup(x => x.ObterPorIdEntregador(locacaoDto.EntregadorId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(entregador);

        // Act
        await _fixture.Service.Inserir(locacaoDto, CancellationToken.None);

        // Assert
        _fixture.LocacaoRepositoryMock.Verify(x =>
            x.InserirLocacao(It.IsAny<LocacaoEntity>(), It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task Inserir_DeveNotificar_QuandoEntregadorNaoExiste()
    {
        // Arrange
        var locacaoDto = _fixture.CriarLocacaoRequest();

        _fixture.LocacaoRepositoryMock
            .Setup(x => x.ObterPorId(locacaoDto!.EntregadorId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(_fixture.CriarLocacaoDominio());

        _fixture.EntregadorRepositoryMock
            .Setup(x => x.ObterPorIdEntregador(locacaoDto.EntregadorId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Mottu.Locacao.Motos.Domain.Entities.Entregador?)null);

        // Act
        await _fixture.Service.Inserir(locacaoDto, CancellationToken.None);

        // Assert
        _fixture.NotificacaoHandlerMock.Verify(n =>
            n.AdicionarNotificacao("LocacaoService-InserirLocacao",
            It.Is<string>(msg => msg.Contains("CNH não encontrada"))), Times.Once);
    }
}
