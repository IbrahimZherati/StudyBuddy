using StudyBuddy.Shared.DTOs.MajorDTO;
using StudyBuddy.Shared.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyBuddy.Application.Services.Majors
{
    public interface IMajorService
    {
        Task<Result> Create(CreateMajorDTO majorDTO);
        Task<Result> Update(UpdateMajorDTO majorDTO);
        Task<Result<GetMajorDTO>> GetMajorById(int id);
        Task<Result> Delete(int id);
        Task<Result<List<GetMajorDTO>>> GetMojors(int skip, int take);


    }
}
