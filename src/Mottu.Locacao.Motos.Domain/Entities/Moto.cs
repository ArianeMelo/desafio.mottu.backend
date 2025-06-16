namespace Mottu.Locacao.Motos.Domain.Entities
{
    public class Moto : Entity
    {
        public string Identificador { get; private set; }
        public int Ano { get; private set; }
        public string Modelo { get; private set; }
        public string Placa { get; private set; }

        public Moto(string identificador, int ano, string modelo, string placa)
        {
            Identificador = identificador;
            Ano = ano;
            Modelo = modelo;
            Placa = placa;
        }

        public bool PodePostarNaFila()
            => Ano == 2024;
    }
}
