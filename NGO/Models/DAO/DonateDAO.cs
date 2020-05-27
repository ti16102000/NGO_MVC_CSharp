using NGO.Models.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NGO.Models.DAO
{
    public class DonateDAO
    {
        public static bool CreateDonate(Donate d)
        {
            NGOEntities e = new NGOEntities();
            d.DonateStatus = 1;
            d.DonateDateCreate = DateTime.Now;
            d.TotalMoney = 0;
            e.Donates.Add(d);
            if (e.SaveChanges() > 0)
            {
                return true;
            }
            return false;
        }
        public static Donate GetDonateByID(int id)
        {
            NGOEntities e = new NGOEntities();
            return e.Donates.Find(id);
        }
        public static List<Donate> GetAllDonateByStt(int stt)
        {
            NGOEntities e = new NGOEntities();
            var ls= e.Donates.Where(s => s.DonateStatus == stt && s.DonateHide==false).OrderByDescending(o => o.DonateDateCreate).ToList();
            CheckStt(ls);
            var lsNew= e.Donates.Where(s => s.DonateStatus == stt && s.DonateHide == false).OrderByDescending(o => o.DonateDateCreate).ToList();
            return lsNew;
        }
        public static List<Donate> GetAllDonateByCate(int cate)
        {
            NGOEntities e = new NGOEntities();
            var ls= e.Donates.Where(s => s.CateID == cate && s.DonateHide==false).OrderByDescending(o => o.DonateDateCreate).ToList();
            CheckStt(ls);
            var lsNew = e.Donates.Where(s => s.CateID == cate && s.DonateHide == false).OrderByDescending(o => o.DonateDateCreate).ToList();
            return lsNew;
        }
        public static List<Donate> GetDonateHide()
        {
            NGOEntities e = new NGOEntities();
            var ls= e.Donates.Where(s => s.DonateHide==true).OrderByDescending(o => o.DonateDateCreate).ToList();
            CheckStt(ls);
            var lsNew = e.Donates.Where(s => s.DonateHide == true).OrderByDescending(o => o.DonateDateCreate).ToList();
            return lsNew;
        }
        public static List<Donate> GetDonates(int stt,int take)
        {
            NGOEntities e = new NGOEntities();
            var ls = e.Donates.Where(s => s.DonateStatus == stt && s.DonateHide == true).OrderByDescending(o => o.DonateDateCreate).Take(take).ToList();
            CheckStt(ls);
            var lsNew = e.Donates.Where(s => s.DonateStatus == stt && s.DonateHide == true).OrderByDescending(o => o.DonateDateCreate).Take(take).ToList();
            return lsNew;
        }
        public static bool UpdateDonate(Donate d)
        {
            NGOEntities e = new NGOEntities();
            var item = e.Donates.Find(d.ID);
            item.DonateName = d.DonateName;
            item.DonateContent = d.DonateContent;
            item.StartDay = d.StartDay;
            item.EndDay = d.EndDay;
            item.DonateHide = d.DonateHide;
            item.CateID = d.CateID;
            if (item.DonateStatus == 1 && d.StartDay <= DateTime.Now)
            {
                d.DonateStatus = 2;
            }
            else if (item.DonateStatus == 2 && d.EndDay < DateTime.Now)
            {
                d.DonateStatus = 3;
            }else if (d.StartDay > DateTime.Now)
            {
                d.DonateStatus = 1;
            }
            if (e.SaveChanges() > 0)
            {
                return true;
            }
            return false;
        }
        public static void CheckStt(List<Donate> ls)
        {
            NGOEntities e = new NGOEntities();
            foreach (var item in ls)
            {
                var d = e.Donates.Find(item.ID);
                if(item.DonateStatus==1 && item.StartDay <= DateTime.Now)
                {
                    d.DonateStatus = 2;
                }else if(item.DonateStatus==2 && item.EndDay < DateTime.Now)
                {
                    d.DonateStatus = 3;
                }
                e.SaveChanges();
            }
        }
    }
}