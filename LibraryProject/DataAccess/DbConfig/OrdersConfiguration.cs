//using LibraryProject.DataAccess.Models;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;

//namespace LibraryProject.DataAccess.DbConfig
//{
//    internal class OrdersConfiguration : IEntityTypeConfiguration<Order>
//    {
//        public void Configure(EntityTypeBuilder<Order> builder)
//        {
//            builder.ToTable("Orders");
//            builder.HasKey(x => x.Id);

//            builder.Property(x => x.Status)
//                    .HasDefaultValue(OrderStatus.CREATED);
//            builder.Property(x => x.Address)
//                    .IsRequired()
//                    .HasMaxLength(100);
//            builder.Property(x => x.MobilePhone)
//                     .IsRequired()
//                     .HasMaxLength(15);
//            builder.Property(x => x.CreatedOn)
//                    .IsRequired();
//            builder.Property(x => x.Deadline);

//            builder.HasOne<User>()
//                   .WithMany()
//                   .HasForeignKey(x => x.UserId)
//                   .OnDelete(DeleteBehavior.Restrict);
//        }
//    }
//}
