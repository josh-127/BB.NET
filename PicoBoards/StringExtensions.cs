using System.ComponentModel.DataAnnotations;
using PicoBoards.DataAnnotations;

namespace PicoBoards
{
    public static class StringExtensions
    {
        public static bool IsValidEmailAddress(this string value)
            => new EmailAddressAttribute().IsValid(value);

        public static bool IsValidUserName(this string value)
            => new IdentifierAttribute().IsValid(value);
    }
}