using Domain.Dishes;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Dish> Dishes { get; set; } = null!;
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Dish>(entity =>
        {
            entity.HasKey(e => e.Id); 

            // 2. Назва страви 
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);

            entity.HasIndex(e => e.Name)
                .IsUnique();

            // 3. Ціна (decimal)
            entity.Property(e => e.Price)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            // 4. Вага в грамах (int)
            entity.Property(e => e.WeightGrams)
                .IsRequired();

            // 5. Чи вегетаріанська (bool)
            entity.Property(e => e.IsVegetarian)
                .IsRequired();

            // 6. Категорія (зберігаємо як рядок тексту)
            entity.Property(e => e.Category)
                .HasConversion<string>()
                .IsRequired();

            entity.Property(e => e.CreatedAt)
                .IsRequired();
        });
    }
}