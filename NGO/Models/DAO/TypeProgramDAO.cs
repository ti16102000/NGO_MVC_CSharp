using NGO.Models.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NGO.Models.DAO
{
    public class TypeProgramDAO
    {
        public static int InsertType(TypeProgram t)
        {
            NGOEntities e = new NGOEntities();
            var checkName = e.TypePrograms.SingleOrDefault(s => s.TypeName.ToLower() == t.TypeName.ToLower());
            if (checkName != null)
            {
                return -1;
            }
            e.TypePrograms.Add(t);
            if (e.SaveChanges() > 0)
            {
                return t.ID;
            }
            return 0;
        }
        public static TypeProgram GetTypeByID(int id)
        {
            NGOEntities e = new NGOEntities();
            return e.TypePrograms.SingleOrDefault(s => s.ID == id);
        }
        public static List<TypeProgram> GetAllType()
        {
            NGOEntities e = new NGOEntities();
            return e.TypePrograms.ToList();
        }
        public static int UpdateType(TypeProgram t)
        {
            NGOEntities e = new NGOEntities();
            var checkName = e.TypePrograms.SingleOrDefault(s => s.TypeName.ToLower() == t.TypeName.ToLower() && s.ID!=t.ID);
            if (checkName != null)
            {
                return -1;
            }
            var item = e.TypePrograms.Find(t.ID);
            item.TypeName = t.TypeName;
            if (e.SaveChanges() > 0)
            {
                return t.ID;
            }
            return 0;
        }
        public static bool DelType(int id)
        {
            NGOEntities e = new NGOEntities();
            e.TypePrograms.Remove(e.TypePrograms.Find(id));
            if (e.SaveChanges() > 0)
            {
                return true;
            }
            return false;
        }
    }
}