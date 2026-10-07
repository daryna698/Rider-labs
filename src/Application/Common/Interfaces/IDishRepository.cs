using Domain.Dishes;

namespace Application.Common.Interfaces;

public interface IDishRepository
{
    Task<IReadOnlyList<Dish>> GetAll(CancellationToken cancellationToken);
    Task<Dish?> GetById(Guid id, CancellationToken cancellationToken);
    Task<Dish?> GetByName(string name, CancellationToken cancellationToken); 
    Task<Dish> Add(Dish dish, CancellationToken cancellationToken);
    Task<Dish> Update(Dish dish, CancellationToken cancellationToken);
    Task Delete(Dish dish, CancellationToken cancellationToken);
}