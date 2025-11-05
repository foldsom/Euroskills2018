using Microsoft.EntityFrameworkCore;
using Microsoft.Data.Sqlite;
using Euroskills2018.Data;

namespace Euroskills2018
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<EuroskillsContext>(options =>
            {
                options.UseSqlite("Data Source=euroskills2018.db");
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            // Adatbázis inicializálás és SQL szkriptek futtatása
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var db = services.GetRequiredService<EuroskillsContext>();
                db.Database.EnsureCreated();

                var dbFile = Path.Combine(Directory.GetCurrentDirectory(), "euroskills2018.db");
                if (File.Exists(dbFile))
                {
                    using var conn = new SqliteConnection($"Data Source={dbFile}");
                    conn.Open();
                    using var cmd = conn.CreateCommand();
                    var sqlFolder = Path.Combine(Directory.GetCurrentDirectory(), "Sql");
                    var schemaPath = Path.Combine(sqlFolder, "tablak.sql");
                    var dataPath = Path.Combine(sqlFolder, "adatok.sql");

                    if (File.Exists(schemaPath))
                    {
                        cmd.CommandText = File.ReadAllText(schemaPath);
                        try { cmd.ExecuteNonQuery(); } catch { }
                    }

                    if (File.Exists(dataPath))
                    {
                        cmd.CommandText = File.ReadAllText(dataPath);
                        try { cmd.ExecuteNonQuery(); } catch { }
                    }
                }
            }

            app.Run();
        }
    }
}
