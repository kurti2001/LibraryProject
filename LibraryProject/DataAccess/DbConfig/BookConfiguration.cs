using LibraryProject.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryProject.DataAccess.DbConfig
{
	internal class BookConfiguration : IEntityTypeConfiguration<Book>
	{
		public void Configure(EntityTypeBuilder<Book> builder)
		{
			builder.ToTable("Books");
			builder.HasKey(x => x.Id);
			builder.Property(x => x.Title)
				   .IsRequired()
				   .HasMaxLength(100);
			builder.Property(x => x.Description)
				   .IsRequired();
			builder.Property(x=> x.ISBN)
				   .IsRequired();
			builder.Property(x=> x.Created)
				   .IsRequired();
			builder.Property(x => x.Updated);

			builder.HasOne(y => y.Category)
				   .WithMany(a => a.Books)
				   .HasForeignKey(y => y.CategoryId);

		}
	}
}
