using NGO.Models.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NGO.Models.DAO
{
    public class ProgramDAO
    {
        public static bool CreateProgram(Program p)
        {
            NGOEntities e = new NGOEntities();
            p.ProDateCreate = DateTime.Now;
            e.Programs.Add(p);
            if (e.SaveChanges() > 0)
            {
                return true;
            }
            return false;
        }
        public static Program GetProgramByID(int id)
        {
            NGOEntities e = new NGOEntities();
            return e.Programs.Find(id);
        }
        public static List<Program> GetProDisplay()
        {
            NGOEntities e = new NGOEntities();
            return e.Programs.Where(w=>w.ProHide==false).OrderByDescending(o=>o.ProDateCreate).ToList();
        }
        public static List<Program> GetProHide()
        {
            NGOEntities e = new NGOEntities();
            return e.Programs.Where(w => w.ProHide == true).OrderByDescending(o=>o.ProDateCreate).ToList();
        }
        public static List<Program> GetProByTypeID(int id)
        {
            NGOEntities e = new NGOEntities();
            return e.Programs.Where(w => w.ProHide == false && w.TypeID==id).OrderByDescending(o => o.ProDateCreate).ToList();
        }
        public static List<Program> GetPros(int take)
        {
            NGOEntities e = new NGOEntities();
            return e.Programs.Where(w => w.ProHide == false).OrderByDescending(o=>o.ProDateCreate).Take(take).ToList();
        }
        public static bool UpdatePro(Program p)
        {
            NGOEntities e = new NGOEntities();
            var item = e.Programs.Find(p.ID);
            item.ProName = p.ProName;
            item.ProContent = p.ProContent;
            item.ProHide = p.ProHide;
            item.TypeID = p.TypeID;
            if (e.SaveChanges() > 0)
            {
                return true;
            }
            return false;
        }
        public static bool DelPro(int id)
        {
            NGOEntities e = new NGOEntities();
            var item = e.Programs.Find(id);
            e.Programs.Remove(item);
            if (e.SaveChanges() > 0)
            {
                return true;
            }
            return false;
        }

        public static List<Program> GetAllPro()
        {
            NGOEntities e = new NGOEntities();
            return e.Programs.ToList();
        }
    }
}