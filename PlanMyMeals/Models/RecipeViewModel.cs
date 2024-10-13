using PlanMyMeals.Models;

public class RecipeViewModel
{
    public Meal thisMeal { get; set; }
    public List<Ingredient> allIngredients { get; set; }
    public List<MealIngredient> mealsIngredients { get; set; }






    //functions
    public List<MealIngredient> addIngToMeal(Meal m, List<MealIngredient> mList, Ingredient mealIng)
    {

        MealIngredient newIng = new MealIngredient();
        newIng.MealId = m.MealId;
        newIng.IngredientId = mealIng.IngredientId;

        mList.Add(newIng);
        Console.WriteLine(newIng);
        return mList;
    }


}