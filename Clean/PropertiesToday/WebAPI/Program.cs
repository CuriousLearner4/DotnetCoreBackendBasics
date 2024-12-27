using System.Reflection;
using Application;
using Infrastructure;
using MediatR;
using WebAPI.Middlewares;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
var cacheSettings = builder.Services.GetCacheSettings(builder.Configuration);
//configure Redis
builder.Services.AddStackExchangeRedisCache(options => { options.Configuration = cacheSettings.DestinationUrl; });
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(opt=>opt.DisplayRequestDuration());
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseMiddleware<ErrorHandlingMiddleWare>();
app.MapControllers();
app.Run();