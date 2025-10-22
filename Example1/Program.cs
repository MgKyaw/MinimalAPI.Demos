using Example1;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IBookService, BookService>();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/books", (IBookService bookService) =>
    TypedResults.Ok(bookService.GetBooks()))
    .WithName("GetBooks")
    .WithOpenApi(x => new OpenApiOperation(x)
    {
        Summary = "Get Library Books",
        Description = "Returns information about all the available books from the Amy's library.",
        Tags = new List<OpenApiTag> { new() { Name = "Amy's Library" } }
    });

app.Run();
