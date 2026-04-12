using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mapster;
using StudyBuddy.Shared.DTOs.DayDTO;
using StudyBuddy.Shared.Results;

namespace StudyBuddy.Application.Services
{
     public interface IDayService
     {
         Task<Result<GetDayDTO>> Create(CreateDayDTO dayDTO);
         Task<Result<GetDayDTO>> Update(UpdateDayDTO dayDTO);
         Task<Result<GetDayDTO>> GetDayById(int id);
         Task<Result> Delete(int id);
         Task<Result<DataResponse<GetDayDTO>>> GetDays(int skip, int take);
     }
}
     
