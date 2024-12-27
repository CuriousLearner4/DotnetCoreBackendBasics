using Microsoft.IdentityModel.Tokens;
using MagicVilla_VillaAPI.Data;
using Microsoft.EntityFrameworkCore;
using MagicVilla_VillaAPI;
using MagicVilla_VillaAPI.Repository.IRepository;
using MagicVilla_VillaAPI.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.OpenApi.Models;
using Azure.Identity;//it is used to create credentials for getting secrets
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureKeyVault;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;//used to get secrets

var builder = WebApplication.CreateBuilder(args);
#region ReferenceForCredentialLessDevelopment
//if (builder.Environment.IsProduction())
//{
//    var keyVaultURL = builder.Configuration.GetSection("KeyVault:KeyVaultURl");

//    #region UsingKeyVault
//    //var keyVaultClientId = builder.Configuration.GetSection("KeyVault:ClientId");
//    //var keyVaultClientSecret = builder.Configuration.GetSection("KeyVault:ClientSecret");
//    //var keyVaultDirectoryId = builder.Configuration.GetSection("KeyVault:DirectoryID");
//    //var credentials = new ClientSecretCredential(keyVaultDirectoryId.Value!.ToString(), keyVaultClientId.Value!.ToString(),keyVaultClientSecret.Value!.ToString());
//    //builder.Configuration.AddAzureKeyVault(keyVaultURL.Value!.ToString(), keyVaultClientId.Value!.ToString(), keyVaultClientSecret.Value!.ToString(), new DefaultKeyVaultSecretManager());
//    //var client = new SecretClient(new Uri(keyVaultURL.Value!.ToString()), credentials);
//    //var secret = client.GetSecret("DbConnection").Value.Value.ToString();
//    //using (StreamWriter outputFile = new StreamWriter(@"C:\Users\HarshaVardhanGopiset\Internship\Interns-BE\Harsha\WebDev\MagicVilla\MagicVilla_VillaAPI\secret.txt"))
//    //{
//    //    outputFile.WriteLine(secret);
//    //}
//    //Console.WriteLine(secret);
//    #endregion

//    #region UsingManagedIdentities
//    var keyVaultClient = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(new AzureServiceTokenProvider().KeyVaultTokenCallback));
//    builder.Configuration.AddAzureKeyVault(keyVaultURL.Value!.ToString(), new DefaultKeyVaultSecretManager());
//    var client = new SecretClient(new Uri(keyVaultURL.Value!.ToString()), new DefaultAzureCredential());
//    #endregion
//    builder.Services.AddDbContext<AppDbContext>(option =>
//    {
//        //string secret = 
//        option.UseSqlServer(client.GetSecret("SqlConnection").Value.Value.ToString());
//    });
//}

//if (builder.Environment.IsDevelopment())
//{
//    builder.Services.AddDbContext<AppDbContext>(option =>
//    {
//        option.UseSqlServer(builder.Configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING"));
//    });
//}
#endregion
#region Macros
//#if DEBUG
//builder.Services.AddDbContext<AppDbContext>(option =>
//{
//    //string secret = 
//    option.UseSqlServer(builder.Configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING"));
//});
//#else

//var keyVaultURL = builder.Configuration.GetSection("KeyVault:KeyVaultURl");
////var keyVaultClient = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(new AzureServiceTokenProvider().KeyVaultTokenCallback));
////builder.Configuration.AddAzureKeyVault(keyVaultURL.Value!.ToString(), new DefaultKeyVaultSecretManager());
//var client = new SecretClient(new Uri(keyVaultURL.Value!.ToString()), new DefaultAzureCredential());
//var connectionString = client.GetSecret("SqlConnection").Value.Value.ToString();
////Console.WriteLine(connectionString);
//builder.Services.AddDbContext<AppDbContext>(option =>
//{
//    //string secret = 
//    option.UseSqlServer(client.GetSecret("SqlConnection").Value.Value.ToString());
//});

//#endif
#endregion
var keyVaultURL = builder.Configuration.GetSection("KeyVault:KeyVaultURl");
var client = new SecretClient(new Uri(keyVaultURL.Value!.ToString()), new DefaultAzureCredential());
var connectionString = client.GetSecret("SqlConnection").Value.Value.ToString();
builder.Services.AddDbContext<AppDbContext>(option =>
{
    //string secret = 
    option.UseSqlServer(client.GetSecret("SqlConnection").Value.Value.ToString());
});

builder.Services.AddAutoMapper(typeof(MappingConfig));
//Log.Logger = new LoggerConfiguration().MinimumLevel.Debug().WriteTo.File("log/villalogs.txt",rollingInterval:  RollingInterval.Day).CreateLogger();
//builder.Host.UseSerilog();
builder.Services.AddControllers().AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description =
        "JWT Autherization header using the Bearer Scheme. \r\n\r\n" +
        "Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\n" +
        "Example: \"Bearer 12345abcdef\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Scheme = "Bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
             new OpenApiSecurityScheme
             {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
             Scheme = "oauth2",
             Name = "Bearer",
             In = ParameterLocation.Header
             },
             new List<string>()
        }

    });
});
builder.Services.AddResponseCaching();
builder.Services.AddScoped<IVillaRepository, VillaRepository>();
builder.Services.AddScoped<IVillaNumberRepository, VillaNumberRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
//builder.Services.AddSingleton<ILogging, Logging>();
var key = builder.Configuration.GetValue<string>("ApiSettings:Secret");
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
        ValidateIssuer = false,
        ValidateAudience = false

    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();

}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
