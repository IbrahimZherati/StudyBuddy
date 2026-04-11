
using Mapster;
using StudyBuddy.Domain.Entities;
using StudyBuddy.Domain.Services.Universities;
using StudyBuddy.Shared.DTOs.UniversityDTO;
using StudyBuddy.Shared.Results;
namespace StudyBuddy.Application.Services
{
    public class UniversityService : IUniversityService
    {
        private readonly IRepo<University> universityRepo;
        private readonly IUniversityDomainService universityDomainService;


        public UniversityService(IRepo<University> universityRepo, IUniversityDomainService universityDomainService)
        {
            this.universityRepo = universityRepo;
            this.universityDomainService = universityDomainService;

        }

        public async Task<Result> Create(CreateUniversityDTO universityDTO)
        {
            var valid = await universityDomainService.Create(universityDTO);
            if (!valid.IsSuccess)
                return Result.Failure(valid.Error!);

            var result = University.Create(universityDTO);

            if (!result.IsSuccess)
                return Result.Failure(result.Error!);

            if(result.Value == null)
                return Result.Failure(Error.CreateFailed);

            var university = result.Value;
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
            var valid = await universityDomainService.Delete(id);
            if(!valid.IsSuccess)
                return Result.Failure(valid.Error!);
            var university = await universityRepo.GetByIdAsync(id);
            if (university == null)
                return Result.Failure(Error.UniversityNotFound);
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
                return Result<GetUniversityDTO>.Failure(Error.UniversityNotFound);
            var universityDTO = university.Adapt<GetUniversityDTO>();
            return Result<GetUniversityDTO>.Success(universityDTO);
        }

        public async Task<Result<List<GetUniversityDTO>>> GetUniversities(int skip, int take)
        {
            var result = universityRepo.GetQuery();

            var query = result.ProjectToType<GetUniversityDTO>();

            var data = await query.Skip(skip).Take(take).ToListAsync();
            return Result<List<GetUniversityDTO>>.Success(data);
        }

        public async Task<Result> Update(UpdateUniversityDTO universityDTO)
        {
            var valid = await universityDomainService.Update(universityDTO);
            if (!valid.IsSuccess)
                return Result.Failure(valid.Error!);

            var university = await universityRepo.GetByIdAsync(universityDTO.Id);
            if (university == null)
                return Result.Failure(Error.UniversityNotFound);

            var result = university.Update(universityDTO);

            if (!result.IsSuccess)
                return Result.Failure(result.Error!);

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
