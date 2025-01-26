namespace Core.Application.Interfaces;

public interface IEmailValidator
{
    bool IsValid(string email);
}