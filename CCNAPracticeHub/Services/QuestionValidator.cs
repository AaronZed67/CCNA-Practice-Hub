
using CCNAPracticeHub.Models;

namespace CCNAPracticeHub.Services;

public static class QuestionValidator
{
    private static readonly HashSet<string> ValidDifficulties =
        new(StringComparer.OrdinalIgnoreCase)
        {
            "Easy",
            "Medium",
            "Hard"
        };

    public static List<string> Validate(Question? question)
    {
        var errors = new List<string>();

        if (question is null)
        {
            errors.Add("Question cannot be null.");
            return errors;
        }

        if (question.Id <= 0)
            errors.Add("Question ID must be positive.");

        if (string.IsNullOrWhiteSpace(question.Domain))
            errors.Add("Domain is required.");

        if (string.IsNullOrWhiteSpace(question.Subtopic))
            errors.Add("Subtopic is required.");

        if (string.IsNullOrWhiteSpace(question.QuestionText))
            errors.Add("Question text is required.");

        if (string.IsNullOrWhiteSpace(question.Explanation))
            errors.Add("Explanation is required.");

        if (!ValidDifficulties.Contains(question.Difficulty ?? ""))
            errors.Add("Difficulty must be Easy, Medium or Hard.");

        if (question.Options is null || question.Options.Count != 4)
        {
            errors.Add("A question must contain exactly four options.");
        }
        else
        {
            if (question.Options.Any(string.IsNullOrWhiteSpace))
                errors.Add("Answer options cannot be empty.");

            if (question.Options
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .Count() != 4)
            {
                errors.Add("Answer options must be unique.");
            }

            if (question.CorrectAnswerIndex < 0 ||
                question.CorrectAnswerIndex >= question.Options.Count)
            {
                errors.Add("Correct answer index is invalid.");
            }
        }

        return errors;
    }

    public static void ValidateBatch(List<Question> questions)
    {
        if (questions.Count == 0)
            throw new InvalidDataException("Question file is empty.");

        var errors = new List<string>();

        var duplicateIds = questions
            .Where(q => q is not null)
            .GroupBy(q => q.Id)
            .Where(g => g.Count() > 1)
            .Select(g => g.Key);

        foreach (var id in duplicateIds)
            errors.Add($"Duplicate ID in file: {id}");

        foreach (var question in questions)
        {
            int id = question?.Id ?? 0;

            foreach (var error in Validate(question))
                errors.Add($"Question {id}: {error}");
        }

        if (errors.Count > 0)
        {
            throw new InvalidDataException(
                "Question validation failed:\n" +
                string.Join("\n", errors));
        }
    }
}
