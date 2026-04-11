using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Mapster;
using StudyBuddy.Domain.Entities;
using StudyBuddy.Domain.Services.Days;
using StudyBuddy.Shared.DTOs.DayDTO;
using StudyBuddy.Shared.Results;
namespace StudyBuddy.Application.Services
{
    public class DayService : IDayService
    {
        private readonly IRepo<Day> dayRepo;
        private readonly IDayDomainService dayDomainService;


        public DayService(IRepo<Day> dayRepo, IDayDomainService dayDomainService)
        {
            this.dayRepo = dayRepo;
            this.dayDomainService = dayDomainService;

        }

        public async Task<Result> Create(CreateDayDTO dayDTO)
        {
            var valid = await dayDomainService.Create(dayDTO);
            if (!valid.IsSuccess)
                return Result.Failure(valid.Error!);

            var result = Day.Create(dayDTO);

            if (!result.IsSuccess)
                return Result.Failure(result.Error!);

            if(result.Value == null)
                return Result.Failure(Error.CreateFailed);

            var day = result.Value;
            await dayRepo.AddAsync(day);

            try
            {
                await dayRepo.SaveAsync();
            }
            catch
            {
                return Result.Failure(Error.CreateFailed);
            }
            return Result.Success();
        }

        public async Task<Result> Delete(int id)
        {
            var valid = await dayDomainService.Delete(id);
            if(!valid.IsSuccess)
                return Result.Failure(valid.Error!);
            var day = await dayRepo.GetByIdAsync(id);
            if (day == null)
                return Result.Failure(Error.DayNotFound);
            dayRepo.Remove(day);
            try
            {
                await dayRepo.SaveAsync();
            }
            catch
            {
                return Result.Failure(Error.DeleteFailed);
            }
            return Result.Success();
        }

        public async Task<Result<GetDayDTO>> GetDayById(int id)
        {
            var day = await dayRepo.GetByIdAsync(id);
            if (day == null)
                return Result<GetDayDTO>.Failure(Error.DayNotFound);
            var dayDTO = day.Adapt<GetDayDTO>();
            return Result<GetDayDTO>.Success(dayDTO);
        }

        public async Task<Result<List<GetDayDTO>>> GetDays(int skip, int take)
        {
            var result = dayRepo.GetQuery();

            var query = result.ProjectToType<GetDayDTO>();

            var data = await query.Skip(skip).Take(take).ToListAsync();
            return Result<List<GetDayDTO>>.Success(data);
        }

        public async Task<Result> Update(UpdateDayDTO dayDTO)
        {
            var valid = await dayDomainService.Update(dayDTO);
            if (!valid.IsSuccess)
                return Result.Failure(valid.Error!);

            var day = await dayRepo.GetByIdAsync(dayDTO.Id);
            if (day == null)
                return Result.Failure(Error.DayNotFound);

            var result = day.Update(dayDTO);

            if (!result.IsSuccess)
                return Result.Failure(result.Error!);

            dayRepo.Update(day);
            try
            {
                await dayRepo.SaveAsync();
            }
            catch
            {
                return Result.Failure(Error.UpdateFailed);
            }

            return Result.Success();
        }
    }
}
