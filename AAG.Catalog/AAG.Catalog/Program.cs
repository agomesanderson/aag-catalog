using AAG.Catalog.Infra.CrossCuttings.Configuration;
using AAG.Catalog.Ioc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var _settingsSection = builder.Configuration.GetSection("Setting");
builder.Services.Configure<AppConfigurations>(_settingsSection);

ServiceIoC.SolveDependencyInjection(builder.Services, builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint($"../swagger/v1/swagger.json", "Catalog");
    });
}

app.UseGlobalExceptionHandlerMiddleware();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
