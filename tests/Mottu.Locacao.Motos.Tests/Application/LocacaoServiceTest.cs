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
        => _fixture = fixture;

    [Fact(DisplayName = "LocacaoService - Inserir")]
    [Trait("Application", "LocacaoService")]
    public async Task RequestValida_NaoExisteLocacaoEEntregadorEstaCadastrado_Inserir()
    {
        // Arrange
        var locacaoDto = _fixture.Setup();
        var request = _fixture.CriarLocacaoRequestDto();

        _fixture.LocacaoRepositoryMock
          .Setup(x => x.ObterLocacaoPorIdEntregador(request.EntregadorId, It.IsAny<CancellationToken>()))
          .ReturnsAsync(value: null);

        _fixture.EntregadorRepositoryMock
            .Setup(x => x.ObterEntregadorPorId(request.EntregadorId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(_fixture.CriarEntregadorValido());

        // Act
        await locacaoDto.Inserir(request, CancellationToken.None);

        // Assert
        _fixture.LocacaoRepositoryMock.Verify(x => x.ObterLocacaoPorIdEntregador(It.IsAny<string>(), It.IsAny<CancellationToken>()),
            Times.Once);

        _fixture.EntregadorRepositoryMock.Verify(x => x.ObterEntregadorPorId(It.IsAny<string>(), It.IsAny<CancellationToken>()),
            Times.Once);

        _fixture.LocacaoRepositoryMock.Verify(x => x.Inserir(It.IsAny<LocacaoEntity>(), It.IsAny<CancellationToken>()),
            Times.Once);

        _fixture.NotificacaoHandlerMock.Verify(x => x.AdicionarNotificacao(It.IsAny<string>(), It.IsAny<string>()),
            Times.Never);
    }

    [Fact(DisplayName = "LocacaoService- Não Inserir. Já Existe Locacao para IdDoEntregador")]
    [Trait("Application", "LocacaoService")]
    public async Task RequestValida_ExisteUmRegistro_NaoInserir()
    {
        // Arrange
        var locacaoDto = _fixture.Setup();
        var request = _fixture.CriarLocacaoRequestDto();

        _fixture.LocacaoRepositoryMock
          .Setup(x => x.ObterLocacaoPorIdEntregador(request.EntregadorId, It.IsAny<CancellationToken>()))
          .ReturnsAsync(_fixture.CriarLocacaoEntity());

        // Act
        await locacaoDto.Inserir(request, CancellationToken.None);

        // Assert
        _fixture.LocacaoRepositoryMock.Verify(x => x.ObterLocacaoPorIdEntregador(It.IsAny<string>(), It.IsAny<CancellationToken>()),
            Times.Once);

        _fixture.EntregadorRepositoryMock.Verify(x => x.ObterEntregadorPorId(It.IsAny<string>(), It.IsAny<CancellationToken>()),
            Times.Never);

        _fixture.LocacaoRepositoryMock.Verify(x => x.Inserir(It.IsAny<LocacaoEntity>(), It.IsAny<CancellationToken>()),
            Times.Never);

        _fixture.NotificacaoHandlerMock.Verify(x => x.AdicionarNotificacao(It.IsAny<string>(), It.IsAny<string>()),
            Times.Once);
    }

    [Fact(DisplayName = "LocacaoService - ObterLocacao Sucesso")]
    [Trait("Application", "LocacaoService")]
    public async Task RequestValida_ExisteLocacao_RetornarDados()
    {
        // Arrange
        var locacaoDto = _fixture.Setup();
        var request = _fixture.CriarLocacaoRequestDto();

        _fixture.LocacaoRepositoryMock
          .Setup(x => x.ObterLocacaoCompletaPorIdEntregador(request.EntregadorId, It.IsAny<CancellationToken>()))
          .ReturnsAsync(_fixture.CriarLocacaoEntity());

        // Act
        var locacao = await locacaoDto.ObterLocacao(request.EntregadorId, CancellationToken.None);

        // Assert
        Assert.NotNull(locacao);

        Assert.Equal(request.EntregadorId, locacao!.EntregadorId);

        _fixture.LocacaoRepositoryMock.Verify(x => x.ObterLocacaoCompletaPorIdEntregador(It.IsAny<string>(), It.IsAny<CancellationToken>()),
            Times.Once);

        _fixture.NotificacaoHandlerMock.Verify(x => x.AdicionarNotificacao(It.IsAny<string>(), It.IsAny<string>()),
            Times.Never);
    }

    [Fact(DisplayName = "LocacaoService - ObterLocacao Sucesso")]
    [Trait("Application", "LocacaoService")]
    public async Task RequestValida_NaoExisteLocacao_RetornarNulo()
    {
        // Arrange
        var locacaoDto = _fixture.Setup();
        var request = _fixture.CriarLocacaoRequestDto();

        _fixture.LocacaoRepositoryMock
          .Setup(x => x.ObterLocacaoCompletaPorIdEntregador(request.EntregadorId, It.IsAny<CancellationToken>()))
          .ReturnsAsync(value: null);
        // Act
        var locacao = await locacaoDto.ObterLocacao(request.EntregadorId, CancellationToken.None);

        // Assert
        Assert.Null(locacao);

        _fixture.LocacaoRepositoryMock.Verify(x => x.ObterLocacaoCompletaPorIdEntregador(It.IsAny<string>(), It.IsAny<CancellationToken>()),
            Times.Once);

        _fixture.NotificacaoHandlerMock.Verify(x => x.AdicionarNotificacao(It.IsAny<string>(), It.IsAny<string>()),
            Times.Once);
    }

    [Fact(DisplayName = "LocacaoService - Atualizar Locacao Sem Custos")]
    [Trait("Application", "LocacaoService")]
    public async Task RequestValida_DevolucaoOcorridoNaDataCorreta_AtualizarValorLocacao()
    {
        // Arrange
        var locacao = _fixture.CriarLocacaoEntity();
        locacao.DataInicio = locacao.DataInicio.Date.AddDays(1); //Um dia após criação 
        locacao.DataTermino = locacao.DataInicio.Date.AddDays(7); //Período de 7 dias
        locacao.DataPrevistaEncerramento = locacao.DataInicio.Date.AddDays(7); //Previsão de Entrega no prazo

        var locacaoDto = _fixture.Setup();
        var entregadorId = "Entregador123";
        var dataDevolucao = _fixture.CriarDevolucaoDto(locacao.DataPrevistaEncerramento);

        _fixture.LocacaoRepositoryMock
          .Setup(x => x.ObterLocacaoCompletaPorIdEntregador(entregadorId, It.IsAny<CancellationToken>()))
          .ReturnsAsync(locacao);
        // Act
        await locacaoDto.AtualizarLocacao(entregadorId, dataDevolucao, CancellationToken.None);

        // Assert       

        Assert.True(locacao.ValorDiaria == 210);

        _fixture.LocacaoRepositoryMock.Verify(x => x.ObterLocacaoCompletaPorIdEntregador(It.IsAny<string>(), It.IsAny<CancellationToken>()),
            Times.Once);

        _fixture.LocacaoRepositoryMock.Verify(x => x.Atualizar(It.IsAny<LocacaoEntity>(), It.IsAny<CancellationToken>()),
            Times.Once);

        _fixture.NotificacaoHandlerMock.Verify(x => x.AdicionarNotificacao(It.IsAny<string>(), It.IsAny<string>()),
            Times.Never);
    }

    [Fact(DisplayName = "LocacaoService - Alterar Locacao e Calcular Valores Devido Devolucao Atrasada")]
    [Trait("Application", "LocacaoService")]
    public async Task RequestValida_DevolucaoOcorridoComAtraso_CalcularValorDosDiasAMais()
    {
        var locacao = _fixture.CriarLocacaoEntity();
        locacao.DataInicio = locacao.DataInicio.Date.AddDays(1); 
        locacao.DataTermino = locacao.DataInicio.Date.AddDays(7); 
        locacao.DataPrevistaEncerramento = locacao.DataInicio.Date.AddDays(7); 

        var locacaoDto = _fixture.Setup();
        var entregadorId = "Entregador123";
        var dataDevolucao = _fixture.CriarDevolucaoDto(locacao.DataPrevistaEncerramento.AddDays(2)); 

        _fixture.LocacaoRepositoryMock
          .Setup(x => x.ObterLocacaoCompletaPorIdEntregador(entregadorId, It.IsAny<CancellationToken>()))
          .ReturnsAsync(locacao);

        // Act
        await locacaoDto.AtualizarLocacao(entregadorId, dataDevolucao, CancellationToken.None);

        // Assert
        // 
        Assert.True(locacao.ValorDiaria == 210);
        Assert.True(locacao.ValorAtraso == 100);

        _fixture.LocacaoRepositoryMock.Verify(x => x.ObterLocacaoCompletaPorIdEntregador(It.IsAny<string>(), It.IsAny<CancellationToken>()),
            Times.Once);

        _fixture.LocacaoRepositoryMock.Verify(x => x.Atualizar(It.IsAny<LocacaoEntity>(), It.IsAny<CancellationToken>()),
            Times.Once);

        _fixture.NotificacaoHandlerMock.Verify(x => x.AdicionarNotificacao(It.IsAny<string>(), It.IsAny<string>()),
            Times.Never);
    }

    [Fact(DisplayName = "LocacaoService - Atualizar Locacao Calcular Valor Devido Devolução Adiantada")]
    [Trait("Application", "LocacaoService")]
    public async Task RequestValida_DevolucaoOcorridoComAtecedencia_AtualizarCalculavalorEmAdiantamento()
    {

        var locacao = _fixture.CriarLocacaoEntity();
        locacao.DataInicio = locacao.DataInicio.Date.AddDays(1);
        locacao.DataTermino = locacao.DataInicio.Date.AddDays(7);
        locacao.DataPrevistaEncerramento = locacao.DataInicio.Date.AddDays(7);

        var locacaoDto = _fixture.Setup();
        var entregadorId = "Entregador123";
        var dataDevolucao = _fixture.CriarDevolucaoDto(locacao.DataPrevistaEncerramento.Date.AddDays(-2));

        _fixture.LocacaoRepositoryMock
            .Setup(x => x.ObterLocacaoCompletaPorIdEntregador(entregadorId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(locacao);

        // Act
        await locacaoDto.AtualizarLocacao(entregadorId, dataDevolucao, CancellationToken.None);

        // Assert
        Assert.True(locacao.ValorDiaria == 150);
        Assert.True(locacao.ValorAdiantamento == 12);

        _fixture.LocacaoRepositoryMock.Verify(x => x.ObterLocacaoCompletaPorIdEntregador(It.IsAny<string>(), It.IsAny<CancellationToken>()),
            Times.Once);

        _fixture.LocacaoRepositoryMock.Verify(x => x.Atualizar(It.IsAny<LocacaoEntity>(), It.IsAny<CancellationToken>()),
            Times.Once);

        _fixture.NotificacaoHandlerMock.Verify(x => x.AdicionarNotificacao(It.IsAny<string>(), It.IsAny<string>()),
            Times.Never);
    }


    [Fact(DisplayName = "LocacaoService - Locação Não Encontra Portantando Não Atualizar")]
    [Trait("Application", "LocacaoService")]

    public async Task RequestValida_NaoEncontrtadoLocacao_NaoAtualizarEGerarNotificacoes()
    {
        var locacaoDto = _fixture.Setup();
        var entregadorId = "Entregador123";
        var dataDevolucao = _fixture.CriarDevolucaoDto(DateTime.Now);    

        _fixture.LocacaoRepositoryMock
          .Setup(x => x.ObterLocacaoCompletaPorIdEntregador(entregadorId, It.IsAny<CancellationToken>()))
          .ReturnsAsync(value: null);

        // Act
        await locacaoDto.AtualizarLocacao(entregadorId, dataDevolucao, CancellationToken.None);

        // Assert       

        _fixture.LocacaoRepositoryMock.Verify(x => x.ObterLocacaoCompletaPorIdEntregador(It.IsAny<string>(), It.IsAny<CancellationToken>()),
            Times.Once);

        _fixture.LocacaoRepositoryMock.Verify(x => x.Atualizar(It.IsAny<LocacaoEntity>(), It.IsAny<CancellationToken>()),
            Times.Never);

        _fixture.NotificacaoHandlerMock.Verify(x => x.AdicionarNotificacao(It.IsAny<string>(), It.IsAny<string>()),
            Times.Once);
    }
}

