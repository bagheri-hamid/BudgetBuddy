namespace Core.Domain.Enums;

public enum MessageEnum  // Central message catalog  
{  
    Success,                // Standard success scenarios  
    CreatedSuccessfully,    // Resource creation  
    UpdatedSuccessfully,    // Update operations  
    DeletedSuccessfully,    // Delete operations  
    InvalidPassword,        // Auth-specific errors  
    UnexpectedError         // Fallback exception  
} 