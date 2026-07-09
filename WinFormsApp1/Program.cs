using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WinFormsApp1.Data;
using WinFormsApp1.Forms;

namespace WinFormsApp1
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            var host = Host.CreateDefaultBuilder()
                .ConfigureAppConfiguration((context, config) =>
                {
                    config.SetBasePath(AppDomain.CurrentDomain.BaseDirectory);
                    config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                })
                .ConfigureServices((context, services) =>
                {
                    // ── DbContext ──────────────────────────────────
                    services.AddDbContext<AppDbContext>(options =>
                        options.UseSqlite(context.Configuration.GetConnectionString("DefaultConnection")));

                    // ── Repositories (open generic) ────────────────
                    services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
                    services.AddScoped<IUnitOfWork, UnitOfWork>();
                })
                .Build();

            // ── Auto-migration ────────────────────────────────────
            using (var scope = host.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                db.Database.EnsureCreated();
            }

            // ── Resolve Form1 and run ─────────────────────────────
            using (var scope = host.Services.CreateScope())
            {
                var form = scope.ServiceProvider.GetRequiredService<Form1>();
                Application.Run(form);
            }
        }
    }
}
