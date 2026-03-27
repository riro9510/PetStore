using System.ComponentModel.DataAnnotations;

namespace PetStore.Models;

public class Pet
{
  public int Id { get; set; }
  [Required]
  public string Name { get; set; } = "";
  [Required]
  public string Category { get; set; } = "";
  [Required]
  public string Breed { get; set; } = "";
  [Required]
  public int Age { get; set; }
  public string Description { get; set; } = "";
  public string ImageUrl { get; set; } = "";
}