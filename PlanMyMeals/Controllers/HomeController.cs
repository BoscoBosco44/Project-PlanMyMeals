using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PlanMyMeals.Models;

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

    private static HttpClient sharedClient = new()
    {
        //-------
        // BaseAddress = new Uri("https://jsonplaceholder.typicode.com")
        //-------
        // BaseAddress = new Uri("https://spoonacular-recipe-food-nutrition-v1.p.rapidapi.com/food/ingredients"),
        BaseAddress = new Uri("https://spoonacular-recipe-food-nutrition-v1.p.rapidapi.com"),
        DefaultRequestHeaders =
        {
            // { "x-rapidapi-key", "1e623056ca1c403d8a79ae81b15135d5" },
            { "x-rapidapi-key", "16fe5f394dmsh681dffdca8ec923p105a46jsnb348004e73a3" },
            { "x-rapidapi-host", "spoonacular-recipe-food-nutrition-v1.p.rapidapi.com" }
        }
    };

//------------------------------- view routes ------------------------------------


    public IActionResult Index() // Meal Plan Page
    {
        return View("MealPlan");
    }

    public IActionResult Recipes()
    {
        SpoonacularApi.GetIngInfoAsync(sharedClient);
        // SpoonacularApi.GetAsync(sharedClient);

        return View();
    }

    public IActionResult Goals()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
