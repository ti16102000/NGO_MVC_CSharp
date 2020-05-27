//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NGO.Models.EntityModels
{
    using System;
    using System.Collections.Generic;
    
    public partial class Donate
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Donate()
        {
            this.UserDonates = new HashSet<UserDonate>();
        }
    
        public int ID { get; set; }
        public string DonateName { get; set; }
        public string DonateContent { get; set; }
        public System.DateTime StartDay { get; set; }
        public System.DateTime EndDay { get; set; }
        public int DonateStatus { get; set; }
        public bool DonateHide { get; set; }
        public System.DateTime DonateDateCreate { get; set; }
        public int CateID { get; set; }
        public decimal TotalMoney { get; set; }
    
        public virtual Category Category { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserDonate> UserDonates { get; set; }
    }
}
