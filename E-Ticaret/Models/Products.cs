using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Ticaret.Models
{
    public class Products
    {

        [Key]
        public int ProductId{ get; set; }
        public string? ProductName { get; set; }=string.Empty;
        public string? ProductDescription { get; set; }= string.Empty;
        public int? ProductPrice { get; set; } = 0;
        public string? ProductPicture { get; set; } = string.Empty;
        public int? CategoryId { get; set; }
        public int? ProductCode { get; set; }
        public virtual Categories Category { get; set; } //kategori tablosu ile ilişkilendirme yapılır.

        [NotMapped]
        public IFormFile? ImageUpload { get; set; } // dosya yükleme işlemi için kullanılır.


    }
}
