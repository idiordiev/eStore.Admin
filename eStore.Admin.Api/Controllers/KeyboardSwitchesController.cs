using System.Threading;
using System.Threading.Tasks;
using eStore.Admin.Application.RequestDTOs;
using eStore.Admin.Application.Requests.KeyboardSwitches.Commands;
using eStore.Admin.Application.Requests.KeyboardSwitches.Queries;
using eStore.Admin.Application.Utility;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eStore.Admin.Api.Controllers;

[Route("api/keyboard-switches")]
[ApiController]
[Authorize]
public class KeyboardSwitchSwitchesController : ControllerBase
{
    private readonly IMediator _mediator;

    public KeyboardSwitchSwitchesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] PagingParameters pagingParameters,
        CancellationToken cancellationToken)
    {
        var request = new GetAllKeyboardSwitchesPagedQuery
        {
            PagingParameters = pagingParameters
        };
        var response = await _mediator.Send(request, cancellationToken);
        
        return Ok(response);
    }

    [HttpGet]
    [Route("{id:int}", Name = "GetKeyboardSwitchById")]
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
    {
        var request = new GetKeyboardSwitchByIdQuery(id);
        var response = await _mediator.Send(request, cancellationToken);

        if (response is null)
        {
            return NotFound();
        }

        return Ok(response);
    }

    [HttpPost]
    [Authorize(Roles = "Administrator, Storage Manager")]
    public async Task<IActionResult> Add([FromBody] KeyboardSwitchDto keyboardSwitch,
        CancellationToken cancellationToken)
    {
        var request = new AddKeyboardSwitchCommand
        {
            KeyboardSwitch = keyboardSwitch
        };
        var response = await _mediator.Send(request, cancellationToken);
        
        return CreatedAtRoute("GetKeyboardSwitchById", new { response.Id }, response);
    }

    [HttpPut]
    [Route("{id:int}")]
    [Authorize(Roles = "Administrator, Storage Manager")]
    public async Task<IActionResult> Update(int id, [FromBody] KeyboardSwitchDto keyboardSwitch,
        CancellationToken cancellationToken)
    {
        var request = new EditKeyboardSwitchCommand(id)
        {
            KeyboardSwitch = keyboardSwitch
        };
        var response = await _mediator.Send(request, cancellationToken);
        
        return CreatedAtRoute("GetKeyboardSwitchById", new { response.Id }, response);
    }

    [HttpDelete]
    [Route("{id:int}")]
    [Authorize(Roles = "Administrator, Storage Manager")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var request = new DeleteKeyboardSwitchCommand(id);
        var isSuccess = await _mediator.Send(request, cancellationToken);
        
        if (!isSuccess)
        {
            return NotFound();
        }

        return Ok();
    }
}