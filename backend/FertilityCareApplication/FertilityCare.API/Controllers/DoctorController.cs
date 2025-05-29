using FertilityCare.Domain.Entities;
using FertilityCare.Shared.Exceptions;
using FertilityCare.UseCase.DTOs;
using FertilityCare.UseCase.DTOs.Doctors;
using FertilityCare.UseCase.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FertilityCare.API.Controllers
{
    [ApiController]
    [Route("api/v1/doctors")]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorService _doctorService;

        public DoctorController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<ApiResponse<DoctorDTO>>> GetById([FromRoute] string doctorId)
        {
            try
            {
                var result = await _doctorService.GetByIdAsync(Guid.Parse(doctorId));

                return Ok(new ApiResponse<DoctorDTO>
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
                    Message = "",
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
                    ResponsedAt= DateTime.Now
                });
            }
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<DoctorDTO>>>> GetAll()
        {
            try
            {
                var result = await _doctorService.GetAllAsync();

                return Ok(new ApiResponse<IEnumerable<DoctorDTO>>
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
                    Message = "",
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

        [HttpPost]
        public async Task<ActionResult<ApiResponse<DoctorDTO>>> Create([FromBody] CreateDoctorRequestDTO request)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse<object>
                {
                    StatusCode = 400,
                    Message = "Info is invalid!",
                    Data = null,
                    ResponsedAt = DateTime.Now
                });
            }

            try
            {
                var result = await _doctorService.CreateAsync(request);

                return CreatedAtAction(nameof(GetById), new { doctorId = result.Id },
                    new ApiResponse<DoctorDTO>
                    {
                        StatusCode = 200,
                        Message = "Created successful!",
                        Data = result,
                        ResponsedAt = DateTime.Now
                    });
            } 
            catch (Exception e)
            {
                return BadRequest(new ApiResponse<Object>
                {
                    StatusCode = 500,
                    Message = e.Message,
                    Data = null,
                    ResponsedAt= DateTime.Now
                });
            }
        }

    }
}
