using WebStore.Infrastructure.Conventions;
using WebStore.Infrastructure.Middleware;
using WebStore.Services.Interfaces;
using WebStore.Services;
using WebStore.DAL.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


var services = builder.Services;

services.AddControllersWithViews(opt =>
{
    opt.Conventions.Add(new TestConvention());
});

services.AddDbContext<WebStoreDB>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));
services.AddTransient<IDbInitializer, DbInitializer>();
services.AddSingleton<IEmployeesData, InMemoryEmployeesData>();
services.AddSingleton<IProductData, InMemoryProductData>();
services.AddSingleton<IPositionsData, InMemoryPositionsData>();

var app = builder.Build();

await using (var scope = app.Services.CreateAsyncScope())
{
    var db_initializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
    await db_initializer.InitializeAsync();
}

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.Map("/testpath", async context => await context.Response.WriteAsync("Test middleware"));

app.UseStaticFiles();
app.UseRouting();
app.UseMiddleware<TestMiddleware>();
app.UseWelcomePage("/welcome");

app.MapGet("/throw", () =>
{
    throw new ApplicationException("Ошибка в программе!");
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=home}/{action=index}/{id?}");

app.Run();
