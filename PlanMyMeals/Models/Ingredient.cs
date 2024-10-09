#pragma warning disable CS8618

using System.ComponentModel.DataAnnotations;
namespace PlanMyMeals.Models;

public class Ingredient
{
    [Key]
    public int IngredientId {get; set;}

    public string Name {get; set;}
    public int ProteinPerGram { get; set; }
    public int CarbPerGram { get; set; }
    public int FatPerGram { get; set; }
    public int CaloriesPerGram { get; set; }






    public DateTime CreatedAt {get; set;} = DateTime.Now;
    public DateTime UpdatedAt {get; set;} = DateTime.Now;
    
}
