using Microsoft.AspNetCore.Mvc;
using Mottu.Locacao.Motos.Api.Response;
using System.Net;

namespace Mottu.Locacao.Motos.Api.Controllers
{

    [Controller]
    public abstract class BaseController : ControllerBase
    {
        protected IActionResult CustomResponse(HttpStatusCode statusCode, bool sucesso, object? dados = null, IEnumerable<string>? erros = null)
        {          

            var response = new ResponseRequest(
                (int)statusCode,
                sucesso,
                dados ,
                erros!
            );

            return StatusCode((int)statusCode, response);
        }

        protected IActionResult OkResponse(object? dados = null)
        {
            return CustomResponse(HttpStatusCode.OK, true, dados);
        }

        protected IActionResult CreatedResponse(object? dados = null)
        {
            return CustomResponse(HttpStatusCode.Created, true, dados);
        }

        protected IActionResult NoContentResponse()
        {
            return CustomResponse(HttpStatusCode.NoContent, true);
        }

        protected IActionResult BadRequestResponse(IEnumerable<string> erros)
        {
            return CustomResponse(HttpStatusCode.BadRequest, false, null, erros);
        }

        protected IActionResult NotFoundResponse(IEnumerable<string> erros)
        {
            return CustomResponse(HttpStatusCode.NotFound, false, null, erros);
        }

        protected IActionResult InternalServerErrorResponse(IEnumerable<string> erros)
        {
            return CustomResponse(HttpStatusCode.InternalServerError, false, null, erros);
        }

        protected IActionResult UnprocessableEntityErrorResponse(IEnumerable<string> erros)
        {
            return CustomResponse(HttpStatusCode.UnprocessableEntity, false, null, erros);
        }
    }
}

