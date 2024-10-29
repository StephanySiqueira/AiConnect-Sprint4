using AiConnect.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AiConnect.Services
{
    public interface IInteracaoService
    {
        Task<IEnumerable<InteracoesDTO>> GetAllInteracoesAsync();
        Task<InteracoesDTO> GetInteracaoByIdAsync(int id);
        Task AddInteracaoAsync(InteracoesDTO interacaoDto);
        Task UpdateInteracaoAsync(InteracoesDTO interacaoDto);
        Task DeleteInteracaoAsync(int id);
        Task<IEnumerable<LeadDTO>> GetLeadsByClientIdAsync(int clientId);
    }
}
