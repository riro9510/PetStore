using System.ComponentModel.DataAnnotations;

namespace PetStore
{
  public class PetCategory
  {
    [Key]
    public int Id { get; set; }
    [Required(ErrorMessage = "*Category name is required")]
    public string Name { get; set; } = "";
  }
}