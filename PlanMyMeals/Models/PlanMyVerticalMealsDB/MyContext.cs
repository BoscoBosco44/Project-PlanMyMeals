#pragma warning disable CS8618

using Microsoft.EntityFrameworkCore;
namespace PlanMyMeals.Models;


public class MyContext : DbContext
{
    public MyContext(DbContextOptions options) : base(options) {}

    public DbSet<Meal> Meals {get; set;}
    public DbSet<Ingredient> Ingredients {get; set;}
    public DbSet<MealIngredient> MealIngredients {get; set;}
    public DbSet<User> Users {get; set;}

}