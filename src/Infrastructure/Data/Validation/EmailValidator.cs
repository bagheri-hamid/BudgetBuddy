using System.Text.RegularExpressions;
using Core.Application.Interfaces;

namespace Infrastructure.Data.Validation;

public partial class EmailValidator : IEmailValidator, IScopedDependency
{
    private readonly Regex _emailRegex = MyRegex();

    public bool IsValid(string email)
    {
        if (string.IsNullOrWhiteSpace(email)) return false;
        return _emailRegex.IsMatch(email) && email.Length <= 256;
    }

    [GeneratedRegex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase | RegexOptions.Compiled, "en-US")]
    private static partial Regex MyRegex();
}