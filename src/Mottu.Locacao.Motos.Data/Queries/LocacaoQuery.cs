namespace Mottu.Locacao.Motos.Data.Queries
{
    public static class LocacaoQuery
    {
        public const string Inserir = @"
            INSERT INTO LOCACAO (ID, ENTREGADOR_ID, MOTO_ID, VALOR_DIARIA, DATA_INICIO, DATA_ENCERRAMENTO, DATA_PREVISTA_ENCERRAMENTO, PLANO_LOCACAO)
            VALUES (@Id, @EntregadorID, @MotoId, @ValorDiaria, @DataInicio, @DataEncerramento, @DataPrevista, @PlanoLocacao);";

        public const string ObterLocacao = @" SELECT ENTREGADOR_ID As EntregadorId , VALOR_DIARIA AS ValorDiaria, MOTO_ID As MotoId, DATA_INICIO AS DataInicio, DATA_ENCERRAMENTO AS DataTermino,
                     DATA_PREVISTA_ENCERRAMENTO As DataPrevistaEncerramento, DATA_DEVOLUCAO As DataDevolucao, PLANO_LOCACAO  as PLanoLocacao, VALOR_DEVOLUCAO_ADIANTAMENTO  AS ValorAdiantamento,
                     VALOR_DEVOLUCAO_ATRASO AS ValorAtraso, VALOR_TOTAL_LOCACAO AS ValorTotalLocacao 
            FROM LOCACAO WHERE ENTREGADOR_ID = @ENTREGADORID;";

        public const string ObterPorId = @"SELECT Moto_Id AS MotoId, ENTREGADOR_ID As EntregadorId FROM LOCACAO WHERE ENTREGADOR_ID = @EntregadorId";

        public const string ObterPorMotoId = @"SELECT ID AS Id, MOTO_ID As MotoId FROM LOCACAO WHERE MOTO_ID = @MotoId";

        public const string Atualizar = @"UPDATE LOCACAO SET DATA_DEVOLUCAO = @DataDevolucao, VALOR_DIARIA = @ValorDiaria, 
            VALOR_DEVOLUCAO_ATRASO = @ValorAtraso, VALOR_TOTAL_LOCACAO = @ValorTotalLocacao,
            VALOR_DEVOLUCAO_ADIANTAMENTO = @ValorAdiantamento WHERE ENTREGADOR_ID = @EntregadorId";
    }
}
