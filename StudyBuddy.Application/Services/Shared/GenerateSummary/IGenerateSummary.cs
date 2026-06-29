using StudyBuddy.Shared.Results;

namespace StudyBuddy.Application.Services.Shared.GenerateSummary;

public interface IGenerateSummary
{
    Task<Result<string>> GetSummary(string text); 
}
