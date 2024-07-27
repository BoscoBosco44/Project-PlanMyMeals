using System.Net.Http.Headers;
var client = new HttpClient();
var request = new HttpRequestMessage
{
	Method = HttpMethod.Get,
	RequestUri = new Uri("https://spoonacular-recipe-food-nutrition-v1.p.rapidapi.com/food/ingredients/search?query=yogurt&addChildren=true&minProteinPercent=5&maxProteinPercent=50&minFatPercent=1&maxFatPercent=10&minCarbsPercent=5&maxCarbsPercent=30&metaInformation=false&intolerances=egg&sort=calories&sortDirection=asc&offset=0&number=10"),
	Headers =
	{
		{ "x-rapidapi-key", "16fe5f394dmsh681dffdca8ec923p105a46jsnb348004e73a3" },
		{ "x-rapidapi-host", "spoonacular-recipe-food-nutrition-v1.p.rapidapi.com" },
	},
};
using (var response = await client.SendAsync(request))
{
	response.EnsureSuccessStatusCode();
	var body = await response.Content.ReadAsStringAsync();
	Console.WriteLine(body);
}