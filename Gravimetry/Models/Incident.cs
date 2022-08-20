using System;
using System.Collections.Generic;

namespace Gravimetry.Models
{
    public class Incident
    {
        public Incident()
        {

        }

        public int Id { get; set; }

        public DateTime Start { get; set; }

        public DateTime? End { get; set; }

        public List<IncidentNote> IncidentNotes { get; set; }

        public bool IsResolved { get; set; } = false;

    }
}
