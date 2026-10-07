using Domain.Dishes;

namespace Application.Dishes.Services.Abstract;

public interface IDishService
{
    Task<IReadOnlyList<Dish>> GetDishes(CancellationToken cancellationToken);
    Task<Dish?> GetDish(Guid id, CancellationToken cancellationToken);
    Task<Dish> Add(string name, decimal price, int weightGrams, bool isVegetarian, DishCategory category, CancellationToken cancellationToken);
    Task<Dish?> Update(Guid id, string name, decimal price, int weightGrams, bool isVegetarian, DishCategory category, CancellationToken cancellationToken);
    Task<bool> Delete(Guid id, CancellationToken cancellationToken);
}