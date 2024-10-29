using System.Threading.Tasks;
using AiConnect.Models;
using AiConnect.Persistence;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace AiConnect.Tests.Integration
{
    public class OracleDbContextIntegrationTests
    {
        private readonly OracleDbContext _context;

        public OracleDbContextIntegrationTests()
        {
            // Configurar o DbContext com opções em memória para testes
            var options = new DbContextOptionsBuilder<OracleDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new OracleDbContext(options);
        }

        [Fact]
        public async Task AddCliente_ShouldPersistClienteInDatabase()
        {
            // Arrange
            var cliente = new Cliente { Id = 1, Nome = "Cliente de Teste", Telefone = "1234567890", Email = "teste@cliente.com", DataNascimento = DateTime.Now, Endereco = "Rua Teste", Empresa = "Empresa Teste", SegmentoMercado = "Segmento Teste", Interesses = "Interesse Teste" };

            // Act
            await _context.Clientes.AddAsync(cliente);
            await _context.SaveChangesAsync();

            // Assert
            var savedCliente = await _context.Clientes.FindAsync(1);
            Assert.NotNull(savedCliente);
            Assert.Equal("Cliente de Teste", savedCliente.Nome); // Corrigido para "Nome"
        }

        [Fact]
        public async Task GetCliente_ShouldReturnCliente()
        {
            // Arrange
            var cliente = new Cliente { Id = 2, Nome = "Cliente para Busca", Telefone = "9876543210", Email = "busca@cliente.com", DataNascimento = DateTime.Now, Endereco = "Avenida Teste", Empresa = "Empresa Busca", SegmentoMercado = "Segmento Busca", Interesses = "Interesse Busca" };
            await _context.Clientes.AddAsync(cliente);
            await _context.SaveChangesAsync();

            // Act
            var fetchedCliente = await _context.Clientes.FindAsync(2);

            // Assert
            Assert.NotNull(fetchedCliente);
            Assert.Equal("Cliente para Busca", fetchedCliente.Nome); // Corrigido para "Nome"
        }
    }
}
