using AiConnect.DTOs;
using AiConnect.Models;
using AiConnect.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class ClienteController : ControllerBase
{
    private readonly IClienteService _clienteService;

    public ClienteController(IClienteService clienteService)
    {
        _clienteService = clienteService;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ClienteDTO>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
    public async Task<IActionResult> GetAllClientes()
    {
        return await ExecuteAsync(async () =>
        {
            var clientes = await _clienteService.GetAllClientesAsync();
            return Ok(clientes);
        });
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ClienteDTO))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
    public async Task<IActionResult> GetClienteById(int id)
    {
        return await ExecuteAsync(async () =>
        {
            var cliente = await _clienteService.GetClienteByIdAsync(id);
            if (cliente == null)
            {
                return NotFound(new ErrorResponse { Message = "Cliente não encontrado." });
            }
            return Ok(cliente);
        });
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ClienteDTO))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
    public async Task<IActionResult> CreateCliente(ClienteDTO clienteDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        return await ExecuteAsync(async () =>
        {
            await _clienteService.AddClienteAsync(clienteDTO);
            return CreatedAtAction(nameof(GetClienteById), new { id = clienteDTO.Id }, clienteDTO);
        });
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
    public async Task<IActionResult> UpdateCliente(int id, ClienteDTO clienteDTO)
    {
        if (id != clienteDTO.Id)
        {
            return BadRequest(new ErrorResponse { Message = "ID do cliente não corresponde." });
        }

        return await ExecuteAsync(async () =>
        {
            await _clienteService.UpdateClienteAsync(clienteDTO);
            return NoContent();
        });
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
    public async Task<IActionResult> DeleteCliente(int id)
    {
        return await ExecuteAsync(async () =>
        {
            await _clienteService.DeleteClienteAsync(id);
            return NoContent();
        });
    }

    // Método auxiliar para tratamento de exceções
    private async Task<IActionResult> ExecuteAsync(Func<Task<IActionResult>> action)
    {
        try
        {
            return await action();
        }
        catch (Exception)
        {
            // Log the exception here if necessary
            return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse { Message = "Erro interno do servidor." });
        }
    }
}
