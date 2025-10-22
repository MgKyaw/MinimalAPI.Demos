using Example1;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IBookService, BookService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseStatusCodePages();
app.UseHttpsRedirection();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

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

app.MapGet("/books/{id}", Results<Ok<Book>, NotFound> (IBookService bookService, int id) =>
        bookService.GetBook(id) is { } book
            ? TypedResults.Ok(book)
            : TypedResults.NotFound()
    )
    .WithName("GetBookById")
    .WithOpenApi(x => new OpenApiOperation(x)
    {
        Summary = "Get Library Book By Id",
        Description = "Returns information about selected book from the Amy's library.",
        Tags = new List<OpenApiTag> { new() { Name = "Amy's Library" } }
    });

app.Run();
