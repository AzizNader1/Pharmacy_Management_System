using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PharmacyManagementSystem.Application.DTOs.MedicineDTOs;
using PharmacyManagementSystem.Application.Features.Medicine.Commands;
using PharmacyManagementSystem.Application.Features.Medicine.Queries;

namespace PharmacyManagementSystem.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MedicinesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MedicinesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Add([FromBody] CreateMedicineDto createMedicineDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var request = new CreateMedicineCommand(createMedicineDto);
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

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var request = new GetAllMedicinesQuery();
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
                var request = new GetMedicineDetailsQuery(id);
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
                var request = new DeleteMedicineCommand(id);
                var result = await _mediator.Send(request);

                return NoContent();
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
        public async Task<IActionResult> Update(int id, [FromBody] UpdateMedicineDto updateMedicineDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var request = new UpdateMedicineCommand(id, updateMedicineDto);
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

        [HttpGet("{medicineName}")]
        [Authorize]
        public async Task<IActionResult> GetMedicineByName(string medicineName)
        {
            try
            {
                var query = new GetMedicineByNameQuery(medicineName);
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

        [HttpGet("{categoryName}")]
        [Authorize]
        public async Task<IActionResult> GetAllMedicinesByCategory(string categoryName)
        {
            try
            {
                var query = new GetMedicinesByCategoryQuery(categoryName);
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
