using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mottu.Locacao.Motos.Api.Response;
using Mottu.Locacao.Motos.Domain.Dtos;
using Mottu.Locacao.Motos.Domain.Interface.Application;
using Mottu.Locacao.Motos.Domain.Notification;
using Newtonsoft.Json;
using System.Net;

namespace Mottu.Locacao.Motos.Api.Controllers
{
    [Route("api/entregadores")]
    [ApiController]
    public class EntregadorController : ControllerBase
    {

        public readonly IMotoService _motoService;
        public readonly ILogger<EntregadorController> _logger;
        private readonly NotificacaoDominioHandler _notificationHandler;

        public EntregadorController(IMotoService motoService, ILogger<EntregadorController> logger, NotificacaoDominioHandler dominioHandler)
        {
            _motoService = motoService;
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
                return BadRequest(new ResponseRequest(HttpStatusCode.BadRequest.GetHashCode(), validationResult.IsValid, null, validationResult.Errors.Select(er => er.ErrorMessage)));

            //var dadoEntrada = JsonConvert.SerializeObject(entregadotDto);

            //_logger.LogInformation(string.Format("MotoController InserirDado : Request {0}", dadoEntrada));

            //await _motoService.Inserir(entregadotDto, cancellation);

            //if (_notificationHandler.ExisteNotificacao())
            //{
            //    _logger.LogInformation(string.Format("Moto Controller InserirDado : Não processado {0}",
            //        _notificationHandler.RecuperarNotificacoes()));

            //    return UnprocessableEntity(new ResponseRequest(HttpStatusCode.UnprocessableEntity.GetHashCode(),
            //        false, null, _notificationHandler.RecuperarListaNotificacoes()));
            //}

            //_logger.LogInformation(string.Format("MotoController InserirDados : Sucesso"));
            return Created();
        }
    }
}
