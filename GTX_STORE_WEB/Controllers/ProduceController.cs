using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GTX_STORE_WEB.Models;
using PagedList;
using PagedList.Mvc;

namespace GTX_STORE_WEB.Controllers
{
    public class ProduceController : Controller
    {
        DB_GTXSTORE db = new DB_GTXSTORE();
        public ActionResult ShowPR(int? page)
        {
            // Lấy tất cả sản phẩm từ cơ sở dữ liệu
            var allProducts = db.SANPHAMs.ToList();
            int pageSize = 10;
            string FindX = Session["NameX"].ToString();
            // Tìm kiếm trong danh sách sản phẩm đã tải lên bộ nhớ
            var result = allProducts.Where(x => x.TENSP.IndexOf(FindX, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
            int pageNumber = (page ?? 1);
            IPagedList<SANPHAM> pagedProducts = result.ToPagedList(pageNumber, pageSize);
            return View(pagedProducts);
        }
        // GET: Produce
       [HttpPost]
        public ActionResult FindPR(int? page, string search)
        {
            // Lấy tất cả sản phẩm từ cơ sở dữ liệu
            var allProducts = db.SANPHAMs.ToList();
            int pageSize = 10;
            Session["NameX"] = search;
            // Tìm kiếm trong danh sách sản phẩm đã tải lên bộ nhớ
            var result = allProducts.Where(x => x.TENSP.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
            int pageNumber = (page ?? 1);
            IPagedList<SANPHAM> pagedProducts = result.ToPagedList(pageNumber, pageSize);
            return View(pagedProducts);
        }
        public ActionResult ShowNSX(int? page)
        {
            // Lấy tất cả sản phẩm từ cơ sở dữ liệu
            var allProducts = db.SANPHAMs.ToList();
            int pageSize = 10;
            string NameNSX = Session["NameNSX"].ToString();
            // Tìm kiếm trong danh sách sản phẩm đã tải lên bộ nhớ
            var result = allProducts.Where(x => x.TENSP.IndexOf(NameNSX, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
            int pageNumber = (page ?? 1);
            IPagedList<SANPHAM> pagedProducts = result.ToPagedList(pageNumber, pageSize);
            return View(pagedProducts);
        }
        public ActionResult FindNSX(string NameNSX, int? page)
        {
            // Lấy tất cả sản phẩm từ cơ sở dữ liệu
            var allProducts = db.SANPHAMs.ToList();
            int pageSize = 10;
            Session["NameNSX"] = NameNSX;
            // Tìm kiếm trong danh sách sản phẩm đã tải lên bộ nhớ
            var result = allProducts.Where(x => x.HANGSX.TENNSX.IndexOf(NameNSX, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
            int pageNumber = (page ?? 1);
            IPagedList<SANPHAM> pagedProducts = result.ToPagedList(pageNumber, pageSize);
            return View(pagedProducts);
        }
        public ActionResult ShowKind(int? page)
        {
            // Lấy tất cả sản phẩm từ cơ sở dữ liệu
            var allProducts = db.SANPHAMs.ToList();
            int pageSize = 10;
            string NameKind = Session["NameKind"].ToString();
            // Tìm kiếm trong danh sách sản phẩm đã tải lên bộ nhớ
            var result = allProducts.Where(x => x.LOAISP.TENLOAI.IndexOf(NameKind, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
            int pageNumber = (page ?? 1);
            IPagedList<SANPHAM> pagedProducts = result.ToPagedList(pageNumber, pageSize);
            return View(pagedProducts);
        }
        public ActionResult FindKind(string NameKind,int ?page)
        {
            // Lấy tất cả sản phẩm từ cơ sở dữ liệu
            var allProducts = db.SANPHAMs.ToList();
            int pageSize = 10;
            Session["NameKind"] = NameKind;
            // Tìm kiếm trong danh sách sản phẩm đã tải lên bộ nhớ
            var result = allProducts.Where(x => x.LOAISP.TENLOAI.IndexOf(NameKind, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
            int pageNumber = (page ?? 1);
            IPagedList<SANPHAM> pagedProducts = result.ToPagedList(pageNumber, pageSize);
            return View(pagedProducts);
        }
        public ActionResult FindInc(int ?page)
        {
            var allProducts = db.SANPHAMs.ToList();
            int pageSize = 10;
            // Tìm kiếm trong danh sách sản phẩm đã tải lên bộ nhớ
            var result = allProducts.OrderBy(x => x.GIA).ToList();
            int pageNumber = (page ?? 1);
            IPagedList<SANPHAM> pagedProducts = result.ToPagedList(pageNumber, pageSize);
            return View(pagedProducts);
        }
        public ActionResult FindDec(int? page)
        {
            var allProducts = db.SANPHAMs.ToList();
            int pageSize = 10;
            // Tìm kiếm trong danh sách sản phẩm đã tải lên bộ nhớ
            var result = allProducts.OrderByDescending(x => x.GIA).ToList();
            int pageNumber = (page ?? 1);
            IPagedList<SANPHAM> pagedProducts = result.ToPagedList(pageNumber, pageSize);
            return View(pagedProducts);
        }
        public ActionResult DetailPR(int IDPR)
        {
            var DTPR = db.SANPHAMs.FirstOrDefault(x=>x.MASP.Equals(IDPR));
            return View(DTPR);
        }
    }
}