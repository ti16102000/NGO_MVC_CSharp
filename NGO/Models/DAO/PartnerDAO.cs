using NGO.Models.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NGO.Models.DAO
{
    public class PartnerDAO
    {
        public static bool InsertPartner(Partner p)
        {
            NGOEntities e = new NGOEntities();
            e.Partners.Add(p);
            return e.SaveChanges() > 0 ? true : false;
        }
        public static Partner GetPartnerByID(int id)
        {
            NGOEntities e = new NGOEntities();
            return e.Partners.Find(id);
        }
        public static List<Partner> GetAllPartner()
        {
            NGOEntities e = new NGOEntities();
            return e.Partners.ToList();
        }
        public static List<Partner> GetPartnerActive()
        {
            NGOEntities e = new NGOEntities();
            return e.Partners.Where(w=>w.PartnerActive==true).ToList();
        }
        public static bool UpdatePartner(Partner p)
        {
            NGOEntities e = new NGOEntities();
            var item = e.Partners.Find(p.ID);
            item.PartnerName = p.PartnerName;
            item.PartnerImage = p.PartnerImage;
            item.PartnerLink = p.PartnerLink;
            item.PartnerActive = p.PartnerActive;
            return e.SaveChanges() > 0 ? true : false;
        }
    }
}