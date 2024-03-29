using System.Threading;
using System.Threading.Tasks;
using eStore.Admin.Application.Filtering.Models;
using eStore.Admin.Application.RequestDTOs;
using eStore.Admin.Application.Requests.Mouses.Commands;
using eStore.Admin.Application.Requests.Mouses.Queries;
using eStore.Admin.Application.Utility;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eStore.Admin.Api.Controllers;

[Route("api/mouses")]
[ApiController]
[Authorize]
public class MousesController : ControllerBase
{
    private readonly IMediator _mediator;

    public MousesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] MouseFilterModel filterModel,
        [FromQuery] PagingParameters pagingParameters,
        CancellationToken cancellationToken)
    {
        var request = new GetMousesByFilterPagedQuery
        {
            FilterModel = filterModel,
            PagingParameters = pagingParameters
        };
        var response = await _mediator.Send(request, cancellationToken);
        return Ok(response);
    }

    [HttpGet]
    [Route("{id:int}", Name = "GetMouseById")]
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
    {
        var request = new GetMouseByIdQuery(id);
        var response = await _mediator.Send(request, cancellationToken);

        if (response is null)
        {
            return NotFound();
        }

        return Ok(response);
    }

    [HttpPost]
    [Authorize(Roles = "Administrator, Storage Manager")]
    public async Task<IActionResult> Add([FromBody] MouseDto mouse, CancellationToken cancellationToken)
    {
        var request = new AddMouseCommand
        {
            Mouse = mouse
        };
        var response = await _mediator.Send(request, cancellationToken);
        
        return CreatedAtRoute("GetMouseById", new { response.Id }, response);
    }

    [HttpPut]
    [Route("{id:int}")]
    [Authorize(Roles = "Administrator, Storage Manager")]
    public async Task<IActionResult> Update(int id, [FromBody] MouseDto mouse, CancellationToken cancellationToken)
    {
        var request = new EditMouseCommand(id)
        {
            Mouse = mouse
        };
        var response = await _mediator.Send(request, cancellationToken);
        
        return CreatedAtRoute("GetMouseById", new { response.Id }, response);
    }

    [HttpDelete]
    [Route("{id:int}")]
    [Authorize(Roles = "Administrator, Storage Manager")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var request = new DeleteMouseCommand(id);
        var isSuccess = await _mediator.Send(request, cancellationToken);
        
        if (!isSuccess)
        {
            return NotFound();
        }

        return Ok();
    }
}