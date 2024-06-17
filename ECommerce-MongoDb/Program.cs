using ECommerce_MongoDb.Models;
using ECommerce_MongoDb.Services.CategoryService;
using ECommerce_MongoDb.Services.CustomerService;
using ECommerce_MongoDb.Services.GCSService;
using ECommerce_MongoDb.Services.OrderService;
using ECommerce_MongoDb.Services.ProductService;
using ECommerce_MongoDb.Settings;
using Microsoft.Extensions.Options;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.Configure<DatabaseSetting>(builder.Configuration.GetSection("DatabaseSettings"));
builder.Services.AddScoped<IDatabaseSetting>(x =>
{
    return x.GetRequiredService<IOptions<DatabaseSetting>>().Value;
});
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IOrderService, OrderService>();

builder.Services.Configure<GCSConfigOptions>(builder.Configuration);
builder.Services.AddSingleton<IGCSService, GCSService>();

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
    name: "default",
    pattern: "{controller=Product}/{action=ProductList}/{id?}");

app.Run();
