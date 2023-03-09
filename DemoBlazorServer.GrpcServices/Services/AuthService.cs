namespace DemoBlazorServer.GrpcServices.Services;

using Api.Identity.Business.Abstractions;
using Api.Identity.Business.Models;
using AuthGrpc;
using DemoBlazorServer.GrpcServices;
using DemoBlazorServer.GrpcServices.Abstractions;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

public class AuthService : Auth.AuthBase, IAuthService
{
    private readonly ILogger<AuthService> logger;
    private readonly IAuthenticationBusiness authenticationBusiness;

    public AuthService(ILogger<AuthService> logger, IAuthenticationBusiness authenticationBusiness)
    {
        this.logger = logger;
        this.authenticationBusiness = authenticationBusiness;
    }

    public override async Task<LoginAsyncReply> LoginAsync(LoginAsyncRequest request, ServerCallContext context)
    {
        LoginRequest loginRequest = new LoginRequest()
        {
            Login = request.Login,
            Password = request.Password,
        };
        var result = await this.authenticationBusiness.LoginAsync(loginRequest);

        if (result == null)
        {
            return new LoginAsyncReply()
            {
                ErrorMessage = "Error",
            };
        }

        return new LoginAsyncReply
        {
            Result = new LoginAsync()
            {
                Token = result.Token,
                RefreshToken = result.RefreshToken,
                ExpirationDate = Timestamp.FromDateTimeOffset(result.ExpirationDate),
            },
        };
    }

    public override async Task<RefreshTokenAsyncReply> RefreshTokenAsync(RefreshTokenAsyncRequest request, ServerCallContext context)
    {
        var result = await this.authenticationBusiness.RefreshTokenAsync(request.RefreshToken, request.ExpiredToken);

        if (result == null)
        {
            return new RefreshTokenAsyncReply()
            {
                ErrorMessage = "Error",
            };
        }

        return new RefreshTokenAsyncReply
        {
            Result = new RefreshTokenAsync()
            {
                Token = result.Token,
                ExpirationDate = Timestamp.FromDateTime(result.ExpirationDate),
                RefreshToken = result.RefreshToken,
            },
        };
    }
}
