using FertilizerShopWeb.Data;
using Microsoft.EntityFrameworkCore;
using Rotativa.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

/// 🔹 1. MVC enable
builder.Services.AddControllersWithViews();
builder.Services.AddSession();

/// 🔹 2. DbContext register (Database connect)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);

/// 🔹 3. Session enable (Login system এর জন্য)
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();
RotativaConfiguration.Setup("wwwroot", "Rotativa");
//RotativaConfiguration.Setup(app.Environment.WebRootPath, "Rotativa");
/// 🔹 4. Error handling
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

/// 🔹 5. Middleware pipeline
app.UseHttpsRedirection();
app.UseStaticFiles();   // wwwroot enable
app.UseRouting();

app.UseSession();    
app.UseAuthorization();

/// 🔹 6. Default route (Login page first)
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}"
);

app.Run();
