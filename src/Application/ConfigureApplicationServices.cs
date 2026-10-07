using Application.Dishes.Services.Abstract;
using Application.Dishes.Services.Implementation;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class ConfigureApplicationServices
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IDishService, DishService>();
    }
}