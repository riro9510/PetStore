using PetStore.Models;

namespace PetStore.Services
{
  public class PetService
  {
    private readonly List<Pet> pets = new()
        {
            new Pet { Id = 1, Name = "Max", Category = "dogs", Description = "A playful and loyal dog.", ImageUrl = "🐶", Age = 3 },
            new Pet { Id = 2, Name = "Bella", Category = "dogs", Description = "Loves long walks and treats.", ImageUrl = "🐶", Age = 2 },

            new Pet { Id = 3, Name = "Luna", Category = "cats", Description = "A calm cat who loves naps.", ImageUrl = "🐱", Age = 4 },
            new Pet { Id = 4, Name = "Milo", Category = "cats", Description = "Curious and full of energy.", ImageUrl = "🐱", Age = 1 },

            new Pet { Id = 5, Name = "Draco", Category = "dragons", Description = "A tiny dragon with a big personality.", ImageUrl = "🐉", Age = 100 },

            new Pet { Id = 6, Name = "Sparkle", Category = "unicorns", Description = "Gentle and magical.", ImageUrl = "🦄", Age = 7 },

            new Pet { Id = 7, Name = "Sunny", Category = "birds", Description = "Bright and cheerful singer.", ImageUrl = "🐦", Age = 2 },

            new Pet { Id = 8, Name = "Daisy", Category = "elephants", Description = "A kind giant with a sweet nature.", ImageUrl = "🐘", Age = 12 }
        };

    public List<Pet> GetAllPets()
    {
      return pets;
    }

    public List<Pet> GetPetsByCategory(string? category)
    {
      if (string.IsNullOrWhiteSpace(category))
      {
        return new List<Pet>();
      }

      return pets
          .Where(p => p.Category.Equals(category, StringComparison.OrdinalIgnoreCase))
          .ToList();
    }

    public Pet? GetPetById(int id)
    {
      return pets.FirstOrDefault(p => p.Id == id);
    }
  }
}