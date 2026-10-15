
namespace CCNAPracticeHub.Models;

public class Question
{
    public int Id { get; set; }

    public string Domain { get; set; } = "";

    public string Subtopic { get; set; } = "";

    public string Difficulty { get; set; } = "Easy";

    public string QuestionText { get; set; } = "";

    public List<string> Options { get; set; } = new();

    public int CorrectAnswerIndex { get; set; }

    public string Explanation { get; set; } = "";
}
