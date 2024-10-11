#pragma warning disable CS8618

using System.ComponentModel.DataAnnotations;
namespace PlanMyMeals.Models;

public class MealIngredients
{
    [Required]
    public int MealId { get; set; }

    public int IngredientId { get;set; }

    public int amount { get; set; }

}