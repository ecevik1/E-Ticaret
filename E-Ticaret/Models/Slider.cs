using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Ticaret.Models
{
    public class Slider //Slider tablosu oluşturulur.
    {
        [Key]
        public int SliderId { get; set; }
        public string? SliderName { get; set; } = string.Empty;
        public string? Header1 { get; set; } = string.Empty;
        public string? Header2 { get; set; } = string.Empty;
        public string? SliderContext { get; set; } = string.Empty;
        public string? SliderDescription { get; set; }
        public string? SliderImage { get; set; }

        [NotMapped]
        public IFormFile? ImageUpload { get; set; }

    }
}
