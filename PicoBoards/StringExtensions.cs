using System.ComponentModel.DataAnnotations;

namespace PicoBoards
{
    public static class StringExtensions
    {
        public static bool IsValidEmailAddress(this string value)
            => new EmailAddressAttribute().IsValid(value);
    }
}