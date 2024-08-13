using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GTX_STORE_WEB.Models;

namespace GTX_STORE_WEB.Controllers
{
    public class UserController : Controller
    {
        DB_GTXSTORE DB = new DB_GTXSTORE();
        // GET: User
        public ActionResult HomeUser(string UserName)
        {
            var UserAccount = DB.KHACHes.FirstOrDefault(x=>x.TENTK.Equals(UserName));
            Session["GENDER"] = UserAccount.GIOITINH;
            Session["USERNAME"] = UserAccount.HOTEN;
            Session["USERACCOUNT"] = UserAccount.TENTK;
            return View(UserAccount);
        }
        public ActionResult UpdateAccount(string UserName)
        {
            var UserAccount = DB.KHACHes.FirstOrDefault(x => x.TENTK.Equals(UserName));
            return View(UserAccount);
        }
        [HttpPost]
        public ActionResult UpdateAccount(KHACH upKH)
        {
            try
            {
                var checkEmail = DB.KHACHes.Any(x => x.EMAIL.Equals(upKH.EMAIL) && x.MAKH != upKH.MAKH);
                var checkTenTK = DB.KHACHes.Any(x => x.TENTK.Equals(upKH.TENTK) && x.MAKH != upKH.MAKH);
                var checkSDT = DB.KHACHes.Any(x => x.SODT.Equals(upKH.SODT) && x.MAKH != upKH.MAKH);
                if (checkEmail == true && checkTenTK == false && checkSDT == false)
                {
                    ViewBag.EmailError = "Email đã tồn tại!";
                    return View();
                }
                else if (checkEmail == false && checkTenTK == true && checkSDT == false)
                {
                    ViewBag.TenTKError = "Tên tài khoản đã tồn tại!";
                    return View();
                }
                else if (checkEmail == false && checkTenTK == false && checkSDT == true)
                {
                    ViewBag.SDTError = "Số điện thoại đã tồn tại!";
                    return View();
                }
                else if (checkEmail == true && checkTenTK == true && checkSDT == false)
                {
                    ViewBag.EmailError = "Email đã tồn tại!";
                    ViewBag.TenTKError = "Tên tài khoản đã tồn tại!";
                    return View();
                }
                else if (checkEmail == false && checkTenTK == true && checkSDT == true)
                {
                    ViewBag.SDTError = "Số điện thoại đã tồn tại!";
                    ViewBag.TenTKError = "Tên tài khoản đã tồn tại!";
                    return View();
                }
                else if (checkEmail == true && checkTenTK == true && checkSDT == true)
                {
                    ViewBag.EmailError = "Email đã tồn tại!";
                    ViewBag.SDTError = "Số điện thoại đã tồn tại!";
                    ViewBag.TenTKError = "Tên tài khoản đã tồn tại!";
                    return View();
                }
                else
                {
                    upKH.NGAYSINH = DB.KHACHes.Where(x => x.MAKH == upKH.MAKH).Select(x => x.NGAYSINH).FirstOrDefault();
                    upKH.PASSWORD_USER = DB.KHACHes.Where(x => x.MAKH == upKH.MAKH).Select(x => x.PASSWORD_USER).FirstOrDefault();
                    DB.Configuration.ValidateOnSaveEnabled = false;
                    DB.Entry(upKH).State = System.Data.Entity.EntityState.Modified;
                    DB.SaveChanges();
                    return RedirectToAction("HomeUser", "User", new { UserName = upKH.TENTK });
                }
            }
            catch
            {
                return RedirectToAction("HomeUser", "User", new { UserName = Session["USERACCOUNT"].ToString() });
            }
        }
        [HttpPost]
        public ActionResult ChangePassword(string password, string newpassword,string renewpassword)
        {
            try
            {
                ViewBag.password = null;
                ViewBag.newpassword = null;
                ViewBag.renewpassword = null;
                string UserName = Session["USERACCOUNT"].ToString();
                var ChangePass = DB.KHACHes.FirstOrDefault(x => x.TENTK.Equals(UserName));
                if(!ChangePass.PASSWORD_USER.Equals(password))
                {
                    ViewBag.password = "Mật khẩu không đúng!";
                    return View();
                }    
                else if(!newpassword.Equals(renewpassword))
                {
                    ViewBag.newpassword = "Không trùng mật khẩu!";
                    ViewBag.renewpassword = "Không trùng mật khẩu!";
                    return View();
                }    
                else
                {
                    ChangePass.PASSWORD_USER = newpassword;
                    DB.Configuration.ValidateOnSaveEnabled = false;
                    DB.Entry(ChangePass).State = System.Data.Entity.EntityState.Modified;
                    DB.SaveChanges();
                    return RedirectToAction("Login", "Account");
                }    
            }
            catch
            {
                return RedirectToAction("HomeUser", "User", new { UserName = Session["USERACCOUNT"].ToString() });
            }
        }
    }
}