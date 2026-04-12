using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using StudyBuddy.Domain.Entities;
using StudyBuddy.Shared.DTOs.DayDTO;
using StudyBuddy.Shared.Results;
using StudyBuddy.Shared.Helpers.ErrorMessages;
namespace StudyBuddy.Domain.Services.Days
{
    public class DayDomainService : IDayDomainService
    {
        private readonly IRepo<Day> dayRepo;


        public DayDomainService(IRepo<Day> dayRepo
        )
        {
            this.dayRepo = dayRepo;

        }

        public async Task<Result> Create(CreateDayDTO dayDTO)
        {
            
            if(await dayRepo.ExistsAsync(a => a.Name == dayDTO.Name))
                return Result.Failure(Error.DayAlreadyExists);
            return Result.Success();
        }

        public async Task<Result> Delete(int Id)
        {
            if(!await dayRepo.ExistsAsync(a => a.Id == Id))
                return Result.Failure(Error.DayNotFound);
            return Result.Success();
        }

        public async Task<Result> Update(UpdateDayDTO dayDTO)
        { 
            if (!await dayRepo.ExistsAsync(a => a.Id == dayDTO.Id))
                return Result.Failure(Error.DayNotFound);
            
            if (await dayRepo.ExistsAsync(a => a.Name == dayDTO.Name && a.Id != dayDTO.Id ))
                return Result.Failure(Error.DayAlreadyExists);
            return Result.Success();
        }
    }
}
