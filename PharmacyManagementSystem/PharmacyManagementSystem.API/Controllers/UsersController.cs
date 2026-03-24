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
            try
            {
                if (createUserDto == null)
                    return BadRequest("you should enter a value inside the all fields");

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var request = new CreateUserCommand(createUserDto);
                var result = await _mediator.Send(request);
                if (result == 0)
                    return BadRequest("Unable to perform this operation, please try again later");

                return Ok(new
                {
                    CreatedUserId = result,
                    Message = "Register Process Complete Successfully!"
                });
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var request = new GetAllUsersQuery();
                var result = await _mediator.Send(request);
                if (result == null)
                    return BadRequest("there is no users avalible at this time, please try again later");

                return Ok(result);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest("Please enter a valid id value");

                var request = new GetUserByIdQuery(id);
                var result = await _mediator.Send(request);
                if (result == null)
                    return BadRequest("there is no users avalible for this id value, please try again later");

                return Ok(result);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest("Please enter a valid id value");

                var request = new DeleteUserCommand(id);
                var result = await _mediator.Send(request);
                if (!result)
                    return BadRequest("the delete process of the requsted user not success");

                return Ok(new
                {
                    Message = "Delete the user done successfully!"
                });
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(int id, UpdateUserDto updateUserDto)
        {
            try
            {
                if (id <= 0)
                    return BadRequest("Please enter a valid id value");

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var request = new UpdateUserCommand(id, updateUserDto);
                var result = await _mediator.Send(request);
                if (result == null)
                    return BadRequest("the update process of the requsted user not success");

                return Ok(new
                {
                    NewData = result,
                    Message = "Update the user done successfully!"
                });
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetByEmail(string email)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetByName(string name)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetByRole(string name)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}