﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryProject.DataAccess.Models
{
	[Table("Books")]
	public class Book
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		public string? Title { get; set; }
		public string? Description { get; set; }
		public string? ISBN { get; set; }
		public DateTime Created { get; set; }
		public DateTime Updated { get; set; }
		public Category? Category { get; set; }
		public int CategoryId { get; set; }

		public string? ImagePath { get; set; }
	}

	public class BookModel
	{
		public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? ISBN { get; set; }
		public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public int CategoryId { get; set; }
		public List<Category>? Categories { get; set;}

		public string? ImagePath { get; set; }
    }
}
