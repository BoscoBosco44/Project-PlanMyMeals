// // HttpClient lifecycle management best practices:



public class SpoonacularApi {
    // private static HttpClient sharedClient = new()
    // {
    //     BaseAddress = new Uri("https://jsonplaceholder.typicode.com")
    // };


    public static async Task GetAsync(HttpClient httpClient)
    {
        using HttpResponseMessage response = await httpClient.GetAsync("todos/3");
        
        response.EnsureSuccessStatusCode()
            .WriteRequestToConsole();
        
        var jsonResponse = await response.Content.ReadAsStringAsync();
        Console.WriteLine($"{jsonResponse}\n");

        // Expected output:
        //   GET https://jsonplaceholder.typicode.com/todos/3 HTTP/1.1
        //   {
        //     "userId": 1,
        //     "id": 3,
        //     "title": "fugiat veniam minus",
        //     "completed": false
        //   }
    }
}


static class HttpResponseMessageExtensions
{
        internal static void WriteRequestToConsole(this HttpResponseMessage response)
        {
            if (response is null)
            {
                return;
            }

            var request = response.RequestMessage;
            Console.Write($"{request?.Method} ");
            Console.Write($"{request?.RequestUri} ");
            Console.WriteLine($"HTTP/{request?.Version}");        
        }
}



