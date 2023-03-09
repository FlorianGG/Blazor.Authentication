namespace DemoBlazorServer.Public.Auth;

using System;
using System.Text;
using System.Text.Json;
using AuthGrpc;
using DemoBlazorServer.Dtos;
using DemoBlazorServer.Services.Authentication;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Components;

public partial class Login
{

    [Inject]
    public CustomAuthenticationStateProvider AuthProvider { get; set; } = null!;

    private LoginModel loginModel = new();

    private async Task LoginAsync()
    {
        var channel = GrpcChannel.ForAddress("http://localhost:5001");
        var client = new AuthGrpc.Auth.AuthClient(channel);

        var loginResult = await client.LoginAsyncAsync(new LoginAsyncRequest()
        {
            Login = this.loginModel.Login,
            Password = this.loginModel.Password,
        }).ConfigureAwait(false);

        if (loginResult.Result is not null)
            this.AuthProvider.SetUserLoggedIn(loginResult.Result.Token, loginResult.Result.RefreshToken, loginResult.Result.ExpirationDate.ToDateTime());
    }
}