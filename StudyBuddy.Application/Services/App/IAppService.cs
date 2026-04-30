using StudyBuddy.Shared.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyBuddy.Application.Services.App
{
    public interface IAppService
    {
        Task<Result> Start(int clientId , string rootPath);
    }
}
