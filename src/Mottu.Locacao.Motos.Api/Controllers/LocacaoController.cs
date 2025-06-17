using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Mottu.Locacao.Motos.Api.Response;
using Mottu.Locacao.Motos.Domain.Dtos;
using Mottu.Locacao.Motos.Domain.Interface.Service;
using Newtonsoft.Json;
using System.Net;

namespace Mottu.Locacao.Motos.Api.Controllers
{
    [Route("api/locacoes")]
    [ApiController]
    public class LocacaoController : BaseController
    {

        public readonly ILocacaoService _locacaoService;
        public readonly ILogger<LocacaoController> _logger;
        private readonly INotificacaoDominioHandler _notificationHandler;

        public LocacaoController(ILocacaoService locacaoService, ILogger<LocacaoController> logger, INotificacaoDominioHandler dominioHandler)
        {
            _locacaoService = locacaoService;
            _logger = logger;
            _notificationHandler = dominioHandler;
        }

        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        [HttpPost]
        public async Task<IActionResult> Inserir([FromBody] LocacaoRequestDto locacaorequest, CancellationToken cancellation)
        {
            var validationResult = new LocacaoRequestDtoValidator().Validate(locacaorequest);

            if (!validationResult.IsValid)
                return BadRequestResponse(validationResult.Errors.Select(er => er.ErrorMessage));

            var dadoEntrada = JsonConvert.SerializeObject(locacaorequest);

            _logger.LogInformation(string.Format("LocacaoController Inserir : Request {0}", dadoEntrada));

            await _locacaoService.Inserir(locacaorequest, cancellation);

            if (_notificationHandler.ExisteNotificacao())
            {
                _logger.LogInformation(_notificationHandler.RecuperarNotificacoes());

                return UnprocessableEntityErrorResponse(_notificationHandler.RecuperarListaNotificacoes());
            }

            _logger.LogInformation(string.Format("LocacaoController Inserir : Sucesso"));

            return CreatedResponse();
        }

        [ProducesResponseType(typeof(LocacaoResponseDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        [HttpGet("{entregadorId}")]
        public async Task<IActionResult> ObterPorId(string entregadorId, CancellationToken cancellation)
        {
            _logger.LogInformation(string.Format("LocacaoController ObterPorId : Request {0}", entregadorId));

            var motoDto = await _locacaoService.ObterLocacao(entregadorId, cancellation);

            if (motoDto is null)
            {
                _logger.LogInformation(_notificationHandler.RecuperarNotificacoes());

                return NotFoundResponse(_notificationHandler.RecuperarListaNotificacoes());
            }
            _logger.LogInformation(string.Format("LocacaoController ObterPorId : Response {0}", JsonConvert.SerializeObject(motoDto)));

            return OkResponse(motoDto);
        }

        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]

        [HttpPut("{entregadorId}")]
        public async Task<IActionResult> InserirDataDevolucao(string entregadorId, [FromBody] DevolucaoDto dto, CancellationToken cancellation)
        {
            var validationResult = new DevolucaoDtoValidator().Validate(dto);

            if (!validationResult.IsValid)
                return BadRequestResponse(validationResult.Errors.Select(er => er.ErrorMessage));

            var dadoEntrada = JsonConvert.SerializeObject(dto);

            _logger.LogInformation(string.Format("LocacaoController InserirDataDevolucao : Request EntregadorId {0} - Data Devoluação {1}", entregadorId, dadoEntrada));

            await _locacaoService.AtualizarLocacao(entregadorId, dto, cancellation);

            if (_notificationHandler.ExisteNotificacao())
            {
                _logger.LogInformation(_notificationHandler.RecuperarNotificacoes());
                return UnprocessableEntityErrorResponse(_notificationHandler.RecuperarListaNotificacoes());
            }

            return OkResponse("Data de devolução inserida com sucesso.");
        }
    }
}
