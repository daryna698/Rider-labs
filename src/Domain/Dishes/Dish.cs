namespace Domain.Dishes;

public class Dish
{
    public Guid Id { get; }
    public string Name { get; private set; }
    public decimal Price { get; private set; }
    public int WeightGrams { get; private set; }
    public bool IsVegetarian { get; private set; }
    public DishCategory Category { get; private set; }
    public DateTime CreatedAt { get; }

    private Dish(Guid id, string name, decimal price, int weightGrams, bool isVegetarian, DishCategory category, DateTime createdAt)
    {
        (Id, Name, Price, WeightGrams, IsVegetarian, Category, CreatedAt) = (id, name, price, weightGrams, isVegetarian, category, createdAt);
    }

    public static Dish New(Guid id, string name, decimal price, int weightGrams, bool isVegetarian, DishCategory category)
        => new(id, name, price, weightGrams, isVegetarian, category, DateTime.UtcNow);

    public void UpdateDetails(string name, decimal price, int weightGrams, bool isVegetarian, DishCategory category)
    {
        Name = name;
        Price = price;
        WeightGrams = weightGrams;
        IsVegetarian = isVegetarian;
        Category = category;
    }
}