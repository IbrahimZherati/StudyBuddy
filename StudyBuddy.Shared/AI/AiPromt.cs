using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyBuddy.Shared.AI
{
    public class AiPromt
    {
       

        public static string GetGenerateSkillPromt(string bio)
        {
            return $"generate skill tag you think from bio {bio}" +
                   $" and return skill with coma sperate each and plz not make anything dont say any thing " +
                   $"just give me skills just best 5 to 8 skill sperate with coma";
        }


    }
}
