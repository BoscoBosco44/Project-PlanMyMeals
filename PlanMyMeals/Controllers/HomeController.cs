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



//------------------------------- view routes ------------------------------------


    public IActionResult Index() // Meal Plan Page
    {
        return View("MealPlan");
    }

    public IActionResult AddIngredient()
    {
        return View();
    }

    //-----

    public IActionResult ViewAlgoDevPg() // Meal Plan Page
    {

        int? UserId = HttpContext.Session.GetInt32("UserId");
        Console.WriteLine("UserId = " + UserId);
        if (UserId == null)
        {
            Console.WriteLine("No logged in user, redirect to login");
            return RedirectToAction("UserIndex", "User");
        }
        else
        {
            Console.WriteLine("User logged in: pulling goal info...");
            User user = _context.Users.FirstOrDefault(u => u.UserId == UserId);


            AlgoViewModel avm = new AlgoViewModel();
            avm.thisUser = user;
            avm.allMeals = _context.Meals.ToList();

            return View("AlgoDevPage", avm);
        }


    }
    
//-----

    public IActionResult Recipes(int mealId)
    {
        Console.WriteLine("-------------------- recipe/mealId --------------------");

        //create rvm to send all info back to build recipes page
        RecipeViewModel rvm = new RecipeViewModel();
        //meal
        rvm.thisMeal = _context.Meals.FirstOrDefault(m => m.MealId == mealId);
        //get list of all ingredients
        List<Ingredient> allIngs = _context.Ingredients.ToList();
        rvm.allIngredients = allIngs;
        //ingredients in thisMeal
        List<MealIngredient> mealIngList = _context.MealIngredients.Where(m => m.MealId == mealId).ToList();
        rvm.mealsIngredients = mealIngList;


        return View("Recipes", rvm);

    }

//-----

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

            return View("AddIngredient");
        }
        else
        {
            return View("AddIngredient");
        }
    }

    [HttpPost("mealIngredient/create")]
    public IActionResult CreateMealIngredient(MealIngredient mealIng)
    {

        Console.WriteLine("-------------------- entered create meal ingredient --------------------");
        Console.WriteLine("IngredientId: " + mealIng.IngredientId);
        Console.WriteLine("MealId: " + mealIng.MealId); 
        Console.WriteLine("ing amout: " + mealIng.amount);

        //create mealIngredient Obj and add ingId, mealId, and amount
        MealIngredient mealIngObj = new MealIngredient();
        mealIngObj.MealId = mealIng.MealId;
        mealIngObj.IngredientId = mealIng.IngredientId;
        mealIngObj.amount = mealIng.amount;

        //add to db
        _context.MealIngredients.Add(mealIngObj);
        _context.SaveChanges();


        ////create rvm to send all info back to build recipes page
        //RecipeViewModel rvm = new RecipeViewModel();
        ////meal
        //rvm.thisMeal = _context.Meals.FirstOrDefault(m => m.MealId == mealIng.MealId);
        ////get list of all ingredients
        //List<Ingredient> allIngs = _context.Ingredients.ToList();
        //rvm.allIngredients = allIngs;
        ////ingredients in thisMeal
        //List<MealIngredient> mealIngList = _context.MealIngredients.Where(m => m.MealId == mealIng.MealId).ToList();
        //rvm.mealsIngredients = mealIngList;
        //return View("Recipes", rvm);

        return RedirectToAction("Recipes", new { mealId = mealIng.MealId });
    }



    [HttpPost("meal/create")]
    public IActionResult CreateMeal(RecipeViewModel rvm)
    {
        Meal meal = rvm.thisMeal;
        Console.WriteLine("-------------- meal / create ---------------");
        Console.WriteLine(meal.Name);
        if (!ModelState.IsValid)
        {
            _context.Meals.Add(meal);
            _context.SaveChanges();
            Console.WriteLine("Meal create success");

            return RedirectToAction("ViewMeals");
        }
        else
        {
            return View("");
        }

    }





    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
