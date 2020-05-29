using NGO.Models;
using NGO.Models.EntityModels;
using NGO.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web;
using System.Web.Mvc;

namespace NGO.Controllers
{
    public class HomeController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var cookieUser = HttpContext.Request.Cookies["userID"];
            if (cookieUser!=null)
            {
                Session["ID"] = cookieUser.Value;
                ViewBag.name = (Repositories.GetUserByID(int.Parse(cookieUser.Value))).UserName;
            }
        }
        public ActionResult Index()
        {
            ViewBag.donate = Repositories.GetSumMoneyDonate();
            ViewBag.user = Repositories.GetUserActive().Count;
            return View();
        }
        #region Donate
        public ActionResult Donate()
        {
            ViewBag.up = Repositories.GetAllDonateByStt(1);
            ViewBag.on = Repositories.GetAllDonateByStt(2);
            ViewBag.end = Repositories.GetAllDonateByStt(3);
            return View();
        }
        public ActionResult DonateInfomation(int id)
        {
            Models.EntityModels.Donate model = Repositories.GetDonateByID(id);
            ViewBag.lsUD = Repositories.GetUDByDonateID(id);
            return View(model);
        }
        public ActionResult UserDonate(UserDonateView u)
        {
            var donate = Repositories.GetDonateByID(u.DonateID);
            if (donate.DonateStatus == 3)
            {
                TempData["error"] = "Sorry, this donate was over!";
                return RedirectToAction("Donate");
            }
            u.DonateName = donate.DonateName;
            Session["ud"] = u;
            return RedirectToAction("PaymentWithPaypal","PayPal");

        }
        #endregion

        #region Program
        public ActionResult Program()
        {
            List<Models.EntityModels.ProgramImage> ls = Repositories.GetListImgMain();
            return View(ls);
        }
        public ActionResult ProgramInfomation(int id)
        {
            Models.EntityModels.Program model = Repositories.GetProgramByID(id);
            ViewBag.imgMain = Repositories.GetImgMain(id);
            ViewBag.lsImg = Repositories.GetAllImg(id);
            return View(model);
        }
        #endregion

        #region About Us
        public ActionResult About()
        {
            List<Models.EntityModels.AboutUs> ls = Repositories.GetAUDisplay();
            return View(ls);
        }

        #endregion

        #region Contact Us
        public ActionResult Contact()
        {
            ContactView dataContact = null;
            string path = Path.Combine(Server.MapPath("~/"), "DataContact.hat");
            if (System.IO.File.Exists(path))
            {
                //Deserialize
                Stream streamD = new FileStream(path, FileMode.OpenOrCreate);
                BinaryFormatter formatterD = new BinaryFormatter();
                //quá trình Deserialize ngược với quá trình Serialize, trả về một object, bạn nhớ ép kiểu để sử dụng.
                dataContact = formatterD.Deserialize(streamD) as ContactView;
                streamD.Close();
            }
            return View(dataContact);
        }
        public ActionResult CreateQues(UserQuestion u)
        {
            if (Repositories.CreateQues(u) == true)
            {
                TempData["success"] = "Your request has been sent to us. We'll get back to you shortly!";
            }
            else
            {
                TempData["error"] = "Something went wrong. Please try again!";
            }
            return RedirectToAction("Contact");
        }
        #endregion

        #region Our Partners
        public ActionResult Partner()
        {
            var ls = Repositories.GetAllPartner().Where(w => w.PartnerActive == true).ToList();
            return View(ls);
        }
        #endregion

        #region Sign In & Sign Up
        public ActionResult SignIn()
        {
            return View();
        }
        public ActionResult Login(string mail, string pwd)
        {
            Models.EntityModels.User user = Repositories.GetUserLogin(mail, pwd);
            if (user == null)
            {
                TempData["error"] = "Your Email or Password incorrect!";
                return View("SignIn");
            }
            if (user.RoleID == 1)
            {
                Session["emp"] = user;
                return RedirectToAction("Index", "Admin");
            }
            HttpCookie cookie = new HttpCookie("userID", user.ID.ToString());
            cookie.Expires = DateTime.Now.AddDays(1);
            Response.Cookies.Add(cookie);
            return RedirectToAction("Index");
        }
        public ActionResult RegisterUser(User u)
        {
            var rs = Repositories.CreateUser(u);
            if (rs<0)
            {
                TempData["error"] = "Email already registered!";
            }
            else if(rs>0)
            {
                TempData["success"] = "Account registration successful!";
            }
            else
            {
                TempData["error"] = "Account registration error occurred. Please try again!";
            }
            return View("SignIn");
        }
        public ActionResult CheckMailForgot(string mail)
        {
            var user = Repositories.GetUserByMail(mail);
            if ( user== null)
            {
                TempData["error"] = "Your email is not registered!";
                return View("SignIn");
            }
            else
            {
                var secret= new Random().Next(1000, 9999);
                try
                {
                    if (ModelState.IsValid)
                    {
                        var senderEmail = new MailAddress("dtmt1610@gmail.com", "NGO");
                        var receiverEmail = new MailAddress(mail, user.UserName);
                        var password = "dtmt16101302";
                        var sub = "NGO support";
                        var body = "Your secret code:" +secret;
                        var smtp = new SmtpClient
                        {
                            Host = "smtp.gmail.com",
                            Port = 25,
                            EnableSsl = true,
                            DeliveryMethod = SmtpDeliveryMethod.Network,
                            UseDefaultCredentials = false,
                            Credentials = new NetworkCredential(senderEmail.Address, password)
                        };
                        using (var mess = new MailMessage(senderEmail, receiverEmail)
                        {
                            Subject = sub,
                            Body = body
                        })
                        {
                            smtp.Send(mess);
                        }
                        Session["secret"] = secret.ToString();
                        TempData["success"] = "Check your mail!";
                        return RedirectToAction("ForgotPwd",new { mail=mail});
                    }
                }
                catch (Exception e)
                {
                    TempData["error"] = "Something went wrong. Please try again!";
                    Console.WriteLine(e.Message);
                }
            }
            return RedirectToAction("SignIn");
        }
        public ActionResult ForgotPwd(string mail)
        {
            ViewBag.mail = mail;
            return View();
        }
        public ActionResult CreateNewPwd(string secret, string mail,string newPwd)
        {
            if (secret != (string)Session["secret"])
            {
                TempData["error"] = "Secret code invalid!";
            }
            else
            {
                if (Repositories.ForgetPwd(mail, newPwd) == true)
                {
                    TempData["success"] = "Create new password successfully! Please login again!";
                    Session.Remove("secret");
                    return View("SignIn");
                }
                else
                {
                    TempData["error"] = "Something went wrong. Please try again!";
                }
            }
            return RedirectToAction("ForgotPwd", new { mail = mail });
        }
        public ActionResult PersonalInfo(int id)
        {
            var model = Repositories.GetUserByID(id);
            ViewBag.lsDonate = Repositories.GetUDByUserID(id);
            return View(model);
        }
        public ActionResult UpdateInfo(User u)
        {
            if (Repositories.UpdateUser(u) == true)
            {
                TempData["success"] = "Update info successfully!";
            }
            else
            {
                TempData["error"] = "Update info failed!";
            }
            return RedirectToAction("PersonalInfo", new { id = u.ID });
        }
        public ActionResult ChangePwd(string mail,string oldPwd,string newPwd)
        {
            if (Repositories.ChangePwd(mail, oldPwd, newPwd) == true)
            {
                TempData["success"] = "Change password successfully!";
            }
            else
            {
                TempData["error"] = "Change password failed!";
            }
            return RedirectToAction("PersonalInfo", new { id = Session["ID"] });
        }
        public ActionResult LogOut()
        {
            Response.Cookies["userID"].Expires = DateTime.Now.AddDays(-1);
            Session.Remove("ID");
            return RedirectToAction("Index");
        }
        #endregion
    }
}