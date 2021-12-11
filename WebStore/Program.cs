var builder = WebApplication.CreateBuilder(args);


var services = builder.Services;
services.AddControllersWithViews();

var app = builder.Build();
//var configuration = app.Configuration;
//var greeting = configuration["CustomGreetings"];
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseRouting();

//app.MapGet("/", () => app.Configuration["CustomGreetings"]);
app.MapGet("/throw", () =>
{
    throw new ApplicationException("Ошибка в программе!");
});

app.MapDefaultControllerRoute();

app.Run();
