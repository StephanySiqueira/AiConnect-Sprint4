using AiConnect.DTOs;

namespace AiConnect.Services
{
    public interface ILeadService
    {
        Task<List<LeadDTO>> GetAllLeadsAsync();
        Task<LeadDTO> GetLeadByIdAsync(int id);
        Task<LeadDTO> CreateLeadAsync(LeadDTO leadDto);
        Task UpdateLeadAsync(int id, LeadDTO leadDto);
        Task DeleteLeadAsync(int id);
    }

}
