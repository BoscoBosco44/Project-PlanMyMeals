using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PlanMyMeals.Models;
using Microsoft.EntityFrameworkCore; 

namespace PlanMyMeals.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private MyContext _context;

    public HomeController(ILogger<HomeController> logger, MyContext context)
    {
        _logger = logger;
        _context = context;
    }

    //private static HttpClient sharedClient = new()
    //{
    //    //-------
    //    // BaseAddress = new Uri("https://jsonplaceholder.typicode.com")
    //    //-------
    //    BaseAddress = new Uri("https://spoonacular-recipe-food-nutrition-v1.p.rapidapi.com"),
    //    DefaultRequestHeaders =
    //    {
    //        { "x-rapidapi-key", "16fe5f394dmsh681dffdca8ec923p105a46jsnb348004e73a3" },
    //        { "x-rapidapi-host", "spoonacular-recipe-food-nutrition-v1.p.rapidapi.com" }
    //    }
    //};

//------------------------------- view routes ------------------------------------


    public IActionResult Index() // Meal Plan Page
    {
        return View("MealPlan");
    }

    public IActionResult Recipes(int mealId)
    {
        Console.WriteLine("-------------------- recipe/mealId --------------------");

        RecipeViewModel mealIngObj = new RecipeViewModel();

        Meal thisMeal = _context.Meals.FirstOrDefault(meal => meal.MealId == mealId);


        //get all ings and send down
        List<Ingredient> allIngs = _context.Ingredients.ToList();

        //package into mealIngObj send to view
        mealIngObj.thisMeal = thisMeal;
        mealIngObj.allIngredients = allIngs;
        mealIngObj.mealsIngredients = new List<MealIngredient>();


        return View("Recipes", mealIngObj);

    }

    public IActionResult ViewMeals()
    {
        Console.WriteLine("-------------------- entered MealsPage --------------------");

        RecipeViewModel rvm = new RecipeViewModel();

        Meal thisMeal = new Meal();

        List<Meal> allMeals = _context.Meals.ToList();

        rvm.thisMeal = thisMeal;
        rvm.allMeals = allMeals;


        return View("MealsPage", rvm);
    }


    public IActionResult AddIngredient()
    {
        return View();
    }



    //------------------------------- Modal??? ------------------------------------
    public IActionResult GetModalContent(int IngredientId, int MealId)
    {
        
        MealIngredient mealIng = new MealIngredient();

        mealIng.MealId = MealId;
        mealIng.IngredientId = IngredientId;

        return PartialView("_AddIngredientModal", mealIng);
    }



    //------------------------------- CRUD routes ------------------------------------

    [HttpPost("ingredient/create")]
    public IActionResult CreateIngredient(Ingredient newIng)
    {
        if (ModelState.IsValid)
        {
            _context.Add(newIng);
            _context.SaveChanges();

            return RedirectToAction("Recipes");
        }
        else
        {
            return View("AddIngredient");
        }
    }

    [HttpPost("mealIngredient/create")]
    public IActionResult CreateMealIngredient(RecipeViewModel rvm) {

        Console.WriteLine("-------------------- entered create meal ingredient --------------------");
        Console.WriteLine("ingId: " + rvm.ingId);
        Console.WriteLine("mealId: " + rvm.mealId); //meal has not been created yet
        Console.WriteLine("thisMeal.Name: " + rvm.thisMeal.Name); 
        Console.WriteLine("amount: " + rvm.amount);

        if (rvm.mealId == 0)
        { //if a meal obj has not been created for this meal create one
            Meal tempMeal = new Meal();
            tempMeal.Name = rvm.thisMeal.Name;
            _context.Meals.Add(tempMeal);
            _context.SaveChanges();
        }

        //get meal obj (so we can get the mealID)
        Meal meal = _context.Meals.FirstOrDefault(m => m.Name == rvm.thisMeal.Name);

        //create mealIngredient Obj and add ingId, mealId, and amount
        MealIngredient mealIngObj = new MealIngredient();
        mealIngObj.MealId = meal.MealId;
        mealIngObj.IngredientId = rvm.ingId;
        mealIngObj.amount = rvm.amount;

        //add to db
        _context.MealIngredients.Add(mealIngObj);
        _context.SaveChanges();

        //get all MealIngredients w a matching mealId
        List<MealIngredient> mealIngList = _context.MealIngredients.Where(mi => mi.MealId == meal.MealId).ToList();
        rvm.mealsIngredients = mealIngList;

        return RedirectToAction("Recipes", rvm);
    }

    [HttpPost("meal/create")]
    public IActionResult CreateMeal(RecipeViewModel rvm)
    {
        Meal meal = rvm.thisMeal;
        Console.WriteLine("-------------- meal.name ---------------");
        Console.WriteLine(meal.Name);
        if (!ModelState.IsValid)
        {
            _context.Meals.Add(meal);
            _context.SaveChanges();

            return RedirectToAction("MealsPage");
        }
        else
        {
            return View("MealsPage");
        }

    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
