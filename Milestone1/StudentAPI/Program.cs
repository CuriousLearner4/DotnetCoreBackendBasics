using Microsoft.EntityFrameworkCore;
using StudentAPI.Data;
using StudentAPI.Data.Repository;
using StudentAPI.Handlers;
using StudentAPI.Handlers.Interface;
using StudentAPI.Handlers.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var httpClientName = builder.Configuration.GetSection("HttpClientConfiguration:StudentHttpClientName").Value!;
var apiUri = builder.Configuration.GetSection("HttpClientConfiguration:ApiBaseAddress").Value!;
var connectionString = builder.Configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(connectionString);
});
#region NamedClients
builder.Services.AddHttpClient(httpClientName, client =>
{
    client.BaseAddress = new Uri(apiUri);
});
#endregion
#region TypedClients
builder.Services.AddHttpClient<ApiHandler>(client =>
{
    client.BaseAddress = new Uri(apiUri);
});
#endregion
builder.Services.AddMemoryCache();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IHandlerService, HandlerService>();
builder.Services.AddTransient<DbHandler>();
builder.Services.AddTransient<CacheHandler>();
builder.Services.AddTransient<ApiHandler>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
