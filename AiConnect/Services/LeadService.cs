using AiConnect.DTOs;
using AiConnect.Models;
using AiConnect.Persistence;
using AiConnect.Services;
using Microsoft.EntityFrameworkCore;

public class LeadService : ILeadService
{
    private readonly OracleDbContext _contexto;

    public LeadService(OracleDbContext contexto)
    {
        _contexto = contexto;
    }

    public async Task<List<LeadDTO>> GetAllLeadsAsync()
    {
        return await _contexto.Leads
            .Include(l => l.Cliente)
            .Select(l => new LeadDTO
            {
                Id = l.Id,
                Nome = l.Nome,
                Telefone = l.Telefone,
                Email = l.Email,
                Cargo = l.Cargo,
                Empresa = l.Empresa,
                ClienteId = l.ClienteId
            })
            .ToListAsync();
    }

    public async Task<LeadDTO> GetLeadByIdAsync(int id)
    {
        var lead = await _contexto.Leads
            .Include(l => l.Cliente)
            .Where(l => l.Id == id)
            .Select(l => new LeadDTO
            {
                Id = l.Id,
                Nome = l.Nome,
                Telefone = l.Telefone,
                Email = l.Email,
                Cargo = l.Cargo,
                Empresa = l.Empresa,
                ClienteId = l.ClienteId
            })
            .FirstOrDefaultAsync();

        if (lead == null)
        {
            throw new KeyNotFoundException("Lead não encontrado.");
        }

        return lead;
    }

    public async Task<LeadDTO> CreateLeadAsync(LeadDTO leadDto)
    {
        var lead = new Lead
        {
            Nome = leadDto.Nome,
            Telefone = leadDto.Telefone,
            Email = leadDto.Email,
            Cargo = leadDto.Cargo,
            Empresa = leadDto.Empresa,
            ClienteId = leadDto.ClienteId
        };

        _contexto.Leads.Add(lead);
        await _contexto.SaveChangesAsync();

        leadDto.Id = lead.Id; // Atualiza o ID no DTO após a criação
        return leadDto;
    }

    public async Task UpdateLeadAsync(int id, LeadDTO leadDto)
    {
        if (id != leadDto.Id)
        {
            throw new ArgumentException("O ID do lead não corresponde ao ID no DTO.");
        }

        var lead = await _contexto.Leads.FindAsync(id);
        if (lead == null)
        {
            throw new KeyNotFoundException("Lead não encontrado.");
        }

        lead.Nome = leadDto.Nome;
        lead.Telefone = leadDto.Telefone;
        lead.Email = leadDto.Email;
        lead.Cargo = leadDto.Cargo;
        lead.Empresa = leadDto.Empresa;
        lead.ClienteId = leadDto.ClienteId;

        _contexto.Leads.Update(lead);
        await _contexto.SaveChangesAsync();
    }

    public async Task DeleteLeadAsync(int id)
    {
        var lead = await _contexto.Leads.FindAsync(id);
        if (lead == null)
        {
            throw new KeyNotFoundException("Lead não encontrado.");
        }

        _contexto.Leads.Remove(lead);
        await _contexto.SaveChangesAsync();
    }
}
