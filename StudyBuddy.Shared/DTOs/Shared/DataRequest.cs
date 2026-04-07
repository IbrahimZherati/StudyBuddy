using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyBuddy.Application.DTOs.Shared
{
    public class DataRequest
    {
        public string ApiData { get; set; } = null!;
        public int Skip { get; set; }
        public int Take { get; set; }

        public string? Filter { get; set; }

        public string? Sort { get; set; }

        public string? ApiOptions { get; set; }
    }
}
