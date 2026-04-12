using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using StudyBuddy.Shared.DTOs.DayDTO;
using StudyBuddy.Shared.Results;
namespace StudyBuddy.Domain.Services.Days
{
    public interface IDayDomainService
    {
        Task<Result> Create(CreateDayDTO dayDTO);
        Task<Result> Update(UpdateDayDTO dayDTO);
        Task<Result> Delete(int Id);
    } 
}
