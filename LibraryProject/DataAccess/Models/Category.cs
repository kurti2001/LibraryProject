using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryProject.DataAccess.Models
{
	public class Category
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		public string? Name { get; set; }
		
		public ICollection<Book> ?Books { get; set;}
	}
	public class CategoryModel
	{
        public string? Name { get; set; }
    }
}
