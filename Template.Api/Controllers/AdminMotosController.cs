using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using MotoManager.Domain.Commands;
using MotoManager.Domain.Commands.Admin.Motos;
using MotoManager.Domain.Interfaces.Commands;
using MotoManager.Domain.Interfaces.Handler;
using MotoManager.Domain.Interfaces.Queries;
using MotoManager.Domain.Dtos;

namespace MotoManager.Api.Controllers
{
    [ApiController]
    [Route("v1/motos", Name = "Motos")]
    public class AdminMotosController : ControllerBase
    {
        [HttpPost]
        [SwaggerOperation(Summary = "Cadastrar uma nova moto")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CommandResult))]
        public async Task<IActionResult> Create(
            [FromBody] MotosCreateCommand command,
            [FromServices] IHandler<MotosCreateCommand> handler,
            CancellationToken cancellationToken = default)
        {
            var result = await handler.Handle(command, cancellationToken);

            if (!result.GetSuccess())
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPut("{id}/placa")]
        [SwaggerOperation(Summary = "Modificar a placa de uma moto")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CommandResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CommandResult))]
        public async Task<IActionResult> Update(
            [FromRoute] string id,
            [FromBody] MotosUpdatePlacaCommand command,
            [FromServices] IHandler<MotosUpdateCommand> handler,
            CancellationToken cancellationToken = default)
        {
            var commandUpdate = new MotosUpdateCommand(command.Placa, id);

            var result = await handler.Handle(commandUpdate, cancellationToken);

            if (!result.GetSuccess())
                return BadRequest(result);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Remover uma moto")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CommandResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CommandResult))]
        public async Task<IActionResult> Delete(
            [FromRoute] string id,
            [FromServices] IHandler<MotosDeleteCommand> handler,
            CancellationToken cancellationToken = default)
        {
            var command = new MotosDeleteCommand(id);

            var result = await handler.Handle(command, cancellationToken);

            if (!result.GetSuccess())
                return BadRequest(result);

            return Ok(result);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Consultar motos existentes por id")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MotoDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CommandResult))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(CommandResult))]
        public async Task<IActionResult> GetMoto(
            string id,
            [FromServices] IMotoQuery query,
            CancellationToken cancellationToken = default)
        {
            var result = await query.GetMotoById(id, cancellationToken);
            return Ok(result);
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Consultar motos existentes")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<MotoDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CommandResult))]
        public async Task<IActionResult> GetMotos(
            [FromServices] IMotoQuery query,
            CancellationToken cancellationToken = default)
        {
            var result = await query.GetMotos(cancellationToken);
            return Ok(result);
        }
    }
}
