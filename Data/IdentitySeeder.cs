using Microsoft.AspNetCore.Identity;

namespace PetStore.Data;

// This seed is to create roles "Admin" and "Client" to app
public static class IdentitySeeder
{
  public static async Task SeedRolesAsync(IServiceProvider services)
  {
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

    string[] roles = { "Admin", "Client" };

    foreach (var role in roles)
    {
      if (!await roleManager.RoleExistsAsync(role))
      {
        await roleManager.CreateAsync(new IdentityRole(role));
      }
    }
  }
}