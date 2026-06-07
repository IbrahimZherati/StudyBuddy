using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyBuddy.Shared.Helpers.ErrorMessages
{
    public static class AuthErrorMessage
    {
        public const string UserCannotFound = "user can not found";
        public const string PasswordNotCorrect = "password not correct";
        public const string LoginFailed = "login failed";
        public const string LogoutFailed = "logout failed";
        public const string RegisterFailed = "Register Failed";
        public static string EmailNotVerify = "Email not Verify";
    }
}
