using System.Text.Json;
using Demo.Products.CQRS.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .AddCQRSDependencies(builder.Configuration)
    .AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
app
    .UseSwagger()
    .UseSwaggerUI()
    .UseStatusCodePages()
    .UseHttpsRedirection()
    .UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }