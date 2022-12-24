using Microsoft.EntityFrameworkCore;
using BalekShop.Repositories.Abstract;
using BalekShop.Repositories.Implementation;
using Microsoft.AspNetCore.Authentication.Cookies;
using BalekShop.Repositories.Language;
using Microsoft.AspNetCore.Localization;
using System.Globalization;
using System.Reflection;
using Microsoft.Extensions.Options;
using BalekShop.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("conn")));

builder.Services.AddScoped<IGenreService, GenreService>();
builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<IPublisherService, PublisherService>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IUserService,UserService>();
builder.Services.AddScoped<IAdminService, AdminService>();
builder.Services.AddHttpContextAccessor();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
{
	options.Cookie.Name = "BalekShop.Auth";
	options.LoginPath = "/User/Login";
	options.AccessDeniedPath = "/User/Login";   
});

//LANGUAGE
builder.Services.AddSingleton<LanguageService>();

builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

builder.Services.AddMvc()
    .AddViewLocalization()
    .AddDataAnnotationsLocalization(options =>
    {
        options.DataAnnotationLocalizerProvider = (type, factory) =>
        {

            var assemblyName = new AssemblyName(typeof(ShareResource).GetTypeInfo().Assembly.FullName);

            return factory.Create("ShareResource", assemblyName.Name);

        };

    });

builder.Services.Configure<RequestLocalizationOptions>(
    options =>
    {
        var supportedCultures = new List<CultureInfo>
            {
							new CultureInfo("ar"),
							new CultureInfo("en-US"),                            
                            new CultureInfo("es"),
                            new CultureInfo("ru"),
                            new CultureInfo("ko"),
                            new CultureInfo("ja"),
                            new CultureInfo("fr"),
							new CultureInfo("tr-TR")

			};

        options.DefaultRequestCulture = new RequestCulture(culture: "tr-TR", uiCulture: "tr-TR");

        options.SupportedCultures = supportedCultures;
        options.SupportedUICultures = supportedCultures;
        options.RequestCultureProviders.Insert(0, new QueryStringRequestCultureProvider());
    });




//LANGUAGE
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

//LANGUAGE
var supportedCultures = new[] { "en-US", "tr-TR","ar","es","fr","it","ja","ko","ru"};

var localizationOptions = new RequestLocalizationOptions()
    .SetDefaultCulture(supportedCultures[1])
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures);

app.UseRequestLocalization(localizationOptions);
//LANGUAGE
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=User}/{action=Store}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=User}/{action=DeleteBook}/{id?}");

app.MapControllerRoute(
	name: "AddCart",
	pattern: "{controller=User}/{action=AddCart}/{id?}"
	); 


app.Run();
