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


    public DateTime CreatedAt {get; set;} = DateTime.Now;
    public DateTime UpdatedAt {get; set;} = DateTime.Now;
    
}

public class IngredientJson
{
        public string name { get; set; }
        public string image { get; set; }
        public int id { get; set; }
        public string aisle { get; set; }
        public List<string> possibleUnits { get; set; }
}