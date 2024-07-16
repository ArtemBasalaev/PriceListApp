using Microsoft.EntityFrameworkCore;
using PriceList.DataAccess;
using PriceList.BusinessLogic.Handlers;
using Microsoft.Extensions.Logging;

namespace PriceList;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var services = builder.Services;
        var environment = builder.Environment;
        var configuration = builder.Configuration;

        // Add services to the container.
        services.AddControllersWithViews();

        services.AddSingleton<IDbInitializer, DbInitializer>();

        services.AddDbContext<PriceListDbContext>(options =>
        {
            options
                .UseSqlServer(configuration.GetConnectionString("PriceListsConnection"))
                .UseLazyLoadingProxies();
        }, ServiceLifetime.Transient, ServiceLifetime.Transient);

        services.AddSignalR();

        var businessLogicAssembly = typeof(IHandler).Assembly;

        var handlerClasses = businessLogicAssembly.GetTypes()
            .Where(t => t.GetInterfaces()
                .Contains(typeof(IHandler)));

        foreach (var type in handlerClasses)
        {
            services.AddTransient(type);
        }

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseDefaultFiles();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllers();
        app.MapFallbackToFile("index.html");

        //app.MapControllerRoute(
        //    name: "default",
        //    pattern: "{controller=Home}/{action=Index}/{id?}");

        // Init database
        using (var scope = app.Services.CreateScope())
        {
            var serviceProvider = scope.ServiceProvider;

            try
            {
                var dbInitializer = serviceProvider.GetRequiredService<IDbInitializer>();
                await dbInitializer.InitializeAsync();
            }
            catch (Exception ex)
            {
                var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error occurred while seeding the database.");

                throw;
            }
        }

        await app.RunAsync();
    }
}