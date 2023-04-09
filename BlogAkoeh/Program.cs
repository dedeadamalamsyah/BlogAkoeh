using BlogAkoeh.Data;
using BlogAkoeh.Models;
using BlogAkoeh.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<MysqlContext>(
    options => options.UseMySQL(builder.Configuration.GetConnectionString("mysql"))
    );
builder.Services.AddAuthentication()
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, option =>
    {
        option.LoginPath = "/account/login";
    });
builder.Services.Configure<MailSettings>
    (
    builder.Configuration.GetSection(nameof(MailSettings))
    );

builder.Services.AddTransient<EmailService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
