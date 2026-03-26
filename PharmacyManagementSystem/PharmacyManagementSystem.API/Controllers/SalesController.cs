using MediatR;
using Microsoft.AspNetCore.Mvc;
using PharmacyManagementSystem.Application.DTOs.SalesDTOs;
using PharmacyManagementSystem.Application.Features.Sale.Commands;
using PharmacyManagementSystem.Application.Features.Sale.Queries;
using PharmacyManagementSystem.Application.Features.Sales.Queries;

namespace PharmacyManagementSystem.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SalesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromForm] CreateSaleDto createSaleDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var request = new CreateSaleCommand(createSaleDto);
                var result = await _mediator.Send(request);

                return Ok(new
                {
                    CreatedSaleId = result,
                    Message = "Adding a new sale record Process Complete Successfully!"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var request = new GetAllSalesQuery();
                var result = await _mediator.Send(request);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var request = new GetSaleDetailsQuery(id);
                var result = await _mediator.Send(request);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var request = new DeleteSaleCommand(id);
                var result = await _mediator.Send(request);

                return Ok(new
                {
                    Message = "Delete the sale record done successfully!"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(int id, [FromForm] UpdateSaleDto updateSaleDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var request = new UpdateSaleCommand(id, updateSaleDto);
                var result = await _mediator.Send(request);

                return Ok(new
                {
                    NewData = result,
                    Message = "Update the sale record done successfully!"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetAllSalesByUserId(int userId)
        {
            try
            {
                var query = new GetAllSalesByUserIdQuery(userId);
                var result = await _mediator.Send(query);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }

        [HttpGet("{userName}")]
        public async Task<IActionResult> GetAllSalesByUserName(string userName)
        {
            try
            {
                var query = new GetAllSalesByUserNameQuery(userName);
                var result = await _mediator.Send(query);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }
    }
}
