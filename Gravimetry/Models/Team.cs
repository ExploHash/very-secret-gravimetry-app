using System;
namespace Gravimetry.Models
{
    public class Team
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsPublic { get; set; }

        public bool UserJoined { get; set; } = false;

        public Team()
        {
           
        }
    }
}
