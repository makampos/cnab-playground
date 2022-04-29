using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Mappings
{
    public class TransactionTypeMapping : IEntityTypeConfiguration<TransactionType>
    {
        public void Configure(EntityTypeBuilder<TransactionType> builder)
        {
            builder.ToTable("tbl_transaction_type");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .HasColumnType("integer")
                .HasColumnName("id");

            builder.Property(x => x.Description)
                .HasColumnType("varchar(20)")
                .HasColumnName("description");

            builder.Property(x => x.Nature)
                .HasColumnType("varchar(20)")
                .HasColumnName("nature");

            builder.Property(x => x.Signal)
                .HasColumnType("varchar(1)")
                .HasColumnName("signal");

            builder.HasData(new TransactionType(1,"Débito", "Entrada", "+"));
            builder.HasData(new TransactionType(2,"Boleto", "Saída", "-"));
            builder.HasData(new TransactionType(3,"Financiamento", "Saída", "-"));
            builder.HasData(new TransactionType(4,"Crédito", "Entrada", "+"));
            builder.HasData(new TransactionType(5,"Recebimento Entrada", "Entrada", "+"));
            builder.HasData(new TransactionType(6,"Vendas", "Entrada", "+"));
            builder.HasData(new TransactionType(7,"Recebimento TED", "Entrada", "+"));
            builder.HasData(new TransactionType(8,"Recebimento DOC", "Entrada", "+"));
            builder.HasData(new TransactionType(9,"Aluguél", "Saída", "-"));
        }
    }
}
