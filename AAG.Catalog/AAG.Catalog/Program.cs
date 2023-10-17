using AAG.Catalog.Infra.CrossCuttings.Configuration;
using AAG.Catalog.Ioc;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "AAG.Catalog",
        Version = "v1",
        Description = "API provedor de serviço AAG.Catalog",
    });

    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlFilePath = Path.Combine(AppContext.BaseDirectory, xmlFilename);

    if (File.Exists(xmlFilePath))
        options.IncludeXmlComments(xmlFilePath);
});

var _settingsSection = builder.Configuration.GetSection("Setting");
builder.Services.Configure<AppConfigurations>(_settingsSection);

ServiceIoC.SolveDependencyInjection(builder.Services, builder.Configuration);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint($"../swagger/v1/swagger.json", "Catalog");
});

app.UseGlobalExceptionHandlerMiddleware();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
