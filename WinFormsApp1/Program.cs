using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WinFormsApp1.Data;
using WinFormsApp1.Forms.Auth;
using WinFormsApp1.Forms.Main;
using WinFormsApp1.Forms.Books;
using WinFormsApp1.Forms.Authors;
using WinFormsApp1.Forms.Categories;
using WinFormsApp1.Forms.Publishers;
using WinFormsApp1.Forms.Members;
using WinFormsApp1.Forms.Reservations;
using WinFormsApp1.Forms.User;
using WinFormsApp1.Forms.Inventory;
using WinFormsApp1.Forms.Report;
using WinFormsApp1.Forms.Borrow;
using WinFormsApp1.Forms.Return;
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
                    services.AddScoped<PublisherService>();
                    services.AddScoped<UserService>();
                    services.AddScoped<LibraryCardService>();
                    services.AddScoped<MemberService>();
                    services.AddScoped<BookService>();
                    services.AddScoped<BookCopyService>();
                    services.AddScoped<BorrowService>();
                    services.AddScoped<ReturnService>();
                    services.AddScoped<ReservationService>();
                    services.AddScoped<InventoryService>();
                    services.AddScoped<ReportService>();
                    services.AddHostedService<ReservationFulfillmentService>();

                    // ── Forms ──────────────────────────────────────
                    services.AddTransient<LoginForm>();
                    services.AddTransient<MainForm>();
                    services.AddTransient<ChangePasswordForm>();
                    services.AddTransient<AuthorForm>();
                    services.AddTransient<CategoryForm>();
                    services.AddTransient<PublisherForm>();
                    services.AddTransient<UserManageForm>();
                    services.AddTransient<LibraryCardForm>();
                    services.AddTransient<MemberListForm>();
                    services.AddTransient<MemberDetailForm>();
                    services.AddTransient<BookForm>();
                    services.AddTransient<BorrowForm>();
                    services.AddTransient<ReturnForm>();
                    services.AddTransient<ReservationForm>();
                    services.AddTransient<InventoryForm>();
                    services.AddTransient<ReportForm>();
                })
                .Build();

            host.Start();

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
