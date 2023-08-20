using System.Threading;
using System.Threading.Tasks;
using eStore.Admin.Application.Filtering.Models;
using eStore.Admin.Application.RequestDTOs;
using eStore.Admin.Application.Requests.Customers.Commands;
using eStore.Admin.Application.Requests.Customers.Queries;
using eStore.Admin.Application.Utility;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eStore.Admin.Api.Controllers;

[Route("api/customers")]
[ApiController]
[Authorize]
public class CustomersController : ControllerBase
{
    private readonly IMediator _mediator;

    public CustomersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] CustomerFilterModel filterModel,
        [FromQuery] PagingParameters pagingParameters,
        CancellationToken cancellationToken)
    {
        var request = new GetCustomerByFilterPagedQuery
        {
            FilterModel = filterModel,
            PagingParameters = pagingParameters
        };
        var response = await _mediator.Send(request, cancellationToken);
        
        return Ok(response);
    }

    [HttpGet]
    [Route("{id:int}", Name = "GetCustomerById")]
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
    {
        var request = new GetCustomerByIdQuery(id);
        var response = await _mediator.Send(request, cancellationToken);

        if (response is null)
        {
            return NotFound();
        }

        return Ok(response);
    }

    [HttpPut]
    [Route("{id:int}")]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> Update(int id, [FromBody] CustomerDto customer,
        CancellationToken cancellationToken)
    {
        var request = new EditCustomerCommand(id)
        {
            Customer = customer
        };
        var response = await _mediator.Send(request, cancellationToken);
        
        return CreatedAtRoute("GetCustomerById", new { response.Id }, response);
    }

    [HttpDelete]
    [Route("{id:int}")]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var request = new DeleteCustomerCommand(id);
        var isSuccess = await _mediator.Send(request, cancellationToken);
        
        if (!isSuccess)
        {
            return NotFound();
        }

        return Ok();
    }
}