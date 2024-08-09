using Microsoft.EntityFrameworkCore;
using PlanMyMeals.Models;




var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//session setup
builder.Services.AddHttpContextAccessor();  
builder.Services.AddSession();  



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
app.UseSession(); //   ----------------------   session setup

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();



//-----------
// private static HttpClient sharedClient = new()
// {
//     BaseAddress = new Uri("https://jsonplaceholder.typicode.com")
// };
//-----------