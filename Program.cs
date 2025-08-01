using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using LearningProject1.Controllers;
using LearningProject1.Repository;
using LearningProject1.Repository.Implementation;
using LearningProject1.ServiceLayer;
using LearningProject1.ServiceLayer.Implementation;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Logging.AzureAppServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//////////////////////////////////////////////////Logs in azure /////////////////////////////////
builder.Logging.AddAzureWebAppDiagnostics();

builder.Services.Configure<AzureFileLoggerOptions>(options => {
    options.FileName = "tryingfilelog-";
    options.RetainedFileCountLimit = 5;
    options.FileSizeLimit = 50 * 1024;
    options.IsEnabled = true;
    });

builder.Services.Configure<AzureBlobLoggerOptions>(options => {
    options.BlobName = "tryingbloblog-";
    options.IsEnabled = true;
});
//////////////////////////////////////////////////////////////////////////////////////////////


// Get Key Vault URL from configuration
var keyVaultUrl = builder.Configuration["AzureKeyVault:VaultUri"];

// Add Azure Key Vault to the ConfigurationBuilder

if (!string.IsNullOrEmpty(keyVaultUrl))
{
    var client = new SecretClient(new Uri(keyVaultUrl), new DefaultAzureCredential());

    // Retrieve and load secrets into IConfiguration
    var secrets = client.GetPropertiesOfSecrets();
    foreach (var secret in secrets)
    {
        var secretValue = client.GetSecret(secret.Name);
        builder.Configuration[secret.Name] = secretValue.Value.Value;
    }
}

builder.Services.AddSingleton(s =>
{
    var cosmosEndpoint = builder.Configuration["CosmosDb:Endpoint"];
    var cosmosKey = builder.Configuration["PrimaryKey"];
    return new CosmosClient(cosmosEndpoint, cosmosKey);
});
builder.Services.AddSingleton<ILogger<TopicsController>, Logger<TopicsController>>();
builder.Services.AddScoped<IRepository,Repository>();
builder.Services.AddScoped<ITopicService, TopicService>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
