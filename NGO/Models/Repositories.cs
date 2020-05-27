using NGO.Models.DAO;
using NGO.Models.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NGO.Models
{
    public class Repositories
    {
        #region Category
        public static int InsertCate(Category c)
        {
            return CategoryDAO.InsertCate(c);
        }
        public static Category GetCateByID(int id)
        {
            return CategoryDAO.GetCateByID(id);
        }
        public static List<Category> GetAllCate()
        {
            return CategoryDAO.GetAllCate();
        }
        public static int UpdateCate(Category c)
        {
            return CategoryDAO.UpdateCate(c);
        }
        public static bool DelCate(int id)
        {
            return CategoryDAO.DelCate(id);
        }
        #endregion

        #region Type Program
        public static int InsertType(TypeProgram t)
        {
            return TypeProgramDAO.InsertType(t);
        }
        public static TypeProgram GetTypeByID(int id)
        {
            return TypeProgramDAO.GetTypeByID(id);
        }
        public static List<TypeProgram> GetAllType()
        {
            return TypeProgramDAO.GetAllType();
        }
        public static int UpdateType(TypeProgram t)
        {
            return TypeProgramDAO.UpdateType(t);
        }
        public static bool DelType(int id)
        {
            return TypeProgramDAO.DelType(id);
        }
        #endregion

        #region Program
        public static bool CreateProgram(Program p)
        {
            return ProgramDAO.CreateProgram(p);
        }
        public static Program GetProgramByID(int id)
        {
            return ProgramDAO.GetProgramByID(id);
        }
        public static List<Program> GetProDisplay()
        {
            return ProgramDAO.GetProDisplay();
        }
        public static List<Program> GetProHide()
        {
            return ProgramDAO.GetProHide();
        }
        public static bool UpdatePro(Program p)
        {
            return ProgramDAO.UpdatePro(p);
        }
        public static bool DelPro(int id)
        {
            return ProgramDAO.DelPro(id);
        }
        public static List<Program> GetProByTypeID(int id)
        {
            return ProgramDAO.GetProByTypeID(id);
        }
        public static List<Program> GetPros(int take)
        {
            return ProgramDAO.GetPros(take);
        }
        public static List<Program> GetAllPro()
        {
            return ProgramDAO.GetAllPro();
        }
        #endregion

        #region Program Image
        public static bool InsertImg(ProgramImage pi)
        {
            return ProgramImageDAO.InsertImg(pi);
        }
        public static ProgramImage GetImgMain(int id)
        {
            return ProgramImageDAO.GetImgMain(id);
        }
        public static List<ProgramImage> GetAllImg(int id)
        {
            return ProgramImageDAO.GetAllImg(id);
        }
        public static bool DelImg(int id)
        {
            return ProgramImageDAO.DelImg(id);
        }
        public static bool CheckImgMain(int id)
        {
            return ProgramImageDAO.CheckImgMain(id);
        }
        public static ProgramImage GetImgByID(int id)
        {
            return ProgramImageDAO.GetImgByID(id);
        }
        #endregion

        #region About Us
        public static bool CreateAU(AboutUs au)
        {
            return AboutUsDAO.CreateAU(au);
        }
        public static AboutUs GetAUByID(int id)
        {
            return AboutUsDAO.GetAUByID(id);
        }
        public static List<AboutUs> GetAUHide()
        {
            return AboutUsDAO.GetAUHide();
        }
        public static List<AboutUs> GetAUDisplay()
        {
            return AboutUsDAO.GetAUDisplay();
        }
        public static bool UpdateAU(AboutUs au)
        {
            return AboutUsDAO.UpdateAU(au);
        }
        public static List<AboutUs> GetAllAU()
        {
            return AboutUsDAO.GetAllAU();
        }
        #endregion

        #region User
        public static int CreateUser(User u)
        {
            return UserDAO.CreateUser(u);
        }
        public static User GetUserByID(int id)
        {
            return UserDAO.GetUserByID(id);
        }
        public static User GetUserByMail(string mail)
        {
            return UserDAO.GetUserByMail(mail);
        }
        public static User GetUserLogin(string mail, string pass)
        {
            return UserDAO.GetUserLogin(mail, pass);
        }
        public static List<User> GetUserActive()
        {
            return UserDAO.GetUserActive();
        }
        public static List<User> GetUserUnactive()
        {
            return UserDAO.GetUserUnactive();
        }
        public static List<User> GetUserVolunteer()
        {
            return UserDAO.GetUserVolunteer();
        }
        public static bool UpdateUser(User u)
        {
            return UserDAO.UpdateUser(u);
        }
        public static bool ChangePwd(string mail, string oldPwd, string newPwd)
        {
            return UserDAO.ChangePwd(mail, oldPwd, newPwd);
        }
        public static bool ForgetPwd(string mail, string newPwd)
        {
            return UserDAO.ForgetPwd(mail, newPwd);
        }
        public static bool CheckActive(int id)
        {
            return UserDAO.CheckActive(id);
        }
        public static bool CheckVolunteer(int id)
        {
            return UserDAO.CheckVolunteer(id);
        }
        #endregion

        #region Donate
        public static bool CreateDonate(Donate d)
        {
            return DonateDAO.CreateDonate(d);
        }
        public static Donate GetDonateByID(int id)
        {
            return DonateDAO.GetDonateByID(id);
        }
        public static List<Donate> GetAllDonateByStt(int stt)
        {
            return DonateDAO.GetAllDonateByStt(stt);
        }
        public static List<Donate> GetAllDonateByCate(int cate)
        {
            return DonateDAO.GetAllDonateByCate(cate);
        }
        public static List<Donate> GetDonateHide()
        {
            return DonateDAO.GetDonateHide();
        }
        public static List<Donate> GetDonates(int stt, int take)
        {
            return DonateDAO.GetDonates(stt, take);
        }
        public static bool UpdateDonate(Donate d)
        {
            return DonateDAO.UpdateDonate(d);
        }
        #endregion

        #region User Donate
        public static bool InsertUD(UserDonate ud)
        {
            return UserDonateDAO.InsertUD(ud);
        }
        public static List<UserDonate> GetUDByDonateID(int id)
        {
            return UserDonateDAO.GetUDByDonateID(id);
        }
        public static List<UserDonate> GetUDToday()
        {
            return UserDonateDAO.GetUDToday();
        }
        public static List<UserDonate> GetUDByUserID(int id)
        {
            return UserDonateDAO.GetUDByUserID(id);
        }
        public static decimal GetTotalDonateByUserID(int id)
        {
            return UserDonateDAO.GetTotalDonateByUserID(id);
        }

        #endregion

        #region User Question
        public static bool CreateQues(UserQuestion q)
        {
            return UserQuestionDAO.CreateQues(q);
        }
        public static UserQuestion GetQuesByID(int id)
        {
            return UserQuestionDAO.GetQuesByID(id);
        }
        public static List<UserQuestion> GetQuesNew()
        {
            return UserQuestionDAO.GetQuesNew();
        }
        public static bool DelQues(int id)
        {
            return UserQuestionDAO.DelQues(id);
        }
        public static bool InsertAns(int id, string answer)
        {
            return UserQuestionDAO.InsertAns(id, answer);
        }
        #endregion

        #region Partner
        public static bool InsertPartner(Partner p)
        {
            return PartnerDAO.InsertPartner(p);
        }
        public static Partner GetPartnerByID(int id)
        {
            return PartnerDAO.GetPartnerByID(id);
        }
        public static List<Partner> GetAllPartner()
        {
            return PartnerDAO.GetAllPartner();
        }
        public static List<Partner> GetPartnerActive()
        {
            return PartnerDAO.GetPartnerActive();
        }
        public static bool UpdatePartner(Partner p)
        {
            return PartnerDAO.UpdatePartner(p);
        }
        #endregion
    }

}