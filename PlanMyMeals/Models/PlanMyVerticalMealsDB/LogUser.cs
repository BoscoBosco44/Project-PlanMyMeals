#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
namespace PlanMyMeals.Models;


public class LogUser
{
    [EmailAddress] //checks if email
    [Display(Name = "Email")]
    public string LogEmail {get; set;}

    [DataType(DataType.Password)] //???
    [Display(Name = "Password")]
    [MinLength(5, ErrorMessage ="Password must be at least 5 char")]
    public string LogPassword {get; set;}
}



//this is in no way connnected to the DB