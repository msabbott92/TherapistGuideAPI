using Microsoft.AspNetCore.StaticFiles;
using IFSPartsMapAPI; // Make sure to include the namespace where IFSPartsMap is located

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(options =>
{
    options.ReturnHttpNotAcceptable = true;
}).AddXmlDataContractSerializerFormatters();

// Configure Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register IFSPartsMap as a singleton service
builder.Services.AddSingleton<IFSPartsMap>();

// Register FileExtensionContentTypeProvider as a singleton service
builder.Services.AddSingleton<FileExtensionContentTypeProvider>();

// Add CORS services
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("http://localhost:5195", "http://172.20.21.42:5195","https://localhost:7282", "https://172.20.21.42:7282"
                             ) // Use localhost for emulator/simulator
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
