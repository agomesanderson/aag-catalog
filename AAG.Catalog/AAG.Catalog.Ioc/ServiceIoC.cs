using AAG.Catalog.Domain.Handlers;
using AAG.Catalog.Domain.Repositories;
using AAG.Catalog.Infra.Data.Repositories;
using AAG.Catalog.Ioc.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AAG.Catalog.Ioc;

public static class ServiceIoC
{
    public static void SolveDependencyInjection(this IServiceCollection services, IConfiguration configuration)
    {
        #region Helpers

        #endregion

        #region Handlers
        services.AddTransient<CategoryHandler>();
        services.AddTransient<ProductHandler>();
        #endregion

        #region Serviços
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        #endregion

        #region Repositórios
        services.AddTransient<ICategoryRepository, CategoryRepository>();
        services.AddTransient<IProductRepository, ProductRepository>();
        #endregion

        #region Global Exception
        services.AddTransient<ConfigureGlobalException>();
        #endregion

    }

    public static IApplicationBuilder UseGlobalExceptionHandlerMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<ConfigureGlobalException>();
        return app;
    }
}
