using LibraryProject.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryProject.DataAccess.DbConfig
{
    internal class CartItemConfiguration : IEntityTypeConfiguration<CartItems>
    {
        public void Configure(EntityTypeBuilder<CartItems> builder)
        {
            builder.ToTable("Cartitems");
            builder.HasKey(x => x.Id);

            builder.HasOne<Book>()
                   .WithMany()
                   .HasForeignKey(x => x.BookId)
                   .OnDelete(DeleteBehavior.Restrict);
        }

    }
}
