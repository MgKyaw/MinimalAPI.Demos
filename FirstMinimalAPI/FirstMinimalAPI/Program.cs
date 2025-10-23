var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapPost("/hugs", (Hug hug) =>
    Results.Ok(new Hugged(hug.Name, "Side Hug"))
);

app.Run();

public record Hug(string Name);
public record Hugged(string Name, string Kind);
