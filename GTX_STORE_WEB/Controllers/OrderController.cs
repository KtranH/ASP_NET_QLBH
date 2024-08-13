using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.UI;
using Common;
using GTX_STORE_WEB.Models;
using PagedList;
using PagedList.Mvc;
using WebGrease.Css.Ast.Animation;

namespace GTX_STORE_WEB.Controllers
{
    public class OrderController : Controller
    {
        DB_GTXSTORE DB = new DB_GTXSTORE();
        // GET: Order
       public ActionResult Order(int ?page)
        {
            try
            {
                string UserOrder = Session["USERACCOUNT"].ToString();
                ViewBag.CheckOrder = 0;
                List<DONHANG> OrderByKH = DB.DONHANGs.Where(x => x.KHACH.TENTK.Equals(UserOrder)).ToList();
                if (OrderByKH.Count > 0)
                {
                    ViewBag.CheckOrder = 1;
                    int pageSize = 10; // Số sản phẩm trên mỗi trang
                    int pageNumber = (page ?? 1);
                    IPagedList<DONHANG> pagedProducts = OrderByKH.ToPagedList(pageNumber, pageSize);
                    return View(pagedProducts);
                }
                return View();
            }
            catch
            {
                return RedirectToAction("Error404", "Home");
            }
        }
        public ActionResult DetailOrder(int IDDH)
        {
           try
            {
                var DetailOr = DB.CHITIETDONHANGs.FirstOrDefault(x=>x.MADH.Equals(IDDH));
                if (DetailOr != null)
                {
                    return View(DetailOr);
                }
                else
                {
                    return RedirectToAction("Error404", "Home");
                }
            }
            catch
            {
                return RedirectToAction("Error404", "Home");
            }
        }
        public ActionResult CovertCartToOrder(int SP)
        {
            try
            {
                string UserName = Session["USERACCOUNT"].ToString();
                var takeIDKH = DB.KHACHes.FirstOrDefault(x => x.TENTK.Equals(UserName));
                var SPinCart = DB.CT_GIOHANG.FirstOrDefault(x => x.MASP.Equals(SP));
                var AmountSP = DB.SANPHAMs.FirstOrDefault(x => x.MASP.Equals(SP));
                DONHANG SPinOrder = new DONHANG();
                SPinOrder.MAKH = takeIDKH.MAKH;
                SPinOrder.NGAYTAO = DateTime.Today;
                SPinOrder.NGAYGIAO = null;
                SPinOrder.TINHTRANG = "Đợi xác nhận";
                DB.Configuration.ValidateOnSaveEnabled = false;
                DB.DONHANGs.Add(SPinOrder);
                var DetailOrder = new CHITIETDONHANG();
                DetailOrder.MADH = SPinOrder.MADH;
                DetailOrder.MASP = SP;
                DetailOrder.SOLUONG = SPinCart.SOLUONG;
                DetailOrder.THANHTIEN = SPinCart.TONGTIEN;
                DB.CHITIETDONHANGs.Add(DetailOrder);
                AmountSP.SOLUONGSP = AmountSP.SOLUONGSP - DetailOrder.SOLUONG;
                DB.Entry(AmountSP).State = System.Data.Entity.EntityState.Modified;

                //sent mail//                
                string tenKhachHang = takeIDKH.HOTEN;
                string tenSanPham = AmountSP.TENSP;
                int soLuongSanPham = (int)DetailOrder.SOLUONG;
                decimal donGia = (decimal)AmountSP.GIA;
                decimal thanhTien = (decimal)DetailOrder.THANHTIEN;
                string diachi = takeIDKH.DIACHI;
                string email = takeIDKH.EMAIL;
                string sdt = takeIDKH.SODT;
                string template = System.IO.File.ReadAllText(Server.MapPath("~/client/template/sentDH.html"));
                template = template.Replace("{{HOTEN}}", tenKhachHang);
                template = template.Replace("{{MADH}}", DetailOrder.MADH.ToString());
                template = template.Replace("{{TENSP}}", tenSanPham);
                template = template.Replace("{{SOLUONG}}", soLuongSanPham.ToString());
                template = template.Replace("{{GIA}}", donGia.ToString());
                template = template.Replace("{{THANHTIEN}}", thanhTien.ToString());
                template = template.Replace("{{DIACHI}}", diachi.ToString());
                template = template.Replace("{{SODT}}", email.ToString());
                template = template.Replace("{{EMAIL}}", sdt.ToString());
                var Email = takeIDKH.EMAIL;
                new MailHelper().SendMail(Email, "Đơn hàng #" + SPinOrder.MADH, template);

                var RemoveSPinCart = DB.CT_GIOHANG.FirstOrDefault(x => x.MASP.Equals(SP));
                DB.CT_GIOHANG.Remove(RemoveSPinCart);
                DB.SaveChanges();
                return RedirectToAction("Order", "Order");
            }
            catch
            {
                return RedirectToAction("Error404", "Home");
            }
        }
    }
}