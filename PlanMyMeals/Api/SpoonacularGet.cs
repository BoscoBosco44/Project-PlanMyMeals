// // HttpClient lifecycle management best practices:


public record class Ing(
    string? Name = null,
    int? Id = null

);


public class SpoonacularApi {
    private static HttpClient SpoonClient = new()
    {
        BaseAddress = new Uri("https://spoonacular-recipe-food-nutrition-v1.p.rapidapi.com/food/ingredients"),
        DefaultRequestHeaders = {
            { "x-rapidapi-key", "16fe5f394dmsh681dffdca8ec923p105a46jsnb348004e73a3" },
            { "x-rapidapi-host", "spoonacular-recipe-food-nutrition-v1.p.rapidapi.com" },
        }
    };


    // public static async Task GetAsync(HttpClient httpClient)
    // {
    //     using HttpResponseMessage response = await httpClient.GetAsync("todos/3");
        
    //     response.EnsureSuccessStatusCode()
    //         .WriteRequestToConsole();
        
    //     var jsonResponse = await response.Content.ReadAsStringAsync();
    //     Console.WriteLine($"{jsonResponse}\n");

    //     // Expected output:
    //     //   GET https://jsonplaceholder.typicode.com/todos/3 HTTP/1.1
    //     //   {
    //     //     "userId": 1,
    //     //     "id": 3,
    //     //     "title": "fugiat veniam minus",
    //     //     "completed": false
    //     //   }
    // }

    public static async Task GetIngInfoAsync(HttpClient httpClient) 
    // public static async Task GetIngInfoAsync(String name) 
    {
        Console.WriteLine("Getting Spoons");
        // using HttpResponseMessage response = await SpoonClient.GetAsync($"autocomplete?query=egg");
        using HttpResponseMessage response = await httpClient.GetAsync($"food/ingredients/autocomplete?query=egg&number=5&metaInformation=true");

        response.EnsureSuccessStatusCode().WriteRequestToConsole();

        // var jsonResponse = await response.Content.ReadAsByteArrayAsync();
        var jsonResponse = await response.Content.ReadAsStringAsync();
        Console.WriteLine($"{jsonResponse}" + "spoon responce");
    }

}

static class HttpResponseMessageExtensions
{
        internal static void WriteRequestToConsole(this HttpResponseMessage response)
        {
            if (response is null)
            {
                Console.Write("response failed or is null");
                return;
            }

            var request = response.RequestMessage;
            Console.Write($"Response Messages:");
            Console.Write($"{response?.RequestMessage} ");
            Console.Write($"{request?.Method} ");
            Console.Write($"{request?.RequestUri} ");
            Console.WriteLine($"HTTP/{request?.Version}");        
        }
}



