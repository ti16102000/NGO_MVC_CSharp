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
    
    public partial class ProgramImage
    {
        public int ID { get; set; }
        public int ProID { get; set; }
        public string ImgFileName { get; set; }
        public bool ImgMain { get; set; }
    
        public virtual Program Program { get; set; }
    }
}
