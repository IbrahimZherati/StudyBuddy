using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyBuddy.Shared.DTOs.ClientUserDTO
{
    public class ClientUserSearchDTO : InfoClientUserDTO
    {
        public bool IsFriend { get; set; }

        public bool isRequestReceived { get; set; }

        public bool isRequestSent { get; set; }
    }
}
