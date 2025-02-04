namespace Core.Domain.Dtos.Auth;

public record SignUpResponse(Guid UserId, string JwtToken);