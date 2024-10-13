#pragma warning disable CS8618

using System.ComponentModel.DataAnnotations;
namespace PlanMyMeals.Models;

public class MealIngredient
{
    [Key]
    public int Id { get; set; }

    [Required]
    //forgin keys
    public int MealId { get; set; }
    public int IngredientId { get;set; }



    public int amount { get; set; }

    




    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

}