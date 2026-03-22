using MediatR;
using Microsoft.AspNetCore.Mvc;
using PharmacyManagementSystem.Application.DTOs.UserDTOs;
using PharmacyManagementSystem.Application.Features.User.Commands;
using PharmacyManagementSystem.Application.Features.User.Queries;

namespace PharmacyManagementSystem.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateUserDto createUserDto)
        {
            if (createUserDto == null)
                return BadRequest("you should enter a value inside the all fields");

            var request = new CreateUserCommand(createUserDto);
            var result = await _mediator.Send(request);
            if (result == null)
                return BadRequest("Unable to perform this operation, please try again later");

            return Ok(new
            {
                CreatedUserId = result,
                Message = "Register Process Complete Successfully!"
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var request = new GetAllUsersQuery();
            var result = await _mediator.Send(request);
            if (result == null)
                return BadRequest("there is no users avalible at this time, please try again later");

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update(int id, UpdateUserDto updateUserDto)
        {
            return Ok();
        }
    }
}
