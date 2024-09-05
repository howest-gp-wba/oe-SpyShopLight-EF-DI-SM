using Microsoft.Extensions.Configuration;
using Wba.SpyshopActual.Domain.Entitys;
using Wba.SpyshopActual.Domain.Shopping;
using Wba.SpyshopActual.Domain;
using Wba.SpyshopActual.Web.Data;
using Wba.SpyshopActual.Web.Services;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<SpyShopContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SpyShopDb")));

builder.Services.AddTransient<IRepository<Product, int>, EFRepository<Product, int>>();
builder.Services.AddTransient<IRepository<Category, int>, EFRepository<Category, int>>();

builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddTransient<ICartService, CookieCartService>();
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.MapControllerRoute(
    name: "AreaRoute",
    pattern: "{area:exists}/{controller=Products}/{action=Index}/{id?}"
    );
app.MapControllerRoute(
     name: "ProductRoute",
     pattern: "Product/{prodID:int}",
     defaults: new { controller = "Product", action = "ProductDetail" }
     );

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();

