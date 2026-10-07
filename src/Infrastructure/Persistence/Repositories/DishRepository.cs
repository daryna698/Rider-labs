using Application.Common.Interfaces;
using Domain.Dishes;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class DishRepository(ApplicationDbContext context) : IDishRepository
{
    public async Task<IReadOnlyList<Dish>> GetAll(CancellationToken cancellationToken)
    {
        return await context.Dishes
            .AsNoTracking() 
            .ToListAsync(cancellationToken);
    }

    public async Task<Dish?> GetById(Guid id, CancellationToken cancellationToken)
    {
        return await context.Dishes
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<Dish?> GetByName(string name, CancellationToken cancellationToken)
    {
        return await context.Dishes
            .FirstOrDefaultAsync(x => x.Name.ToLower() == name.ToLower(), cancellationToken);
    }

    public async Task<Dish> Add(Dish dish, CancellationToken cancellationToken)
    {
        await context.Dishes.AddAsync(dish, cancellationToken);
        await context.SaveChangesAsync(cancellationToken); 
        return dish;
    }

    public async Task<Dish> Update(Dish dish, CancellationToken cancellationToken)
    {
        context.Dishes.Update(dish);
        await context.SaveChangesAsync(cancellationToken);
        return dish;
    }

    public async Task Delete(Dish dish, CancellationToken cancellationToken)
    {
        context.Dishes.Remove(dish);
        await context.SaveChangesAsync(cancellationToken);
    }
}