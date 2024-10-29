using AiConnect.DTOs;
using AiConnect.Models;
using AiConnect.Services; 
using Microsoft.AspNetCore.Mvc;

namespace AiConnect.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LeadController : ControllerBase
    {
        private readonly ILeadService _leadService;

        public LeadController(ILeadService leadService)
        {
            _leadService = leadService;
        }

        // GET: api/leads
        [HttpGet]
        public async Task<IActionResult> GetLeads()
        {
            try
            {
                var leads = await _leadService.GetAllLeadsAsync();
                return Ok(leads);
            }
            catch (Exception ex)
            {
                // Retorna um erro 500 com a mensagem personalizada
                return StatusCode(StatusCodes.Status500InternalServerError,
                                  new ErrorResponse { Message = ex.Message });
            }
        }

        // GET: api/leads/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetLead(int id)
        {
            try
            {
                var lead = await _leadService.GetLeadByIdAsync(id);
                return Ok(lead);
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new ErrorResponse { Message = "Lead não encontrado." });
            }
            catch (Exception ex)
            {
                // Retorna um erro 500 com a mensagem personalizada
                return StatusCode(StatusCodes.Status500InternalServerError,
                                  new ErrorResponse { Message = ex.Message });
            }
        }

        // POST: api/leads
        [HttpPost]
        public async Task<IActionResult> CreateLead([FromBody] LeadDTO leadDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var createdLead = await _leadService.CreateLeadAsync(leadDto);
                return CreatedAtAction(nameof(GetLead), new { id = createdLead.Id }, createdLead);
            }
            catch (Exception ex)
            {
                // Retorna um erro 500 com a mensagem personalizada
                return StatusCode(StatusCodes.Status500InternalServerError,
                                  new ErrorResponse { Message = ex.Message });
            }
        }

        // PUT: api/leads/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLead(int id, [FromBody] LeadDTO leadDto)
        {
            if (id != leadDto.Id || !ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                await _leadService.UpdateLeadAsync(id, leadDto);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new ErrorResponse { Message = "Lead não encontrado." });
            }
            catch (Exception ex)
            {
                // Retorna um erro 500 com a mensagem personalizada
                return StatusCode(StatusCodes.Status500InternalServerError,
                                  new ErrorResponse { Message = ex.Message });
            }
        }

        // DELETE: api/leads/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLead(int id)
        {
            try
            {
                await _leadService.DeleteLeadAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new ErrorResponse { Message = "Lead não encontrado." });
            }
            catch (Exception ex)
            {
                // Retorna um erro 500 com a mensagem personalizada
                return StatusCode(StatusCodes.Status500InternalServerError,
                                  new ErrorResponse { Message = ex.Message });
            }
        }
    }
}
