using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using MotoManager.Domain.Commands;
using MotoManager.Domain.Commands.Admin.Motos;
using MotoManager.Domain.Interfaces.Commands;
using MotoManager.Domain.Interfaces.Handler;
using MotoManager.Domain.Commands.Public.Entregador;

namespace MotoManager.Api.Controllers
{
    [ApiController]
    [Route("v1/entregadores", Name = "Entregadores")]
    public class EntregadorControllerr : ControllerBase
    {
        [HttpPost]
        [SwaggerOperation(Summary = "Cadastrar entregador")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CommandResult))]
        public async Task<IActionResult> Create(
            [FromBody] EntregadorCreateCommand command,
            [FromServices] IHandler<EntregadorCreateCommand> handler,
            CancellationToken cancellationToken = default)
        {
            var result = await handler.Handle(command, cancellationToken);

            if (!result.GetSuccess())
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPost("{id:int}/cnh")]
        [SwaggerOperation(Summary = "Enviar CNH")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CommandResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CommandResult))]
        public async Task<IActionResult> Update(
            [FromRoute] int id,
            [FromBody] EntregadorUpdateCnhCommand command,
            [FromServices] IHandler<EntregadorUpdateCommand> handler,
            CancellationToken cancellationToken = default)
        {
            var commandUpdate = new EntregadorUpdateCommand(id, command.FotoCnh);

            var result = await handler.Handle(commandUpdate, cancellationToken);

            if (!result.GetSuccess())
                return BadRequest(result);

            return Ok(result);
        }

    }
}
