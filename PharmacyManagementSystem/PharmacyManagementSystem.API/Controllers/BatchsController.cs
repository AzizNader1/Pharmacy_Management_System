using MediatR;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize]
        public async Task<IActionResult> Add([FromForm] CreateBatchDto createBatchDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var request = new CreateBatchCommand(createBatchDto);
                var result = await _mediator.Send(request);

                return Ok(new
                {
                    CreatedBatchId = result,
                    Message = "Adding a new batch Process Complete Successfully!"
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
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var request = new GetAllBatchsQuery();
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
        [Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var request = new GetBatchDetailsQuery(id);
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
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var request = new DeleteBatchCommand(id);
                var result = await _mediator.Send(request);

                return Ok(new
                {
                    Message = "Delete the batch done successfully!"
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
        [Authorize]
        public async Task<IActionResult> Update(int id, [FromForm] UpdateBatchDto updateBatchDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var request = new UpdateBatchCommand(id, updateBatchDto);
                var result = await _mediator.Send(request);

                return Ok(new
                {
                    NewData = result,
                    Message = "Update the batch done successfully!"
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
    }
}
