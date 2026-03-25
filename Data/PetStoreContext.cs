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


}