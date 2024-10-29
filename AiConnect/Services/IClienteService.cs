using AiConnect.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IClienteService
{
    Task<IEnumerable<ClienteDTO>> GetAllClientesAsync();
    Task<ClienteDTO> GetClienteByIdAsync(int id);
    Task AddClienteAsync(ClienteDTO clienteDTO);
    Task UpdateClienteAsync(ClienteDTO clienteDTO);
    Task DeleteClienteAsync(int id);
}
