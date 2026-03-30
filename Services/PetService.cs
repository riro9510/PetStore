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
    pet.Category = pet.Category.Trim().ToLower();

    _context.Pets.Add(pet);
    _context.SaveChanges();
  }

  public void UpdatePet(Pet updatedPet)
  {
    var pet = _context.Pets.FirstOrDefault(p => p.Id == updatedPet.Id);

    if (pet != null)
    {
      pet.Name = updatedPet.Name;
      pet.Category = updatedPet.Category.Trim().ToLower();
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
    category.Name = category.Name.Trim().ToLower();

    bool exists = _context.PetCategories.Any(c => c.Name == category.Name);

    if (!exists)
    {
      _context.PetCategories.Add(category);
      _context.SaveChanges();
    }
  }

  public bool DeleteCategory(int id)
  {
    var category = _context.PetCategories.FirstOrDefault(c => c.Id == id);

    if (category == null)
    {
      return false;
    }

    bool categoryInUse = _context.Pets.Any(p => p.Category.ToLower() == category.Name.ToLower());

    if (categoryInUse)
    {
      return false;
    }

    _context.PetCategories.Remove(category);
    _context.SaveChanges();

    return true;
  }

  public void UpdateCategory(PetCategory updatedCategory)
  {
    var category = _context.PetCategories.FirstOrDefault(c => c.Id == updatedCategory.Id);

    if (category != null)
    {
      string oldName = category.Name;
      string newName = updatedCategory.Name.Trim().ToLower();

      category.Name = newName;

      var petsToUpdate = _context.Pets
          .Where(p => p.Category.ToLower() == oldName.ToLower())
          .ToList();

      foreach (var pet in petsToUpdate)
      {
        pet.Category = newName;
      }

      _context.SaveChanges();
    }
  }
}