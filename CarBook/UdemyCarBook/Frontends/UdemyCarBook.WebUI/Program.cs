using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// 1. SERVÝSLERÝN KAYDEDÝLMESÝ (BUILDER)
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();

// Kimlik Doðrulama Yapýlandýrmasý (Sadece Cookie yeterli)
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login/Index/";        // Giriþ yapmayan buraya gider
        options.LogoutPath = "/Login/LogOut/";      // Çýkýþ adresi
        options.AccessDeniedPath = "/Pages/AccessDenied/";
        options.Cookie.SameSite = SameSiteMode.Strict;
        options.Cookie.HttpOnly = true;
        options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
        options.Cookie.Name = "CarBookCookie";
    });

var app = builder.Build();

// 2. ARA KATMANLARIN (MIDDLEWARE) SIRALAMASI
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // CSS ve Resimler için þart

app.UseRouting();

// Siralama ÇOK önemli: Önce Authentication, sonra Authorization
app.UseAuthentication();
app.UseAuthorization();

// 3. ROUTE (YÖNLENDÝRME) TANIMLARI
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Default}/{action=Index}/{id?}");

app.Run();