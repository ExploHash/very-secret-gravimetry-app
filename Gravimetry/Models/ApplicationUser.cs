using System;
namespace Gravimetry.Models
{
    public class ApplicationUser
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public bool UserJoined { get; set; } = false;
    }
}
