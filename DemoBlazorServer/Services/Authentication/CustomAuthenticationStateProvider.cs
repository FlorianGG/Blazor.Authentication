namespace DemoBlazorServer.Services.Authentication;

using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using AuthGrpc;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Components.Authorization;

public class CustomAuthenticationStateProvider : AuthenticationStateProvider
{
    private string? token;

    private string? internalRefreshToken;

    private ClaimsPrincipal? principal;

    public string? Email
        => this.principal?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;

    public string? Login
        => this.principal?.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Sub)?.Value;

    private DateTime tokenExpiration { get; set; }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var newToken = await this.GetTokenAsync().ConfigureAwait(false);

        var identity = string.IsNullOrEmpty(newToken)
                           ? new()
                           : new ClaimsIdentity(ParseClaimsFromJwt(newToken), "jwt");

        this.principal = new(identity);
        return new(this.principal);
    }

    public async Task<string?> GetTokenAsync()
    {
        if (this.token is null)
            return null;

        if (DateTime.UtcNow >= this.tokenExpiration)
        {
            var result = await this.UpdateTokenAsync().ConfigureAwait(false);

            this.SetToken(result.Token, result.RefreshToken, result.Expiration);
        }

        return this.token;
    }

    public string? GetRefreshToken()
    {
        if (this.token is null)
            return null;

        return this.internalRefreshToken;
    }

    public string SetUserLoggedIn(string? newToken, string? refreshToken, DateTime expiration)
    {
        this.SetToken(newToken, refreshToken, expiration);
        return "/";
    }

    public void LogOutUser()
        => this.SetToken(null, null, DateTime.UtcNow);


    public void SetToken(string? newToken, string? refreshToken, DateTime expiration)
    {
        this.token = newToken;
        this.internalRefreshToken = refreshToken;
        this.tokenExpiration = expiration;

        this.NotifyAuthenticationStateChanged(this.GetAuthenticationStateAsync());
    }

    private static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
    {
        if (string.IsNullOrEmpty(jwt))
            return new List<Claim>();

        var claims = new List<Claim>();
        var payload = jwt.Split('.')[1];
        var jsonBytes = ParseBase64WithoutPadding(payload);

        var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

        if (keyValuePairs == null)
            return new List<Claim>();

        keyValuePairs.TryGetValue(ClaimTypes.Role, out var roles);

        if (roles != null)
        {
            if (roles.ToString()
                    ?.Trim()
                     .StartsWith("[", StringComparison.CurrentCultureIgnoreCase) ?? false)
            {
                var parsedRoles = JsonSerializer.Deserialize<string[]>(roles.ToString()!) ?? Array.Empty<string>();

                claims.AddRange(parsedRoles.Select(parsedRole => new Claim(ClaimTypes.Role, parsedRole)));
            }
            else
            {
                claims.Add(new(ClaimTypes.Role, roles.ToString() ?? string.Empty));
            }

            keyValuePairs.Remove(ClaimTypes.Role);
        }

        claims.AddRange(keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString() ?? string.Empty)));

        return claims;
    }

    private static byte[] ParseBase64WithoutPadding(string base64)
    {
        switch (base64.Length % 4)
        {
            case 2:
                base64 += "==";
                break;
            case 3:
                base64 += "=";
                break;
        }

        return Convert.FromBase64String(base64);
    }

    private async Task<(string? Token, string? RefreshToken, DateTime Expiration)> UpdateTokenAsync()
    {
        var channel = GrpcChannel.ForAddress("http://localhost:5001");
        var client = new AuthGrpc.Auth.AuthClient(channel);

        var identityResult = await client.RefreshTokenAsyncAsync(new RefreshTokenAsyncRequest() {
            ExpiredToken = this.token,
            RefreshToken = this.internalRefreshToken,
        }).ConfigureAwait(false);

        if (identityResult.ErrorMessage is not null)
            return (null, null, DateTime.UtcNow);

        return identityResult.Result switch
        {
            not null => (identityResult.Result.Token, identityResult.Result.RefreshToken, identityResult.Result.ExpirationDate.ToDateTime()),
            _ => (null, null, DateTime.UtcNow),
        };
    }
}