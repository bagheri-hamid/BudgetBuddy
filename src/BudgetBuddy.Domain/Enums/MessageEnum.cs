﻿using System.ComponentModel;

namespace BudgetBuddy.Domain.Enums;

public enum MessageEnum  // Central message catalog  
{  
    [Description("Successful")]
    Success,
    
    [Description("Created successfully")] 
    CreatedSuccessfully,  
    
    [Description("Updated successfully")]
    UpdatedSuccessfully,
    
    [Description("Deleted successfully")]
    DeletedSuccessfully,  
    
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
    
    [Description("Password length must be more than {0} characters.")]
    InvalidPasswordLength,
    
    [Description("Invalid Username or Password.")]
    InvalidUsernameOrPassword,
    
    [Description("{0} can't be empty.")]
    FiledIsEmpty,
    
    [Description("Parent category not found!")]
    ParentCategoryNotFound,
    
    [Description("{0} not found!")]
    ObjectNotFound,
    
    [Description("Value of {0} is invalid.")]
    ValueIsInvalid,
    
    [Description("Value must be greater than 0!")]
    CanNotBeLessThanZero,
    
    [Description("Invalid date!")]
    InvalidDate,
    
    [Description("One or more validation errors occurred!")]
    ValidationError, 
} 