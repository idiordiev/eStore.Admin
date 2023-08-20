using System.Threading;
using System.Threading.Tasks;
using eStore.Admin.Application.Filtering.Models;
using eStore.Admin.Application.RequestDTOs;
using eStore.Admin.Application.Requests.Gamepads.Commands;
using eStore.Admin.Application.Requests.Gamepads.Queries;
using eStore.Admin.Application.Utility;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eStore.Admin.Api.Controllers;

[Route("api/gamepads")]
[ApiController]
[Authorize]
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
        var request = new GetGamepadsByFilterPagedQuery
        {
            FilterModel = filterModel,
            PagingParameters = pagingParameters
        };
        var response = await _mediator.Send(request, cancellationToken);
        return Ok(response);
    }

    [HttpGet]
    [Route("{id:int}", Name = "GetGamepadById")]
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
    {
        var request = new GetGamepadByIdQuery(id);
        var response = await _mediator.Send(request, cancellationToken);

        if (response is null)
        {
            return NotFound();
        }

        return Ok(response);
    }

    [HttpPost]
    [Authorize(Roles = "Administrator, Storage Manager")]
    public async Task<IActionResult> Add([FromBody] GamepadDto gamepad, CancellationToken cancellationToken)
    {
        var request = new AddGamepadCommand
        {
            Gamepad = gamepad
        };
        var response = await _mediator.Send(request, cancellationToken);
        
        return CreatedAtRoute("GetGamepadById", new { response.Id }, response);
    }

    [HttpPut]
    [Route("{id:int}")]
    [Authorize(Roles = "Administrator, Storage Manager")]
    public async Task<IActionResult> Update(int id, [FromBody] GamepadDto gamepad,
        CancellationToken cancellationToken)
    {
        var request = new EditGamepadCommand(id)
        {
            Gamepad = gamepad
        };
        var response = await _mediator.Send(request, cancellationToken);
        
        return CreatedAtRoute("GetGamepadById", new { response.Id }, response);
    }

    [HttpDelete]
    [Route("{id:int}")]
    [Authorize(Roles = "Administrator, Storage Manager")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var request = new DeleteGamepadCommand(id);
        var isSuccess = await _mediator.Send(request, cancellationToken);
        
        if (!isSuccess)
        {
            return NotFound();
        }

        return Ok();
    }
}