using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Get Key Vault URL from configuration
//var keyVaultUrl = builder.Configuration["AzureKeyVault:VaultUri"];
var keyVaultUrl = "https://myref.vault.azure.net/";
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
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
