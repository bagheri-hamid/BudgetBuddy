using Core.Domain.Extensions;

namespace Core.Domain.Exceptions;

/// <summary>
/// Exception for invalid email format.
/// </summary>
public class InvalidEmailFormatException : DomainException
{
    public InvalidEmailFormatException() : base(Enums.MessageEnum.InvalidEmailFormat.GetDescription())
    {
        StatusCode = 400;
        MessageEnum = Enums.MessageEnum.InvalidEmailFormat;
    }
}

/// <summary>
/// Exception for empty or white space username.
/// </summary>
public class InvalidUsernameInSignUpException : DomainException
{
    public InvalidUsernameInSignUpException() : base(Enums.MessageEnum.InvalidUsernameInSignUp.GetDescription())
    {
        StatusCode = 400;
        MessageEnum = Enums.MessageEnum.InvalidUsernameInSignUp;
    }
}

/// <summary>
/// Thrown when a username is already in use, returning HTTP 400 with <see cref="Enums.MessageEnum.UsernameHasTaken"/>.
/// </summary>
public class UsernameHasAlreadyTakenException : DomainException
{
    public UsernameHasAlreadyTakenException() : base(Enums.MessageEnum.UsernameHasTaken.GetDescription())
    {
        StatusCode = 409;
        MessageEnum = Enums.MessageEnum.UsernameHasTaken;
    }
}

/// <summary>
/// Thrown when an email address is already registered, returning HTTP 400 with <see cref="Enums.MessageEnum.EmailHasBeenRegistered"/>.
/// </summary>
public class EmailHasBeenRegisteredException : DomainException
{
    public EmailHasBeenRegisteredException() : base(Enums.MessageEnum.EmailHasBeenRegistered.GetDescription())
    {
        StatusCode = 409;
        MessageEnum = Enums.MessageEnum.EmailHasBeenRegistered;
    }
}

/// <summary>
/// Thrown when password length is invalid, returning HTTP 400 with formatted <see cref="Enums.MessageEnum.InvalidPasswordLength"/> (requires {minLength} characters).
/// </summary>
public class InvalidPasswordLengthException : DomainException
{
    public InvalidPasswordLengthException(int minLength) : base(string.Format(Enums.MessageEnum.InvalidPasswordLength.GetDescription(), minLength))
    {
        StatusCode = 400;
        MessageEnum = Enums.MessageEnum.InvalidPasswordLength;
    }
}

/// <summary>
/// Thrown when authentication fails due to invalid credentials, returning HTTP 400 with <see cref="Enums.MessageEnum.InvalidUsernameOrPassword"/>.
/// </summary>
public class InvalidUsernameOrPasswordException : DomainException
{
    public InvalidUsernameOrPasswordException() : base(Enums.MessageEnum.InvalidUsernameOrPassword.GetDescription())
    {
        StatusCode = 400; // security best practices is 400
        MessageEnum = Enums.MessageEnum.InvalidUsernameOrPassword;
    }
}