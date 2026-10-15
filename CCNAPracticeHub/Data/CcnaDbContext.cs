
using CCNAPracticeHub.Models;
using Microsoft.EntityFrameworkCore;

namespace CCNAPracticeHub.Data;

public class CcnaDbContext : DbContext
{
    public CcnaDbContext(
        DbContextOptions<CcnaDbContext> options)
        : base(options)
    {
    }

    public DbSet<Question> Questions =>
        Set<Question>();

    protected override void OnModelCreating(
        ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Question>(entity =>
        {
            entity.HasKey(q => q.Id);

            entity.Property(q => q.Domain)
                .IsRequired();

            entity.Property(q => q.Subtopic)
                .IsRequired();

            entity.Property(q => q.QuestionText)
                .IsRequired();

            entity.Property(q => q.Explanation)
                .IsRequired();

            entity.Property(q => q.Options)
                .HasConversion(
                    options => System.Text.Json.JsonSerializer.Serialize(
                        options, (System.Text.Json.JsonSerializerOptions?)null),
                    json => System.Text.Json.JsonSerializer.Deserialize<List<string>>(
                        json, (System.Text.Json.JsonSerializerOptions?)null)
                        ?? new List<string>())
                .HasColumnType("TEXT");
        });
    }
}
