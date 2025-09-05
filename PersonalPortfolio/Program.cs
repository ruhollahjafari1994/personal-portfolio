using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using PersonalPortfolio.Data;

namespace PersonalPortfolio
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddRazorPages();
            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                var env = builder.Environment;
                string? connectionString;

                if (env.IsDevelopment())
                    connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
                connectionString = Environment.GetEnvironmentVariable("PROD_CONNECTION_STRING");

                if (string.IsNullOrEmpty(connectionString))
                    throw new InvalidOperationException("Connection string not found.");

                options.UseNpgsql(connectionString);
            });


            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();

            // Razor Pages routing
            app.MapRazorPages();

            app.Run();
        }
    }                       

        public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
        {
            public AppDbContext CreateDbContext(string[] args)
            {                                                  
                var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";

                var configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())  
                    .AddJsonFile("appsettings.json", optional: false)
                    .AddJsonFile($"appsettings.{environment}.json", optional: true)
                    .AddEnvironmentVariables()
                    .Build();

                var connectionString = configuration.GetConnectionString("DefaultConnection");

                var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
                optionsBuilder.UseNpgsql(connectionString);

                return new AppDbContext(optionsBuilder.Options);
            }
        }
                                            
}
