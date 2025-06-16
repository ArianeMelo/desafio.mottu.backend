using Mottu.Locacao.Motos.Domain.Dtos;
using Mottu.Locacao.Motos.Domain.Entities;

namespace Mottu.Locacao.Motos.Application.Extensions
{
    public static class MotoExtensions
    {
        public static MotoDto ParaMotoDto(this Moto moto)
        {
            if (moto is null) return null;

            return new MotoDto(moto.Identificador, moto.Ano, moto.Modelo, moto.Placa);
        }

        public static Moto ParaMoto(this MotoDto motoDto)
        {
            if (motoDto is null) return null;

            return new Moto(motoDto.Identificador, motoDto.Ano, motoDto.Modelo, motoDto.Placa);
        }
    }
}

