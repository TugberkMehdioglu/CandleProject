using Microsoft.AspNetCore.Localization;
using Project.BLL.ServiceExtensions;
using System.Globalization;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAuthentication();

builder.Services.AddDbContextService();
builder.Services.AddIdentityService();
builder.Services.AddManagerRepositoy();

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Home/Login";
    options.LogoutPath = new PathString("/Home/Logout");
    options.AccessDeniedPath = "/Home/AccessDenied";

    options.Cookie.Name = "HiGuys";
    options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
    options.SlidingExpiration = true;
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(20);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.Cookie.Name = "urWelcome";
});

builder.Services.Configure<RequestLocalizationOptions>(configuration =>
{
    List<CultureInfo> supportedCultures = new List<CultureInfo>()
    {
        new CultureInfo("tr-TR")
    };

    configuration.DefaultRequestCulture = new RequestCulture("tr-TR");
    configuration.SupportedCultures = supportedCultures;
    configuration.SupportedUICultures = supportedCultures;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseRequestLocalization();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
