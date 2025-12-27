using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ciceksepeti.Data;
using ciceksepeti.Models;
using ciceksepeti.Entities;
using Microsoft.AspNetCore.Authentication.Cookies;
using ciceksepeti.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();

builder.Services.AddControllersWithViews();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();

builder.Services.AddRazorPages();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<OrderService>();
builder.Services.AddScoped<CreditCardService>();
builder.Services.AddScoped<SalesService>();


builder.Services.AddScoped<ProductRepository>();


// Session'ı ekliyoruz
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Session süresi
    options.Cookie.IsEssential = true;  // Cookie zorunlu
});


var app = builder.Build();

// Uygulamanın HTTP isteği işleme hattını yapılandırıyoruz
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts(); // HSTS kullanımı
}

app.UseHttpsRedirection();
app.UseStaticFiles();



app.UseRouting();

app.UseAuthentication(); // Kimlik doğrulama işlemi
app.UseAuthorization();  // Yetkilendirme işlemi

app.UseSession();        // Session işlemleri


app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
    endpoints.MapRazorPages();
});


app.Run();
