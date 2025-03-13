using InventoryIT.Models;
using InventoryIT.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// Register the repository with the DI container
builder.Services.AddScoped<IMastCompRepository, MastCompRepository>();
builder.Services.AddScoped<IMastBranchRepository, MastBranchRepository>();
builder.Services.AddScoped<IFinancialYearRepository, FinancialYearRepository>();
builder.Services.AddScoped<IUserMasterRepository, UserMasterRepository>();

builder.Services.AddScoped<IMastCityRepository, MastCityRepository>();
builder.Services.AddScoped<IMastStateRepository, MastStateRepository>();
builder.Services.AddScoped<IMastCountryRepository, MastCountryRepository>();

builder.Services.AddScoped<IItemCatagoryRepository, ItemCatagoryRepository>();
builder.Services.AddScoped<IItemTypeRepository, ItemTypeRepository>();
builder.Services.AddScoped<IItemCompanytRepository, ItemCompanyRepository>();
builder.Services.AddScoped<IItemUnitRepository, ItemUnitRepository>();
builder.Services.AddScoped<IItemSubCatagoryRepository, ItemSubCatagoryRepository>();
builder.Services.AddScoped<ISupplierMasterRepository, SupplierMasterRepository>();
builder.Services.AddScoped<IMastItemSupplierRateRepository, MastItemSupplierRateRepository>();
builder.Services.AddScoped<IMastItemStkRepository, MastItemStkRepository>();
builder.Services.AddScoped<IItemMasterRepository, ItemMasterRepository>();

builder.Services.AddScoped<IWarehouseLocationRepository, WarehouseLocationRepository>();
builder.Services.AddScoped<IWarehouseAreaRepository, WarehouseAreaRepository>();
builder.Services.AddScoped<IWarehouseRackRepository, WarehouseRackRepository>();
builder.Services.AddScoped<IWarehouseShelfRepository, WarehouseShelfRepository>();

builder.Services.AddDbContext<InventoryContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("connect")));

builder.Services.AddSession();
// Add and configure session
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;

});

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
    pattern: "{controller=MasterSetup}/{action=Login}/{id?}");
app.Run();