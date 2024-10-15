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
}