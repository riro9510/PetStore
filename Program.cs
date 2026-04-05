// This file configures the entire application including services, authentication, middleware pipeline, and custom routes.

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.HttpOverrides;
using PetStore.Components;
using PetStore.Data;
using PetStore.Services;
using PetStore.Models;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// ====================== Services ======================

// Retrieve database connection string from configuration
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? "Data Source=PetStore.db";

// Configure Entity Framework with SQLite database
builder.Services.AddDbContext<PetStoreContext>(options =>
    options.UseSqlite(connectionString));

// Configure Identity system for authentication and user management
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
  // Set password rules for users
  options.Password.RequireDigit = true;
  options.Password.RequireLowercase = true;
  options.Password.RequireUppercase = true;
  options.Password.RequireNonAlphanumeric = false;
  options.Password.RequiredLength = 6;

  // Disable email confirmation requirement for easier login
  options.SignIn.RequireConfirmedAccount = false;
})
.AddEntityFrameworkStores<PetStoreContext>()
.AddDefaultTokenProviders();

// Configure external authentication using Google login
builder.Services.AddAuthentication()
    .AddGoogle(options =>
    {
      // Load Google OAuth credentials from configuration
      options.ClientId = builder.Configuration["Authentication:Google:ClientId"]!;
      options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"]!;

      // Map Google "given_name" to ClaimTypes.GivenName for easy access
      options.ClaimActions.MapJsonKey(ClaimTypes.GivenName, "given_name");
    });

// Enable Blazor components with server-side interactivity
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Register application services
builder.Services.AddScoped<PetService>();

// Enable cascading authentication state for components
builder.Services.AddCascadingAuthenticationState();

var app = builder.Build();

// ====================== Middleware Pipeline ======================

// Configure error handling for production environment
if (!app.Environment.IsDevelopment())
{
  app.UseExceptionHandler("/Error", createScopeForErrors: true);
  app.UseHsts();
}

// Handle 404 errors by redirecting to custom "not found" page
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);

// Overwrite HTTP to HTTPS
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
  ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

// Standard middleware setup
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAntiforgery();

// Enable authentication and authorization
app.UseAuthentication();
app.UseAuthorization();

// Map static assets and Razor components
app.MapStaticAssets();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// ====================== Google Login Routes ======================

// Route to initiate Google login
app.MapGet("/login-google", async (HttpContext context) =>
{
  await context.ChallengeAsync(GoogleDefaults.AuthenticationScheme, new AuthenticationProperties
  {
    // Redirect here after successful login
    RedirectUri = "/signin-google-callback"
  });
});

// Callback route after Google authentication completes
app.MapGet("/signin-google-callback", async (
    HttpContext context,
    UserManager<ApplicationUser> userManager,
    SignInManager<ApplicationUser> signInManager,
    IConfiguration config) =>
{
  // Retrieve external login result from Google
  var result = await context.AuthenticateAsync(IdentityConstants.ExternalScheme);

  // If authentication failed, send user back to sign-in page
  if (!result.Succeeded || result.Principal == null)
  {
    return Results.Redirect("/signin");
  }

  // Extract user info from Google claims
  var email = result.Principal.FindFirstValue(ClaimTypes.Email);
  var fullName = result.Principal.FindFirstValue(ClaimTypes.Name);
  var firstName = result.Principal.FindFirstValue(ClaimTypes.GivenName);

  if (string.IsNullOrWhiteSpace(email))
  {
    return Results.Redirect("/signin");
  }

  // Check if user already exists in database
  var user = await userManager.FindByEmailAsync(email);

  // If user does not exist, create a new account
  if (user == null)
  {
    user = new ApplicationUser
    {
      UserName = email,
      Email = email
    };

    var createResult = await userManager.CreateAsync(user);

    if (!createResult.Succeeded)
    {
      return Results.Redirect("/signin");
    }
  }

  // ====================== Claim Management ======================

  // Store or update first name from Google
  if (!string.IsNullOrWhiteSpace(firstName))
  {
    var existingClaims = await userManager.GetClaimsAsync(user);
    var existingFirstNameClaim = existingClaims.FirstOrDefault(c => c.Type == ClaimTypes.GivenName);

    if (existingFirstNameClaim == null)
    {
      await userManager.AddClaimAsync(user, new Claim(ClaimTypes.GivenName, firstName));
    }
    else if (existingFirstNameClaim.Value != firstName)
    {
      await userManager.ReplaceClaimAsync(
        user,
        existingFirstNameClaim,
        new Claim(ClaimTypes.GivenName, firstName));
    }
  }

  // Store or update full name
  if (!string.IsNullOrWhiteSpace(fullName))
  {
    var existingClaims = await userManager.GetClaimsAsync(user);
    var existingNameClaim = existingClaims.FirstOrDefault(c => c.Type == ClaimTypes.Name);

    if (existingNameClaim == null)
    {
      await userManager.AddClaimAsync(user, new Claim(ClaimTypes.Name, fullName));
    }
    else if (existingNameClaim.Value != fullName)
    {
      await userManager.ReplaceClaimAsync(
        user,
        existingNameClaim,
        new Claim(ClaimTypes.Name, fullName));
    }
  }

  // ====================== Role Assignment ======================

  // Assign admin role if email matches configured admin email
  var adminEmail = config["AdminSettings:SeedAdminEmail"];

  if (!string.IsNullOrWhiteSpace(adminEmail) &&
      email.Equals(adminEmail, StringComparison.OrdinalIgnoreCase))
  {
    if (!await userManager.IsInRoleAsync(user, Roles.Admin))
    {
      await userManager.AddToRoleAsync(user, Roles.Admin);
    }
  }
  else
  {
    // Default all other users to Client role
    if (!await userManager.IsInRoleAsync(user, Roles.Client))
    {
      await userManager.AddToRoleAsync(user, Roles.Client);
    }
  }

  // Sign user into the application
  await signInManager.SignInAsync(user, isPersistent: false);

  // Clear temporary external login cookie
  await context.SignOutAsync(IdentityConstants.ExternalScheme);

  return Results.Redirect("/");
});

// Logout route
app.MapGet("/logout", async (SignInManager<ApplicationUser> signInManager) =>
{
  await signInManager.SignOutAsync();
  return Results.Redirect("/");
});

// ====================== Database Seeding ======================

// Ensure database is created and roles are seeded
using (var scope = app.Services.CreateScope())
{
  var db = scope.ServiceProvider.GetRequiredService<PetStoreContext>();
  db.Database.Migrate();

  await IdentitySeeder.SeedRolesAsync(scope.ServiceProvider);
}

app.Run();