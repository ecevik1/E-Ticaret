using System.ComponentModel.DataAnnotations;

namespace E_Ticaret.Models
{
    public class Categories
    {
        [Key]
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }

        virtual public List<Products> Products { get; set; } // kategori tablosu ile ürün tablosu arasında ilişki kurulur.

    }
}
