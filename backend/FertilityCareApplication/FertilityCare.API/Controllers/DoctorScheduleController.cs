using FertilityCare.Shared.Exceptions;
using FertilityCare.UseCase.DTOs;
using FertilityCare.UseCase.DTOs.DoctorSchedules;
using FertilityCare.UseCase.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FertilityCare.API.Controllers
{
    [ApiController]
    [Route("api/v1/doctor-schedules")]
    public class DoctorScheduleController : ControllerBase
    {
        private readonly IDoctorScheduleService _doctorScheduleService;

        public DoctorScheduleController(IDoctorScheduleService doctorScheduleService)
        {
            _doctorScheduleService = doctorScheduleService;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<DoctorScheduleDTO>>>> GetWorkDateByDoctorId([FromQuery] string doctorId)
        {
            try
            {
                var result = await _doctorScheduleService.GetWorkScheduleByDoctorIdAsync(Guid.Parse(doctorId));

                return Ok(new ApiResponse<IEnumerable<DoctorScheduleDTO>>
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
        [Route("{id}")]
        public async Task<ActionResult<ApiResponse<DoctorScheduleDTO>>> GetById([FromRoute] long id)
        {
            try
            {
                var result = await _doctorScheduleService.GetByIdAsync(id);

                return Ok(new ApiResponse<DoctorScheduleDTO>
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

        [HttpPost]
        public async Task<ActionResult<ApiResponse<DoctorScheduleDTO>>> Create([FromBody] CreateDoctorScheduleRequestDTO request)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse<object>
                {
                    StatusCode = 400,
                    Message = "Input is invalid",
                    Data = ModelState,
                    ResponsedAt = DateTime.Now
                });
            }

            try
            {
                var result = await _doctorScheduleService.AddWorkScheduleAsync(request);

                return CreatedAtAction(nameof(GetById), new { id = result.Id },
                    new ApiResponse<DoctorScheduleDTO>
                    {
                        StatusCode = 201,
                        Message = "Created successful!",
                        Data = result,
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
