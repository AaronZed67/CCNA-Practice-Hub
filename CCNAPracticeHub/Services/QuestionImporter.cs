
using CCNAPracticeHub.Data;
using CCNAPracticeHub.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace CCNAPracticeHub.Services;

public static class QuestionImporter
{
    public static async Task<int> ImportAsync(
        IDbContextFactory<CcnaDbContext> contextFactory,
        string filePath)
    {
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException(
                "Question file was not found.", filePath);
        }

        string json = await File.ReadAllTextAsync(filePath);

        var questions = JsonSerializer.Deserialize<List<Question>>(
            json,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

        if (questions is null)
        {
            throw new InvalidDataException(
                "Unable to read question JSON.");
        }

        // Reject invalid batches before accessing the database.
        QuestionValidator.ValidateBatch(questions);

        await using var db =
            await contextFactory.CreateDbContextAsync();

        var incomingIds = questions.Select(q => q.Id).ToList();

        var existing = await db.Questions
            .AsNoTracking()
            .Where(q => incomingIds.Contains(q.Id))
            .ToDictionaryAsync(q => q.Id);

        var newQuestions = new List<Question>();

        foreach (var question in questions)
        {
            if (existing.TryGetValue(question.Id, out var stored))
            {
                // An existing ID must have identical content.
                // Never silently overwrite an existing question.
                if (!SameContent(stored, question))
                {
                    throw new InvalidDataException(
                        $"Question ID {question.Id} already exists " +
                        "with different content.");
                }

                // Already imported: skip.
                continue;
            }

            newQuestions.Add(question);
        }

        // Only save after the whole batch passes validation.
        db.Questions.AddRange(newQuestions);
        await db.SaveChangesAsync();

        return newQuestions.Count;
    }

    private static bool SameContent(Question a, Question b)
    {
        return a.Domain == b.Domain
            && a.Subtopic == b.Subtopic
            && a.Difficulty == b.Difficulty
            && a.QuestionText == b.QuestionText
            && a.CorrectAnswerIndex == b.CorrectAnswerIndex
            && a.Explanation == b.Explanation
            && a.Options.SequenceEqual(b.Options);
    }
}
