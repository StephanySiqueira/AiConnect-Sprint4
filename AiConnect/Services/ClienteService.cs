using AiConnect.DTOs;
using AiConnect.Models;
using AiConnect.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

public class ClienteService : IClienteService
{
    private readonly IClienteRepository _clienteRepository;

    public ClienteService(IClienteRepository clienteRepository)
    {
        _clienteRepository = clienteRepository;
    }

    public async Task<IEnumerable<ClienteDTO>> GetAllClientesAsync()
    {
        // Recupera todos os clientes e os retorna como DTOs
        return await _clienteRepository.GetAllClientesAsync();
    }

    public async Task<ClienteDTO> GetClienteByIdAsync(int id)
    {
        // Recupera um cliente específico pelo ID
        return await _clienteRepository.GetClienteByIdAsync(id);
    }

    public async Task AddClienteAsync(ClienteDTO clienteDTO)
    {
        // Adiciona um novo cliente diretamente usando o DTO
        await _clienteRepository.AddClienteAsync(clienteDTO);
    }

    public async Task UpdateClienteAsync(ClienteDTO clienteDTO)
    {
        // Atualiza um cliente existente usando o DTO
        await _clienteRepository.UpdateClienteAsync(clienteDTO);
    }

    public async Task DeleteClienteAsync(int id)
    {
        // Remove um cliente com base no ID
        await _clienteRepository.DeleteClienteAsync(id);
    }
}
