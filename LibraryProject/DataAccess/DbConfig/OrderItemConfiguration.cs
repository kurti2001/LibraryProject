//using LibraryProject.DataAccess.Models;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;

//namespace LibraryProject.DataAccess.DbConfig
//{
//    internal class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
//    {
//        public void Configure(EntityTypeBuilder<OrderItem> builder)
//        {
//            builder.HasKey(x => x.Id);
//            builder.ToTable("OrderItems");
//            builder.HasOne<Order>()
//                   .WithMany()
//                   .HasForeignKey(x => x.OrderId)
//                   .OnDelete(DeleteBehavior.Restrict);

//            builder.HasOne<Book>()
//                   .WithMany()
//                   .HasForeignKey(x => x.BookId)
//                   .OnDelete(DeleteBehavior.Restrict);
//        }
//    }
//}
