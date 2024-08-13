using GTX_STORE_WEB.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
namespace GTX_STORE_WEB.Controllers
{
    public class AccountController : Controller
    {
        DB_GTXSTORE DB = new DB_GTXSTORE();
        public ActionResult SignUp ()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SignUp(KHACH newKH)
        {
            if (ModelState.IsValid)
            {
                var checkEmail = DB.KHACHes.FirstOrDefault(x => x.EMAIL == newKH.EMAIL);
                var checkTenTK = DB.KHACHes.FirstOrDefault(x => x.TENTK == newKH.TENTK);
                var checkSDT = DB.KHACHes.FirstOrDefault(x => x.SODT == newKH.SODT);
                if (checkEmail != null && checkTenTK == null && checkSDT == null)
                {
                    ViewBag.EmailError = "Email đã tồn tại!";
                    return View();
                }
                else if (checkEmail == null && checkTenTK != null && checkSDT == null)
                {
                    ViewBag.TenTKError = "Tên tài khoản đã tồn tại!";
                    return View();
                }
                else if (checkEmail == null && checkTenTK == null && checkSDT != null)
                {
                    ViewBag.SDTError = "Số điện thoại đã tồn tại!";
                    return View();
                }
                else if (checkEmail != null && checkTenTK != null && checkSDT == null)
                {
                    ViewBag.EmailError = "Email đã tồn tại!";
                    ViewBag.TenTKError = "Tên tài khoản đã tồn tại!";
                    return View();
                }
                else if (checkEmail == null && checkTenTK != null && checkSDT != null)
                {
                    ViewBag.SDTError = "Số điện thoại đã tồn tại!";
                    ViewBag.TenTKError = "Tên tài khoản đã tồn tại!";
                    return View();
                }
                else if (checkEmail != null && checkTenTK != null && checkSDT != null)
                {
                    ViewBag.EmailError = "Email đã tồn tại!";
                    ViewBag.SDTError = "Số điện thoại đã tồn tại!";
                    ViewBag.TenTKError = "Tên tài khoản đã tồn tại!";
                    return View();
                }
                else
                {
                    newKH.ISADMIN = 0;
                    DB.Configuration.ValidateOnSaveEnabled = false;
                    DB.KHACHes.Add(newKH);
                    DB.SaveChanges();
                    return RedirectToAction("Login", "Account");
                }
            }
            return RedirectToAction("Error404", "Home");
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string UserName, string Password)
        {
            if(ModelState.IsValid)
            {
                var checkAccount = DB.KHACHes.Where(x => x.TENTK.Equals(UserName) && x.PASSWORD_USER.Equals(Password)).ToList();
                if (checkAccount.Count() > 0)
                {
                    Session["USERNAME"] = checkAccount.FirstOrDefault().HOTEN;
                    Session["USERACCOUNT"] = checkAccount.FirstOrDefault().TENTK;
                    Session["GENDER"] = checkAccount.FirstOrDefault().GIOITINH;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.UserNameError = "Tên tài khoản không trùng khớp!";
                    ViewBag.UserPassError = "Password không trùng khớp";
                    return View();
                }
            }
            return RedirectToAction("Error404", "Home");
        }
        public ActionResult ForgotPass()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ForgotPass(string Email,string SDT,string Address,string BirthDate,string UserName)
        {
            if (ModelState.IsValid)
            {
                var checkEmail = DB.KHACHes.Any(x => x.EMAIL.Equals(Email));
                var checkSDT = DB.KHACHes.Any(x=>x.SODT.Equals(SDT));
                var checkAddress = DB.KHACHes.Any(x=>x.DIACHI.Equals(Address));
                DateTime birthDateValue = DateTime.ParseExact(BirthDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                var checkBirthDate = DB.KHACHes.Any(x => DbFunctions.TruncateTime(x.NGAYSINH) == birthDateValue.Date);
                var checkUserName = DB.KHACHes.Any(x=>x.TENTK.Equals(UserName));
                if(!checkEmail || !checkSDT || !checkAddress || !checkBirthDate || !checkUserName) 
                {
                    ViewBag.FailedRecovery = "Dữ liệu nhập vào không khớp";
                    return View();
                }
                else
                {
                    return RedirectToAction("Recovery", "Account", new { NameAccount = UserName});
                }
            }
            return RedirectToAction("Error404", "Home");
        }
        public ActionResult Recovery(string NameAccount) 
        {
            if (ModelState.IsValid)
            {
                Session["AccountRecovery"] = NameAccount;
                return View();
            }
            else
            {
                return RedirectToAction("Error404", "Home");
            }
        }
        [HttpPost]
        public ActionResult Recovery (string Password,string RePassword)
        {
            if (ModelState.IsValid)
            {
                if(Password != RePassword)
                {
                    ViewBag.Password = "Mật khẩu không trùng khớp!";
                    return View();
                }   
                else
                {
                    string UserAccount = Session["AccountRecovery"].ToString();
                    var ChangePass = DB.KHACHes.FirstOrDefault(x => x.TENTK.Equals(UserAccount));
                    if (ChangePass != null)
                    {
                        ChangePass.PASSWORD_USER = RePassword;
                        DB.Configuration.ValidateOnSaveEnabled = false;
                        DB.Entry(ChangePass).State = System.Data.Entity.EntityState.Modified;
                        DB.SaveChanges();
                        Session["AccountRecovery"] = null;
                        return RedirectToAction("Login", "Account");
                    }
                    else
                    {
                        return RedirectToAction("Error404", "Home");
                    }
                }    
            }
            else
            {
                return RedirectToAction("Error404", "Home");
            }
        }
        public ActionResult Logout()
        {
            Session["USERACCOUNT"] = null;
            Session["GENDER"] = null;
            return RedirectToAction("Index", "Home");
        }
    }
}