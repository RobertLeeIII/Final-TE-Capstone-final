using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Capstone.Models
{
    public class Potluck
    {
        public int PotluckId { get; set; }
        public int HostId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Summary { get; set; }
        [Required]
        public string Location { get; set; }
        [Required]
        public DateTime Time { get; set; }
        public string Theme { get; set; }
        public bool isRecurring { get; set; }
        public int RepeatInterval { get; set; }
        public string Status { get; set; }
        public Courses CourseRequest { get; set; } 

    }
    //TODO: Revisit this model
    public class    NewPotluckDTO
    {
        public int HostId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Summary { get; set; }
        [Required]
        public string Location { get; set; }
        [Required]
        public DateTime Time { get; set; }
        public string Theme { get; set; }
        public bool isRecurring { get; set; }
        public int RepeatInterval { get; set; }
        public string Status { get; set; }
        public Courses CourseRequest { get; set; }

    }
    public class UpdatePotluckDTO
    {
        public int HostId { get; set; }
        public string Name { get; set; }
        public string Summary { get; set; }
        public string Location { get; set; }
        public DateTime Time { get; set; }
        public string Theme { get; set; }
        public bool isRecurring { get; set; }
        public int RepeatInterval { get; set; }
        public Courses CourseRequest { get; set; }
        public string Status { get; set; }

    }
}
