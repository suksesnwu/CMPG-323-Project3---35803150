using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DeviceManagement_WebApp.Models
{
    public partial class Zone
    {
        public Zone()
        {
            Device = new HashSet<Device>();
        }

        [DisplayName("Zone ID")]
        public Guid ZoneId { get; set; }

        [Required]
        [DisplayName("Zone Name")]
        public string ZoneName { get; set; }

        [Required]
        [DisplayName("Zone Description")]
        public string ZoneDescription { get; set; }
        [DisplayName("Date Created")]
        public DateTime DateCreated { get; set; }

        [DisplayName("Device")]
        public virtual ICollection<Device> Device { get; set; }
    }
}
