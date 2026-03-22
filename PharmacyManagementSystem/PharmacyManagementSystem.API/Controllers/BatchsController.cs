using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace PharmacyManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BatchsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BatchsController(IMediator mediator)
        {
            _mediator = mediator;
        }
    }
}
