using Microsoft.EntityFrameworkCore;
using PetStore.Models;

namespace PetStore.Data;

public class PetStoreContext : DbContext
{
  public PetStoreContext(DbContextOptions<PetStoreContext> options)
      : base(options)
  {
  }

  public DbSet<Pet> Pets => Set<Pet>();

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<Pet>().HasData(
        new Pet
        {
          Id = 1,
          Name = "Max",
          Category = "Dog",
          Breed = "Golden Retriever",
          Age = 3,
          Description = "Friendly, loyal, and ready for a home.",
          ImageUrl = "images/dog1.jpg"
        },
        new Pet
        {
          Id = 2,
          Name = "Luna",
          Category = "Cat",
          Breed = "Siamese",
          Age = 2,
          Description = "Curious, calm, and full of personality.",
          ImageUrl = "images/cat1.jpg"
        },
        new Pet
        {
          Id = 3,
          Name = "Draco",
          Category = "Dragon",
          Breed = "Mini Flame",
          Age = 100,
          Description = "Magical companion with fiery charm.",
          ImageUrl = "images/dragon1.jpg"
        },
        new Pet
        {
          Id = 4,
          Name = "Sparkle",
          Category = "Unicorn",
          Breed = "Silver Mane",
          Age = 5,
          Description = "Rare, graceful, and full of wonder.",
          ImageUrl = "images/unicorn1.jpg"
        }
    );
  }
}