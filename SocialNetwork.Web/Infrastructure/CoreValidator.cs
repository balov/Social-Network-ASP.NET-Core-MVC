using System;

namespace SocialNetwork.Web.Infrastructure
{
    public static class CoreValidator
    {
        public static bool CheckIfStringIsNullOrEmpty(string input) => String.IsNullOrEmpty(input);
    }
}