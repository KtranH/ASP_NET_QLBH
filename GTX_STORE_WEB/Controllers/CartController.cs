using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using GTX_STORE_WEB.Models;
using PagedList;
using PagedList.Mvc;

namespace GTX_STORE_WEB.Controllers
{
    public class CartController : Controller
    {
        DB_GTXSTORE DB = new DB_GTXSTORE();
        // GET: Cart
        public ActionResult ShowCart(int ?page)
        {
            try
            {
                string IDKH = Session["USERACCOUNT"].ToString();
                ViewBag.Cart = null;
                var checkKHGH = DB.GIOHANGs.Any(x => x.KHACH.TENTK.Equals(IDKH));
                if (checkKHGH)
                {
                    ViewBag.Cart = 1;
                    var showGH = DB.CT_GIOHANG.Where(x => x.GIOHANG.KHACH.TENTK.Equals(IDKH)).ToList();
                    var sumtt = DB.CT_GIOHANG.Where(x => x.GIOHANG.KHACH.TENTK.Equals(IDKH)).Sum(y=>y.TONGTIEN);
                    var sumsp = DB.CT_GIOHANG.Where(x => x.GIOHANG.KHACH.TENTK.Equals(IDKH)).Sum(y => y.SOLUONG);
                    int pageSize = 10; // Số sản phẩm trên mỗi trang
                    int pageNumber = (page ?? 1);
                    Session["SUMSP"] = sumsp;
                    Session["SUMTT"] = sumtt;
                    if (Session["SUMSP"]== null || Session["SUMTT"] == null)
                    {
                        Session["SUMSP"] = 0;
                        Session["SUMTT"] = 0;
                        ViewBag.Cart = 0;
                        return View();
                    }    
                    IPagedList<CT_GIOHANG> pagedProducts = showGH.ToPagedList(pageNumber, pageSize);
                    return View(pagedProducts);
                }
                else
                {
                    ViewBag.Cart = 0;
                    return View();
                }
            }
            catch
            {
                return RedirectToAction("Error404", "Home");
            }
}
        [HttpPost]
        public ActionResult Cart(int IDPR, int Amount)
        {
            try
            {
                Session["SENDERROR"] = null;
                var PRCart = DB.SANPHAMs.FirstOrDefault(x => x.MASP.Equals(IDPR));
                if(PRCart.SOLUONGSP - Amount <= 0)
                {
                    Session["SENDERROR"] = "Số lượng sản phẩm không đủ yêu cầu!";
                    int DLPR = IDPR;
                    return RedirectToAction("DetailPR", "Produce", new { IDPR = DLPR });
                }
                else if(Session["USERACCOUNT"] == null)
                {
                    Session["SENDERROR"] = "Vui lòng đăng nhập để đặt mua sản phẩm!";
                    int DLPR = IDPR;
                    return RedirectToAction("DetailPR", "Produce", new { IDPR = DLPR });
                }    
                else
                {
                    string TenTK = Session["USERACCOUNT"].ToString();
                    var CHECKGH = DB.GIOHANGs.Any(x => x.KHACH.TENTK.Equals(TenTK));
                    if(CHECKGH)
                    {
                        GIOHANG newGH = DB.GIOHANGs.FirstOrDefault(x=>x.KHACH.TENTK.Equals(TenTK));
                        return RedirectToAction("AddCart", "Cart", new { GH = newGH.MAGH, SP = IDPR, SL = Amount });
                    }
                    else
                    {
                        GIOHANG newGH = new GIOHANG();
                        var KHCart = DB.KHACHes.FirstOrDefault(x=>x.TENTK.Equals(TenTK));
                        newGH.MAKH = KHCart.MAKH;
                        DB.Configuration.ValidateOnSaveEnabled = false;
                        DB.GIOHANGs.Add(newGH);
                        DB.SaveChanges();
                        return RedirectToAction("AddCart", "Cart", new { GH = newGH.MAGH, SP = IDPR, SL = Amount });
                    }    
                }
            }
            catch
            {
                return RedirectToAction("Error404", "Home");
            }
        }
        public ActionResult AddCart(int GH, int SP, int SL)
        {
            try
            {
                var checkGH = DB.CT_GIOHANG.Any(x=>x.MAGH.Equals(GH));
                if(!checkGH)
                {
                    var GiaSP = DB.SANPHAMs.FirstOrDefault(x=>x.MASP.Equals(SP));
                    CT_GIOHANG newCTGH = new CT_GIOHANG();
                    newCTGH.MAGH = GH;
                    newCTGH.MASP = SP;
                    newCTGH.SOLUONG = SL;
                    newCTGH.TONGTIEN = GiaSP.GIA * SL;
                    DB.Configuration.ValidateOnSaveEnabled = false;
                    DB.CT_GIOHANG.Add(newCTGH);
                    DB.SaveChanges();
                    return RedirectToAction("ShowCart","Cart");
                }    
                else
                {
                    var checkSPGH = DB.CT_GIOHANG.Any(x=>x.MASP.Equals(SP) && x.MAGH.Equals(GH));  
                    if(!checkSPGH)
                    {
                        CT_GIOHANG newCTGH = new CT_GIOHANG();
                        var GiaSP = DB.SANPHAMs.FirstOrDefault(x => x.MASP.Equals(SP));
                        newCTGH.MAGH = GH;
                        newCTGH.MASP = SP;
                        newCTGH.SOLUONG = SL;
                        newCTGH.TONGTIEN = GiaSP.GIA * SL;
                        DB.Configuration.ValidateOnSaveEnabled = false;
                        DB.CT_GIOHANG.Add(newCTGH);
                        DB.SaveChanges();
                        return RedirectToAction("ShowCart", "Cart");
                    }
                    else
                    {
                        var updateSPGH = DB.CT_GIOHANG.FirstOrDefault(x=>x.MASP.Equals(SP) && x.MAGH.Equals(GH));
                        updateSPGH.SOLUONG = updateSPGH.SOLUONG + SL;
                        updateSPGH.TONGTIEN = updateSPGH.SANPHAM.GIA * updateSPGH.SOLUONG;
                        DB.Configuration.ValidateOnSaveEnabled = false;
                        DB.Entry(updateSPGH).State = System.Data.Entity.EntityState.Modified;
                        DB.SaveChanges();
                        return RedirectToAction("ShowCart", "Cart");
                    }
                }
            }
            catch
            {
                return RedirectToAction("Error404", "Home");
            }
        }
        public ActionResult RemoveSPCart(int SP)
        {
            try
            {
                var Remove = DB.CT_GIOHANG.FirstOrDefault(x => x.MASP.Equals(SP));
                DB.Configuration.ValidateOnSaveEnabled = false;
                DB.CT_GIOHANG.Remove(Remove);
                DB.SaveChanges();
                return RedirectToAction("ShowCart", "Cart");
            }
            catch
            {
                return RedirectToAction("Error404", "Home");
            }
        }
    }
}