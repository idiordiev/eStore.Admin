using System.Threading;
using System.Threading.Tasks;
using eStore.Admin.Application.Filtering.Models;
using eStore.Admin.Application.RequestDTOs;
using eStore.Admin.Application.Requests.Keyboards.Commands;
using eStore.Admin.Application.Requests.Keyboards.Queries;
using eStore.Admin.Application.Responses;
using eStore.Admin.Application.Utility;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eStore.Admin.WebApi.Controllers;

[Route("api/keyboards")]
[ApiController]
[Authorize]
public class KeyboardsController : ControllerBase
{
    private readonly IMediator _mediator;

    public KeyboardsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] KeyboardFilterModel filterModel,
        [FromQuery] PagingParameters pagingParameters,
        CancellationToken cancellationToken)
    {
        var request = new GetKeyboardsByFilterPagedQuery
            { FilterModel = filterModel, PagingParameters = pagingParameters };
        var response = await _mediator.Send(request, cancellationToken);
        return Ok(response);
    }

    [HttpGet]
    [Route("{id}", Name = "GetKeyboardById")]
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
    {
        var request = new GetKeyboardByIdQuery(id);
        KeyboardResponse response = await _mediator.Send(request, cancellationToken);

        if (response is null)
        {
            return NotFound();
        }

        return Ok(response);
    }

    [HttpPost]
    [Authorize(Roles = "Administrator, Storage Manager")]
    public async Task<IActionResult> Add([FromBody] KeyboardDto keyboard, CancellationToken cancellationToken)
    {
        var request = new AddKeyboardCommand { Keyboard = keyboard };
        KeyboardResponse response = await _mediator.Send(request, cancellationToken);
        return CreatedAtRoute("GetKeyboardById", new { response.Id }, response);
    }

    [HttpPut]
    [Route("{id}")]
    [Authorize(Roles = "Administrator, Storage Manager")]
    public async Task<IActionResult> Update(int id, [FromBody] KeyboardDto keyboard,
        CancellationToken cancellationToken)
    {
        var request = new EditKeyboardCommand(id) { Keyboard = keyboard };
        KeyboardResponse response = await _mediator.Send(request, cancellationToken);
        return CreatedAtRoute("GetKeyboardById", new { response.Id }, response);
    }

    [HttpDelete]
    [Route("{id}")]
    [Authorize(Roles = "Administrator, Storage Manager")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var request = new DeleteKeyboardCommand(id);
        bool isSuccess = await _mediator.Send(request, cancellationToken);
        if (!isSuccess)
        {
            return NotFound();
        }

        return Ok();
    }
}