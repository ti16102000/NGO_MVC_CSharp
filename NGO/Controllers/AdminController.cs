using NGO.Models;
using NGO.Models.EntityModels;
using NGO.Models.ViewModels;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace NGO.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        #region Category
        public ActionResult Category()
        {
            System.Collections.Generic.List<Category> ls = Repositories.GetAllCate();
            return View(ls);
        }
        public ActionResult CreateCategory(Category c)
        {
            int stt = Repositories.InsertCate(c);
            if (stt == -1)
            {
                TempData["error"] = "Duplicate name!";
            }
            else if (stt == 0)
            {
                TempData["error"] = "Create new category failed!";
            }
            else
            {
                TempData["success"] = "Create new category successfully!";
            }
            return RedirectToAction("Category");
        }
        public ActionResult EditCate(int id)
        {
            Category cate = Repositories.GetCateByID(id);
            return View(cate);
        }
        public ActionResult UpdateCate(Category c)
        {
            int stt = Repositories.UpdateCate(c);
            if (stt == -1)
            {
                TempData["error"] = "Duplicate name!";
                return RedirectToAction("EditCate", new { id = c.ID });
            }
            else if (stt == 0)
            {
                TempData["error"] = "Update category failed!";
            }
            else
            {
                TempData["success"] = "Update category successfully!";
            }
            return RedirectToAction("Category");
        }
        #endregion

        #region Type Program
        public ActionResult TypeProgram()
        {
            System.Collections.Generic.List<TypeProgram> ls = Repositories.GetAllType();
            return View(ls);
        }
        public ActionResult CreateType(TypeProgram tp)
        {
            int stt = Repositories.InsertType(tp);
            if (stt == -1)
            {
                TempData["error"] = "Duplicate name!";
            }
            else if (stt == 0)
            {
                TempData["error"] = "Create new type failed!";
            }
            else
            {
                TempData["success"] = "Create new type successfully!";
            }
            return RedirectToAction("TypeProgram");
        }
        public ActionResult EditType(int id)
        {
            TypeProgram type = Repositories.GetTypeByID(id);
            return View(type);
        }
        public ActionResult UpdateType(TypeProgram tp)
        {
            int stt = Repositories.UpdateType(tp);
            if (stt == -1)
            {
                TempData["error"] = "Duplicate name!";
                return RedirectToAction("EditType", new { id = tp.ID });
            }
            else if (stt == 0)
            {
                TempData["error"] = "Update type failed!";
            }
            else
            {
                TempData["success"] = "Update type successfully!";
            }
            return RedirectToAction("TypeProgram");
        }
        #endregion

        #region Program and Images
        public ActionResult Program()
        {
            System.Collections.Generic.List<TypeProgram> lsType = Repositories.GetAllType();
            return View(lsType);
        }
        public ActionResult ListProgram()
        {
            System.Collections.Generic.List<Program> ls = Repositories.GetAllPro();
            ViewBag.hide = Repositories.GetProHide();
            return View(ls);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CreateProgram(Program p)
        {
            if (Repositories.CreateProgram(p) == true)
            {
                TempData["success"] = "Create new program successfully!";
                return RedirectToAction("ListProgram");
            }
            TempData["error"] = "Create new program failed!";
            return RedirectToAction("Program");
        }
        public ActionResult ProgramDetail(int id)
        {
            Program pro = Repositories.GetProgramByID(id);
            ViewBag.type = Repositories.GetAllType();
            ViewBag.imgMain = Repositories.GetImgMain(id);
            ViewBag.lsImg = Repositories.GetAllImg(id);
            return View(pro);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult UpdateProgram(Program p)
        {
            if (Repositories.UpdatePro(p) == true)
            {
                TempData["success"] = "Update program successfully!";
            }
            else
            {
                TempData["error"] = "Update program failed!";
            }
            return RedirectToAction("ProgramDetail", new { id = p.ID });
        }
        public ActionResult InsertImg(ProgramImage pi, HttpPostedFileBase imgMain)
        {
            string FileName = DateTime.Now.Ticks + Path.GetFileName(imgMain.FileName);
            string path = Path.Combine(Server.MapPath("~/Images"), FileName);
            imgMain.SaveAs(path);
            pi.ImgFileName = FileName;
            if (Repositories.InsertImg(pi) == true)
            {
                TempData["success"] = "Insert image successfully!";
            }
            else
            {
                TempData["error"] = "Insert image failed!";
            }
            return RedirectToAction("ProgramDetail", new { id = pi.ProID });
        }
        public ActionResult CheckMain(int id, int idPro)
        {
            if (Repositories.CheckImgMain(id) == true)
            {
                TempData["success"] = "Uncheck/Check the main image successfully!";
            }
            else
            {
                TempData["error"] = "Uncheck/Check the main image failed!";
            }
            return RedirectToAction("ProgramDetail", new { id = idPro });
        }
        public ActionResult DelImg(int id)
        {
            ProgramImage model = Repositories.GetImgByID(id);
            string fullPath = Request.MapPath("~/Images/" + model.ImgFileName);
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }
            if (Repositories.DelImg(id) == true)
            {
                TempData["success"] = "Delete image successfully!";
            }
            else
            {
                TempData["error"] = "Delete image failed!";
            }
            return RedirectToAction("ProgramDetail", new { id = model.ProID });
        }
        #endregion

        #region Donate
        public ActionResult Donate()
        {
            System.Collections.Generic.List<Category> lsCate = Repositories.GetAllCate();
            return View(lsCate);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CreateDonate(Donate d)
        {
            if (Repositories.CreateDonate(d) == true)
            {
                TempData["success"] = "Create new donate successfully!";
            }
            else
            {
                TempData["error"] = "Create new donate failed!";
            }
            return RedirectToAction("ListDonate");
        }
        public ActionResult ListDonate()
        {
            ViewBag.up = Repositories.GetAllDonateByStt(1);
            ViewBag.on = Repositories.GetAllDonateByStt(2);
            ViewBag.end = Repositories.GetAllDonateByStt(3);
            ViewBag.hide = Repositories.GetDonateHide();
            return View();
        }
        public ActionResult DonateDetail(int id)
        {
            Donate model = Repositories.GetDonateByID(id);
            ViewBag.cate = Repositories.GetAllCate();
            ViewBag.ud = Repositories.GetUDByDonateID(id);
            return View(model);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult UpdateDonate(Donate d)
        {
            if (Repositories.UpdateDonate(d) == true)
            {
                TempData["success"] = "Update donate successfully!";
            }
            else
            {
                TempData["error"] = "Update donate failed!";
            }
            return RedirectToAction("DonateDetail", new { id = d.ID });
        }
        #endregion

        #region User
        public ActionResult ListUser()
        {
            ViewBag.active = Repositories.GetUserActive();
            ViewBag.unactive = Repositories.GetUserUnactive();
            ViewBag.volunteer = Repositories.GetUserVolunteer();
            return View();
        }
        public ActionResult UserDetail(int id)
        {
            User model = Repositories.GetUserByID(id);
            ViewBag.donate = Repositories.GetUDByUserID(id);
            return View(model);
        }
        public ActionResult CheckActive(int id)
        {
            if (Repositories.CheckActive(id) == true)
            {
                TempData["success"] = "Check/Uncheck active successfully!";
            }
            else
            {
                TempData["error"] = "Check/Uncheck active failed!";
            }
            return RedirectToAction("UserDetail", new { id = id });
        }
        public ActionResult CheckVolunteer(int id)
        {
            if (Repositories.CheckVolunteer(id) == true)
            {
                TempData["success"] = "Check/Uncheck volunteer successfully!";
            }
            else
            {
                TempData["error"] = "Check/Uncheck volunteer failed!";
            }
            return RedirectToAction("UserDetail", new { id = id });
        }
        #endregion

        #region About Us
        public ActionResult AboutUs()
        {
            return View();
        }
        public ActionResult ListAbout()
        {
            System.Collections.Generic.List<AboutUs> ls = Repositories.GetAllAU();
            return View(ls);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CreateAbout(AboutUs a, HttpPostedFileBase imgAbout)
        {
            string FileName = DateTime.Now.Ticks + Path.GetFileName(imgAbout.FileName);
            string path = Path.Combine(Server.MapPath("~/Images"), FileName);
            imgAbout.SaveAs(path);
            a.AboutImage = FileName;
            if (Repositories.CreateAU(a) == true)
            {
                TempData["success"] = "Create new about successfully!";
            }
            else
            {
                TempData["error"] = "Create new about failed!";
            }
            return RedirectToAction("ListAbout");
        }

        public ActionResult EditAbout(int id)
        {
            AboutUs model = Repositories.GetAUByID(id);
            return View(model);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult UpdateAbout(AboutUs a, HttpPostedFileBase imgAbout)
        {
            AboutUs model = Repositories.GetAUByID(a.ID);
            if (imgAbout == null)
            {
                a.AboutImage = model.AboutImage;
            }
            else
            {
                string fullPath = Request.MapPath("~/Images/" + model.AboutImage);
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                    string FileName = DateTime.Now.Ticks + Path.GetFileName(imgAbout.FileName);
                    string path = Path.Combine(Server.MapPath("~/Images"), FileName);
                    imgAbout.SaveAs(path);
                    a.AboutImage = FileName;
                }
            }
            if (Repositories.UpdateAU(a) == true)
            {
                TempData["success"] = "Update about successfully!";
            }
            else
            {
                TempData["error"] = "Update about failed!";
            }
            return RedirectToAction("ListAbout");
        }
        #endregion

        #region Contact
        public ActionResult ContactUs()
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
        public ActionResult InsertContact(ContactView cv)
        {
            string path = Path.Combine(Server.MapPath("~/"), "DataContact.hat");
            //Serialize
            using (Stream stream = new FileStream(path, FileMode.OpenOrCreate))
            {
                // đổi tượng BinaryFormatter được sử dụng cho quá trình tuần tự hóa nhị phân.
                BinaryFormatter formatter = new BinaryFormatter();
                //Phương thức Serialize để thực thi tuần tự hóa.
                formatter.Serialize(stream, cv);
                stream.Close();
            }
            
            return RedirectToAction("ContactUs");
        }
        #endregion

        #region Partner
        public ActionResult Partner()
        {
            return View();
        }
        public ActionResult ListPartner()
        {
            System.Collections.Generic.List<Partner> ls = Repositories.GetAllPartner();
            return View(ls);
        }
        public ActionResult CreatePartner(Partner p, HttpPostedFileBase imgPartner)
        {
            string FileName = DateTime.Now.Ticks + Path.GetFileName(imgPartner.FileName);
            string path = Path.Combine(Server.MapPath("~/Images"), FileName);
            imgPartner.SaveAs(path);
            p.PartnerImage = FileName;
            if (Repositories.InsertPartner(p) == true)
            {
                TempData["success"] = "Create new partner successfully!";
            }
            else
            {
                TempData["error"] = "Create new partner failed!";
                return View("Partner");
            }
            return RedirectToAction("ListPartner");

        }
        public ActionResult EditPartner(int id)
        {
            Partner model = Repositories.GetPartnerByID(id);
            return View(model);
        }
        public ActionResult UpdatePartner(Partner p, HttpPostedFileBase imgPartner)
        {
            Partner model = Repositories.GetPartnerByID(p.ID);
            if (imgPartner == null)
            {
                p.PartnerImage = model.PartnerImage;
            }
            else
            {
                string fullPath = Request.MapPath("~/Images/" + model.PartnerImage);
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                    string FileName = DateTime.Now.Ticks + Path.GetFileName(imgPartner.FileName);
                    string path = Path.Combine(Server.MapPath("~/Images"), FileName);
                    imgPartner.SaveAs(path);
                    p.PartnerImage = FileName;
                }

            }
            if (Repositories.UpdatePartner(p) == true)
            {
                TempData["success"] = "Update partner successfully!";
                return RedirectToAction("ListPartner");
            }
            else
            {
                TempData["error"] = "Update partner failed!";
            }
            return RedirectToAction("EditPartner", new { id = p.ID });
        }
        #endregion

        public ActionResult LogOut()
        {
            Session.Remove("admin");
            return RedirectToAction("Index", "Home");
        }
    }
}