﻿//using LibraryProject.DataAccess.Models;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;

//namespace LibraryProject.DataAccess.DbConfig
//{
//	internal class CategoryConfiguration : IEntityTypeConfiguration<Category>
//	{
//		public void Configure(EntityTypeBuilder<Category> builder)
//		{
//			builder.ToTable("Categories");
//			builder.HasKey(x => x.Id);
//			builder.Property(x => x.Name)
//				   .IsRequired()
//				   .HasMaxLength(100);

//		}
//	}
//}
