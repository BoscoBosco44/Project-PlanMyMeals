namespace PlanMyMeals.Models;


public class IngredientJson
{
        public string name { get; set; }
        public string image { get; set; }
        public int id { get; set; }
        public string aisle { get; set; }
        public List<string> possibleUnits { get; set; }
}