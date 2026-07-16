using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WinFormsApp1.Data;
using WinFormsApp1.Forms;
using WinFormsApp1.Services;

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

                    // ── Services ───────────────────────────────────
                    services.AddScoped<AuthService>();
                    services.AddScoped<AuditService>();
                    services.AddScoped<AuthorService>();
                    services.AddScoped<CategoryService>();
                    services.AddScoped<UserService>();
                    services.AddScoped<LibraryCardService>();
                    services.AddScoped<MemberService>();
                    services.AddScoped<BookService>();

                    // ── Forms ──────────────────────────────────────
                    services.AddTransient<LoginForm>();
                    services.AddTransient<MainForm>();
                    services.AddTransient<ChangePasswordForm>();
                    services.AddTransient<AuthorForm>();
                    services.AddTransient<CategoryForm>();
                    services.AddTransient<UserManageForm>();
                    services.AddTransient<LibraryCardForm>();
                    services.AddTransient<MemberListForm>();
                    services.AddTransient<MemberDetailForm>();
                    services.AddTransient<BookForm>();
                })
                .Build();

            // ── Auto-migration ────────────────────────────────────
            using (var scope = host.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                db.Database.EnsureCreated();
            }

            // ── Resolve LoginForm and run ─────────────────────────
            using (var scope = host.Services.CreateScope())
            {
                var loginForm = scope.ServiceProvider.GetRequiredService<LoginForm>();
                Application.Run(loginForm);
            }
        }
    }
}
