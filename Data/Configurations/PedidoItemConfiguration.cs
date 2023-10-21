using Commerce.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Commerce.Data.Configurations
{
    public class PedidoItemConfiguration : IEntityTypeConfiguration<PedidoItem>
    {
        public void Configure(EntityTypeBuilder<PedidoItem> builder)
        {
            {
                builder.ToTable("PedidoItem");
                builder.HasKey(p => p.Id);
                builder.Property(p => p.Quantidade).HasDefaultValue(0).IsRequired();
                builder.Property(p => p.Valor).HasColumnType("DECIMAL").HasDefaultValue(0).IsRequired();
                builder.Property(p => p.Desconto).HasColumnType("DECIMAL").HasDefaultValue(0).IsRequired();
            }
        }
    }
}