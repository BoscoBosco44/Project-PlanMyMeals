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

    public IActionResult Recipes()
    {
        RecipeViewModel mealIngObj = new RecipeViewModel();

        Meal thisMeal = new Meal();


        //get all ings and send down
        List<Ingredient> allIngs = _context.Ingredients.ToList();

        //package into mealIngObj send to view
        mealIngObj.thisMeal = thisMeal;
        mealIngObj.allIngredients = allIngs;
        mealIngObj.mealsIngredients = new List<MealIngredient>();


        return View(mealIngObj);

    }

    public IActionResult Goals()
    {
        return View();
    }

    public IActionResult AddIngredient()
    {
        return View();
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

        Console.WriteLine("entered create meal--------------------");
        Console.WriteLine("entered create meal");
        Console.WriteLine("entered create meal");


        return View("Recipes", rvm);
    }



    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
