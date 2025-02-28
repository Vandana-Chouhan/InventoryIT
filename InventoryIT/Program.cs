using InventoryIT.Models;
using InventoryIT.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// Register the repository with the DI container
builder.Services.AddScoped<IMastCompRepository, MastCompRepository>();
builder.Services.AddScoped<IMastBranchRepository, MastBranchRepository>();
builder.Services.AddScoped<IFinancialYearRepository, FinancialYearRepository>();

builder.Services.AddDbContext<InventoryContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("connect")));

builder.Services.AddSession();

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
app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=MasterSetup}/{action=Session}/{id?}");

app.Run();
