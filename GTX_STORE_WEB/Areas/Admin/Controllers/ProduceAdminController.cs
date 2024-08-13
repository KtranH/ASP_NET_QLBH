using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using GTX_STORE_WEB.Models;
using Microsoft.Ajax.Utilities;
using PagedList;
using PagedList.Mvc;

namespace GTX_STORE_WEB.Areas.Admin.Controllers
{
    public class ProduceAdminController : Controller
    {
        DB_GTXSTORE DB = new DB_GTXSTORE();
        // GET: Admin/ProduceAdmin
        public ActionResult MorePR()
        {
            try
            {
                List<HANGSX> tenHang = DB.HANGSXes.ToList();
                List<LOAISP> tenloai = DB.LOAISPs.ToList();
                SelectList tenHangList = new SelectList(tenHang, "MANSX", "TENNSX");
                SelectList tenLoaiList = new SelectList(tenloai, "MALOAI", "TENLOAI");
                ViewBag.tenHangList = tenHangList;
                ViewBag.tenLoaiList = tenLoaiList;
                return View();
            }
            catch
            {
                return RedirectToAction("Error404", "HomeAdmin");
            }
        }
        [HttpPost]
        public ActionResult MorePR(SANPHAM newSP)
        {
           try
            {
                DB.Configuration.ValidateOnSaveEnabled = false;
                DB.SANPHAMs.Add(newSP);
                DB.SaveChanges();
                return RedirectToAction("Index","HomeAdmin");
            }
            catch
            {
                return RedirectToAction("Error404", "HomeAdmin");
            }
        }
        public ActionResult DetailPRAdmin(int id)
        {
           try
            {
                var DetailPR = DB.SANPHAMs.FirstOrDefault(x=>x.MASP.Equals(id));
                return View(DetailPR);
            }
            catch
            {
                return RedirectToAction("Error404", "HomeAdmin");
            }
        }
        public ActionResult UpdateProduce(int id)
        {
            try
            {
                var UpdateProduce = DB.SANPHAMs.FirstOrDefault(x => x.MASP.Equals(id));
                List<HANGSX> tenHang = DB.HANGSXes.ToList();
                List<LOAISP> tenloai = DB.LOAISPs.ToList();
                SelectList tenHangList = new SelectList(tenHang, "MANSX", "TENNSX");
                SelectList tenLoaiList = new SelectList(tenloai, "MALOAI", "TENLOAI");
                ViewBag.tenHangList = tenHangList;
                ViewBag.tenLoaiList = tenLoaiList;
                return View(UpdateProduce);
            }
            catch
            {
                return RedirectToAction("Error404", "HomeAdmin");
            }
        }
        [HttpPost]
        public ActionResult UpdateProduce(SANPHAM upSP)
        {
            try
            {
                var UpdateProduce = new SANPHAM();
                UpdateProduce = upSP;
                DB.Configuration.ValidateOnSaveEnabled = false;
                DB.Entry(UpdateProduce).State = System.Data.Entity.EntityState.Modified;
                DB.SaveChanges();
                return RedirectToAction("Index", "HomeAdmin");
            }
            catch
            {
                return RedirectToAction("Error404", "HomeAdmin");
            }
        }
        public ActionResult BrandPR (int ? page)
        {
            try
            {
                int pageSize = 10; // Số sản phẩm trên mỗi trang
                var ListBrand = DB.HANGSXes.ToList();
                int pageNumber = (page ?? 1); // Trang mặc định là 1 nếu không có trang được chỉ định
                IPagedList<HANGSX> pagedProducts = ListBrand.ToPagedList(pageNumber, pageSize);
                return View(pagedProducts);
            }
            catch
            {
                return RedirectToAction("Error404", "HomeAdmin");
            }
        }
        public ActionResult UpdateBrand(int id)
        {
            try
            {
                var BrandPR = DB.HANGSXes.FirstOrDefault(x=>x.MANSX.Equals(id));
                return View(BrandPR);
            }
            catch
            {
                return RedirectToAction("Error404", "HomeAdmin");
            }
        }
        [HttpPost]
        public ActionResult UpdateBrand(HANGSX upNSX)
        {
            try
            {
                var BrandPR = new HANGSX();
                BrandPR = upNSX;
                DB.Configuration.ValidateOnSaveEnabled = false;
                DB.Entry(BrandPR).State = System.Data.Entity.EntityState.Modified;
                DB.SaveChanges();
                return RedirectToAction("BrandPR", "ProduceAdmin");
            }
            catch
            {
                return RedirectToAction("Error404", "HomeAdmin");
            }
        }
        public ActionResult MoreBrand()
        {
            try
            {
                return View();
            }
            catch
            {
                return RedirectToAction("Error404", "HomeAdmin");
            }
        }
        [HttpPost]
        public ActionResult MoreBrand(HANGSX newNSX)
        {
            try
            {
                var MoreBrand = new HANGSX();
                MoreBrand = newNSX;
                DB.Configuration.ValidateOnSaveEnabled = false;
                DB.HANGSXes.Add(newNSX); 
                DB.SaveChanges();
                return RedirectToAction("BrandPR", "ProduceAdmin");
            }
            catch
            {
                return RedirectToAction("Error404", "HomeAdmin");
            }
        }
        public ActionResult KindPR(int ?page)
        {
            try
            {
                int pageSize = 10; // Số sản phẩm trên mỗi trang
                var ListKind = DB.LOAISPs.ToList();
                int pageNumber = (page ?? 1); // Trang mặc định là 1 nếu không có trang được chỉ định
                IPagedList<LOAISP> pagedProducts = ListKind.ToPagedList(pageNumber, pageSize);
                return View(pagedProducts);
            }
            catch
            {
                return RedirectToAction("Error404", "HomeAdmin");
            }
        }
        public ActionResult UpdateKind(int id)
        {
            try
            {
                var KindPR = DB.LOAISPs.FirstOrDefault(x => x.MALOAI.Equals(id));
                return View(KindPR);
            }
            catch
            {
                return RedirectToAction("Error404", "HomeAdmin");
            }
        }
        [HttpPost]
        public ActionResult UpdateKind(LOAISP upKind)
        {
            try
            {
                var KindPR = new LOAISP();
                KindPR = upKind;
                DB.Configuration.ValidateOnSaveEnabled = false;
                DB.Entry(KindPR).State = System.Data.Entity.EntityState.Modified;
                DB.SaveChanges();
                return RedirectToAction("KindPR", "ProduceAdmin");
            }
            catch
            {
                return RedirectToAction("Error404", "HomeAdmin");
            }
        }
        public ActionResult MoreKind()
        {
            try
            {
                return View();
            }
            catch
            {
                return RedirectToAction("Error404", "HomeAdmin");
            }
        }
        [HttpPost]
        public ActionResult MoreKind(LOAISP newKind)
        {
            try
            {
                var MoreKind = new LOAISP();
                MoreKind = newKind;
                DB.Configuration.ValidateOnSaveEnabled = false;
                DB.LOAISPs.Add(newKind);
                DB.SaveChanges();
                return RedirectToAction("KindPR", "ProduceAdmin");
            }
            catch
            {
                return RedirectToAction("Error404", "HomeAdmin");
            }
        }
    }
}