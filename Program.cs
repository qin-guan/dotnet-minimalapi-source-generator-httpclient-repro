using System.Text.Json.Serialization;

var builder = WebApplication.CreateSlimBuilder(args);

builder.Services.AddHttpClient();

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.TypeInfoResolverChain.Insert(0, AppJsonSerializerContext.Default);
});

var app = builder.Build();

app.MapGet("/",
    (HttpClient httpClient) =>
        httpClient.GetFromJsonAsync("https://ipinfo.io/", AppJsonSerializerContext.Default.Response));

app.Run();

public record Response([property: JsonPropertyName("ip")] string Ip);

[JsonSerializable(typeof(Response))]
internal partial class AppJsonSerializerContext : JsonSerializerContext
{
}