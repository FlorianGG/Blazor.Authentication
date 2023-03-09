namespace DemoBlazorServer.Dtos;

public record TokenInfo
{
    public string Token { get; init; }

    public DateTime TokenExpiration { get; init; }
}