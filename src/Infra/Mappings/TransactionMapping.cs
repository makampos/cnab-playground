using Domain;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Repository.Mappings
{
    public class TransactionMapping : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.ToTable("tbl_transaction");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .HasColumnType("integer")
                .HasColumnName("id");

            builder.Property(x => x.TransactionTypeId)
                .HasColumnType("integer")
                .HasColumnName("fk_transaction_type_id");

            builder.Property(x => x.Date)
                .HasColumnType("varchar(10)")
                .HasColumnName("date");

            builder.Property(x => x.Amount)
                .HasColumnType("decimal(18,2)")
                .HasColumnName("amount");

            builder.Property(x => x.CPF)
                .HasColumnType("varchar(11)")
                .HasColumnName("cpf");

            builder.Property(x => x.CardNumber)
                .HasColumnType("varchar(16)")
                .HasColumnName("card_number");

            builder.Property(x => x.Hour)
                .HasColumnType("time(7)")
                .HasColumnName("hour");

            builder.HasOne(x => x.TransactionType)
                .WithMany(x => x.Transactions)
                .HasForeignKey(x => x.TransactionTypeId);
        }
    }
}
