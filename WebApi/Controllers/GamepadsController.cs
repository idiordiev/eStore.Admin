using System.Threading;
using System.Threading.Tasks;
using eStore_Admin.Application.Filtering.Models;
using eStore_Admin.Application.RequestDTOs;
using eStore_Admin.Application.Requests.Gamepads.Commands.Add;
using eStore_Admin.Application.Requests.Gamepads.Commands.Delete;
using eStore_Admin.Application.Requests.Gamepads.Commands.Edit;
using eStore_Admin.Application.Requests.Gamepads.Queries.GetByFilterPaged;
using eStore_Admin.Application.Requests.Gamepads.Queries.GetById;
using eStore_Admin.Application.Utility;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace eStore_Admin.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamepadsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public GamepadsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GamepadFilterModel filterModel,
            [FromQuery] PagingParameters pagingParameters,
            CancellationToken cancellationToken)
        {
            var request = new GetGamepadsByFilterPagedQuery { FilterModel = filterModel, PagingParameters = pagingParameters };
            var response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }

        [HttpGet]
        [Route("{id}", Name = "GetGamepadById")]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
        {
            var request = new GetGamepadByIdQuery(id);
            var response = await _mediator.Send(request, cancellationToken);

            if (response is null)
                return NotFound();

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] GamepadDto gamepad, CancellationToken cancellationToken)
        {
            var request = new AddGamepadCommand { Gamepad = gamepad };
            var response = await _mediator.Send(request, cancellationToken);
            return CreatedAtRoute("GetGamepadById", new { response.Id }, response);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] GamepadDto gamepad, CancellationToken cancellationToken)
        {
            var request = new EditGamepadCommand(id) { Gamepad = gamepad };
            var response = await _mediator.Send(request, cancellationToken);
            return CreatedAtRoute("GetGamepadById", new { response.Id }, response);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var request = new DeleteGamepadCommand(id);
            var isSuccess = await _mediator.Send(request, cancellationToken);
            if (!isSuccess)
                return NotFound();

            return Ok();
        }
    }
}