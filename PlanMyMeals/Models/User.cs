#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace PlanMyMeals.Models;


public class User
{
    [Key]
    public int UserId {get; set;}

    [MinLength(3, ErrorMessage = "Username must be longer than 2")]
    public string Username {get; set;}

    [EmailAddress] //checks if email
    [UniqueEmail]
    public string Email {get; set;}

    [DataType(DataType.Password)] //???
    [MinLength(8, ErrorMessage ="Password must be at least 8 char")] //this would be 8 but it would break all my accounts 
    public string Password {get; set;}

    [NotMapped]
    [Compare("Password")] //build-in attribute for comparing two class feilds
    public string ConfirmPassword {get; set;}



    public DateTime CreatedAt {get; set;} = DateTime.Now;
    public DateTime UpdatedAt {get; set;} = DateTime.Now;


}


public class UniqueEmailAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
    	// Though we have Required as a validation, sometimes we make it here anyways
    	// In which case we must first verify the value is not null before we proceed
        if(value == null)
        {
    	    // If it was, return the required error
            return new ValidationResult("Email is required!");
        }
    
    	// This will connect us to our database since we are not in our Controller
        MyContext _context = (MyContext)validationContext.GetService(typeof(MyContext));
        // Check to see if there are any records of this email in our database
    	if(_context.Users.Any(e => e.Email == value.ToString()))
        {
    	    // If yes, throw an error
            return new ValidationResult("Email must be unique!");
        } else {
    	    // If no, proceed
            return ValidationResult.Success;
        }
    }
}


// public class UniquePasswordCharsAttribute : ValidationAttribute
// {
//     protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
//     {
//     	// Though we have Required as a validation, sometimes we make it here anyways
//     	// In which case we must first verify the value is not null before we proceed
//         if(value == null)
//         {
//     	    // If it was, return the required error
//             return new ValidationResult("Email is required!");
//         }
    
//     	// This will connect us to our database since we are not in our Controller
//         MyContext _context = (MyContext)validationContext.GetService(typeof(MyContext));
//         // Check to see if there are any records of this email in our database
//     	if(_context.Users.Any(e => e.Email == value.ToString()))
//         {
//     	    // If yes, throw an error
//             return new ValidationResult("Email must be unique!");
//         } else {
//     	    // If no, proceed
//             return ValidationResult.Success;
//         }
//     }
// }

