using E_Ticaret.Data;
using Microsoft.EntityFrameworkCore;

namespace E_Ticaret
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            // Hizmetleri konteynere ekleyin.
            builder.Services.AddControllersWithViews();
            builder.Services.AddSession(); // Oturum hizmetlerini ekleyin

            var app = builder.Build();

            // HTTP istek hatt?n? yap?land?r?n.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseSession(); // Oturum yönetimini etkinle?tirin
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
