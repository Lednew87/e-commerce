using Commerce.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Commerce.Data.Configurations
{
    public class PedidosConfiguration : IEntityTypeConfiguration<Pedidos>
    {
        public void Configure(EntityTypeBuilder<Pedidos> builder)
        {
            {
                builder.ToTable("Pedidos");
                builder.HasKey(p => p.Id);
                builder.Property(p => p.IniciadoEm).HasDefaultValueSql("GET DATE()").ValueGeneratedOnAdd();
                builder.Property(p => p.Status).HasConversion<string>();
                builder.Property(p => p.TipoFrete).HasConversion<int>();
                builder.Property(p => p.Observacao).HasColumnType("VARCHAR(512)");

                builder.HasMany(p => p.Itens)
                    .WithOne(p => p.Pedidos)
                    .OnDelete(DeleteBehavior.Cascade);
            }
        }
    }
}