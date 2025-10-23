using static System.Security.Cryptography.RandomNumberGenerator;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapPost("/hugs", (Hug hug, HuggingService hugger) =>
    Results.Ok(hugger.Hug(hug))
);

app.Run();

public record Hug(string Name);
public record Hugged(string Name, string Kind);

public class HuggingService
{
    private readonly string[] _hugKinds =
    {
        "Side Hug",
        "Bear Hug",
        "Polite Hug",
        "Back Hug",
        "Self Hug"
    };

    private string RandomKind =>
        _hugKinds[GetInt32(0, _hugKinds.Length)];

    public Hugged Hug(Hug hug) =>
        new Hugged(hug.Name, RandomKind);
}
