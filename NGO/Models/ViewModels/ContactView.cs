using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NGO.Models.ViewModels
{
    [Serializable]
    public class ContactView
    {
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
    }
}