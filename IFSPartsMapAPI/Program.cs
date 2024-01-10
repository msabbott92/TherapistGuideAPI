using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using IFSPartsMapAPI.Data; // Assuming your DbContext is in this namespace
using Microsoft.Extensions.Configuration;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(options =>
{
    options.ReturnHttpNotAcceptable = true;
}).AddXmlDataContractSerializerFormatters();

// Configure Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register your DbContext
builder.Services.AddDbContext<IFSPartsMapDbContext>(options =>
    options.UseInMemoryDatabase("IFSGuideDB"));

// Register FileExtensionContentTypeProvider as a singleton service
builder.Services.AddSingleton<FileExtensionContentTypeProvider>();

// Add CORS services
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:5195", "http://172.20.21.42:5195",
                           "https://localhost:7282", "https://172.20.21.42:7282")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

// Use CORS with the default policy
app.UseCors();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
