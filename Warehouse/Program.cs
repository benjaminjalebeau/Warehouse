using Warehouse.Components;
using Warehouse.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Blazored.LocalStorage;
using System.Text;
using Microsoft.AspNetCore.Components.Authorization;


var builder = WebApplication.CreateBuilder(args);

//Pulls secret from the appsettings.json file.
var jwtSecret = builder.Configuration["JwtSettings:Secret"];


//Checks to make sure JWT token secret was imported correctly, or if it exists. 
if (string.IsNullOrEmpty(jwtSecret))
{
    throw new InvalidOperationException("Could not retrieve JWT secret.");
}

// Configures SQLite for the project.
builder.Services.AddDbContext<WarehouseDbContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("WarehouseDbContext")));

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// This makes all CRUD operations available in any of the pages. Write "@inject DataService DataService" at the top of the page or component to import CRUD functions.
builder.Services.AddScoped<DataService>();

// This makes authorization accessible to any page.
builder.Services.AddScoped<AuthService>();

// Sets up JWT token authentication for the app.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret)),
            ValidateIssuer = false,
            ValidateAudience = false,
            RoleClaimType = "role"
        };
    });

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<CustomAuthStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RoleBasedPolicy", policy =>
        policy.RequireRole("Worker", "Customer"));
});







var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// Apply any pending migrations to the database (for development)
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<WarehouseDbContext>();
    db.Database.Migrate();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
