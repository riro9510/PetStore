using PetStore.Data;
using PetStore.Models;

namespace PetStore.Services;

public class PetService
{
  private readonly PetStoreContext _context;

  public PetService(PetStoreContext context)
  {
    _context = context;
  }

  public List<Pet> GetAllPets()
  {
    return _context.Pets.ToList();
  }

  public List<Pet> GetPetsByCategory(string? category)
  {
    if (string.IsNullOrWhiteSpace(category))
    {
      return new List<Pet>();
    }

    return _context.Pets
        .Where(p => p.Category.ToLower() == category.ToLower())
        .ToList();
  }

  public Pet? GetPetById(int id)
  {
    return _context.Pets.FirstOrDefault(p => p.Id == id);
  }

  public void AddPet(Pet pet)
  {
    _context.Pets.Add(pet);
    _context.SaveChanges();
  }

  public void UpdatePet(Pet updatedPet)
  {
    var pet = _context.Pets.FirstOrDefault(p => p.Id == updatedPet.Id);

    if (pet != null)
    {
      pet.Name = updatedPet.Name;
      pet.Category = updatedPet.Category;
      pet.Breed = updatedPet.Breed;
      pet.Age = updatedPet.Age;
      pet.Description = updatedPet.Description;
      pet.ImageUrl = updatedPet.ImageUrl;

      _context.SaveChanges();
    }
  }

  public void DeletePet(int id)
  {
    var pet = _context.Pets.FirstOrDefault(p => p.Id == id);

    if (pet != null)
    {
      _context.Pets.Remove(pet);
      _context.SaveChanges();
    }
  }

  public List<PetCategory> GetAllCategories()
  {
    return _context.PetCategories.ToList();
  }

  public void AddCategory(PetCategory category)
  {
    _context.PetCategories.Add(category);
    _context.SaveChanges();
  }
}