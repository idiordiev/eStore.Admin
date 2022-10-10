using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using eStore_Admin.Application.Filtering.Models;
using eStore_Admin.Application.RequestModels;
using eStore_Admin.Application.Requests.Mousepads.Commands.Add;
using eStore_Admin.Application.Requests.Mousepads.Commands.Delete;
using eStore_Admin.Application.Requests.Mousepads.Commands.Edit;
using eStore_Admin.Application.Requests.Mousepads.Queries.GetByFilterPaged;
using eStore_Admin.Application.Requests.Mousepads.Queries.GetById;
using eStore_Admin.Application.Utility;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eStore_Admin.WebApi.Controllers
{
    [Route("api/mousepads")]
    [ApiController]
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
            var request = new GetMousepadsByFilterPagedQuery { FilterModel = filterModel, PagingParameters = pagingParameters };
            var response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }
        
        [HttpGet]
        [Route("{id}", Name = "GetMousepadById")]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
        {
            var request = new GetMousepadByIdQuery(id);
            var response = await _mediator.Send(request, cancellationToken);
            
            if (response is null)
                return NotFound();

            return Ok(response);
        }
        
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] MousepadRequest mousepad, CancellationToken cancellationToken)
        {
            var request = new AddMousepadCommand() { Mousepad = mousepad };
            var response = await _mediator.Send(request, cancellationToken);
            return CreatedAtRoute("GetMousepadById", new { response.Id }, response);
        }
        
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] MousepadRequest mousepad, CancellationToken cancellationToken)
        {
            var request = new EditMousepadCommand(id) { Mousepad = mousepad };
            var response = await _mediator.Send(request, cancellationToken);
            return CreatedAtRoute("GetMousepadById", new { response.Id }, response);
        }
        
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var request = new DeleteMousepadCommand(id);
            var isSuccess = await _mediator.Send(request, cancellationToken);
            if (!isSuccess)
                return NotFound();

            return Ok();
        }
    }
}