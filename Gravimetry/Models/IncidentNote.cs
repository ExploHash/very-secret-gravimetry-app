using System;
namespace Gravimetry.Models
{
    public class IncidentNote
    {
        public int Id { get; set; }

        public string Message { get; set; }

        public bool IsPublic { get; set; } = false;

        public ApplicationUser ApplicationUser { get; set; }

    }
}
