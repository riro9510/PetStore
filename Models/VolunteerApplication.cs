using System.ComponentModel.DataAnnotations;

namespace PetStore.Models
{
    public class VolunteerApplication
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please select a volunteer type")]
        public VolunteerType  VolunteerType { get; set; }

        [Required(ErrorMessage = "Please enter your full name or institution name")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Please enter your contact phone number")]
        [Phone(ErrorMessage = "Please enter a valid phone number")]
        public string? PhoneNumber { get; set; }

        [Required(ErrorMessage = "Please enter your preferred email")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        public string? Email { get; set; }

        public DateTime SubmittedAt { get; set; } = DateTime.Now;
    }
}