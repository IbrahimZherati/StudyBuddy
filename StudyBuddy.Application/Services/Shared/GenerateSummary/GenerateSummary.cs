using StudyBuddy.Application.Services.Shared.Interfaces;
using StudyBuddy.Shared.AI;
using StudyBuddy.Shared.Results;

namespace StudyBuddy.Application.Services.Shared.GenerateSummary;

public class GenerateSummary : IGenerateSummary
{
    private readonly IAiService aiService;

    public GenerateSummary(IAiService aiService)
    {
        this.aiService = aiService;
    }
    public async Task<Result<string>> GetSummary(string text)
    {
        try
        {
           var summary = await aiService.GenerateAsync(AiPromt.GetGenerateSummaryPromt(text));
           return Result<string>.Success(summary);
        }
        catch{
            return Result<string>.Failure(Error.AiServiceFailed);
        }



    }
}
