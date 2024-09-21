#pragma warning disable CS8618

using System.ComponentModel.DataAnnotations;
namespace PlanMyMeals.Models;

public class Ingredient
{
    [Key]
    public int IngredientId {get; set;}

    public string IngredientName {get; set;}
    public int IngredientAmount {get; set;}
    public bool IngredientIsGrams {get; set;}
    public int IngredientRange {get; set;}


    public int IngCaloriesPerGram {get; set;}
    public int IngProteinPerGram {get; set;}
    public int IngCarbsPerGram {get; set;}
    public int IngFatPerGram {get; set;}


    public DateTime CreatedAt {get; set;} = DateTime.Now;
    public DateTime UpdatedAt {get; set;} = DateTime.Now;
    
}
