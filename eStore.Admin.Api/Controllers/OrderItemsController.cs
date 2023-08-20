using System.Threading;
using System.Threading.Tasks;
using eStore.Admin.Application.RequestDTOs;
using eStore.Admin.Application.Requests.OrderItems.Commands;
using eStore.Admin.Application.Requests.OrderItems.Queries;
using eStore.Admin.Application.Utility;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eStore.Admin.Api.Controllers;

[Route("api/orders/{orderId:int}/items")]
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
        var request = new GetOrderItemsByOrderIdPagedQuery(orderId)
        {
            PagingParameters = pagingParameters
        };
        var response = await _mediator.Send(request, cancellationToken);
        
        return Ok(response);
    }

    [HttpGet]
    [Route("{orderItemId:int}", Name = "GetOrderItemById")]
    public async Task<IActionResult> GetById(int orderId, int orderItemId, CancellationToken cancellationToken)
    {
        var request = new GetOrderItemByIdQuery(orderItemId);
        var response = await _mediator.Send(request, cancellationToken);
        
        return Ok(response);
    }

    [HttpPost]
    [Authorize(Roles = "Administrator, Sales Manager")]
    public async Task<IActionResult> Create(int orderId, [FromBody] OrderItemDto orderItem,
        CancellationToken cancellationToken)
    {
        var request = new AddOrderItemCommand(orderId)
        {
            OrderItem = orderItem
        };
        var response = await _mediator.Send(request, cancellationToken);
        
        return CreatedAtRoute("GetOrderItemById", new { response.Id }, response);
    }

    [HttpPut]
    [Route("{orderItemId:int}")]
    [Authorize(Roles = "Administrator, Sales Manager")]
    public async Task<IActionResult> Update(int orderId, int orderItemId, [FromBody] OrderItemDto orderItem,
        CancellationToken cancellationToken)
    {
        var request = new EditOrderItemCommand(orderItemId)
        {
            OrderItem = orderItem
        };
        var response = await _mediator.Send(request, cancellationToken);
        
        return CreatedAtRoute("GetOrderItemById", new { response.Id }, response);
    }

    [HttpDelete]
    [Route("{orderItemId:int}")]
    [Authorize(Roles = "Administrator, Sales Manager")]
    public async Task<IActionResult> Delete(int orderId, int orderItemId, CancellationToken cancellationToken)
    {
        var request = new DeleteOrderItemCommand(orderItemId);
        var isSuccess = await _mediator.Send(request, cancellationToken);
        
        if (isSuccess)
        {
            return Ok();
        }

        return NotFound();
    }
}