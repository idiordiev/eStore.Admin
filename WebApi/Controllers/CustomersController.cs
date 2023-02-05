using System.Threading;
using System.Threading.Tasks;
using eStore_Admin.Application.Filtering.Models;
using eStore_Admin.Application.RequestDTOs;
using eStore_Admin.Application.Requests.Customers.Commands;
using eStore_Admin.Application.Requests.Customers.Queries;
using eStore_Admin.Application.Responses;
using eStore_Admin.Application.Utility;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eStore_Admin.WebApi.Controllers;

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
            { FilterModel = filterModel, PagingParameters = pagingParameters };
        var response = await _mediator.Send(request, cancellationToken);
        return Ok(response);
    }

    [HttpGet]
    [Route("{id}", Name = "GetCustomerById")]
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
    {
        var request = new GetCustomerByIdQuery(id);
        CustomerResponse response = await _mediator.Send(request, cancellationToken);

        if (response is null)
        {
            return NotFound();
        }

        return Ok(response);
    }

    [HttpPut]
    [Route("{id}")]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> Update(int id, [FromBody] CustomerDto customer,
        CancellationToken cancellationToken)
    {
        var request = new EditCustomerCommand(id) { Customer = customer };
        CustomerResponse response = await _mediator.Send(request, cancellationToken);
        return CreatedAtRoute("GetCustomerById", new { response.Id }, response);
    }

    [HttpDelete]
    [Route("{id}")]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var request = new DeleteCustomerCommand(id);
        bool isSuccess = await _mediator.Send(request, cancellationToken);
        if (!isSuccess)
        {
            return NotFound();
        }

        return Ok();
    }
}