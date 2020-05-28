using NGO.CommonHelper;
using NGO.Models.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NGO.Models.DAO
{
    public class UserDAO
    {
        public static int CreateUser(User u)
        {
            NGOEntities e = new NGOEntities();
            var checkMail = GetUserByMail(u.UserMail);
            if (checkMail != null)
            {
                return -1;
            }
            u.UserActive = true;
            u.RoleID = 2;
            u.UserDateCreate = DateTime.Now;
            u.UserVolunteer = false;
            u.UserPwd = Encrypt.EncryptPasswordMD5(u.UserPwd);
            u.MoneyDonate = 0;
            e.Users.Add(u);
            if (e.SaveChanges() > 0)
            {
                return u.ID;
            }
            return 0;
        }
        public static User GetUserByID(int id)
        {
            NGOEntities e = new NGOEntities();
            return e.Users.Find(id);
        }
        public static User GetUserByMail(string mail)
        {
            NGOEntities e = new NGOEntities();
            return e.Users.SingleOrDefault(s => s.UserMail == mail && s.UserActive==true);
        }
        public static User GetUserLogin(string mail, string pass)
        {
            NGOEntities e = new NGOEntities();
            var pwd= Encrypt.EncryptPasswordMD5(pass);
            return e.Users.SingleOrDefault(s => s.UserMail == mail && s.UserPwd == pwd && s.UserActive==true);
        }
        public static List<User> GetUserActive()
        {
            NGOEntities e = new NGOEntities();
            return e.Users.Where(w => w.UserActive == true && w.RoleID==2).OrderByDescending(o=>o.UserDateCreate).ToList();
        }
        public static List<User> GetUserUnactive()
        {
            NGOEntities e = new NGOEntities();
            return e.Users.Where(w => w.UserActive == false && w.RoleID == 2).OrderByDescending(o => o.UserDateCreate).ToList();
        }

        public static List<User> GetUserVolunteer()
        {
            NGOEntities e = new NGOEntities();
            return e.Users.Where(w => w.UserVolunteer == true && w.RoleID == 2).ToList();
        }
        public static bool UpdateUser(User u)
        {
            NGOEntities e = new NGOEntities();
            var item= e.Users.Find(u.ID);
            item.UserName = u.UserName;
            item.UserGender = u.UserGender;
            item.UserDOB = u.UserDOB;
            if (e.SaveChanges() > 0)
            {
                return true;
            }
            return false;
        }
        public static bool ChangePwd(string mail,string oldPwd, string newPwd)
        {
            NGOEntities e = new NGOEntities();
            var old = Encrypt.EncryptPasswordMD5(oldPwd);
            var user= e.Users.SingleOrDefault(s=>s.UserMail==mail && s.UserPwd==old && s.UserActive==true);
            if (user != null)
            {
                user.UserPwd = Encrypt.EncryptPasswordMD5(newPwd);
                if (e.SaveChanges() > 0)
                {
                    return true;
                }
            }
            return false;
        }
        public static bool ForgetPwd(string mail,string newPwd)
        {
            NGOEntities e = new NGOEntities();
            var user = e.Users.SingleOrDefault(s=>s.UserMail==mail && s.UserActive==true);
            if (user != null)
            {
                user.UserPwd = Encrypt.EncryptPasswordMD5(newPwd);
                if (e.SaveChanges() > 0)
                {
                    return true;
                }
            }
            return false;
        }
        public static bool CheckActive(int id)
        {
            NGOEntities e = new NGOEntities();
            var item = e.Users.Find(id);
            if (item.UserActive == true)
            {
                item.UserActive = false;
            }
            else
            {
                item.UserActive = true;
            }
            if (e.SaveChanges() > 0)
            {
                return true;
            }
            return false;
        }
        public static bool CheckVolunteer(int id)
        {
            NGOEntities e = new NGOEntities();
            var item = e.Users.Find(id);
            if (item.UserVolunteer == true)
            {
                item.UserVolunteer = false;
            }
            else
            {
                item.UserVolunteer = true;
            }
            if (e.SaveChanges() > 0)
            {
                return true;
            }
            return false;
        }
         
    }
}