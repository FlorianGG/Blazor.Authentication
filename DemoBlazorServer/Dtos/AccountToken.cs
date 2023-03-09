namespace DemoBlazorServer.Dtos;

using System.Text.Json.Serialization;

public record AccountToken
{
    [JsonPropertyName("token")]
    public string? Token { get; set; }

    [JsonPropertyName("refreshToken")]
    public string? RefreshToken { get; set; }

    [JsonPropertyName("expirationDate")]
    public DateTime ExpirationDate { get; set; }
}