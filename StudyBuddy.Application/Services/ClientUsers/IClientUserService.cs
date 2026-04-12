using StudyBuddy.Shared.DTOs.ClientUserDTO;
using StudyBuddy.Shared.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyBuddy.Application.Services.ClientUsers
{
    public interface IClientUserService
    {
        Task<Result<InfoClientUserDTO>> Update(UpdateClientUserDTO clientUserDTO);
        Task<Result<GetProfileClientUserDTO>> GetProfile(Guid userId);
    }
}
