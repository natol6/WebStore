using WebStore.Infrastructure.Conventions;
using WebStore.Infrastructure.Middleware;
using WebStore.Services.Interfaces;
using WebStore.Services;



var builder = WebApplication.CreateBuilder(args);


var services = builder.Services;

services.AddControllersWithViews(opt =>
{
    opt.Conventions.Add(new TestConvention());
});

services.AddSingleton<IEmployeesData, InMemoryEmployeesData>();
services.AddSingleton<IProductData, InMemoryProductData>();

var app = builder.Build();

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
