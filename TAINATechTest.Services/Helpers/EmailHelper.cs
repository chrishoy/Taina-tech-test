using System.Text.RegularExpressions;

namespace TAINATechTest.Services.Helpers
{
    public static class EmailHelper
    {
        public static bool IsEmailValid(string emailAddress)
        {
            //Match match = Regex.Match(emailAddress, "/^\\S+@\\S+\\.\\S+$/");
            var match = Regex.Match(emailAddress, "^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$");
            return match.Success;
        }
    }
}
