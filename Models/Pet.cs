using System.ComponentModel.DataAnnotations;

namespace PetStore.Models
{
  // Represents a pet in the system. This model is used for storing pet data and validating user input when creating or updating a pet.
  public class Pet
  {
    // Unique identifier for each pet (used for database lookups)
    public string? Id { get; set; }

    // Pet's name with validation rules for length and required input
    [Required(ErrorMessage = "Please, write a valid Name")]
    [MinLength(2, ErrorMessage = "The Name is too short (minimum 2 letters).")]
    [MaxLength(100, ErrorMessage = "The Name is too long (maximum 100 letters).")]
    [Display(Name = "Name")]
    public string Name { get; set; } = string.Empty;

    // Type of animal (ex: dog, cat, etc.)
    // Used for filtering and categorizing pets in the application
    [Required(ErrorMessage = "Please use a valid Type: specify whether Cat, Dog, or other.")]
    [MinLength(2, ErrorMessage = "The Type is too short (minimum 2 letters).")]
    [MaxLength(100, ErrorMessage = "The Type is too long (maximum 100 letters).")]
    [Display(Name = "Animal Type")]
    public string Type { get; set; } = string.Empty;

    // Specific breed or subtype of the animal
    [Required(ErrorMessage = "Please, write a valid Name")]
    [MinLength(3, ErrorMessage = "The Breed is too short (minimum 3 letters).")]
    [MaxLength(100, ErrorMessage = "The Breed is too long (maximum 100 letters).")]
    [Display(Name = "Breed or Subtype")]
    public string Breed { get; set; } = string.Empty;

    // Year the pet was born; used instead of exact birthday to simplify age grouping (ex: puppy vs adult)
    [Required(ErrorMessage = "Please set a valid year of birth (e.g. 2021)")]
    [Range(2010, 2026, ErrorMessage = "Age must be between 0 and 16 years")]
    [Display(Name = "Year of Birth")]
    public int YearOfBirth { get; set; }

    // Short description of the pet's personality or traits
    [Required]
    [MaxLength(100, ErrorMessage = "Please use a description less than 100 letters.")]
    public string Description { get; set; } = "Beautiful soul";

    // URL for the pet's image (can be empty if no image is uploaded yet)
    public string ImageUrl { get; set; } = "";

    // Energy level of the pet (1 = low energy, 5 = very active)
    [Required(ErrorMessage = "Please indicate how active the animal is")]
    [Range(1, 5, ErrorMessage = "Energy level must be between 1 and 5.")]
    public int Energy { get; set; }

    // Indicates if the pet is friendly toward people or other animals
    [Required(ErrorMessage = "Please indicate if it is friendly")]
    public bool Is_Friendly { get; set; }

    // Indicates if the pet is available for adoption
    [Required(ErrorMessage = "Please indicate if it is available for adoption")]
    public bool Is_Adopt { get; set; }

    // Indicates if the pet is available for temporary fostering
    [Required(ErrorMessage = "Please indicate if it is available for fostering for a short time")]
    public bool Is_Foster { get; set; }

    // Foreign key linking the pet to a shelter. This should be assigned when the pet is created
    public string Shelter_id { get; set; } = string.Empty;
  }
}