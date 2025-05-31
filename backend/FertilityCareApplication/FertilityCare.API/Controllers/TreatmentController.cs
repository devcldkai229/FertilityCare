using FertilityCare.Shared.Exceptions;
using FertilityCare.UseCase.DTOs;
using FertilityCare.UseCase.DTOs.TreatmentServices;
using FertilityCare.UseCase.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FertilityCare.API.Controllers
{
    [Route("api/v1/treament-services")]
    [ApiController]
    public class TreatmentController : ControllerBase
    {

        private readonly ITreatmentService _treatmentService;

        private readonly ILogger<TreatmentController> _logger;

        public TreatmentController(ITreatmentService treatmentService, ILogger<TreatmentController> logger)
        {
            _treatmentService = treatmentService;
            _logger = logger;
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ResponseCache(Duration = 300)]
        [ProducesResponseType(typeof(ApiResponse<TreatmentServiceDTO>), 200)]
        [ProducesResponseType(typeof(ApiResponse<object>), 404)]
        [ProducesResponseType(typeof(ApiResponse<object>), 500)]
        public async Task<ActionResult<ApiResponse<TreatmentServiceDTO>>> GetById([FromRoute] Guid id)
        {
            if(id == Guid.Empty)
            {
                return BadRequest(new ApiResponse<TreatmentServiceDTO>
                {
                    StatusCode = 400,
                    Message = "Id is empty!",
                    Data = null,
                    ResponsedAt = DateTime.Now
                });
            }
      
            try
            {
                var result = await _treatmentService.GetByIdAsync(id);
                return Ok(new ApiResponse<TreatmentServiceDTO>
                {
                    StatusCode = 200,
                    Message = string.Empty,
                    Data = result,
                    ResponsedAt = DateTime.Now
                });
            } 
            catch (NotFoundException e)
            {
                return NotFound(new ApiResponse<TreatmentServiceDTO>
                {
                    StatusCode = 404,
                    Message = e.Message,
                    Data = null,
                    ResponsedAt = DateTime.Now
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving treatment service with ID: {Id}", id);

                return StatusCode(500, new ApiResponse<TreatmentServiceDTO>
                {
                    StatusCode = 500,
                    Message = "An internal server error occurred",
                    Data = null,
                    ResponsedAt = DateTime.UtcNow
                });
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<TreatmentServiceDTO>>), 200)]
        [ProducesResponseType(typeof(ApiResponse<object>), 500)]
        public async Task<ActionResult<ApiResponse<IEnumerable<TreatmentServiceDTO>>>> GetAll()
        {
            try
            {
                var result = await _treatmentService.GetAllAsync();

                return Ok(new ApiResponse<IEnumerable<TreatmentServiceDTO>>
                {
                    StatusCode = 200,
                    Message = string.Empty,
                    Data = result,
                    ResponsedAt = DateTime.Now
                });
            }
            catch (Exception e)
            {
                _logger.LogError($"{e.Message}", e);

                return StatusCode(500, new ApiResponse<object>
                {
                    StatusCode = 500,
                    Message = "An internal server error occurred",
                    Data = null,
                    ResponsedAt = DateTime.UtcNow
                });
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<TreatmentServiceDTO>), 200)]
        [ProducesResponseType(typeof(ApiResponse<object>), 404)]
        public async Task<ActionResult<ApiResponse<TreatmentServiceDTO>>> Create([FromBody] CreateTreatmentServiceRequestDTO request)
        {
            try
            {
                var result = await _treatmentService.CreateAsync(request);

                return CreatedAtAction(nameof(GetById), new { id = result.Id },
                    new ApiResponse<TreatmentServiceDTO>
                    {
                        StatusCode = 201,
                        Message = "Created Successful!",
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
                    ResponsedAt = DateTime.UtcNow
                });
            }
        }

    }
}
