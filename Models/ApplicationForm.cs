using System.ComponentModel.DataAnnotations;

namespace PetStore.Models
{
  public class ApplicationForm
  {
    [Required]
    public string? FullName { get; set; }

    [Required]
    [EmailAddress]
    public string? Email { get; set; }

    [Required]
    public string? PhoneNumber { get; set; }

    [Required]
    public string? Address { get; set; }

    [Required]
    public string? HelpType { get; set; }

    public string? HousingStatus { get; set; }
    public string? OtherPets { get; set; }
    public string? FosterLength { get; set; }
    public string? Experience { get; set; }
    public string? Reason { get; set; }
    public decimal? MonthlyAmount { get; set; }

    [Range(typeof(bool), "true", "true", ErrorMessage = "You must agree before submitting.")]
    public bool AgreeToTerms { get; set; }
  }
}