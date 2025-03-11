using BL;

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Reflection;
using LapShop.Domain.Entities;
using Microsoft.AspNetCore.Localization;
using System.Globalization;


var builder = WebApplication.CreateBuilder(args);
// Add localization
builder.Services.AddLocalization(options =>
{
    options.ResourcesPath = "Resources";
});
// Add services to the container.

builder.Services.AddControllersWithViews()
    .AddViewLocalization();
#region Custom  Services
builder.Services.AddDbContext<LapShopContext>
(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddIdentity<ApplicationUsers, IdentityRole>(options =>
{
    options.Password.RequiredLength = 8;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireDigit = false;
    options.User.RequireUniqueEmail = true;
}).AddEntityFrameworkStores<LapShopContext>();
builder.Services.ConfigureApplicationCookie(options =>
{
    options.AccessDeniedPath = "/Users/AccessDenied";
    options.Cookie.Name = "Cookie";
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(720);
    options.LoginPath = "/Users/Login";
    options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
    options.SlidingExpiration = true;
});



builder.Services.AddScoped<ICategores<TbCategory>, ClCategores>();
builder.Services.AddScoped<IOS<TbO>, ClOS>();
builder.Services.AddScoped<IItems<TbItem>, ClItems>();
builder.Services.AddScoped<IItemTypes<TbItemType>, ClItemsTypes>();
builder.Services.AddScoped<IItemImages<TbItemImage>, ClItemImages>();
builder.Services.AddScoped<ISalesInvoices, ClsSalesInvoices>();
builder.Services.AddScoped<ISalesInvoiceItems<TbSalesInvoiceItem>, ClsSalesInvoiceItems>();
builder.Services.AddScoped<ISettings<TbSetting>, ClsSettings>();
builder.Services.AddScoped<IPages<TbPage>, ClsPages>();
builder.Services.AddScoped<ISliders<TbSlider>, ClSliders>();
builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();
builder.Services.AddDistributedMemoryCache();
//builder.Services.AddSwaggerGen(options =>
//{
//    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
//    {
//        Version = "v1",
//        Title = "Todo API",
//        Description = "An ASP.NET Core web API for to  accse all API Coureses",
//        TermsOfService = new Uri("https://itlegend.net/"),
//        Contact = new Microsoft.OpenApi.Models.OpenApiContact
//        {
//            Email ="info@itlegend.net",
//            Name ="Ahmed Sabry",
//            Url=new Uri("https://itlegend.net/")
//        },
//        License =new Microsoft.OpenApi.Models.OpenApiLicense
//        {
//            Name = "Courses Programing",

//            Url = new Uri("https://itlegend.net/")
//        }
//    });

//});
#endregion
#region SWagger

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Version = "v1",
        Title = "Todo API",
        Description = "An ASP.NET Core web API for to  accse all API LapShop",
        TermsOfService = new Uri("https://itlegend.net/"),
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Email = "https://myaccount.google.com/email",
            Name = "Ahmed Sabry",
            Url = new Uri("https://itlegend.net/")
        },
        License = new Microsoft.OpenApi.Models.OpenApiLicense
        {
            Name = "LapShop",

            Url = new Uri("https://itlegend.net/")
        }

    });
    var xmlComments = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var FullPath = Path.Combine(AppContext.BaseDirectory, xmlComments);
    options.IncludeXmlComments(FullPath);

});
#endregion
#region Jwt
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true, // Ensure tokens are not expired
        ClockSkew = TimeSpan.Zero // Optional: Reduce tolerance on token expiration
    };

});

#endregion

var app = builder.Build();
// Configure localization middleware
var supportedCultures = new[] { "en-US" }.Select(c => CultureInfo.CreateSpecificCulture(c)).ToArray();
app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("en-US"),
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures
});

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
//#region UseSwagger

//app.UseSwagger();
//app.UseSwaggerUI(options =>
//{
//    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
//    options.RoutePrefix = string.Empty;
//});

//#endregion
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();
app.UseAuthorization();
#region Routing
#pragma warning disable ASP0014 // Suggest using top level route registrations
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
    name: "admin",
    pattern: "{area:exists}/{controller=Home}/{action=Index}");

    //endpoints.MapControllerRoute(
    //name: "LandingPages",
    //pattern: "{area:exists}/{controller=Home}/{action=Index}");

    endpoints.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

    //endpoints.MapControllerRoute(
    //name: "ali",
    //pattern: "ali/{controller=Home}/{action=Index}/{id?}");



}
);
#pragma warning restore ASP0014 // Suggest using top level route registrations
#endregion
app.Run();
