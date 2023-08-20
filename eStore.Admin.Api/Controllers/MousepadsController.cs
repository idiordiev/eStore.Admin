using System.Threading;
using System.Threading.Tasks;
using eStore.Admin.Application.Filtering.Models;
using eStore.Admin.Application.RequestDTOs;
using eStore.Admin.Application.Requests.Mousepads.Commands;
using eStore.Admin.Application.Requests.Mousepads.Queries;
using eStore.Admin.Application.Utility;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eStore.Admin.Api.Controllers;

[Route("api/mousepads")]
[ApiController]
[Authorize]
public class MousepadsController : ControllerBase
{
    private readonly IMediator _mediator;

    public MousepadsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] MousepadFilterModel filterModel,
        [FromQuery] PagingParameters pagingParameters,
        CancellationToken cancellationToken)
    {
        var request = new GetMousepadsByFilterPagedQuery
        {
            FilterModel = filterModel,
            PagingParameters = pagingParameters
        };
        var response = await _mediator.Send(request, cancellationToken);
        
        return Ok(response);
    }

    [HttpGet]
    [Route("{id:int}", Name = "GetMousepadById")]
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
    {
        var request = new GetMousepadByIdQuery(id);
        var response = await _mediator.Send(request, cancellationToken);

        if (response is null)
        {
            return NotFound();
        }

        return Ok(response);
    }

    [HttpPost]
    [Authorize(Roles = "Administrator, Storage Manager")]
    public async Task<IActionResult> Add([FromBody] MousepadDto mousepad, CancellationToken cancellationToken)
    {
        var request = new AddMousepadCommand { Mousepad = mousepad };
        var response = await _mediator.Send(request, cancellationToken);
        
        return CreatedAtRoute("GetMousepadById", new { response.Id }, response);
    }

    [HttpPut]
    [Route("{id:int}")]
    [Authorize(Roles = "Administrator, Storage Manager")]
    public async Task<IActionResult> Update(int id, [FromBody] MousepadDto mousepad,
        CancellationToken cancellationToken)
    {
        var request = new EditMousepadCommand(id)
        {
            Mousepad = mousepad
        };
        var response = await _mediator.Send(request, cancellationToken);
        
        return CreatedAtRoute("GetMousepadById", new { response.Id }, response);
    }

    [HttpDelete]
    [Route("{id:int}")]
    [Authorize(Roles = "Administrator, Storage Manager")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var request = new DeleteMousepadCommand(id);
        var isSuccess = await _mediator.Send(request, cancellationToken);
        
        if (!isSuccess)
        {
            return NotFound();
        }

        return Ok();
    }
}