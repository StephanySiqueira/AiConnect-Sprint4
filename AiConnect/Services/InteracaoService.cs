using AiConnect.DTOs;
using AiConnect.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AiConnect.Services
{
    public class InteracaoService : IInteracaoService
    {
        private readonly IInteracaoRepository _interacaoRepository;

        public InteracaoService(IInteracaoRepository interacaoRepository)
        {
            _interacaoRepository = interacaoRepository;
        }

        public async Task<IEnumerable<InteracoesDTO>> GetAllInteracoesAsync()
        {
            return await _interacaoRepository.GetAllInteracoesAsync();
        }

        public async Task<InteracoesDTO> GetInteracaoByIdAsync(int id)
        {
            return await _interacaoRepository.GetInteracaoByIdAsync(id);
        }

        public async Task AddInteracaoAsync(InteracoesDTO interacaoDto)
        {
            await _interacaoRepository.AddInteracaoAsync(interacaoDto);
        }

        public async Task UpdateInteracaoAsync(InteracoesDTO interacaoDto)
        {
            await _interacaoRepository.UpdateInteracaoAsync(interacaoDto);
        }

        public async Task DeleteInteracaoAsync(int id)
        {
            await _interacaoRepository.DeleteInteracaoAsync(id);
        }

        public async Task<IEnumerable<LeadDTO>> GetLeadsByClientIdAsync(int clientId)
        {
            return await _interacaoRepository.GetLeadsByClientIdAsync(clientId);
        }
    }
}
