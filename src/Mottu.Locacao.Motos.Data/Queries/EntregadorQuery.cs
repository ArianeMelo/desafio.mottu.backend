namespace Mottu.Locacao.Motos.Data.Queries
{
    public static class EntregadorQuery
    {
        public const string Inserir = @"INSERT INTO ENTREGADOR (ID, IDENTIFICADOR, NOME, CNPJ, DATA_NASCIMENTO, NUMERO_CNH, TIPO_CNH)
     VALUES (@Id, @Identificador, @Nome, @Cnpj, @DataNascimento, @NumeroCnh, @TipoCnh)";

        public const string ObterCnpj = @"SELECT CNPJ FROM ENTREGADOR WHERE CNPJ = @CNPJ";

        public const string ObterPorCnh = @"SELECT TIPO_CNH AS TipoCnh, Numero_CNH AS NumeroCnh FROM ENTREGADOR WHERE Numero_CNH = @Numero_CNH";

        public const string ObterPorCnpj = @"SELECT IDENTIFICADOR, NOME, CNPJ, DATA_NASCIMENTO As DataNascimento, NUMERO_CNH As NumeroCnh, TIPO_CNH As TipoCnh FROM ENTREGADOR WHERE CNPJ = @CNPJ";

        public const string ObterTipoCnh = @"SELECT TIPO_CNH AS TipoCnh, Numero_CNH AS NumeroCnh FROM ENTREGADOR WHERE IDENTIFICADOR = @Identificador";
    }
}

