using System.ComponentModel.DataAnnotations;

namespace PetStore.Models;

public class Pet
{
  public int Id { get; set; }
  [Required]
  public string Name { get; set; } = "";
  public string Category { get; set; } = "";
  public string Breed { get; set; } = "";
  public int Age { get; set; }
  public string Description { get; set; } = "";
  public string ImageUrl { get; set; } = "";
}