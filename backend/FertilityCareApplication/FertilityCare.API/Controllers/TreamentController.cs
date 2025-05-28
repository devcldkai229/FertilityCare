using FertilityCare.UseCase.DTOs;
using FertilityCare.UseCase.DTOs.TreatmentServices;
using FertilityCare.UseCase.Exceptions;
using FertilityCare.UseCase.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FertilityCare.API.Controllers
{
    [Route("api/v1/treament-services")]
    [ApiController]
    public class TreamentController : ControllerBase
    {

        private readonly ITreatmentService _treatmentService;

        private readonly ILogger _logger;

        public TreamentController(ITreatmentService treatmentService)
        {
            _treatmentService = treatmentService;   
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ResponseCache(Duration = 300)]
        [ProducesResponseType(typeof(ApiResponse<TreatmentServiceDTO>), 200)]
        [ProducesResponseType(typeof(ApiResponse<TreatmentServiceDTO>), 404)]
        [ProducesResponseType(typeof(ApiResponse<TreatmentServiceDTO>), 500)]
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
                var result = await _treatmentService.GetById(id);
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

    }
}
