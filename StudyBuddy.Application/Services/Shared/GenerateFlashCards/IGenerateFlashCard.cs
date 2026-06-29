using StudyBuddy.Shared.DTOs.ClientFileDTO;
using StudyBuddy.Shared.Results;

namespace StudyBuddy.Application.Services.Shared.GenerateFlashCards;

public interface IGenerateFlashCard
{
    Task<Result<List<GetFlashCardDTO>>> GetFlashes(string text , int take);
}