#pragma warning disable CS8618

using System.ComponentModel.DataAnnotations;
namespace PlanMyMeals.Models;

public class Meal
{
    [Key]
    public int MealId {get; set;}

    public string MealName {get; set;}
    public int MealCalories {get; set;}
    public int MealProtein {get; set;}
    public int MealCarbs {get; set;}
    public int MealFat {get; set;}

    public List<Ingredient> IngredientList {get; set;} = []; //Check this ASAP

    public DateTime CreatedAt {get; set;} = DateTime.Now;
    public DateTime UpdatedAt {get; set;} = DateTime.Now;

}