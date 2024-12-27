using System.Security.Cryptography;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Client;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.Resource;
using static System.Formats.Asn1.AsnWriter;
using static System.Net.WebRequestMethods;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));
builder.Services.AddAuthorization();

// Add services to the container.

var app = builder.Build();

// Configure the HTTP request pipeline.

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

var scopeRequiredByApi = app.Configuration["AzureAd:Scopes"];

app.MapGet("/weatherforecast", (HttpContext httpContext) =>
{
    httpContext.VerifyUserHasAnyAcceptedScope(scopeRequiredByApi);
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.RequireAuthorization();

app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

//https://login.microsoftonline.com/{8f6bd982-92c3-4de0-985d-0e287c55e379}/oauth2/v2.0/authorize?
//client_id ={ 4d9136f8 - c87e - 430e-9873 - 3c37a8367fc1}
//&response_type = token
//& redirect_uri = http % 3A % 2F % 2Flocalhost % 3A5279 % 2Fsignin - oidc
//& scope = api % 3A % 2F % 2F 4d9136f8 - c87e - 430e-9873 - 3c37a8367fc1 % 2Faccess_as_user
//& response_mode = fragment
//& state = 12345
//& nonce = 678910
//& prompt = none