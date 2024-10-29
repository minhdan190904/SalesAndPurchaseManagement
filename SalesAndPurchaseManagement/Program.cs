using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using SalesAndPurchaseManagement.Data;
using SalesAndPurchaseManagement.Services;
using SalesAndPurchaseManagement.Helpers;
using Microsoft.AspNetCore.Localization;
using System.Globalization;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Cấu hình dịch vụ
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<IEmailService, EmailService>();

builder.Services.AddDbContext<SAPManagementContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("QLBGLocalContext")));

// Cấu hình Globalization
var supportedCultures = new[]
{
    new CultureInfo("en-US"), // Ví dụ: Văn hóa cho Mỹ
    new CultureInfo("fr-FR"), // Ví dụ: Văn hóa cho Pháp
};

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.DefaultRequestCulture = new RequestCulture("en-US"); // Văn hóa mặc định
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});

// Cấu hình Authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Access/Login";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(AppDefaults.TimeOut);
    });

builder.Services.AddHttpContextAccessor();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireClaim("IsAdmin", "True"));
});

var app = builder.Build();

// Sử dụng middleware cho Globalization
var localizationOptions = app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value;
app.UseRequestLocalization(localizationOptions);

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Access}/{action=Login}/{id?}");

app.Run();
