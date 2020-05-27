using NGO.Models.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NGO.Models.DAO
{
    public class CategoryDAO
    {
        public static int InsertCate(Category c)
        {
            NGOEntities e = new NGOEntities();
            var checkName = e.Categories.SingleOrDefault(s => s.CateName.ToLower() == c.CateName.ToLower());
            if (checkName != null)
            {
                return -1;
            }
            e.Categories.Add(c);
            if (e.SaveChanges() > 0)
            {
                return c.ID;
            }
            return 0;
        }
        public static Category GetCateByID(int id)
        {
            NGOEntities e = new NGOEntities();
            return e.Categories.SingleOrDefault(s => s.ID == id);
        }
        public static List<Category> GetAllCate()
        {
            NGOEntities e = new NGOEntities();
            return e.Categories.ToList();
        }
        public static int UpdateCate(Category c)
        {
            NGOEntities e = new NGOEntities();
            var checkName = e.Categories.SingleOrDefault(s => s.CateName.ToLower() == c.CateName.ToLower() && s.ID!=c.ID);
            if (checkName != null)
            {
                return -1;
            }
            var item = e.Categories.Find(c.ID);
            item.CateName = c.CateName;
            if (e.SaveChanges() > 0)
            {
                return c.ID;
            }
            return 0;
        }
        public static bool DelCate(int id)
        {
            NGOEntities e = new NGOEntities();
            e.Categories.Remove(e.Categories.Find(id));
            if (e.SaveChanges() > 0)
            {
                return true;
            }
            return false;
        }
    }
}