using Microsoft.EntityFrameworkCore;
using StockApp.Infrastructure.Persistence.Context;
using StockApp.Infrastructure.Persistence;
using WebApp.NewStockApp.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSession(); 
builder.Services.AddPersistenceInfrastructure(builder.Configuration);
builder.Services.AddApplicationLayer();
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>(); 
builder.Services.AddTransient<ValidateUserSession, ValidateUserSession>(); 

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSession(); 
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=User}/{action=Index}/{id?}");

app.Run();
