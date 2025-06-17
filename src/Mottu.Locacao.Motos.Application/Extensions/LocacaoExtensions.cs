using Mottu.Locacao.Motos.Domain.Dtos;
using Mottu.Locacao.Motos.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mottu.Locacao.Motos.Application.Extensions
{
    public static class LocacaoExtensions
    {

        public static LocacaoEntity ParaDominio(this LocacaoRequestDto request)
        {
            if (request is null) return null;

            return new LocacaoEntity(request.EntregadorId,
                request.MotoId, 
                null,
                                      request.DataInicio,
                                      request.DataEncerramento,
                                      request.DataPrevistaEncerramento,
                                      null,
                                      request.PlanoLocacao);
        }

        public static LocacaoResponseDto ParaResponseDto(this LocacaoEntity request)
        {
            if (request is null) return null;

            return new LocacaoResponseDto
            {
                EntregadorId = request.EntregadorId,
                MotoId = request.MotoId,
                ValorDiaria = request.ValorDiaria,
                DataInicio = request.DataInicio,
                DataEncerramento = request.DataTermino,
                DataPrevistaEncerramento = request.DataPrevistaEncerramento,
                DataDevolucao = request.DataDevolucao,
                ValorPorAdiantamento = request.ValorAdiantamento,
                ValorAtraso = request.ValorAtraso,
                ValorTotalLocacao = request.ValorTotalLocacao
            };
        }
    }
}
