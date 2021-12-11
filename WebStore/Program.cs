var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
var configuration = app.Configuration;
var greeting = configuration["CustomGreetings"];
app.MapGet("/", () => greeting);

app.Run();
