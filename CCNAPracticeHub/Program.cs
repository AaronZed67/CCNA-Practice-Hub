
using CCNAPracticeHub.Components;
using CCNAPracticeHub.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add Blazor services.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Register SQLite database factory.
builder.Services.AddDbContextFactory<CcnaDbContext>(options =>
    options.UseSqlite(
        builder.Configuration.GetConnectionString("CcnaDatabase")
        ?? throw new InvalidOperationException(
            "CcnaDatabase connection string is missing.")));

var app = builder.Build();

// Configure HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
