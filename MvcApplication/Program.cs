using AspNet.Security.OAuth.Keycloak;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MvcApplication.Data;

var builder = WebApplication.CreateBuilder(args);

// Add Environment Variables to Configuration
builder.Configuration.AddEnvironmentVariables();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
                       throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContextPool<ApplicationDbContext>(options => options.UseSqlite(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();

/*
 Integrating the Keycloak Provider:
 https://github.com/aspnet-contrib/AspNet.Security.OAuth.Providers/blob/dev/docs/keycloak.md 
*/
builder.Services.AddAuthentication()
    .AddKeycloak(options =>
    {
        options.AccessType = KeycloakAuthenticationAccessType.Confidential;
        options.BaseAddress = new Uri(builder.Configuration["KEYCLOAK_BASE_ADDRESS"]);
        options.ClientId = builder.Configuration["KEYCLOAK_CLIENT_ID"];
        options.ClientSecret = builder.Configuration["KEYCLOAK_CLIENT_SECRET"];
        options.Realm = builder.Configuration["KEYCLOAK_REALM"];
        options.Version = new Version(19, 0);
    });

builder.Services.AddControllersWithViews();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();

app.UseRouting();

// Add Authentication
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();