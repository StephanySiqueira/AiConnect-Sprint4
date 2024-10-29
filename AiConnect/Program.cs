using AiConnect.Models;
using AiConnect.Persistence;
using AiConnect.Repositories;
using AiConnect.Services;
using AiConnect.Training;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Configure o DbContext com a string de conex�o correta
builder.Services.AddDbContext<OracleDbContext>(options =>
    options.UseOracle(builder.Configuration.GetConnectionString("OracleConnection")));

// Configure a inje��o de depend�ncia para os reposit�rios e services
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<ILeadRepository, LeadRepository>();
builder.Services.AddScoped<ILeadService, LeadService>();
builder.Services.AddScoped<IInteracaoRepository, InteracaoRepository>();
builder.Services.AddScoped<IInteracaoService, InteracaoService>();
builder.Services.AddScoped<IFirebaseAuthService, FirebaseAuthService>();

// Adicione o servi�o de an�lise de sentimentos
builder.Services.AddScoped<SentimentAnalysisService>();

// Configurar o AppConfigurationManager para inje��o de depend�ncia
builder.Services.AddSingleton<AppConfigurationManager>();

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "AiConnect API",
        Version = "v1",
        Description = "API para gerenciar clientes, leads e intera��es.",
        Contact = new OpenApiContact
        {
            Name = "Stephany",
            Email = "rm98258@fiap.com.br"
        }
    });
});

var app = builder.Build();

// Treinar e salvar o modelo antes de iniciar a aplica��o
var trainer = new SentimentAnalysisTrainer();
trainer.TrainAndSaveModel(@"C:\Users\tesiq\Downloads\AiConnect-Sprint4\AiConnect-Sprint4\AiConnect\TrainedModels\sentimentModel.zip");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "AiConnect API v1");
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();


