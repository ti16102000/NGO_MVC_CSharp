using NGO.Models.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NGO.Models.DAO
{
    public class ProgramImageDAO
    {
        public static bool InsertImg(ProgramImage pi)
        {
            NGOEntities e = new NGOEntities();
            e.ProgramImages.Add(pi);
            if (e.SaveChanges() > 0)
            {
                return true;
            }
            return false;
        }
        public static ProgramImage GetImgMain(int id)
        {
            NGOEntities e = new NGOEntities();
            return e.ProgramImages.SingleOrDefault(s => s.ProID == id && s.ImgMain == true );
        }
        public static List<ProgramImage> GetAllImg(int id)
        {
            NGOEntities e = new NGOEntities();
            return e.ProgramImages.Where(w=>w.ProID==id).ToList();
        }
        public static bool DelImg(int id)
        {
            NGOEntities e = new NGOEntities();
            e.ProgramImages.Remove(e.ProgramImages.Find(id));
            if (e.SaveChanges() > 0)
            {
                return true;
            }
            return false;
        }

        public static bool CheckImgMain(int id)
        {
            NGOEntities e = new NGOEntities();
            var item = e.ProgramImages.Find(id);
            if (item.ImgMain == true)
            {
                item.ImgMain = false;
            }
            else
            {
                item.ImgMain = true;
            }
            if (e.SaveChanges() > 0)
            {
                return true;
            }
            return false;
        }
        public static ProgramImage GetImgByID(int id)
        {
            NGOEntities e = new NGOEntities();
            return e.ProgramImages.Find(id);
        }
    }
}