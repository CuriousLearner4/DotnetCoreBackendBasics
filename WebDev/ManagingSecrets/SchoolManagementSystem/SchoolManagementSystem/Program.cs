using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SchoolManagementSystem.Data;
using SchoolManagementSystem.Repository;
using SchoolManagementSystem.Repository.Interface;

var builder = WebApplication.CreateBuilder(args);

//var keyVaultUri = builder.Configuration.GetSection("KeyVault:URL");
//var keyVaultUri = "https://smsapi-dev-kv.vault.azure.net/";
//var credential = new ChainedTokenCredential(
//    //new EnvironmentCredential(),
//    //new AzureCliCredential(),
//    //new VisualStudioCredential(),
//    //new AzurePowerShellCredential(),
//    //new InteractiveBrowserCredential()
//    //new AzureDeveloperCliCredential()
//);
var keyVaultUrl = new Uri(builder.Configuration.GetSection("KeyVaultUrl").Value!);
var azureCredentials = new DefaultAzureCredential();
//builder.Configuration.AddAzureKeyVault(keyVaultUrl, azureCredentials);
var client = new SecretClient(keyVaultUrl, azureCredentials);
var accessToken = azureCredentials.GetToken(new Azure.Core.TokenRequestContext(new[] { "https://database.windows.net/.default" }));
var connectionString = builder.Configuration.GetConnectionString("DefaultSQLConnectionString1");
//Console.WriteLine(client.GetSecret("AZURE_SQL_SQL_E6084_CONNECTIONSTRING").Value.Value.ToString());
// Add services to the container.

builder.Services.AddDbContext<ApplicationDbContext>
    (options => {
        options.UseSqlServer(client.GetSecret("azure-sql-sql-e6084-connectionstring-6619d").Value.Value.ToString());
        //var conn = Environment.GetEnvironmentVariable("SQL_PL_CS");
        //options.UseSqlServer(conn);
    });
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()|| app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
