using Mapster;
using Microsoft.EntityFrameworkCore;
using StudyBuddy.Application.DTOs.Shared;
using StudyBuddy.Domain.Entities;
using StudyBuddy.Shared.DTOs.CityDTO;
using StudyBuddy.Shared.DTOs.NotificationDTO;
using StudyBuddy.Shared.DTOs.UniversityDTO;
using StudyBuddy.Shared.Results;

namespace StudyBuddy.Application.Services.Universities
{
    public class UniversityService : IUniversityService
    {
        private readonly IRepo<University> universityRepo;

        public UniversityService(IRepo<University> universityRepo)
        {
            this.universityRepo = universityRepo;
        }

        public async Task<Result> Create(CreateUniversityDTO universityDTO)
        {
            if (await universityRepo.ExistsAsync(m => m.Name == universityDTO.Name))
                return Result.Failure(Error.UniversityAlreadyExists);

            var university = new University();
            universityDTO.Adapt(university);
            await universityRepo.AddAsync(university);
            try
            {
                await universityRepo.SaveAsync();
            }
            catch
            {
                return Result.Failure(Error.CreateFailed);
            }
            return Result.Success();
        }

        public async Task<Result> Delete(int id)
        {
            var university = await universityRepo.GetByIdAsync(id);
            if (university == null)
                return Result.Failure(Error.ItemNotFound);
            universityRepo.Remove(university);
            try
            {
                await universityRepo.SaveAsync();
            }
            catch
            {
                return Result.Failure(Error.DeleteFailed);
            }
            return Result.Success();
        }

        public async Task<Result<GetUniversityDTO>> GetUniversityById(int id)
        {
            var university = await universityRepo.GetByIdAsync(id);
            if (university == null)
                return Result<GetUniversityDTO>.Failure(Error.ItemNotFound);
            var universityDTO = university.Adapt<GetUniversityDTO>();
            return Result<GetUniversityDTO>.Success(universityDTO);
        }

        public async Task<Result<DataResponse<GetUniversityDTO>>> GetUniversities(int skip, int take)
        {
            var result = universityRepo.GetQuery();

            var query = result.ProjectToType<GetUniversityDTO>();

            var data = new DataResponse<GetUniversityDTO>();
            data.Count = await query.CountAsync();
            data.Data = await query.Skip(skip).Take(take).ToListAsync();
            return Result<DataResponse<GetUniversityDTO>>.Success(data);
        }

        public async Task<Result> Update(UpdateUniversityDTO universityDTO)
        {
            var university = await universityRepo.GetByIdAsync(universityDTO.Id);
            if (university == null)
                return Result.Failure(Error.ItemNotFound);

            if (await universityRepo.ExistsAsync(m => m.Name == universityDTO.Name && m.Id != universityDTO.Id))
                return Result.Failure(Error.UniversityAlreadyExists);

            universityDTO.Adapt(university);
            universityRepo.Update(university);
            try
            {
                await universityRepo.SaveAsync();
            }
            catch
            {
                return Result.Failure(Error.UpdateFailed);
            }

            return Result.Success();
        }
    }
}