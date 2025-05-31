using FertilityCare.Shared.Exceptions;
using FertilityCare.UseCase.DTOs;
using FertilityCare.UseCase.DTOs.Appointments;
using FertilityCare.UseCase.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FertilityCare.API.Controllers
{
    [ApiController]
    [Route("api/v1/appointments")]
    public class AppointmentController : ControllerBase
    {

        private readonly IAppointmentService _appointmentService;

        public AppointmentController(IAppointmentService appointmentService)
        {
             _appointmentService = appointmentService;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<AppointmentDTO>>>> GetAppointmentByPatientId([FromQuery] string patientId)
        {
            try
            {
                var result = await _appointmentService.GetAppointmentByPatientIdAsync(Guid.Parse(patientId));

                return Ok(new ApiResponse<IEnumerable<AppointmentDTO>>
                {
                    StatusCode = 200,
                    Message = "Appointments retrieved successfully",
                    Data = result,
                    ResponsedAt = DateTime.Now
                });
            } 
            catch(NotFoundException e)
            {
                return NotFound(new ApiResponse<object>
                {
                    StatusCode = 404,
                    Message = e.Message,
                    Data = null,
                    ResponsedAt = DateTime.Now
                });
            }
            catch(Exception e)
            {
                return StatusCode(500, new ApiResponse<object>
                {
                    StatusCode = 500,
                    Message = "An error occurred while retrieving appointments",
                    Data = null,
                    ResponsedAt = DateTime.Now
                });
            }
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<ActionResult<ApiResponse<AppointmentDTO>>> GetAppointmentById([FromRoute] Guid id)
        {
            try
            {
                var result = await _appointmentService.GetAppointmentByIdAsync(id);

                return Ok(new ApiResponse<AppointmentDTO>
                {
                    StatusCode = 200,
                    Message = "Appointments retrieved successfully",
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
                return StatusCode(500, new ApiResponse<object>
                {
                    StatusCode = 500,
                    Message = "An error occurred while retrieving appointments",
                    Data = null,
                    ResponsedAt = DateTime.Now
                });
            }
        }


        [HttpPost]

        public async Task<ActionResult<ApiResponse<AppointmentDTO>>> PlaceAppointment([FromBody] PlaceAppointmentRequestDTO request)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse<object>
                {
                    StatusCode = 400,
                    Message = "Invalid request data",
                    Data = null,
                    ResponsedAt = DateTime.Now
                });
            }

            try
            {
                var result =  await _appointmentService.PlaceAppointmentLockAsync(request);

                return CreatedAtAction(nameof(GetAppointmentById), new {id = result.Id}, new ApiResponse<AppointmentDTO>
                {
                    StatusCode = 201,
                    Message = "Appointment created successfully",
                    Data = result,
                    ResponsedAt = DateTime.Now
                });
            } 
            catch(NotFoundException e)
            {
                return NotFound(new ApiResponse<object>
                {
                    StatusCode = 404,
                    Message = e.Message,
                    Data = null,
                    ResponsedAt = DateTime.Now
                });
            }
            catch(Exception e)
            {
                return StatusCode(500, new ApiResponse<object>
                {
                    StatusCode = 500,
                    Message = "An error occurred while creating the appointment",
                    Data = null,
                    ResponsedAt = DateTime.Now
                });
            }
        }

    }
}
