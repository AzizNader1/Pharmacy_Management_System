using PharmacyManagementSystem.WebAppMVC.Services.Implementations;
using PharmacyManagementSystem.WebAppMVC.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient("PharmacyApi", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ApiSettings:BaseUrl"]!);
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(60); // Session timeout.
    options.Cookie.IsEssential = true; // Make the session cookie essential
});

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IApiAccountServices, ApiAccountServices>();
builder.Services.AddScoped<IApiSaleServices, ApiSaleServices>();
builder.Services.AddScoped<IApiSaleItemsServices, ApiSaleItemsServices>();
builder.Services.AddScoped<IApiBatchServices, ApiBatchServices>();
builder.Services.AddScoped<IApiMedicineServices, ApiMedicineServices>();
builder.Services.AddScoped<IApiUserServices, ApiUserServices>();

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

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Accounts}/{action=Login}/{id?}")
    .WithStaticAssets();


app.Run();
