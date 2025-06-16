using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mottu.Locacao.Motos.Api.Response;
using Mottu.Locacao.Motos.Application.Service;
using Mottu.Locacao.Motos.Domain.Dtos;
using Mottu.Locacao.Motos.Domain.Interface.Application;
using Mottu.Locacao.Motos.Domain.Notification;
using Newtonsoft.Json;
using System.Net;

namespace Mottu.Locacao.Motos.Api.Controllers
{
    [Route("api/motos")]
    [ApiController]
    public class MotoController : ControllerBase
    {
        public readonly IMotoService _motoService;
        public readonly ILogger<MotoController> _logger;
        private readonly NotificacaoDominioHandler _notificationHandler;

        public MotoController(IMotoService motoService, ILogger<MotoController> logger, NotificacaoDominioHandler dominioHandler)
        {
            _motoService = motoService;
            _logger = logger;
            _notificationHandler = dominioHandler;
        }

        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        [HttpPost]
        public async Task<IActionResult> Inserir([FromBody] MotoDto motoDto, CancellationToken cancellation)
        {
            var validationResult = new MotoDtoValidator().Validate(motoDto);

            if (!validationResult.IsValid)
                return BadRequest(new ResponseRequest(HttpStatusCode.BadRequest.GetHashCode(), validationResult.IsValid, null, validationResult.Errors.Select(er => er.ErrorMessage)));

            var dadoEntrada = JsonConvert.SerializeObject(motoDto);

            _logger.LogInformation(string.Format("MotoController InserirDado : Request {0}", dadoEntrada));

            await _motoService.Inserir(motoDto, cancellation);

            if (_notificationHandler.ExisteNotificacao())
            {
                _logger.LogInformation(string.Format("Moto Controller InserirDado : Não processado {0}",
                    _notificationHandler.RecuperarNotificacoes()));

                return UnprocessableEntity(new ResponseRequest(HttpStatusCode.UnprocessableEntity.GetHashCode(),
                    false, null, _notificationHandler.RecuperarListaNotificacoes()));
            }

            _logger.LogInformation(string.Format("MotoController InserirDados : Sucesso"));
            return Created();
        }

        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        [HttpGet]
        public async Task<IActionResult> ObterPorPlaca([FromQuery] string placa, CancellationToken cancellation)
        {

            if (string.IsNullOrEmpty(placa))
                return BadRequest(new ResponseRequest(HttpStatusCode.BadRequest.GetHashCode(), false, null, new[] { "" }));

            _logger.LogInformation(string.Format("MotoController ObterPorPlaca : Request {0}", placa));

            var motoDto = await _motoService.ObterPorPlaca(placa, cancellation);

            if (motoDto is null)
            {
                _logger.LogInformation(_notificationHandler.RecuperarNotificacoes());

                return NotFound(new ResponseRequest(HttpStatusCode.NotFound.GetHashCode(), false, null, _notificationHandler.RecuperarListaNotificacoes()));
            }

            _logger.LogInformation(string.Format("MotoController ObterPorPlaca : Response {0}", JsonConvert.SerializeObject(motoDto)));

            return Ok(new ResponseRequest(HttpStatusCode.OK.GetHashCode(), true, motoDto, new[] { "" }));
        }

        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        [HttpPut("{id}/placa")]
        public async Task<IActionResult> AtualizarPlaca(string id, [FromBody] PlacaMotoDto placaDto, CancellationToken cancellation)
        {

            var validationResult = new PlacaMotoDtoValidator().Validate(placaDto);

            if (!validationResult.IsValid)
                return BadRequest(new ResponseRequest(HttpStatusCode.BadRequest.GetHashCode(), validationResult.IsValid, null, validationResult.Errors.Select(er => er.ErrorMessage)));

            _logger.LogInformation(string.Format("MotoController AtualizarPlaca : Request Id {0} - Placa {1}", id, placaDto.Placa));

            var motoDto = await _motoService.AlterarPlaca(placaDto.Placa, id, cancellation);

            if (_notificationHandler.ExisteNotificacao())
            {
                _logger.LogInformation(_notificationHandler.RecuperarNotificacoes());

                return NotFound(new ResponseRequest(HttpStatusCode.NotFound.GetHashCode(), false, null, _notificationHandler.RecuperarListaNotificacoes()));
            }


            _logger.LogInformation(string.Format("MotoController AtualizarPlaca : Sucesso"));

            return Ok(new ResponseRequest(HttpStatusCode.OK.GetHashCode(), true, "Placa modificada com sucesso", new[] { "" }));

        }

        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        [HttpGet("{id}")]
        public async Task<IActionResult> ObterPorId(string id, CancellationToken cancellation)
        {

            _logger.LogInformation(string.Format("MotoController ObterPorId : Request {0}", id));

            var motoDto = await _motoService.ObterPorIdentificador(id, cancellation);

            if (motoDto is null)
            {
                _logger.LogInformation(_notificationHandler.RecuperarNotificacoes());

                return NotFound(new ResponseRequest(HttpStatusCode.NotFound.GetHashCode(), false, null, _notificationHandler.RecuperarListaNotificacoes()));
            }
            _logger.LogInformation(string.Format("MotoController ObterPorId : Response {0}", JsonConvert.SerializeObject(motoDto)));

            return Ok(new ResponseRequest(HttpStatusCode.OK.GetHashCode(), true, motoDto, new[] { "" }));
        }

        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Cancelar(string id, CancellationToken cancellation)
        {

            return Ok();
        }
    }
}
