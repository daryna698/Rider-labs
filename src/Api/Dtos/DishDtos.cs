using System.ComponentModel.DataAnnotations;
using Domain.Dishes;

namespace Api.Dtos;

public record DishDto(Guid Id, string Name, decimal Price, int WeightGrams, bool IsVegetarian, DishCategory Category, DateTime CreatedAt)
{
    public static DishDto FromDomainModel(Dish dish)
        => new(dish.Id, dish.Name, dish.Price, dish.WeightGrams, dish.IsVegetarian, dish.Category, dish.CreatedAt);
}

public record CreateDishDto(
    [Required, MaxLength(100)] string Name,
    [Range(0.01, 100000)] decimal Price,
    [Range(1, 5000)] int WeightGrams,
    [Required] bool IsVegetarian,
    [Required] DishCategory Category
);

public record UpdateDishDto(
    [Required, MaxLength(100)] string Name,
    [Range(0.01, 100000)] decimal Price,
    [Range(1, 5000)] int WeightGrams,
    [Required] bool IsVegetarian,
    [Required] DishCategory Category
);