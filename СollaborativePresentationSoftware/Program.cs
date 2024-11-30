using Microsoft.EntityFrameworkCore;
using PresentationApp.Data;
using PresentationApp.Hubs;
using PresentationApp.Services.Implementatons;
using PresentationApp.Services.Interfases;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnetion")
    ));
builder.Services.AddSignalR();
builder.Services.AddAuthentication("Cookies")
    .AddCookie(options => options.LoginPath = "/Enter/LogIn");
builder.Services.AddAuthorization();
builder.Services.AddScoped<IUserService, UserService>();


var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapHub<ChatHub>("/chat");
app.Run();
