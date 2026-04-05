using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace PetStore.Models;

// Represents a user in the application. This class extends IdentityUser to include additional profile information beyond the default authentication fields (email, password, etc.).
public class ApplicationUser : IdentityUser
{
    // User's first name (required for personalization and display)
    [Required]
    [MaxLength(50)]
    [Display(Name = "Name")]
    public string FirstName { get; set; } = string.Empty;

    // User's last name (required for full identity display)
    [Required]
    [MaxLength(50)]
    [Display(Name = "Last Name")]
    public string LastName { get; set; } = string.Empty;

    // Optional nickname that can be used instead of full name. This allows a more personalized or casual display name
    [MaxLength(20)]
    [Display(Name = "Nick name")]
    public string? NickName { get; set; } = string.Empty;

    // Optional address field for user profile information
    [MaxLength(100)]
    [Display(Name = "Address")]
    public string? Address { get; set; } = string.Empty;

    // Optional date of birth for user profile details
    [Display(Name = "Date of Birth")]
    public DateTime? DateOfBirth { get; set; }

    // Computed property used to display the user's name in the UI. If a nickname exists, use it. Otherwise, fall back to full name.
    public string DisplayNam =>
        !string.IsNullOrEmpty(NickName)
        ? NickName
        : $"{FirstName} {LastName}";
}