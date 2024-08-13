using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using GTX_STORE_WEB.Models;
using PagedList;
using PagedList.Mvc;

namespace GTX_STORE_WEB.Areas.Admin.Controllers
{
    public class HomeAdminController : Controller
    {
        DB_GTXSTORE DB = new DB_GTXSTORE(); 
        // GET: Admin/HomeAdmin
        public ActionResult Index(int ?page)
        {
            if (Session["USERACCOUNT"] != null)
            {
                string NameAdmin = Session["USERACCOUNT"].ToString();
                var checkAdmin = DB.KHACHes.Where(x=>x.TENTK.Equals(NameAdmin) && x.ISADMIN == 1).ToList();
                if(checkAdmin.Count > 0)
                {
                    int pageSize = 10; // Số sản phẩm trên mỗi trang
                    List<SANPHAM> products = DB.SANPHAMs.ToList();
                    int pageNumber = (page ?? 1); // Trang mặc định là 1 nếu không có trang được chỉ định
                    IPagedList<SANPHAM> pagedProducts = products.ToPagedList(pageNumber, pageSize);
                    return View(pagedProducts);
                }    
               else
                {
                    return RedirectToAction("DenyAccess");
                }
            }    
            else
            {
                return RedirectToAction("Error404");
            }    
        }
        public ActionResult Error404()
        {
            return View();
        }
        public ActionResult DenyAccess()
        {
            return View();
        }
    }
}