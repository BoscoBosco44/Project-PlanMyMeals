using System.Text.Json;
using PlanMyMeals.Models;




public class SpoonacularApi {

    // public SpoonacularApi() { // constructor

    // }

    private static HttpClient SpoonClient = new()
    {
        BaseAddress = new Uri("https://spoonacular-recipe-food-nutrition-v1.p.rapidapi.com"),
        DefaultRequestHeaders = {
            { "x-rapidapi-key", "16fe5f394dmsh681dffdca8ec923p105a46jsnb348004e73a3" },
            { "x-rapidapi-host", "spoonacular-recipe-food-nutrition-v1.p.rapidapi.com" },
        }
    };



    public async Task<List<IngredientJson>> GetIngInfoAsync(string input) 
    {
        Console.WriteLine("Getting Spoons");
        using HttpResponseMessage response = await SpoonClient.GetAsync($"food/ingredients/autocomplete?query={input}&number=5&metaInformation=true");

        // response.EnsureSuccessStatusCode().WriteRequestToConsole();

        var jsonResponse = await response.Content.ReadAsStringAsync();
        Console.WriteLine(jsonResponse);

        var ingredients = JsonSerializer.Deserialize<List<IngredientJson>>(jsonResponse);

        return ingredients;
    }


    public Meal ConvertIngredientJsonListToMeal(List<IngredientJson> ings, Meal thisMeal)
    {
        //get meal from session
        //convert all IngredientJson to ingredient
            //check ingredient name in DB
            //if name exists, pull the ingredient and add it to a list
            //else create a new ingredient, save it to DB, and add it to the list

        //add all ingredients to IngredientList in the meal in session

        foreach(IngredientJson ij in ings)
        {
            Console.WriteLine(ij.name);

            Ingredient ing = new Ingredient();
            ing.IngredientId = ij.id;
            ing.IngredientName = ij.name;
            
            //check if ing exists in DB

            thisMeal.IngredientList.Add(ing);
        };

        return thisMeal;
    }




}

// static class HttpResponseMessageExtensions
// {
//         internal static void WriteRequestToConsole(this HttpResponseMessage response)
//         {
//             if (response is null)
//             {
//                 Console.Write("response failed or is null");
//                 return;
//             }

//             var request = response.RequestMessage;
//             Console.Write($"Response Messages:");
//             Console.Write($"{response?.RequestMessage} ");
//             Console.Write($"{request?.Method} ");
//             Console.Write($"{request?.RequestUri} ");
//             Console.WriteLine($"HTTP/{request?.Version}");        
//         }
// }



