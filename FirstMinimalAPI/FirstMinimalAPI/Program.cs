var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();

public record Hug(string Name);
public record Hugged(string Name, string Kind);
