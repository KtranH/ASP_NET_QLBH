using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.UI;
using Common;
using GTX_STORE_WEB.Models;
using PagedList;
using PagedList.Mvc;

namespace GTX_STORE_WEB.Areas.Admin.Controllers
{
    public class OrderAdminController : Controller
    {
        DB_GTXSTORE DB = new DB_GTXSTORE();
        // GET: Admin/OrderAdmin
        public ActionResult UserAccount(int ?page)
        {
            string NameAdmin = Session["USERACCOUNT"].ToString();
            int pageSize = 10; // Số sản phẩm trên mỗi trang
            List<KHACH> KH = DB.KHACHes.Where(x=>x.TENTK != NameAdmin).ToList();
            int pageNumber = (page ?? 1); // Trang mặc định là 1 nếu không có trang được chỉ định
            IPagedList<KHACH> pagedProducts = KH.ToPagedList(pageNumber, pageSize);
            return View(pagedProducts);
        }
        public ActionResult OrderUser(int ?page)
        {
            string NameAdmin = Session["USERACCOUNT"].ToString();
            int pageSize = 10; // Số sản phẩm trên mỗi trang
            List<DONHANG> OrderU = DB.DONHANGs.Where(x => x.KHACH.TENTK != NameAdmin).ToList();
            int pageNumber = (page ?? 1); // Trang mặc định là 1 nếu không có trang được chỉ định
            IPagedList<DONHANG> pagedProducts = OrderU.ToPagedList(pageNumber, pageSize);
            return View(pagedProducts);
        }
        public ActionResult DetailOrderAd(int id)
        {
            try
            {
                var DetailOrderUser = DB.CHITIETDONHANGs.FirstOrDefault(x=>x.MADH.Equals(id));
                return View(DetailOrderUser);
            }
            catch
            {
                return RedirectToAction("Error404", "HomeAdmin");
            }
        }
        public ActionResult UpdateOrderAd(int id)
        {
            try
            {
                var DetailOrderUser = DB.DONHANGs.FirstOrDefault(x => x.MADH.Equals(id));
                return View(DetailOrderUser);
            }
            catch
            {
                return RedirectToAction("Error404", "HomeAdmin");
            }
        }
        [HttpPost]
        public ActionResult UpdateOrderAd(DONHANG confOrder)
        {
           
                if(confOrder.TINHTRANG.ToString() == "Đã xác nhận")
                {
                    DateTime NgayTao = DateTime.Now;
                    if(NgayTao > confOrder.NGAYGIAO)
                    {
                        ViewBag.ErrorDate = "Ngày giao không được nhỏ hơn ngày hiện tại";
                        return View();
                    }
                    else
                    {
                        var upOrder = DB.DONHANGs.FirstOrDefault(x=>x.MADH.Equals(confOrder.MADH));
                        upOrder.NGAYGIAO = confOrder.NGAYGIAO;
                        upOrder.TINHTRANG = confOrder.TINHTRANG;
                        DB.Configuration.ValidateOnSaveEnabled = false;
                        DB.Entry(upOrder).State = System.Data.Entity.EntityState.Modified;
                        DB.SaveChanges();
                    
                        return RedirectToAction("OrderUser", "OrderAdmin");
                    }    
                }    
                else
                {
                    confOrder.NGAYGIAO = null;
                    var upOrder = DB.DONHANGs.FirstOrDefault(x => x.MADH.Equals(confOrder.MADH));
                    upOrder.NGAYGIAO = confOrder.NGAYGIAO;
                    upOrder.TINHTRANG = confOrder.TINHTRANG;
                    DB.Configuration.ValidateOnSaveEnabled = false;
                    DB.Entry(upOrder).State = System.Data.Entity.EntityState.Modified;
                    DB.SaveChanges();
                    return RedirectToAction("OrderUser", "OrderAdmin");
                }
                

            }
           
        }
    }
