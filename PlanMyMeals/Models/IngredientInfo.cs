using System.Text.Json.Serialization;

namespace PlanMyMeals.Models;

public record class IngredientInfo(
    [property: JsonPropertyName("id")] int Id,
    [property: JsonPropertyName("name")] int Name
);