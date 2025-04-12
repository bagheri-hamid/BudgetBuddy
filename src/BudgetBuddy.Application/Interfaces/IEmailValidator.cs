namespace BudgetBuddy.Application.Interfaces;

public interface IEmailValidator
{
    bool IsValid(string email);
}