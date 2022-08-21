using System;
using System.Collections.Generic;

namespace Gravimetry.Models
{
    public class SiteMonitor
    {
        public int Id { get; set; }

        public string Instance { get; set; }

        public string Job { get; set; }

        public int AverageUptime { get; set; }

        public List<Incident> Incidents { get; set; }

        public bool UserJoined { get; set; } = false;
    }
}
