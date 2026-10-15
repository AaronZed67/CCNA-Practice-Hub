
using CCNAPracticeHub.Components;
using CCNAPracticeHub.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Register Blazor services.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Register SQLite database.
builder.Services.AddDbContextFactory<CcnaDbContext>(options =>
    options.UseSqlite(
        builder.Configuration.GetConnectionString("CcnaDatabase")
        ?? throw new InvalidOperationException(
            "CcnaDatabase connection string is missing.")));

var app = builder.Build();

// Seed our initial CCNA question bank.
if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();

    var contextFactory = scope.ServiceProvider
        .GetRequiredService<IDbContextFactory<CcnaDbContext>>();

    await QuestionSeeder.SeedAsync(contextFactory);
}

// Configure HTTP pipeline.
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
