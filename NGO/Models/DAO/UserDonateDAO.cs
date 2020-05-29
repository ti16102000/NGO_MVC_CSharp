using NGO.Models.EntityModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace NGO.Models.DAO
{
    public class UserDonateDAO
    {
        public static int InsertUD(UserDonate ud)
        {
            NGOEntities e = new NGOEntities();
            ud.TypeCard = "VISA";
            ud.DateCreate = DateTime.Now;
            e.UserDonates.Add(ud);
            if (e.SaveChanges() > 0)
            {
                var donate = e.Donates.Find(ud.DonateID);
                donate.TotalMoney += ud.Money;
                var user = e.Users.Find(ud.UserID);
                user.MoneyDonate += ud.Money;
                e.SaveChanges();
                return 1;
            }
            return 0;
        }
        public static List<UserDonate> GetUDByDonateID(int id)
        {
            NGOEntities e = new NGOEntities();
            return e.UserDonates.Where(s => s.DonateID == id).OrderByDescending(o => o.DateCreate).ToList();
        }
        public static List<UserDonate> GetUDToday()
        {
            NGOEntities e = new NGOEntities();
            return e.UserDonates.Where(s => s.DateCreate.ToString("yyyy/MM/dd")==DateTime.Now.ToString("yyyy/MM/dd")).OrderByDescending(o => o.DateCreate).ToList();
        }
        public static List<UserDonate> GetUDByUserID(int id)
        {
            NGOEntities e = new NGOEntities();
            return e.UserDonates.Where(s => s.UserID == id).OrderByDescending(o => o.DateCreate).ToList();
        }
        public static decimal GetTotalDonateByUserID(int id)
        {
            NGOEntities e = new NGOEntities();
            return e.UserDonates.Where(s => s.UserID == id).Sum(s => s.Money);
        }
        public static List<UserDonate> GetDonateCurrentDay()
        {
            NGOEntities e = new NGOEntities();
            var currDay = DateTime.Now;
            return e.UserDonates.Where(w => DbFunctions.TruncateTime(w.DateCreate)== DbFunctions.TruncateTime(currDay)).ToList();
        }
    }
}