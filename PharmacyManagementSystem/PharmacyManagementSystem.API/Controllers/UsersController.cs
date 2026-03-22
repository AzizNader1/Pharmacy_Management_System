using MediatR;
using Microsoft.AspNetCore.Mvc;
using PharmacyManagementSystem.Application.DTOs.UserDTOs;
using PharmacyManagementSystem.Application.Features.User.Commands;

namespace PharmacyManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Register(CreateUserDto createUserDto)
        {
            var request = new CreateUserCommand(createUserDto);
            var result = _mediator.Send(request);
            return Ok(result);
        }
    }
}
