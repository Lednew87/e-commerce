using Commerce.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Commerce.Data.Configurations
{
    public class ProdutosConfiguration : IEntityTypeConfiguration<Produtos>
    {
        public void Configure(EntityTypeBuilder<Produtos> builder)
        {
            {
                builder.ToTable("Produtos");
                builder.HasKey(p => p.Id);
                builder.Property(p => p.CodigoBarras).HasColumnType("VARCHAR(14)").IsRequired();
                builder.Property(p => p.Descricao).HasColumnType("VARCHAR(60)");
                builder.Property(p => p.Valor).HasColumnType("DECIMAL").IsRequired();
                builder.Property(p => p.TipoProduto).HasConversion<string>();
            }
        }
    }
}