using E_Ticaret.Models;

namespace E_Ticaret.Dto
{
    public class CartViewModel
    {
        // Sepetteki ürünlerin listesini tutar
        public List<CartItems> CartItems { get; set; }

        // Sepetin toplam fiyatını tutar
        public decimal TotalPrice { get; set; }
        public int TotalItemCount => CartItems?.Sum(item => item.Quantity) ?? 0;
    }
}

/*
 Açıklamalar:
•	public List<CartItems> CartItems { get; set; }: Sepetteki ürünlerin listesini tutar. CartItems sınıfından nesnelerin bir listesidir.
•	public decimal TotalPrice { get; set; }: Sepetin toplam fiyatını tutar. Bu, sepet içindeki tüm ürünlerin toplam fiyatıdır.
Bu sınıf, sepetin görüntülenmesi için kullanılan bir modeldir. CartViewModel, sepetin içeriğini (CartItems) ve toplam fiyatını (TotalPrice) tutar. 
Bu model, Razor Pages veya MVC görünümlerinde sepetin görüntülenmesi için kullanılır.
 
 */