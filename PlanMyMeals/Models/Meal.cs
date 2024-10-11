#pragma warning disable CS8618

using System.ComponentModel.DataAnnotations;
namespace PlanMyMeals.Models;

public class Meal
{
    [Key]
    public int MealId {get; set;}

    public string Name {get; set;}

    public string Instructions { get; set; }


    public string MealImgLocation {get; set;} 

    public List<Ingredient> IngredientList {get; set;} = []; // Ask paul




    public DateTime CreatedAt {get; set;} = DateTime.Now;
    public DateTime UpdatedAt {get; set;} = DateTime.Now;

}