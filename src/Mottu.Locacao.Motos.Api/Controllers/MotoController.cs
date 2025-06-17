using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mottu.Locacao.Motos.Api.Response;
using Mottu.Locacao.Motos.Api.RoleFilter;
using Mottu.Locacao.Motos.Domain.Dtos;
using Mottu.Locacao.Motos.Domain.Interface.Application;
using Mottu.Locacao.Motos.Domain.Interface.Service;
using Newtonsoft.Json;
using System.Net;

namespace Mottu.Locacao.Motos.Api.Controllers
{
    [Authorize]
    [Route("api/motos")]
    [ApiController]
    public class MotoController : BaseController
    {
        public readonly IMotoService _motoService;
        public readonly ILogger<MotoController> _logger;
        private readonly INotificacaoDominioHandler _notificationHandler;

        public MotoController(IMotoService motoService, ILogger<MotoController> logger, INotificacaoDominioHandler dominioHandler)
        {
            _motoService = motoService;
            _logger = logger;
            _notificationHandler = dominioHandler;
        }

        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        [RoleAuthorizer(roles: ["Admin"])]
        [HttpPost]
        public async Task<IActionResult> Inserir([FromBody] MotoDto motoDto, CancellationToken cancellation)
        {
            var validationResult = new MotoDtoValidator().Validate(motoDto);

            if (!validationResult.IsValid)
                return BadRequestResponse(validationResult.Errors.Select(er => er.ErrorMessage));

            var dadoEntrada = JsonConvert.SerializeObject(motoDto);

            _logger.LogInformation(string.Format("MotoController InserirDado : Request {0}", dadoEntrada));

            await _motoService.Inserir(motoDto, cancellation);

            if (_notificationHandler.ExisteNotificacao())
            {
                _logger.LogInformation(_notificationHandler.RecuperarNotificacoes());

                return UnprocessableEntityErrorResponse(_notificationHandler.RecuperarListaNotificacoes());
            }

            _logger.LogInformation(string.Format("MotoController InserirDados : Sucesso"));
            return CreatedResponse();
        }

        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [RoleAuthorizer(roles: ["Admin"])]
        [HttpGet]
        public async Task<IActionResult> ObterPorPlaca([FromQuery] string placa, CancellationToken cancellation)
        {
            _logger.LogInformation(string.Format("MotoController ObterPorPlaca : Request {0}", placa));

            var motoDto = await _motoService.ObterPorPlaca(placa, cancellation);

            if (motoDto is null)
            {
                _logger.LogInformation(_notificationHandler.RecuperarNotificacoes());

                return NotFoundResponse(_notificationHandler.RecuperarListaNotificacoes());
            }

            _logger.LogInformation(string.Format("MotoController ObterPorPlaca : Response {0}", JsonConvert.SerializeObject(motoDto)));

            return OkResponse(motoDto);
        }

        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        [RoleAuthorizer(roles: ["Admin"])]
        [HttpPut("{id}/placa")]
        public async Task<IActionResult> AtualizarPlaca(string id, [FromBody] PlacaDto placaDto, CancellationToken cancellation)
        {

            var validationResult = new PlacaMotoDtoValidator().Validate(placaDto);

            if (!validationResult.IsValid)
                return BadRequestResponse(validationResult.Errors.Select(er => er.ErrorMessage));

            _logger.LogInformation(string.Format("MotoController AtualizarPlaca : Request Id {0} - Placa {1}", id, placaDto.Placa));

            var motoDto = await _motoService.AlterarPlaca(placaDto.Placa, id, cancellation);

            if (_notificationHandler.ExisteNotificacao())
            {
                _logger.LogInformation(_notificationHandler.RecuperarNotificacoes());

                return NotFoundResponse(_notificationHandler.RecuperarListaNotificacoes());
            }

            _logger.LogInformation(string.Format("MotoController AtualizarPlaca : Sucesso"));

            return OkResponse("Placa modificada com sucesso");

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

                return NotFoundResponse(_notificationHandler.RecuperarListaNotificacoes());
            }
            _logger.LogInformation(string.Format("MotoController ObterPorId : Response {0}", JsonConvert.SerializeObject(motoDto)));

            return OkResponse(motoDto);
        }

        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        [RoleAuthorizer(roles: ["Admin"])]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Remover(string id, CancellationToken cancellation)
        {
            _logger.LogInformation(string.Format("MotoController Remover : Request {0}", id));

            await _motoService.Remover(id, cancellation);

            if (_notificationHandler.ExisteNotificacao())
            {
                _logger.LogInformation(_notificationHandler.RecuperarNotificacoes());

                return BadRequestResponse(_notificationHandler.RecuperarListaNotificacoes());
            }

            return OkResponse("Registro removido com sucesso");
        }
    }
}
