namespace Core.Domain.Responses.User;

public class SignUpResponse
{
    public bool IsSuccess { get; set; }
    public required string Message { get; set; }
    public Guid UserId { get; set; }
    public string? JwtToken { get; set; }
    public long? SessionId { get; set; }
}