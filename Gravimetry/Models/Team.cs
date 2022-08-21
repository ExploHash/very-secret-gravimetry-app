using System;
using System.Collections.Generic;

namespace Gravimetry.Models
{
    public class Team
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsPublic { get; set; }

        public bool UserJoined { get; set; } = false;

        public List<SiteMonitor> SiteMonitors { get; set; }

        public List<ApplicationUser> ApplicationUsers { get; set; }
    }
}
