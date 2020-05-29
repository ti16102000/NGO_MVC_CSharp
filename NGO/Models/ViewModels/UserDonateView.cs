using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NGO.Models.ViewModels
{
    public class UserDonateView
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public int DonateID { get; set; }
        public string TypeCard { get; set; }
        public decimal Money { get; set; }
        public string DonateName { get; set; }
    }
}