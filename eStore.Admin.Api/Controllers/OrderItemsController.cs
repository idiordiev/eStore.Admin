using System.Threading;
using System.Threading.Tasks;
using eStore.Admin.Application.RequestDTOs;
using eStore.Admin.Application.Requests.OrderItems.Commands;
using eStore.Admin.Application.Requests.OrderItems.Queries;
using eStore.Admin.Application.Responses;
using eStore.Admin.Application.Utility;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eStore.Admin.Api.Controllers;

[Route("api/orders/{orderId}/items")]
[ApiController]
[Authorize]
public class OrderItemsController : ControllerBase
{
    private readonly IMediator _mediator;

    public OrderItemsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetByOrderId(int orderId, [FromQuery] PagingParameters pagingParameters,
        CancellationToken cancellationToken)
    {
        var request = new GetOrderItemsByOrderIdPagedQuery(orderId) { PagingParameters = pagingParameters };
        var response = await _mediator.Send(request, cancellationToken);
        return Ok(response);
    }

    [HttpGet]
    [Route("{orderItemId}", Name = "GetOrderItemById")]
    public async Task<IActionResult> GetById(int orderId, int orderItemId, CancellationToken cancellationToken)
    {
        var request = new GetOrderItemByIdQuery(orderItemId);
        OrderItemResponse response = await _mediator.Send(request, cancellationToken);
        return Ok(response);
    }

    [HttpPost]
    [Authorize(Roles = "Administrator, Sales Manager")]
    public async Task<IActionResult> Create(int orderId, [FromBody] OrderItemDto orderItem,
        CancellationToken cancellationToken)
    {
        var request = new AddOrderItemCommand(orderId) { OrderItem = orderItem };
        OrderResponse response = await _mediator.Send(request, cancellationToken);
        return CreatedAtRoute("GetOrderItemById", new { response.Id }, response);
    }

    [HttpPut]
    [Route("{orderItemId}")]
    [Authorize(Roles = "Administrator, Sales Manager")]
    public async Task<IActionResult> Update(int orderId, int orderItemId, [FromBody] OrderItemDto orderItem,
        CancellationToken cancellationToken)
    {
        var request = new EditOrderItemCommand(orderItemId) { OrderItem = orderItem };
        OrderResponse response = await _mediator.Send(request, cancellationToken);
        return CreatedAtRoute("GetOrderItemById", new { response.Id }, response);
    }

    [HttpDelete]
    [Route("{orderItemId}")]
    [Authorize(Roles = "Administrator, Sales Manager")]
    public async Task<IActionResult> Delete(int orderId, int orderItemId, CancellationToken cancellationToken)
    {
        var request = new DeleteOrderItemCommand(orderItemId);
        bool isSuccess = await _mediator.Send(request, cancellationToken);
        if (isSuccess)
        {
            return Ok();
        }

        return NotFound();
    }
}