using MediatR;
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
        public async Task<IActionResult> Add([FromForm] CreateMedicineDto createMedicineDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var request = new CreateMedicineCommand(createMedicineDto);
                var result = await _mediator.Send(request);

                return Ok(new
                {
                    CreatedMedicineId = result,
                    Message = "Adding a new medicine Process Complete Successfully!"
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
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var request = new DeleteMedicineCommand(id);
                var result = await _mediator.Send(request);

                return Ok(new
                {
                    Message = "Delete the medicine done successfully!"
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
        public async Task<IActionResult> Update(int id, [FromForm] UpdateMedicineDto updateMedicineDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var request = new UpdateMedicineCommand(id, updateMedicineDto);
                var result = await _mediator.Send(request);

                return Ok(new
                {
                    NewData = result,
                    Message = "Update the medicine done successfully!"
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

        [HttpGet("{medicineName}")]
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
