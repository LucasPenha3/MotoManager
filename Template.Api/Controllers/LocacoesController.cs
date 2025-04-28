using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using MotoManager.Domain.Commands;
using MotoManager.Domain.Commands.Admin.Motos;
using MotoManager.Domain.Interfaces.Commands;
using MotoManager.Domain.Interfaces.Handler;
using MotoManager.Domain.Commands.Public.Entregador;
using MotoManager.Domain.Commands.Public.Locacao;
using MotoManager.Domain.Dtos;
using MotoManager.Domain.Interfaces.Queries;

namespace MotoManager.Api.Controllers
{
    [ApiController]
    [Route("v1/locacoes", Name = "Locacoes")]
    public class LocacoesController : ControllerBase
    {
        [HttpPost]
        [SwaggerOperation(Summary = "Adicionar uma locação")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CommandResult))]
        public async Task<IActionResult> Create(
            [FromBody] LocacaoRentCommand command,
            [FromServices] IHandler<LocacaoRentCommand> handler,
            CancellationToken cancellationToken = default)
        {
            var result = await handler.Handle(command, cancellationToken);

            if (!result.GetSuccess())
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPost("{id:int}/devolucao")]
        [SwaggerOperation(Summary = "Efetuar uma devolução")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CommandResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CommandResult))]
        public async Task<IActionResult> Update(
            [FromRoute] int id,
            [FromBody] LocacaoDataDevolucaoCommand command,
            [FromServices] IHandler<LocacaoReturnCommand> handler,
            CancellationToken cancellationToken = default)
        {
            var commandUpdate = new LocacaoReturnCommand(id, command.DataDevolucao);

            var result = await handler.Handle(commandUpdate, cancellationToken);

            if (!result.GetSuccess())
                return BadRequest(result);

            return Ok(result);
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Consultar locações existentes")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<LocacaoDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CommandResult))]
        public async Task<IActionResult> GetMotos(
            [FromServices] ILocacaoQuery query,
            CancellationToken cancellationToken = default)
        {
            var result = await query.GetLocacoes(cancellationToken);
            return Ok(result);
        }

    }
}
