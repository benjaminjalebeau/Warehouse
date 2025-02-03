using Warehouse.Components;
using Warehouse.Data;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Configures SQLite for the project.
builder.Services.AddDbContext<WarehouseDbContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("WarehouseDbContext")));

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// This makes all CRUD operations available in any of the pages. Write "@inject DataService DataService" at the top of the page or component to import CRUD functions.
builder.Services.AddScoped<DataService>();

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
