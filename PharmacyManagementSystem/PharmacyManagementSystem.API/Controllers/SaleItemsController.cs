using MediatR;
using Microsoft.AspNetCore.Mvc;
using PharmacyManagementSystem.Application.DTOs.SalesItemsDTOs;
using PharmacyManagementSystem.Application.Features.SaleItem.Commands;
using PharmacyManagementSystem.Application.Features.SaleItem.Queries;
using PharmacyManagementSystem.Application.Features.SaleItems.Queries;

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
                if (createSaleItemDto == null)
                    return BadRequest("you should enter a value inside the all fields");

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var request = new CreateSaleItemCommand(createSaleItemDto);
                var result = await _mediator.Send(request);
                if (result == 0)
                    return BadRequest("Unable to perform this operation, please try again later");

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
                if (result == null)
                    return BadRequest("there is no saleitems avalible at this time, please try again later");

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
                if (id <= 0)
                    return BadRequest("Please enter a valid id value");

                var request = new GetSaleItemsDetailsQuery(id);
                var result = await _mediator.Send(request);
                if (result == null)
                    return BadRequest("there is no saleitems avalible for this id value, please try again later");

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
                if (id <= 0)
                    return BadRequest("Please enter a valid id value");

                var request = new DeleteSaleItemCommand(id);
                var result = await _mediator.Send(request);
                if (!result)
                    return BadRequest("the delete process of the requsted sale item not success");

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
                if (id <= 0)
                    return BadRequest("Please enter a valid id value");

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var request = new UpdateSaleItemCommand(id, updateSaleItemDto);
                var result = await _mediator.Send(request);
                if (result == null)
                    return BadRequest("the update process of the requsted sale item not success");

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
                throw new NotImplementedException();
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
