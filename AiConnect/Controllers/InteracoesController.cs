using AiConnect.DTOs;
using AiConnect.Models;
using AiConnect.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AiConnect.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InteracoesController : ControllerBase
    {
        private readonly IInteracaoService _interacaoService;

        public InteracoesController(IInteracaoService interacaoService)
        {
            _interacaoService = interacaoService;
        }

        // GET: api/Interacoes
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<InteracoesDTO>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> GetInteracoes()
        {
            return await ExecuteAsync(async () =>
            {
                var interacoes = await _interacaoService.GetAllInteracoesAsync();
                return Ok(interacoes);
            });
        }

        // GET: api/Interacoes/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(InteracoesDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> GetInteracao(int id)
        {
            return await ExecuteAsync(async () =>
            {
                var interacao = await _interacaoService.GetInteracaoByIdAsync(id);
                if (interacao == null)
                {
                    return NotFound(new ErrorResponse { Message = "Interação não encontrada." });
                }
                return Ok(interacao);
            });
        }

        // POST: api/Interacoes
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(InteracoesDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> PostInteracao([FromBody] InteracoesDTO interacaoDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ErrorResponse { Message = "Dados inválidos fornecidos." });
            }

            return await ExecuteAsync(async () =>
            {
                await _interacaoService.AddInteracaoAsync(interacaoDto);
                return CreatedAtAction(nameof(GetInteracao), new { id = interacaoDto.Id }, interacaoDto);
            });
        }

        // PUT: api/Interacoes/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> PutInteracao(int id, [FromBody] InteracoesDTO interacaoDto)
        {
            if (id != interacaoDto.Id || !ModelState.IsValid)
            {
                return BadRequest(new ErrorResponse { Message = "ID da interação não corresponde ou dados inválidos fornecidos." });
            }

            return await ExecuteAsync(async () =>
            {
                await _interacaoService.UpdateInteracaoAsync(interacaoDto);
                return NoContent();
            });
        }

        // DELETE: api/Interacoes/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> DeleteInteracao(int id)
        {
            return await ExecuteAsync(async () =>
            {
                await _interacaoService.DeleteInteracaoAsync(id);
                return NoContent();
            });
        }

        // GET: api/Interacoes/leads/{clientId}
        [HttpGet("leads/{clientId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<LeadDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> GetLeadsByClientId(int clientId)
        {
            return await ExecuteAsync(async () =>
            {
                var leads = await _interacaoService.GetLeadsByClientIdAsync(clientId);
                if (leads == null)
                {
                    return NotFound(new ErrorResponse { Message = "Leads não encontrados para o cliente especificado." });
                }
                return Ok(leads);
            });
        }

        // Método auxiliar para tratamento de exceções
        private async Task<IActionResult> ExecuteAsync(Func<Task<IActionResult>> action)
        {
            try
            {
                return await action();
            }
            catch (Exception ex)
            {
                // Log the exception here if necessary
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse { Message = "Erro interno do servidor." });
            }
        }
    }
}
