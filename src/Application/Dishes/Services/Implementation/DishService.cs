using Application.Common.Interfaces;
using Application.Dishes.Services.Abstract;
using Domain.Dishes;

namespace Application.Dishes.Services.Implementation;

public class DishService(IDishRepository dishRepository) : IDishService
{
    public async Task<IReadOnlyList<Dish>> GetDishes(CancellationToken cancellationToken)
        => await dishRepository.GetAll(cancellationToken);

    public async Task<Dish?> GetDish(Guid id, CancellationToken cancellationToken)
        => await dishRepository.GetById(id, cancellationToken);

    public async Task<Dish> Add(string name, decimal price, int weightGrams, bool isVegetarian, DishCategory category, CancellationToken cancellationToken)
    {
        var existingDish = await dishRepository.GetByName(name, cancellationToken);
        if (existingDish is not null)
            throw new ArgumentException($"Dish with name '{name}' already exists"); 

        var dish = Dish.New(Guid.NewGuid(), name, price, weightGrams, isVegetarian, category);
        return await dishRepository.Add(dish, cancellationToken);
    }

    public async Task<Dish?> Update(Guid id, string name, decimal price, int weightGrams, bool isVegetarian, DishCategory category, CancellationToken cancellationToken)
    {
        var dish = await dishRepository.GetById(id, cancellationToken);
        if (dish is null) return null;

        var dishWithSameName = await dishRepository.GetByName(name, cancellationToken);
        if (dishWithSameName is not null && dishWithSameName.Id != id)
            throw new ArgumentException($"Dish with name '{name}' already exists");

        dish.UpdateDetails(name, price, weightGrams, isVegetarian, category);
        return await dishRepository.Update(dish, cancellationToken);
    }

    public async Task<bool> Delete(Guid id, CancellationToken cancellationToken)
    {
        var dish = await dishRepository.GetById(id, cancellationToken);
        if (dish is null) return false;

        await dishRepository.Delete(dish, cancellationToken);
        return true;
    }
}
