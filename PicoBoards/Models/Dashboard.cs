using System.ComponentModel.DataAnnotations;

namespace PicoBoards.Models
{
    public class Dashboard
    {
        [Display(Name = "Username")]
        public string UserName { get; set; }

        public Dashboard() { }

        public Dashboard(string userName)
            => UserName = userName;
    }
}