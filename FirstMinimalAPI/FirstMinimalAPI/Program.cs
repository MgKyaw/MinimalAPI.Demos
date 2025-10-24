using Microsoft.AspNetCore.Http.HttpResults;
using static System.Security.Cryptography.RandomNumberGenerator;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton(new HuggingService());
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

async ValueTask<object?> Timestamp(
    EndpointFilterInvocationContext ctx,
    EndpointFilterDelegate next
)
{
    var result = await next(ctx);
    if (result is Ok<Hugged> { Value: { } } hugged)
        hugged.Value.Timestamp = DateTime.UtcNow;
    return result;
}

var hugs = app.MapGroup("hugs")
    .AddEndpointFilter(Timestamp);

hugs.MapPost("", (Hug hug, HuggingService hugger) =>
    Results.Ok(hugger.Hug(hug))
);
hugs.MapGet("", (HuggingService hugger) =>
    Results.Ok(hugger.Hug(new Hug("Test")))
);

app.Run();

public record Hug(string Name);

public record Hugged(string Name, string Kind)
{
    public DateTime Timestamp { get; set; } = DateTime.UnixEpoch;
}

public class HuggingService
{
    private readonly string[] _hugKinds = {
        "Side Hug", "Bear Hug",
        "Polite Hug", "Back Hug",
        "Self Hug"
    };

    private string RandomKind =>
        _hugKinds[GetInt32(0, _hugKinds.Length)];

    public Hugged Hug(Hug hug)
        => new(hug.Name, RandomKind);
}