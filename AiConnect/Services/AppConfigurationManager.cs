using Microsoft.Extensions.Configuration;

namespace AiConnect.Services
{
    public class AppConfigurationManager
    {
        // Propriedades que armazenam as configurações
        public string ConnectionString { get; private set; }
        public int MaxUploadFileSize { get; private set; }
        public static object Instance { get; internal set; }

        // Construtor que aceita IConfiguration
        public AppConfigurationManager(IConfiguration configuration)
        {
            // Inicializa as configurações a partir do arquivo de configuração
            ConnectionString = configuration.GetConnectionString("OracleConnection");
            MaxUploadFileSize = int.Parse(configuration["MaxUploadFileSize"] ?? "10485760"); // Valor padrão de 10MB
        }
    }
}
