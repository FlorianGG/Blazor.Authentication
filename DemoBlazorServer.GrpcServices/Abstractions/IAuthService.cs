namespace DemoBlazorServer.GrpcServices.Abstractions;

using Grpc.Core;
using AuthGrpc;
using global::Grpc.Core;

public interface IAuthService
{
    Task<LoginAsyncReply> LoginAsync(LoginAsyncRequest request, ServerCallContext context);

    Task<RefreshTokenAsyncReply> RefreshTokenAsync(RefreshTokenAsyncRequest request, ServerCallContext context);
}