using Mapster;
using StudyBuddy.Domain.Entities;
using StudyBuddy.Shared.DTOs.MajorDTO;
using StudyBuddy.Shared.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyBuddy.Application.Services.Majors
{
    public class MajorService : IMajorService
    {
        private readonly IRepo<Major> majorRepo;

        public MajorService(IRepo<Major> majorRepo)
        {
            this.majorRepo = majorRepo;
        }
        public async Task<Result> Create(CreateMajorDTO majorDTO)
        {
            if (await majorRepo.ExistsAsync(m => m.Name == majorDTO.Name))
                return Result.Failure(Error.MajorAlreadyExists);

            var major = new Major();
            majorDTO.Adapt(major);
            await majorRepo.AddAsync(major);
            try
            {
                await majorRepo.SaveAsync();
            }
            catch
            {
                return Result.Failure(Error.CreateFailed);
            }
            return Result.Success();
        }

        public async Task<Result> Delete(int id)
        {
            var major = await majorRepo.GetByIdAsync(id);
            if (major == null)
                return Result.Failure(Error.ItemNotFound);
            majorRepo.Remove(major);
            try
            {
                await majorRepo.SaveAsync();
            }
            catch
            {
                return Result.Failure(Error.DeleteFailed);
            }
            return Result.Success();
        }

        public async Task<Result<GetMajorDTO>> GetMajorById(int id)
        {
            var major = await majorRepo.GetByIdAsync(id);
            if (major == null)
                return Result<GetMajorDTO>.Failure(Error.ItemNotFound);
            var majorDTO = major.Adapt<GetMajorDTO>();
            return Result<GetMajorDTO>.Success(majorDTO);
        }

        public async Task<Result<List<GetMajorDTO>>> GetMojors(int skip, int take)
        {
            var result = majorRepo.GetQuery();

            var query = result.ProjectToType<GetMajorDTO>();

            var data = await query.Skip(skip).Take(take).ToListAsync();
            return Result<List<GetMajorDTO>>.Success(data);
        }

        public async Task<Result> Update(UpdateMajorDTO majorDTO)
        {
            var major = await majorRepo.GetByIdAsync(majorDTO.Id);
            if (major == null)
                return Result.Failure(Error.ItemNotFound);

            if (await majorRepo.ExistsAsync(m => m.Name == majorDTO.Name && m.Id != majorDTO.Id))
                return Result.Failure(Error.MajorAlreadyExists);

            majorDTO.Adapt(major);
            majorRepo.Update(major);
            try
            {
                await majorRepo.SaveAsync();
            }
            catch
            {
                return Result.Failure(Error.UpdateFailed);
            }

            return Result.Success();
        }
    }
}
