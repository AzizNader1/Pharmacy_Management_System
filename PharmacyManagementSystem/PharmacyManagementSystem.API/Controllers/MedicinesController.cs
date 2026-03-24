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
        public async Task<IActionResult> Add(CreateMedicineDto createMedicineDto)
        {
            try
            {
                if (createMedicineDto == null)
                    return BadRequest("you should enter a value inside the all fields");

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var request = new CreateMedicineCommand(createMedicineDto);
                var result = await _mediator.Send(request);
                if (result == 0)
                    return BadRequest("Unable to perform this operation, please try again later");

                return Ok(new
                {
                    CreatedMedicineId = result,
                    Message = "Adding a new medicine Process Complete Successfully!"
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
                var request = new GetAllMedicinesQuery();
                var result = await _mediator.Send(request);
                if (result == null)
                    return BadRequest("there is no medicines avalible at this time, please try again later");

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

                var request = new GetMedicineDetailsQuery(id);
                var result = await _mediator.Send(request);
                if (result == null)
                    return BadRequest("there is no medicines avalible for this id value, please try again later");

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

                var request = new DeleteMedicineCommand(id);
                var result = await _mediator.Send(request);
                if (!result)
                    return BadRequest("the delete process of the requsted medicine not success");

                return Ok(new
                {
                    Message = "Delete the medicine done successfully!"
                });
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(int id, UpdateMedicineDto updateMedicineDto)
        {
            try
            {
                if (id <= 0)
                    return BadRequest("Please enter a valid id value");

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var request = new UpdateMedicineCommand(id, updateMedicineDto);
                var result = await _mediator.Send(request);
                if (result == null)
                    return BadRequest("the update process of the requsted medicine not success");

                return Ok(new
                {
                    NewData = result,
                    Message = "Update the medicine done successfully!"
                });
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IActionResult> GetMedicineByName(string medicineName)
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
        public async Task<IActionResult> GetAllMedicinesByCategory(string categoryName)
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
