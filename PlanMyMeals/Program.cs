using Microsoft.EntityFrameworkCore;
using PlanMyMeals.Models;

using System.Net.Http.Headers;
using System.Text.Json;
using HttpClient client = new();
client.DefaultRequestHeaders.Accept.Clear();
client.DefaultRequestHeaders.Accept.Add(
    new MediaTypeWithQualityHeaderValue("application/json"));




var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();



var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<MyContext>(options =>
{
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});




var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();


static async Task<IngredientInfo> GetIngredientInfoAsync(HttpClient client, string url)
{
    await using Stream stream =
    await client.GetStreamAsync(url);
    IngredientInfo? Result =
        await JsonSerializer.DeserializeAsync<IngredientInfo>(stream);
    return Result;
}