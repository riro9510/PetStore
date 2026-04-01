using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PetStore.Models;

namespace PetStore.Data;

public class PetStoreContext : IdentityDbContext<ApplicationUser>
{
  public PetStoreContext(DbContextOptions<PetStoreContext> options)
      : base(options)
  {
  }

  public DbSet<Pet> Pets { get; set; }

  public DbSet<PetCategory> PetCategories { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);

    modelBuilder.Entity<Pet>().HasData(
        new Pet
        {
          Id = 1,
          Name = "Max",
          Category = "dogs",
          Breed = "Golden Retriever",
          Age = 3,
          Description = "Friendly, loyal, and ready for a home.",
          ImageUrl = "https://raw.githubusercontent.com/vsyang/pet-images/main/goldenr.jpg"
        },
        new Pet
        {
          Id = 2,
          Name = "Luna",
          Category = "cats",
          Breed = "Siamese",
          Age = 2,
          Description = "Curious, calm, and full of personality.",
          ImageUrl = "https://raw.githubusercontent.com/vsyang/pet-images/main/siamese.jpg"
        },
        new Pet
        {
          Id = 3,
          Name = "Toothless",
          Category = "dragons",
          Breed = "Mini Flame",
          Age = 100,
          Description = "Magical companion with fiery charm.",
          ImageUrl = "https://raw.githubusercontent.com/vsyang/pet-images/main/toothless.jpg"
        },
        new Pet
        {
          Id = 4,
          Name = "Sparkle",
          Category = "unicorns",
          Breed = "Silver Mane",
          Age = 5,
          Description = "Rare, graceful, and full of wonder.",
          ImageUrl = "https://raw.githubusercontent.com/vsyang/pet-images/main/unicorn.jpg"
        },
        new Pet
        {
          Id = 5,
          Name = "Cloud",
          Category = "dogs",
          Breed = "Maltese",
          Age = 1,
          Description = "Intelligent and handsome. He just loves to be with you.",
          ImageUrl = "https://raw.githubusercontent.com/vsyang/pet-images/main/maltese.jpg"
        },
        new Pet
        {
          Id = 6,
          Name = "Jimbo",
          Category = "elephants",
          Breed = "Asian Elephant",
          Age = 4,
          Description = "Curious, adorable, and intelligent.",
          ImageUrl = "https://raw.githubusercontent.com/vsyang/pet-images/main/elephant.jpg"
        },
        new Pet
        {
          Id = 7,
          Name = "Raja",
          Category = "cats",
          Breed = "Siberian Tiger",
          Age = 7,
          Description = "Majestic, regal, and content. All she wants is to bathe in the sun.",
          ImageUrl = "https://raw.githubusercontent.com/vsyang/pet-images/main/tiger.jpg"
        },
        new Pet
        {
          Id = 8,
          Name = "Marshmallow",
          Category = "birds",
          Breed = "White Cockatoo",
          Age = 10,
          Description = "Loves to talk, sing, and dance.",
          ImageUrl = "https://raw.githubusercontent.com/vsyang/pet-images/main/white-parrot.jpg"
        }
    );

    modelBuilder.Entity<PetCategory>().HasData(
        new PetCategory
        {
          Id = 1,
          Name = "dogs"
        },
        new PetCategory
        {
          Id = 2,
          Name = "cats"
        },
        new PetCategory
        {
          Id = 3,
          Name = "dragons"
        },
        new PetCategory
        {
          Id = 4,
          Name = "unicorns"
        },
        new PetCategory
        {
          Id = 5,
          Name = "birds"
        },
        new PetCategory
        {
          Id = 6,
          Name = "elephants"
        }
    );
  }
}