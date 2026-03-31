using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyBuddy.Shared.Helpers.ErrorMessages
{
    public class Error
    {
        public const string MessageMustNotNull = "the message must`t null";
        public const string UserNotFound = "user not found";
        public const string DeleteFailed = "Delete Failed";
        public const string CreateFailed = "Create Failed";
        public const string UpdateFailed = "Update Failed";
        public const string ItemNotFound = "Item Not Found";
        public const string GroupChatNameAlreadyExists = "group chat name alreadyExists";
    }
}
