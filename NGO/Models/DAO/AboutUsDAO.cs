using NGO.Models.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NGO.Models.DAO
{
    public class AboutUsDAO
    {
        public static bool CreateAU(AboutUs au)
        {
            NGOEntities e = new NGOEntities();
            e.AboutUs1.Add(au);
            if (e.SaveChanges() > 0)
            {
                return true;
            }
            return false;
        }
        public static AboutUs GetAUByID(int id)
        {
            NGOEntities e = new NGOEntities();
            return e.AboutUs1.Find(id);
        }
        public static List<AboutUs> GetAUHide()
        {
            NGOEntities e = new NGOEntities();
            return e.AboutUs1.Where(w => w.AboutHide == true).ToList();
        }
        public static List<AboutUs> GetAUDisplay()
        {
            NGOEntities e = new NGOEntities();
            return e.AboutUs1.Where(w=>w.AboutHide==false).ToList();
        }
        public static bool UpdateAU(AboutUs au)
        {
            NGOEntities e = new NGOEntities();
            var item= e.AboutUs1.Find(au.ID);
            item.AboutName = au.AboutName;
            item.AboutContent = au.AboutContent;
            item.AboutHide = au.AboutHide;
            item.AboutImage = au.AboutImage;
            if (e.SaveChanges() > 0)
            {
                return true;
            }
            return false;
        }
        public static List<AboutUs> GetAllAU()
        {
            NGOEntities e = new NGOEntities();
            return e.AboutUs1.ToList();
        }
    }
}