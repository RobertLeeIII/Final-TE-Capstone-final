using System;

namespace Capstone.Models
{
    public class Potluck
    {
        public int PotluckId { get; set; }
        public int HostId { get; set; }
        public string Name { get; set; }
        public string Summary { get; set; }
        public string Location { get; set; }
        public DateTime Time { get; set; }
        public string Theme { get; set; }
        public bool isRecurring { get; set; }
        public int RepeatInterval { get; set; }
        public string Status { get; set; }
    }
    public class NewPotluckDTO
    {
        public int HostId { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public DateTime Time { get; set; }
    }
    public class UpdatePotluckDTO
    {
        public string Name { get; set; }
        public string Summary { get; set; }
        public string Location { get; set; }
        public DateTime Time { get; set; }
        public string Theme { get; set; }
        public bool isRecurring { get; set; }
        public int RepeatInterval { get; set; }
    }
}
