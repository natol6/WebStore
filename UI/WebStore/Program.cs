using WebStore.Infrastructure.Conventions;
using WebStore.Infrastructure.Middleware;
using WebStore.DAL.Context;
using Microsoft.EntityFrameworkCore;
using WebStore.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using WebStore.Interfaces.Services;
using WebStore.Services.Services;
using WebStore.Services.Services.InSQL;
using WebStore.Services.Services.InCookies;
using WebStore.WebAPI.Clients.Values;
using WebStore.Interfaces.TestAPI;
using WebStore.WebAPI.Clients.Employees;
using WebStore.WebAPI.Clients.Products;

var builder = WebApplication.CreateBuilder(args);


var services = builder.Services;

services.AddControllersWithViews(opt =>
{
    opt.Conventions.Add(new TestConvention());
});

services.AddDbContext<WebStoreDB>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));
services.AddTransient<IDbInitializer, DbInitializer>();
services.AddIdentity<User, Role>().AddEntityFrameworkStores<WebStoreDB>().AddDefaultTokenProviders();
services.Configure<IdentityOptions>(opt =>
{
#if DEBUG
    opt.Password.RequireDigit = false;
    opt.Password.RequireLowercase = false;
    opt.Password.RequireUppercase = false;
    opt.Password.RequireNonAlphanumeric = false;
    opt.Password.RequiredLength = 3;
    opt.Password.RequiredUniqueChars = 3;
#endif
    opt.User.RequireUniqueEmail = false;
    opt.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIGKLMNOPQRSTUVWXYZ1234567890";

    opt.Lockout.AllowedForNewUsers = false;
    opt.Lockout.MaxFailedAccessAttempts = 10;
    opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
});
services.ConfigureApplicationCookie(opt =>
{
    opt.Cookie.Name = "WebStore.GB";
    opt.Cookie.HttpOnly = true;

    //opt.Cookie.Expiration = TimeSpan.FromDays(10); // устарело
    opt.ExpireTimeSpan = TimeSpan.FromDays(10);

    opt.LoginPath = "/Account/Login";
    opt.LogoutPath = "/Account/Logout";
    opt.AccessDeniedPath = "/Account/AccessDenied";

    opt.SlidingExpiration = true;
});

//services.AddScoped<IEmployeesData, SqlEmployeesData>();
//services.AddScoped<IProductData, SqlProductData>();
services.AddScoped<IPositionsData, SqlPositionsData>();
services.AddScoped<IOrderService, SqlOrderService>();
services.AddScoped<ICartService, InCookiesCartService>();

var configuration = builder.Configuration;
services.AddHttpClient<IValuesService, ValuesClient>(client => client.BaseAddress = new(configuration["WebAPI"]));
services.AddHttpClient<IEmployeesData, EmployeesClient>(client => client.BaseAddress = new(configuration["WebAPI"]));
services.AddHttpClient<IProductData, ProductsClient>(client => client.BaseAddress = new(configuration["WebAPI"]));

var app = builder.Build();

await using (var scope = app.Services.CreateAsyncScope())
{
    var db_initializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
    await db_initializer.InitializeAsync(RemoveBefore: false).ConfigureAwait(true);
}

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.Map("/testpath", async context => await context.Response.WriteAsync("Test middleware"));

app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<TestMiddleware>();
app.UseWelcomePage("/welcome");

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
    endpoints.MapControllerRoute(
    name: "default",
    pattern: "{controller=home}/{action=index}/{id?}");
});

app.MapGet("/throw", () =>
{
    throw new ApplicationException("Ошибка в программе!");
});

app.Run();
