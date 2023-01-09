using System.Threading;
using System.Threading.Tasks;
using eStore_Admin.Application.Filtering.Models;
using eStore_Admin.Application.RequestDTOs;
using eStore_Admin.Application.Requests.Mousepads.Commands.Add;
using eStore_Admin.Application.Requests.Mousepads.Commands.Delete;
using eStore_Admin.Application.Requests.Mousepads.Commands.Edit;
using eStore_Admin.Application.Requests.Mousepads.Queries.GetByFilterPaged;
using eStore_Admin.Application.Requests.Mousepads.Queries.GetById;
using eStore_Admin.Application.Responses;
using eStore_Admin.Application.Utility;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eStore_Admin.WebApi.Controllers
{
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
                { FilterModel = filterModel, PagingParameters = pagingParameters };
            var response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }

        [HttpGet]
        [Route("{id}", Name = "GetMousepadById")]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
        {
            var request = new GetMousepadByIdQuery(id);
            MousepadResponse response = await _mediator.Send(request, cancellationToken);

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
            MousepadResponse response = await _mediator.Send(request, cancellationToken);
            return CreatedAtRoute("GetMousepadById", new { response.Id }, response);
        }

        [HttpPut]
        [Route("{id}")]
        [Authorize(Roles = "Administrator, Storage Manager")]
        public async Task<IActionResult> Update(int id, [FromBody] MousepadDto mousepad,
            CancellationToken cancellationToken)
        {
            var request = new EditMousepadCommand(id) { Mousepad = mousepad };
            MousepadResponse response = await _mediator.Send(request, cancellationToken);
            return CreatedAtRoute("GetMousepadById", new { response.Id }, response);
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = "Administrator, Storage Manager")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var request = new DeleteMousepadCommand(id);
            bool isSuccess = await _mediator.Send(request, cancellationToken);
            if (!isSuccess)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}