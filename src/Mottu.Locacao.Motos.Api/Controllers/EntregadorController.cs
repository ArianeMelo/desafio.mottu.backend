using Microsoft.AspNetCore.Mvc;
using Mottu.Locacao.Motos.Domain.Dtos;
using Mottu.Locacao.Motos.Domain.Interface.Service;
using Newtonsoft.Json;
using System.Net;

namespace Mottu.Locacao.Motos.Api.Controllers
{
    [Route("api/entregadores")]
    [ApiController]
    public class EntregadorController : BaseController
    {
        public readonly IEntregadorService _entregadorService;
        public readonly ILogger<EntregadorController> _logger;
        private readonly INotificacaoDominioHandler _notificationHandler;

        public EntregadorController(IEntregadorService entregadorService, ILogger<EntregadorController> logger, INotificacaoDominioHandler dominioHandler)
        {
            _entregadorService = entregadorService;
            _logger = logger;
            _notificationHandler = dominioHandler;
        }

        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        [HttpPost]
        public async Task<IActionResult> Inserir([FromBody] EntregadorDto entregadotDto, CancellationToken cancellation)
        {
            var validationResult = new EntregadorDtoValidation().Validate(entregadotDto);

            if (!validationResult.IsValid)
                return BadRequestResponse(validationResult.Errors.Select(er => er.ErrorMessage));

            var dadoEntrada = JsonConvert.SerializeObject(entregadotDto);

            _logger.LogInformation(string.Format("EntregadorController Inserir : Request {0}", dadoEntrada));

            await _entregadorService.Inserir(entregadotDto, cancellation);

            if (_notificationHandler.ExisteNotificacao())
            {
                _logger.LogInformation(_notificationHandler.RecuperarNotificacoes());

                return UnprocessableEntity(_notificationHandler.RecuperarListaNotificacoes());
            }

            _logger.LogInformation(string.Format("EntregadorController Inserir : Sucesso"));
            return CreatedResponse();
        }
    }
}
