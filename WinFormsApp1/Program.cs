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

                    // ── Services ───────────────────────────────────
                    services.AddScoped<AuthService>();
                    services.AddScoped<AuditService>();
                    services.AddScoped<AuthorService>();
                    services.AddScoped<CategoryService>();
                    services.AddScoped<PublisherService>();
                    services.AddScoped<UserService>();
                    services.AddScoped<LibraryCardService>();
                    services.AddScoped<MemberService>();

                    services.AddScoped<LateFeeService>();
                    services.AddScoped<FeePaymentService>();

                    services.AddScoped<BookService>();
                    services.AddScoped<BookCopyService>();
                    services.AddScoped<BorrowService>();
                    services.AddScoped<RenewalService>();
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
                    services.AddTransient<FeeForm>();
                    services.AddTransient<MemberListForm>();
                    services.AddTransient<MemberDetailForm>();
                    services.AddTransient<BookForm>();
                    services.AddTransient<BorrowForm>();
                    services.AddTransient<RenewForm>();
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
                InitializeDatabase(db);
            }

            // ── Resolve LoginForm and run ─────────────────────────
            using (var scope = host.Services.CreateScope())
            {
                var loginForm = scope.ServiceProvider.GetRequiredService<LoginForm>();
                Application.Run(loginForm);
            }
        }

        private static void InitializeDatabase(AppDbContext db)
        {
            var connection = db.Database.GetDbConnection();
            if (connection.State != System.Data.ConnectionState.Open)
                connection.Open();

            try
            {
                var hasHistoryTable = TableExists(connection, "__EFMigrationsHistory");
                var hasAnyTables = HasAnyUserTables(connection);

                if (hasHistoryTable || !hasAnyTables)
                {
                    db.Database.Migrate();
                }
                else
                {
                    // Legacy database created with EnsureCreated(): keep it usable and avoid forcing
                    // an initial migration replay against an already-populated schema.
                    EnsureMemberColumn(db, "Department", "TEXT NULL");
                    EnsureMemberColumn(db, "StudentId", "TEXT NULL");
                }

                // Keep the legacy schema compatible with the current model.
                EnsureMemberColumn(db, "Department", "TEXT NULL");
                EnsureMemberColumn(db, "StudentId", "TEXT NULL");
            }
            finally
            {
                if (connection.State == System.Data.ConnectionState.Open)
                    connection.Close();
            }
        }

        private static bool HasAnyUserTables(System.Data.Common.DbConnection connection)
        {
            using var command = connection.CreateCommand();
            command.CommandText = "SELECT 1 FROM sqlite_master WHERE type = 'table' AND name NOT LIKE 'sqlite_%' AND name <> '__EFMigrationsHistory' LIMIT 1;";

            using var reader = command.ExecuteReader();
            return reader.Read();
        }

        private static bool TableExists(System.Data.Common.DbConnection connection, string tableName)
        {
            using var command = connection.CreateCommand();
            command.CommandText = "SELECT 1 FROM sqlite_master WHERE type = 'table' AND name = $name LIMIT 1;";
            var parameter = command.CreateParameter();
            parameter.ParameterName = "$name";
            parameter.Value = tableName;
            command.Parameters.Add(parameter);

            using var reader = command.ExecuteReader();
            return reader.Read();
        }

        private static void EnsureMemberColumn(AppDbContext db, string columnName, string columnTypeSql)
        {
            var connection = db.Database.GetDbConnection();
            if (connection.State != System.Data.ConnectionState.Open)
                connection.Open();

            try
            {
                using var tableInfoCommand = connection.CreateCommand();
                tableInfoCommand.CommandText = "PRAGMA table_info(Members);";

                var hasColumn = false;
                using (var reader = tableInfoCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var existingColumnName = reader["name"]?.ToString();
                        if (string.Equals(existingColumnName, columnName, StringComparison.OrdinalIgnoreCase))
                        {
                            hasColumn = true;
                            break;
                        }
                    }
                }

                if (!hasColumn)
                {
                    using var alterCommand = connection.CreateCommand();
                    alterCommand.CommandText = $"ALTER TABLE Members ADD COLUMN {columnName} {columnTypeSql};";
                    alterCommand.ExecuteNonQuery();
                }
            }
            finally
            {
                if (connection.State == System.Data.ConnectionState.Open)
                    connection.Close();
            }
        }
    }
}
