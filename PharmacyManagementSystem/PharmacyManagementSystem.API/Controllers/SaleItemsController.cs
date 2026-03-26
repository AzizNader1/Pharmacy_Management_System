using MediatR;
using Microsoft.AspNetCore.Mvc;
using PharmacyManagementSystem.Application.DTOs.SalesItemsDTOs;
using PharmacyManagementSystem.Application.Features.SaleItem.Commands;
using PharmacyManagementSystem.Application.Features.SaleItem.Queries;
using PharmacyManagementSystem.Application.Features.SaleItems.Queries;
using PharmacyManagementSystem.Application.Features.SalesItems.Queries;

namespace PharmacyManagementSystem.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SaleItemsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SaleItemsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromForm] CreateSaleItemDto createSaleItemDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var request = new CreateSaleItemCommand(createSaleItemDto);
                var result = await _mediator.Send(request);

                return Ok(new
                {
                    CreatedSaleItemId = result,
                    Message = "Adding a new saleitem Process Complete Successfully!"
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
                var request = new GetAllSaleItemsQuery();
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
                var request = new GetSaleItemsDetailsQuery(id);
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
                var request = new DeleteSaleItemCommand(id);
                var result = await _mediator.Send(request);

                return Ok(new
                {
                    Message = "Delete the sale item done successfully!"
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
        public async Task<IActionResult> Update(int id, [FromForm] UpdateSaleItemDto updateSaleItemDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var request = new UpdateSaleItemCommand(id, updateSaleItemDto);
                var result = await _mediator.Send(request);

                return Ok(new
                {
                    NewData = result,
                    Message = "Update the sale item done successfully!"
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

        [HttpGet("{saleId}")]
        public async Task<IActionResult> GetAllSaleItemesBySaleId(int saleId)
        {
            try
            {
                var query = new GetAllSaleItemsBySaleIdQuery(saleId);
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
