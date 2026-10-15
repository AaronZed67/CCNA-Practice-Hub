
using CCNAPracticeHub.Components;
using CCNAPracticeHub.Data;
using CCNAPracticeHub.Services;
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

// Import CCNA questions during development.
if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();

    var factory = scope.ServiceProvider
        .GetRequiredService<IDbContextFactory<CcnaDbContext>>();

    // Import original prototype questions.
    await QuestionSeeder.SeedAsync(factory);

    // Locate question data directory.
    string questionFolder = Path.Combine(
        app.Environment.ContentRootPath,
        "QuestionData");

    if (Directory.Exists(questionFolder))
    {
        // Find every JSON file, including subfolders.
        string[] files = Directory.GetFiles(
            questionFolder,
            "*.json",
            SearchOption.AllDirectories);

        int totalImported = 0;

        foreach (string file in files.OrderBy(f => f))
        {
            int imported = await QuestionImporter.ImportAsync(
                factory,
                file);

            totalImported += imported;

            app.Logger.LogInformation(
                "Imported {Count} questions from {File}",
                imported,
                Path.GetFileName(file));
        }

        app.Logger.LogInformation(
            "Question import completed. {Count} new questions added.",
            totalImported);
    }
    else
    {
        app.Logger.LogWarning(
            "QuestionData folder was not found.");
    }
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
