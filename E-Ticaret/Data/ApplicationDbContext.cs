using E_Ticaret.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Ticaret.Data
{
    // ApplicationDbContext sınıfı, Entity Framework Core kullanarak veritabanı bağlantısını ve işlemlerini yönetir.
    public class ApplicationDbContext : DbContext
    {
        // ApplicationDbContext yapıcı metodu, DbContextOptions parametresi alır ve bu parametreyi base sınıfa (DbContext) iletir.
        // Bu, veritabanı bağlantı ayarlarının yapılandırılmasını sağlar.
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // Products DbSet'i, Products tablosunu temsil eder ve veritabanı işlemlerini yönetir.
        public DbSet<Products>? Products { get; set; }

        // Categories DbSet'i, Categories tablosunu temsil eder ve veritabanı işlemlerini yönetir.
        public DbSet<Categories>? Categories { get; set; }

        // Slider DbSet'i, Slider tablosunu temsil eder ve veritabanı işlemlerini yönetir.
        public DbSet<Slider>? Slider { get; set; }
    }
}
