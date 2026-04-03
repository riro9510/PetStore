using System.ComponentModel.DataAnnotations;

namespace PetStore.Models
{
  public class ApplicationForm
  {
    [Required(ErrorMessage = "Please fill in your full name")]
    [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Name can only contain letters")]
    public string? FullName { get; set; }

    [Required(ErrorMessage = "Please fill in your email")]
    [EmailAddress(ErrorMessage = "Please enter a valid email address")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "Please fill in your phone number")]
    [RegularExpression(@"^\(?\d{3}\)?[-.\s]?\d{3}[-.\s]?\d{4}$",
    ErrorMessage = "Enter a valid phone number")]
    public string? PhoneNumber { get; set; }

    [Required(ErrorMessage = "Please fill in your address")]
    public string? Address { get; set; }

    [Required(ErrorMessage = "Please select one")]
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