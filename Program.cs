using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PetStore.Components;
using Microsoft.EntityFrameworkCore;
using PetStore.Data;
using PetStore.Services;
using PetStore.Models;

var builder = WebApplication.CreateBuilder(args);

// ====================== Services ======================
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<PetStoreContext>(options =>
    options.UseSqlite(connectionString));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;
    options.SignIn.RequireConfirmedAccount = false;
})
.AddEntityFrameworkStores<PetStoreContext>()
.AddDefaultTokenProviders();

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddDbContext<PetStoreContext>(options =>
    options.UseSqlite("Data Source=petstore.db"));

builder.Services.AddScoped<PetService>();

builder.Services.AddCascadingAuthenticationState();

var app = builder.Build();

// ====================== Middleware Pipeline ======================
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAntiforgery();           

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<PetStoreContext>();
    db.Database.EnsureCreated();
}

app.Run();