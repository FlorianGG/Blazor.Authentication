namespace DemoBlazorServer.Dtos;

public record LoginModel
{
    public string Login { get; set; }

    public string Password { get; set; }
}