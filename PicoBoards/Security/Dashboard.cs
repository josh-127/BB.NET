using System.ComponentModel.DataAnnotations;

namespace PicoBoards.Security
{
    public class Dashboard
    {
        public int UserId { get; set; }

        [Display(Name = "Username")]
        public string UserName { get; set; }

        public Dashboard() { }

        public Dashboard(int userId, string userName)
            => (UserId, UserName) = (userId, userName);
    }
}