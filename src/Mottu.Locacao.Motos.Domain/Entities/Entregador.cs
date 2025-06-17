using Mottu.Locacao.Motos.Domain.Enum;

namespace Mottu.Locacao.Motos.Domain.Entities
{
    public class Entregador : Entity
    {
        public string Identificador { get; set; }
        public string Nome { get; set; }
        public string Cnpj { get; set; }
        public DateTime DataNascimento { get; set; }
        public string NumeroCnh { get; set; }
        public int TipoCnh { get; set; }
        public string ImagemCnh { get; set; }

        public Entregador(string identificador, string nome, string cnpj, DateTime dataNascimento, string numeroCnh, int tipoCnh, string imagemCnh)
        {
            Identificador = identificador;
            Nome = nome;
            Cnpj = cnpj;
            DataNascimento = dataNascimento;
            NumeroCnh = numeroCnh;
            TipoCnh = tipoCnh;
            ImagemCnh = imagemCnh;
        }
        public Entregador()
        {
            
        }

        public bool CategoriaCnhA()
            => TipoCnh.Equals((int)CategoriaCnh.A);
    }
}
