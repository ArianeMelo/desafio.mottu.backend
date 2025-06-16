using Mottu.Locacao.Motos.Domain.Enum;

namespace Mottu.Locacao.Motos.Domain.Entities
{
    public class Locacao
    {
        public string EntregadorId { get; set; }
        public string MotorId { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataEncerramento { get; set; }
        public DateTime DataPrevisaoEncerramento { get; set; }
        public int PlanoLocacao { get; set; }

        public Locacao(string entregadorId, string motorId, DateTime dataInicio, DateTime dataEncerramento, DateTime dataPrevisaoEncerramento, int planoLocacao)
        {
            EntregadorId = entregadorId;
            MotorId = motorId;
            DataInicio = dataInicio;
            DataEncerramento = dataEncerramento;
            DataPrevisaoEncerramento = dataPrevisaoEncerramento;
            PlanoLocacao = planoLocacao;
        }
    }

    public interface IPlanoLocacao
    {
        bool AtendePeriodo(int dias);
        decimal CalcularValor(int dias);
    }

    public class Plano7Dias : IPlanoLocacao
    {
        public bool AtendePeriodo(int dias) => dias == 7;
        public decimal CalcularValor(int dias) => dias * 30.00m;
    }

    public class Plano15Dias : IPlanoLocacao
    {
        public bool AtendePeriodo(int dias) => dias == 15;
        public decimal CalcularValor(int dias) => dias * 28.00m;
    }

    public class Plano30Dias : IPlanoLocacao
    {
        public bool AtendePeriodo(int dias) => dias == 30;
        public decimal CalcularValor(int dias) => dias * 22.00m;
    }

    public class Plano45Dias : IPlanoLocacao
    {
        public bool AtendePeriodo(int dias) => dias == 45;
        public decimal CalcularValor(int dias) => dias * 20.00m;
    }

    public class Plano50Dias : IPlanoLocacao
    {
        public bool AtendePeriodo(int dias) => dias == 50;
        public decimal CalcularValor(int dias) => dias * 18.00m;
    }

    public class CalculadoraPlanoLocacao
    {
        private readonly List<IPlanoLocacao> _planos;

        public CalculadoraPlanoLocacao()
        {
            _planos = new List<IPlanoLocacao>
        {
            new Plano7Dias(),
            new Plano15Dias(),
            new Plano30Dias(),
            new Plano45Dias(),
            new Plano50Dias()
        };
        }

        public decimal CalcularValor(int dias)
        {
            var plano = _planos.FirstOrDefault(p => p.AtendePeriodo(dias));
            if (plano == null)
                throw new ArgumentException("Período de locação inválido.");

            return plano.CalcularValor(dias);
        }
    }
}
