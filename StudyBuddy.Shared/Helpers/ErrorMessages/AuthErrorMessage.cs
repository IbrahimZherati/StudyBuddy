﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyBuddy.Shared.Helpers.ErrorMessages
{
    public static class AuthErrorMessage
    {
        public const string UserCannotFound = "User was not found";
        public const string PasswordNotCorrect = "Password is not correct";
        public const string LoginFailed = "Login failed";
        public const string LogoutFailed = "Logout failed";
        public const string RegisterFailed = "Register Failed";
        public static string EmailNotVerify = "Email was not verified";
    }
}