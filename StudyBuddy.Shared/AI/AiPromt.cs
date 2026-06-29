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


        public static string GetGenerateSummaryPromt(string text)
        {
            return $"generate summary in html tags to that summary for this text {text}" +
            $"get this summary in text with html tag so i can read it in front" ;
        }

        public static string GetGenerateFlashCard(string text, int take)
        {
            return $"generate small question and answer this question i need simple and fast to answer" + 
            $"and make in json format {{Question : [the question],Answer : [the answer]}} and return in json list " + 
            $"make generate this {take} times so that i need {take} in the list so that the final is [{{Question:[the question], Answer:[the answer]}},{{Question:[the question], Answer:[the answer]}}]";
        }
    }
}
