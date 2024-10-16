#pragma warning disable CS8618

using System.ComponentModel.DataAnnotations;
namespace PlanMyMeals.Models;

public class Ingredient
{
    [Key]
    public int IngredientId {get; set;}

    public string Name {get; set;}
    public int ProteinPercent { get; set; }
    public int CarbPercent { get; set; }
    public int FatPercent { get; set; }
    public int CaloriesPer100Gram { get; set; }
    





    public DateTime CreatedAt {get; set;} = DateTime.Now;
    public DateTime UpdatedAt {get; set;} = DateTime.Now;
    
}
