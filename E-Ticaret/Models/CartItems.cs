namespace E_Ticaret.Models
{
    public class CartItems
    {
        // Ürünün kimliğini tutar
        public long ProductId { get; set; }

        // Ürünün adını tutar
        public string ProductName { get; set; }

        // Ürünün resmini tutar
        public string Image { get; set; }

        // Ürünün fiyatını tutar
        public decimal Price { get; set; }

        // Ürünün miktarını tutar (adet)
        public int Quantity { get; set; } // adet

        // Ürünün toplam fiyatını hesaplar (miktar * fiyat)
        public decimal Total { get { return Quantity * Price; } } // toplam fiyat

        // Parametresiz kurucu metot
        public CartItems() { }

        // Ürün bilgilerini alarak CartItems nesnesi oluşturan kurucu metot
        public CartItems(Products product)
        {
            // Ürünün kimliğini atar
            ProductId = product.ProductId;

            // Ürünün adını atar
            ProductName = product.ProductName;

            // Ürünün resmini atar
            Image = product.ProductPicture;

            // Ürünün fiyatını atar ve decimal türüne dönüştürür
            Price = Convert.ToDecimal(product.ProductPrice);

            // Ürünün miktarını 1 olarak başlatır
            Quantity = 1;
        }
    }
}

/*
 * Açıklamalar:
•	ProductId: Ürünün kimliğini tutar.
•	ProductName: Ürünün adını tutar.
•	Image: Ürünün resmini tutar.
•	Price: Ürünün fiyatını tutar.
•	Quantity: Ürünün miktarını tutar (adet).
•	Total: Ürünün toplam fiyatını hesaplar (miktar * fiyat).
•	CartItems(): Parametresiz kurucu metot.
•	CartItems(Products product): Ürün bilgilerini alarak CartItems nesnesi oluşturan kurucu metot. Bu metot, Products sınıfından bir ürün alır ve CartItems nesnesinin özelliklerini bu ürünün bilgileriyle doldurur.
Miktarı (Quantity) 1 olarak başlatır.
*/