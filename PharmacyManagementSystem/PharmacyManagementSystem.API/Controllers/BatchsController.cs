using MediatR;
using Microsoft.AspNetCore.Mvc;
using PharmacyManagementSystem.Application.DTOs.BatchDTOs;
using PharmacyManagementSystem.Application.Features.Batch.Commands;
using PharmacyManagementSystem.Application.Features.Batch.Queries;

namespace PharmacyManagementSystem.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BatchsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BatchsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Add(CreateBatchDto createBatchDto)
        {
            try
            {
                if (createBatchDto == null)
                    return BadRequest("you should enter a value inside the all fields");

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var request = new CreateBatchCommand(createBatchDto);
                var result = await _mediator.Send(request);
                if (result == 0)
                    return BadRequest("Unable to perform this operation, please try again later");

                return Ok(new
                {
                    CreatedBatchId = result,
                    Message = "Adding a new batch Process Complete Successfully!"
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
                var request = new GetAllBatchsQuery();
                var result = await _mediator.Send(request);
                if (result == null)
                    return BadRequest("there is no batchs avalible at this time, please try again later");

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

                var request = new GetBatchDetailsQuery(id);
                var result = await _mediator.Send(request);
                if (result == null)
                    return BadRequest("there is no batchs avalible for this id value, please try again later");

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

                var request = new DeleteBatchCommand(id);
                var result = await _mediator.Send(request);
                if (!result)
                    return BadRequest("the delete process of the requsted batch not success");

                return Ok(new
                {
                    Message = "Delete the batch done successfully!"
                });
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(int id, UpdateBatchDto updateBatchDto)
        {
            try
            {
                if (id <= 0)
                    return BadRequest("Please enter a valid id value");

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var request = new UpdateBatchCommand(id, updateBatchDto);
                var result = await _mediator.Send(request);
                if (result == null)
                    return BadRequest("the update process of the requsted batch not success");

                return Ok(new
                {
                    NewData = result,
                    Message = "Update the batch done successfully!"
                });
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
