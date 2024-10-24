#pragma warning disable CS8618

using System.ComponentModel.DataAnnotations;
namespace PlanMyMeals.Models;

public class Meal
{
    [Key]
    public int MealId {get; set;}

    public string Name {get; set;}

    public string? Instructions { get; set; }


    public string? MealImgLocation {get; set;}




    //nav props
    public List<MealIngredient> MealIngredientsList {get; set;} //should be a list of IngredientIDs



    public DateTime CreatedAt {get; set;} = DateTime.Now;
    public DateTime UpdatedAt {get; set;} = DateTime.Now;

}