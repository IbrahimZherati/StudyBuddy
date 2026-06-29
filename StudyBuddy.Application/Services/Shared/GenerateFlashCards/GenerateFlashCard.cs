using System.Text.Json;
using StudyBuddy.Application.Services.Shared.Interfaces;
using StudyBuddy.Shared.AI;
using StudyBuddy.Shared.DTOs.ClientFileDTO;
using StudyBuddy.Shared.Results;

namespace StudyBuddy.Application.Services.Shared.GenerateFlashCards;

public class GenerateFlashCard : IGenerateFlashCard
{
    private readonly IAiService aiService;

    public GenerateFlashCard(IAiService aiService)
    {
        this.aiService = aiService;
    }
    public async Task<Result<List<GetFlashCardDTO>>> GetFlashes(string text, int take)
    {
        try
        {
            var json = await aiService.GenerateAsync(AiPromt.GetGenerateFlashCard(text, take));
            var list = JsonSerializer.Deserialize<List<GetFlashCardDTO>>(json);
            if (list == null)
                return Result<List<GetFlashCardDTO>>.Failure(Error.AiServiceFailed);
            foreach(var item in list)
            {
                item.Id = Guid.NewGuid();
            }
            return Result<List<GetFlashCardDTO>>.Success(list);
        }
        catch
        {
            return Result<List<GetFlashCardDTO>>.Failure(Error.AiServiceFailed);
        }
    }
}
