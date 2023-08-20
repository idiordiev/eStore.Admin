using System.Threading;
using System.Threading.Tasks;
using eStore.Admin.Application.Filtering.Models;
using eStore.Admin.Application.RequestDTOs;
using eStore.Admin.Application.Requests.Orders.Commands;
using eStore.Admin.Application.Requests.Orders.Queries;
using eStore.Admin.Application.Responses;
using eStore.Admin.Application.Utility;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eStore.Admin.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class OrdersController : ControllerBase
{
    private readonly IMediator _mediator;

    public OrdersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] OrderFilterModel filterModel,
        [FromQuery] PagingParameters pagingParams,
        CancellationToken cancellationToken)
    {
        var request = new GetOrdersByFilterPagedQuery
            { PagingParameters = pagingParams, FilterModel = filterModel };
        var response = await _mediator.Send(request, cancellationToken);
        return Ok(response);
    }

    [HttpGet]
    [Route("{id}", Name = "GetOrderById")]
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
    {
        var request = new GetOrderByIdQuery(id);
        OrderResponse response = await _mediator.Send(request, cancellationToken);

        if (response is null)
        {
            return NotFound();
        }

        return Ok(response);
    }

    [HttpPost]
    [Authorize(Roles = "Administrator, Sales Manager")]
    public async Task<IActionResult> Create([FromBody] OrderDto order, CancellationToken cancellationToken)
    {
        var request = new AddOrderCommand { Order = order };
        OrderResponse response = await _mediator.Send(request, cancellationToken);
        return CreatedAtRoute("GetOrderById", new { response.Id }, response);
    }

    [HttpPut]
    [Route("{id}")]
    [Authorize(Roles = "Administrator, Sales Manager")]
    public async Task<IActionResult> Update(int id, [FromBody] OrderDto order, CancellationToken cancellationToken)
    {
        var request = new EditOrderCommand(id) { Order = order };
        OrderResponse response = await _mediator.Send(request, cancellationToken);
        return CreatedAtRoute("GetOrderById", new { response.Id }, response);
    }

    [HttpDelete]
    [Route("{id}")]
    [Authorize(Roles = "Administrator, Sales Manager")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var request = new DeleteOrderCommand(id);
        bool isSuccess = await _mediator.Send(request, cancellationToken);
        if (isSuccess)
        {
            return Ok();
        }

        return NotFound();
    }
}