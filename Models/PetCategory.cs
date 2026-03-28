using System.ComponentModel.DataAnnotations;
namespace PetStore.Models
{
  public class PetCategory
  {
    [Key]
    public int Id { get; set; }
    [Required(ErrorMessage = "*Category name is required")]
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public string Icon { get; set; } = "";
    public string Route { get; set; } = "";
  }
}