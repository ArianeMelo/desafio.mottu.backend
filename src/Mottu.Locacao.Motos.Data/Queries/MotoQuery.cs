namespace Mottu.Locacao.Motos.Data.Queries
{
    public static class MotoQuery
    {

        public const string Inserir = @"INSERT INTO MOTO_CADASTRADA (ID, IDENTIFICADOR, ANO, MODELO, PLACA) VALUES (@ID, @IDENTIFICADOR, @ANO, @MODELO, @PLACA)";

        public const string ObterPorPlaca = @"SELECT IDENTIFICADOR, ANO, MODELO, PLACA FROM MOTO_CADASTRADA WHERE PLACA = @PLACA";

        public const string ObterPlaca = @"SELECT PLACA FROM MOTO_CADASTRADA WHERE PLACA = @PLACA";

        public const string ObterPorIdentificador = @"SELECT IDENTIFICADOR, ANO, MODELO, PLACA FROM MOTO_CADASTRADA WHERE IDENTIFICADOR = @IDENTIFICADOR";

        public const string AlterarPlaca = @"UPDATE MOTO_CADASTRADA SET PLACA = @PLACA WHERE IDENTIFICADOR = @IDENTIFICADOR";


        public const string InserirAno2024 =
            @"INSERT INTO MOTO_CADASTRADA_ANO_2024 (ID, ANO, PLACA) VALUES (@ID, @ANO, @PLACA)";

        public const string ObterPlaca2024 =
           @"SELECT PLACA FROM MOTO_CADASTRADA_ANO_2024 WHERE PLACA = @PLACA";

    }
}
