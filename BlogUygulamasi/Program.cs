using BlogUygulamasi.Data;
using BlogUygulamasi.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<BlogDbContext>(options =>
options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options=>
{
    options.Cookie.Name = "NetCoreMvc.Auth";
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=BlogIndex}/{action=BlogIndex}/{id?}");

app.Run();
