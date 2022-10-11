using System.Threading;
using System.Threading.Tasks;
using eStore_Admin.Application.RequestDTOs;
using eStore_Admin.Application.Requests.KeyboardSwitches.Commands.Add;
using eStore_Admin.Application.Requests.KeyboardSwitches.Commands.Delete;
using eStore_Admin.Application.Requests.KeyboardSwitches.Commands.Edit;
using eStore_Admin.Application.Requests.KeyboardSwitches.Queries.GetAllPaged;
using eStore_Admin.Application.Requests.KeyboardSwitches.Queries.GetById;
using eStore_Admin.Application.Utility;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace eStore_Admin.WebApi.Controllers
{
    [Route("api/keyboardswitches")]
    [ApiController]
    public class KeyboardSwitchSwitchesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public KeyboardSwitchSwitchesController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PagingParameters pagingParameters, CancellationToken cancellationToken)
        {
            var request = new GetAllKeyboardSwitchesPagedQuery() { PagingParameters = pagingParameters };
            var response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }
        
        [HttpGet]
        [Route("{id}", Name = "GetKeyboardSwitchById")]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
        {
            var request = new GetKeyboardSwitchByIdQuery(id);
            var response = await _mediator.Send(request, cancellationToken);
            
            if (response is null)
                return NotFound();

            return Ok(response);
        }
        
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] KeyboardSwitchDto keyboardSwitch, CancellationToken cancellationToken)
        {
            var request = new AddKeyboardSwitchCommand() { KeyboardSwitch = keyboardSwitch };
            var response = await _mediator.Send(request, cancellationToken);
            return CreatedAtRoute("GetKeyboardSwitchById", new { response.Id }, response);
        }
        
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] KeyboardSwitchDto keyboardSwitch, CancellationToken cancellationToken)
        {
            var request = new EditKeyboardSwitchCommand(id) { KeyboardSwitch = keyboardSwitch };
            var response = await _mediator.Send(request, cancellationToken);
            return CreatedAtRoute("GetKeyboardSwitchById", new { response.Id }, response);
        }
        
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var request = new DeleteKeyboardSwitchCommand(id);
            var isSuccess = await _mediator.Send(request, cancellationToken);
            if (!isSuccess)
                return NotFound();

            return Ok();
        }
    }
}