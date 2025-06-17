namespace Mottu.Locacao.Motos.Data.Queries
{
    public static class MotoQuery
    {

        public const string Inserir = @"INSERT INTO MOTOS (ID, IDENTIFICADOR, ANO, MODELO, PLACA) VALUES (@ID, @IDENTIFICADOR, @ANO, @MODELO, @PLACA)";

        public const string ObterPorPlaca = @"SELECT IDENTIFICADOR, ANO, MODELO, PLACA FROM MOTOS WHERE PLACA = @PLACA";

        public const string ObterPlaca = @"SELECT PLACA FROM MOTOS WHERE PLACA = @PLACA";

        public const string ObterPorIdentificador = @"SELECT IDENTIFICADOR, ANO, MODELO, PLACA FROM MOTOS WHERE IDENTIFICADOR = @IDENTIFICADOR";

        public const string AlterarPlaca = @"UPDATE MOTOS SET PLACA = @PLACA WHERE IDENTIFICADOR = @IDENTIFICADOR";

        public const string Remover = @"DELETE FROM MOTOS WHERE IDENTIFICADOR = @IDENTIFICADOR";


        public const string InserirAno2024 =
            @"INSERT INTO PLACAS (ID, ANO, PLACA) VALUES (@ID, @ANO, @PLACA)";

        public const string ObterPlaca2024 =
           @"SELECT PLACA FROM PLACAS WHERE PLACA = @PLACA";
    }
}
