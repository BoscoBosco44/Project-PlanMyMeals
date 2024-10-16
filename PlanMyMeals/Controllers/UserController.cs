using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
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
    public IActionResult Login() 
    {
        return View("Index");
    }







    //------------------------------- CRUD routes ------------------------------------

    [HttpPost("user/create")]
    public IActionResult RegisterUser(User newUser)
    {
        if (ModelState.IsValid)
        {
            Console.WriteLine("New user is valid: adding user");


            return View("Index", "Home");
        }
        else
        {
            return View("Index");
        }
    }




    //------------------------------- Action routes ------------------------------------

    [HttpPost("user/loginSuccess")]
    public IActionResult LoginUser(LogUser user)
    {
        if (ModelState.IsValid) { 
            //get user from db by email\
            //if user not in db addModelError
            //initialise password hasher obj
            //VerityHashedPassword
            //if result == 0 AddModelError
            //else add UserId and username to sesstion
            //get user id from session and console log it to confirm 
            //redirectToAction("Index", "Home")
        }




    }



}