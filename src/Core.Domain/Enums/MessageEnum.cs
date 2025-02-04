using System.ComponentModel;

namespace Core.Domain.Enums;

public enum MessageEnum  // Central message catalog  
{  
    [Description("Successful")]
    Success,                // Standard success scenarios  
    CreatedSuccessfully,    // Resource creation  
    UpdatedSuccessfully,    // Update operations  
    DeletedSuccessfully,    // Delete operations  
    
    [Description("Unexpected error")]
    UnexpectedError,
    
    [Description("Username can't be empty.")]
    InvalidUsernameInSignUp,
    
    [Description("Invalid email address.")]
    InvalidEmailFormat,
    
    [Description("Username has already taken.")]
    UsernameHasTaken,
    
    [Description("Email has already registered.")]
    EmailHasBeenRegistered,
    
    [Description("Password lenght must be more than {0} characters.")]
    InvalidPasswordLength,
    
    [Description("Invalid Username or Password.")]
    InvalidUsernameOrPassword
} 