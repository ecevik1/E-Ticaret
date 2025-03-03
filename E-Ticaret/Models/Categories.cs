using System.ComponentModel.DataAnnotations;

namespace E_Ticaret.Models
{
    public class Categories
    {
        [Key]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Category Name is required")]
        public string? CategoryName { get; set; } = string.Empty;

        public virtual List<Products>? Products { get; set; } = new List<Products>(); // kategori tablosu ile ürün tablosu arasında ilişki kurulur.
    }
}


