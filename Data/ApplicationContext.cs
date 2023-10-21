using Commerce.Domain;
using Microsoft.EntityFrameworkCore;


namespace Commerce.Data
{
    public class ApplicationContext : DbContext
    {
        private static readonly ILoggerFactory _logger = LoggerFactory.Create(p => p.AddConsole());
        public DbSet<Pedidos> Pedidos { get; set; }
        public DbSet<Produtos> Produtos { get; set; }
        public DbSet<Cliente> Clientes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            _ = optionsBuilder
                    .UseLoggerFactory(_logger)
                    .EnableSensitiveDataLogging()
                    .UseSqlServer("Data Source=DevDianaPC\\SQLEXPRESS;Initial Catalog=commerce;Integrated Security=SSPI;Trust Server Certificate=True",
                    // Método para reconectar a aplicação com a base de dados automaticamente nomeando a tabela do histórico de migrations.
                    p=>p.EnableRetryOnFailure(maxRetryCount: 2, maxRetryDelay: TimeSpan.FromSeconds(5),
                    errorNumbersToAdd: null).MigrationsHistoryTable("ecommerce_db"));
        }
    }

}
