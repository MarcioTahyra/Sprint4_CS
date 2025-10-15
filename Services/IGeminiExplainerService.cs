public interface IGeminiExplainerService
{
    Task<string> GenerateExplanation(string prompt);
}
