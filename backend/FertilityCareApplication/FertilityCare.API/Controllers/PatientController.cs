using FertilityCare.Shared.Exceptions;
using FertilityCare.UseCase.DTOs;
using FertilityCare.UseCase.DTOs.Patient;
using FertilityCare.UseCase.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FertilityCare.API.Controllers
{
    [ApiController]
    [Route("api/v1/patients")]
    public class PatientController : ControllerBase
    {

        private readonly IPatientService _patientService;

        public PatientController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<ApiResponse<PatientDTO>>> GetById([FromRoute] string id)
        {
            try
            {
                var result = await _patientService.GetByIdAsync(Guid.Parse(id));

                return Ok(new ApiResponse<PatientDTO>
                {
                    StatusCode = 200,
                    Message = "",
                    Data = result,
                    ResponsedAt = DateTime.Now
                });
            }
            catch (NotFoundException e)
            {
                return NotFound(new ApiResponse<object>
                {
                    StatusCode = 404,
                    Message = e.Message,
                    Data = null,
                    ResponsedAt = DateTime.Now
                });
            }
            catch (Exception e)
            {
                return BadRequest(new ApiResponse<object>
                {
                    StatusCode = 500,
                    Message = e.Message,
                    Data = null,
                    ResponsedAt = DateTime.Now
                });
            }
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<PatientDTO>>>> GetAll()
        {
            try
            {
                var result = await _patientService.GetAllAsync();

                return Ok(new ApiResponse<IEnumerable<PatientDTO>>
                {
                    StatusCode = 200,
                    Message = "",
                    Data = result,
                    ResponsedAt = DateTime.Now
                });
            }
            catch (NotFoundException e)
            {
                return NotFound(new ApiResponse<object>
                {
                    StatusCode = 404,
                    Message = e.Message,
                    Data = null,
                    ResponsedAt = DateTime.Now
                });
            }
            catch (Exception e)
            {
                return BadRequest(new ApiResponse<object>
                {
                    StatusCode = 500,
                    Message = e.Message,
                    Data = null,
                    ResponsedAt = DateTime.Now
                });
            }
        }

    }
}
