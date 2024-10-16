using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using PlanMyMeals.Models;
using Microsoft.EntityFrameworkCore;

namespace PlanMyMeals.Controllers;

public class UserController : Controller
{
    private readonly ILogger<UserController> _logger;
    private MyContext _context;

    public UserController(ILogger<UserController> logger, MyContext context)
    {
        _logger = logger;
        _context = context;
    }


    //------------------------------- view routes ------------------------------------


    [HttpGet("login")]
    public IActionResult UserIndex() 
    {
        return View("UserIndex");
    }

    [HttpGet("user/goals")]
    public IActionResult Goals()
    {
        int? UserId = HttpContext.Session.GetInt32("UserId");
        Console.WriteLine("UserId = " + UserId);
        if (UserId == null)
        {
            Console.WriteLine("No logged in user, redirect to login");
            return RedirectToAction("UserIndex");
        }
        else
        {
            Console.WriteLine("User logged in: pulling goal info...");
            User user = _context.Users.FirstOrDefault(u => u.UserId == UserId);
            return View("Goals", user);
        }
        //return View("Goals");
    }


    //------------------------------- CRUD routes ------------------------------------

    [HttpPost("user/create")]
    public IActionResult RegisterUser(User newUser)
    {
        if (ModelState.IsValid)
        {
            Console.WriteLine("New user is valid: adding user");

            //create and use PasswordHasher Obj
            PasswordHasher<User> hasher = new();
            newUser.Password = hasher.HashPassword(newUser, newUser.Password); //overriding newUser's password w a hashed vertion

            //add to db
            _context.Users.Add(newUser);
            _context.SaveChanges();

            //login newUser
            HttpContext.Session.SetInt32("UserId", newUser.UserId);
            HttpContext.Session.SetString("Username", newUser.Username);

            //Session check
            int? userIdFromSession = HttpContext.Session.GetInt32("UserId");
            Console.WriteLine("Login Success: userIdFromSession = " + userIdFromSession);

            return RedirectToAction("Index", "Home");
        }
        else
        {
            return View("UserIndex");
        }
    }




    //------------------------------- Action routes ------------------------------------

    [HttpPost("user/loginSuccess")]
    public IActionResult LoginUser(LogUser user)
    {
        if (ModelState.IsValid) {
            //get user from db by email
            User? userInDb = _context.Users.SingleOrDefault(u => u.Email == user.LogEmail);

            //if user not in db addModelError
            if (userInDb == null) {
                ModelState.AddModelError("LogEmail", "Email does not exist or is incorect");
                return View("Index");
            }
            //initialise password hasher obj
            PasswordHasher<LogUser> Hasher = new PasswordHasher<LogUser>();
            //VerityHashedPassword
            var result = Hasher.VerifyHashedPassword(user, userInDb.Password, user.LogPassword);
            if (result == 0) {
                ModelState.AddModelError("LogPassword", "Password is incorect");
                return View("Index");
            }
            else {
                HttpContext.Session.SetInt32("UserId", userInDb.UserId);
                HttpContext.Session.SetString("Username", userInDb.Username);

                //get user id from session and console log it to confirm 
                int? userIdFromSession = HttpContext.Session.GetInt32("UserId");
                Console.WriteLine("Login Success: userIdFromSession = " + userIdFromSession);

                return RedirectToAction("Index", "Home");
            }
        }
        else
        {
            Console.WriteLine("LogUser invalid");
            return View("Index");
        }




        }







    //wtf is this:
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }


}