using System.Threading;
using System.Threading.Tasks;
using eStore_Admin.Application.Filtering.Models;
using eStore_Admin.Application.RequestModels;
using eStore_Admin.Application.Requests.Customers.Commands.Add;
using eStore_Admin.Application.Requests.Customers.Commands.Delete;
using eStore_Admin.Application.Requests.Customers.Commands.Edit;
using eStore_Admin.Application.Requests.Customers.Queries.GetByFilterPaged;
using eStore_Admin.Application.Requests.Customers.Queries.GetById;
using eStore_Admin.Application.Utility;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace eStore_Admin.WebApi.Controllers
{
    [Route("api/customers")]
    [ApiController]
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
            var request = new GetCustomerByFilterPagedQuery { FilterModel = filterModel, PagingParameters = pagingParameters };
            var response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }
        
        [HttpGet]
        [Route("{id}", Name = "GetCustomerById")]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
        {
            var request = new GetCustomerByIdQuery(id);
            var response = await _mediator.Send(request, cancellationToken);
            
            if (response is null)
                return NotFound();

            return Ok(response);
        }
        
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CustomerRequest customer, CancellationToken cancellationToken)
        {
            var request = new AddCustomerCommand() { Customer = customer };
            var response = await _mediator.Send(request, cancellationToken);
            return CreatedAtRoute("GetCustomerById", new { response.Id }, response);
        }
        
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CustomerRequest customer, CancellationToken cancellationToken)
        {
            var request = new EditCustomerCommand(id) { Customer = customer };
            var response = await _mediator.Send(request, cancellationToken);
            return CreatedAtRoute("GetCustomerById", new { response.Id }, response);
        }
        
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var request = new DeleteCustomerCommand(id);
            var isSuccess = await _mediator.Send(request, cancellationToken);
            if (!isSuccess)
                return NotFound();

            return Ok();
        }
    }
}