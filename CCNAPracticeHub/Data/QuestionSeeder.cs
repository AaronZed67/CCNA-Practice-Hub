
using CCNAPracticeHub.Services;
using Microsoft.EntityFrameworkCore;

namespace CCNAPracticeHub.Data;

public static class QuestionSeeder
{
    public static async Task SeedAsync(
        IDbContextFactory<CcnaDbContext> contextFactory)
    {
        await using var db =
            await contextFactory.CreateDbContextAsync();

        var questions = QuestionBank.GetQuestions();

        foreach (var question in questions)
        {
            bool exists = await db.Questions
                .AnyAsync(q => q.Id == question.Id);

            if (!exists)
            {
                db.Questions.Add(question);
            }
        }

        await db.SaveChangesAsync();
    }
}
