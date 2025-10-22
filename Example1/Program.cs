using Example1;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IBookService, BookService>();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
