using System.Threading;
using System.Threading.Tasks;
using eStore_Admin.Application.Filtering.Models;
using eStore_Admin.Application.RequestDTOs;
using eStore_Admin.Application.Requests.Mouses.Commands.Add;
using eStore_Admin.Application.Requests.Mouses.Commands.Delete;
using eStore_Admin.Application.Requests.Mouses.Commands.Edit;
using eStore_Admin.Application.Requests.Mouses.Queries.GetByFilterPaged;
using eStore_Admin.Application.Requests.Mouses.Queries.GetById;
using eStore_Admin.Application.Utility;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace eStore_Admin.WebApi.Controllers
{
    [Route("api/mouses")]
    [ApiController]
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
            var request = new GetMousesByFilterPagedQuery { FilterModel = filterModel, PagingParameters = pagingParameters };
            var response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }

        [HttpGet]
        [Route("{id}", Name = "GetMouseById")]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
        {
            var request = new GetMouseByIdQuery(id);
            var response = await _mediator.Send(request, cancellationToken);

            if (response is null)
                return NotFound();

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] MouseDto mouse, CancellationToken cancellationToken)
        {
            var request = new AddMouseCommand { Mouse = mouse };
            var response = await _mediator.Send(request, cancellationToken);
            return CreatedAtRoute("GetMouseById", new { response.Id }, response);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] MouseDto mouse, CancellationToken cancellationToken)
        {
            var request = new EditMouseCommand(id) { Mouse = mouse };
            var response = await _mediator.Send(request, cancellationToken);
            return CreatedAtRoute("GetMouseById", new { response.Id }, response);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var request = new DeleteMouseCommand(id);
            var isSuccess = await _mediator.Send(request, cancellationToken);
            if (!isSuccess)
                return NotFound();

            return Ok();
        }
    }
}