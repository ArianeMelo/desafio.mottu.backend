using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mottu.Locacao.Motos.Domain.Entities
{
    public class Entregador
    {
        public string Identificador { get; set; }
        public string Nome { get; set; }
        public string Cnpj { get; set; }
        public string DataNascimento { get; set; }
        public string NumeroCnh { get; set; }
        public string TipoCnh { get; set; }
        public string ImagemCnh { get; set; }

        public Entregador(string identificador, string nome, string cnpj, string dataNascimento, string numeroCnh, string tipoCnh, string imagemCnh)
        {
            Identificador = identificador;
            Nome = nome;
            Cnpj = cnpj;
            DataNascimento = dataNascimento;
            NumeroCnh = numeroCnh;
            TipoCnh = tipoCnh;
            ImagemCnh = imagemCnh;
        }

       
    }
}
