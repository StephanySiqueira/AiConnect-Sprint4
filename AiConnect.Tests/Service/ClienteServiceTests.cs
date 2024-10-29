using AiConnect.DTOs;
using AiConnect.Services;
using AiConnect.Repositories;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace AiConnect.Tests.Service
{
    public class ClienteServiceTests
    {
        private readonly Mock<IClienteRepository> _mockClienteRepository;
        private readonly ClienteService _clienteService;

        public ClienteServiceTests()
        {
            // Cria o mock do repositório
            _mockClienteRepository = new Mock<IClienteRepository>();

            // Cria uma instância do ClienteService com o repositório mockado
            _clienteService = new ClienteService(_mockClienteRepository.Object);
        }

        [Fact]
        public async Task GetAllClientesAsync_ReturnsListOfClientes()
        {
            // Arrange: Define o que o mock deve retornar
            var clientes = new List<ClienteDTO>
            {
                new ClienteDTO { Nome = "Cliente 1", Telefone = "123456789", Email = "cliente1@example.com" },
                new ClienteDTO { Nome = "Cliente 2", Telefone = "987654321", Email = "cliente2@example.com" }
            };

            _mockClienteRepository.Setup(repo => repo.GetAllClientesAsync()).ReturnsAsync(clientes);

            // Act: Chama o método a ser testado
            var result = await _clienteService.GetAllClientesAsync();

            // Assert: Verifica se o resultado é o esperado
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.Equal("Cliente 1", result.First().Nome);
            Assert.Equal("Cliente 2", result.Last().Nome);
        }
    }
}
