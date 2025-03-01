// Sayfa yüklendiğinde çalışacak işlemler
document.addEventListener("DOMContentLoaded", function () {
    console.log("VAGO Collection ana sayfasına hoş geldiniz!");

    // Satın al butonlarına tıklanınca alert göster
    document.querySelectorAll(".btn-dark").forEach(button => {
        button.addEventListener("click", function () {
            alert("Ürün sepete eklendi!");
        });
    });
});
