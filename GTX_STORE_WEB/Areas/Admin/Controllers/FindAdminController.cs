using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using GTX_STORE_WEB.Models;
using PagedList;
using PagedList.Mvc;

namespace GTX_STORE_WEB.Areas.Admin.Controllers
{
    public class FindAdminController : Controller
    {
        DB_GTXSTORE DB = new DB_GTXSTORE();
        // GET: Admin/FindAdmin
        public ActionResult ShowFindAdPR(int ?page)
        {
            // Lấy tất cả sản phẩm từ cơ sở dữ liệu
            var allProducts = DB.SANPHAMs.ToList();
            int pageSize = 10;
            string FindX = Session["NameAdminPR"].ToString();
            // Tìm kiếm trong danh sách sản phẩm đã tải lên bộ nhớ
            var result = allProducts.Where(x => x.TENSP.IndexOf(FindX, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
            int pageNumber = (page ?? 1);
            IPagedList<SANPHAM> pagedProducts = result.ToPagedList(pageNumber, pageSize);
            return View(pagedProducts);
        }
        [HttpPost]
        public ActionResult FindAdminPR(int? page, string FindX)
        {
            // Lấy tất cả sản phẩm từ cơ sở dữ liệu
            var allProducts = DB.SANPHAMs.ToList();
            int pageSize = 10;
            Session["NameAdminPR"] = FindX;
            // Tìm kiếm trong danh sách sản phẩm đã tải lên bộ nhớ
            var result = allProducts.Where(x => x.TENSP.IndexOf(FindX, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
            int pageNumber = (page ?? 1);
            IPagedList<SANPHAM> pagedProducts = result.ToPagedList(pageNumber, pageSize);
            return View(pagedProducts);
        }
        public ActionResult ShowFindAdNSX(int? page)
        {
            // Lấy tất cả sản phẩm từ cơ sở dữ liệu
            var allProducts = DB.HANGSXes.ToList();
            int pageSize = 10;
            string FindX = Session["NameAdminNSX"].ToString();
            // Tìm kiếm trong danh sách sản phẩm đã tải lên bộ nhớ
            var result = allProducts.Where(x => x.TENNSX.IndexOf(FindX, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
            int pageNumber = (page ?? 1);
            IPagedList<HANGSX> pagedProducts = result.ToPagedList(pageNumber, pageSize);
            return View(pagedProducts);
        }
        [HttpPost]
        public ActionResult FindAdminNSX(int? page, string FindX)
        {
            // Lấy tất cả sản phẩm từ cơ sở dữ liệu
            var allProducts = DB.HANGSXes.ToList();
            int pageSize = 10;
            Session["NameAdminNSX"] = FindX;
            // Tìm kiếm trong danh sách sản phẩm đã tải lên bộ nhớ
            var result = allProducts.Where(x => x.TENNSX.IndexOf(FindX, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
            int pageNumber = (page ?? 1);
            IPagedList<HANGSX> pagedProducts = result.ToPagedList(pageNumber, pageSize);
            return View(pagedProducts);
        }
        public ActionResult ShowFindAdKind(int? page)
        {
            // Lấy tất cả sản phẩm từ cơ sở dữ liệu
            var allProducts = DB.LOAISPs.ToList();
            int pageSize = 10;
            string FindX = Session["NameAdminKind"].ToString();
            // Tìm kiếm trong danh sách sản phẩm đã tải lên bộ nhớ
            var result = allProducts.Where(x => x.TENLOAI.IndexOf(FindX, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
            int pageNumber = (page ?? 1);
            IPagedList<LOAISP> pagedProducts = result.ToPagedList(pageNumber, pageSize);
            return View(pagedProducts);
        }
        [HttpPost]
        public ActionResult FindAdminKind(int? page, string FindX)
        {
            // Lấy tất cả sản phẩm từ cơ sở dữ liệu
            var allProducts = DB.LOAISPs.ToList();
            int pageSize = 10;
            Session["NameAdminKind"] = FindX;
            // Tìm kiếm trong danh sách sản phẩm đã tải lên bộ nhớ
            var result = allProducts.Where(x => x.TENLOAI.IndexOf(FindX, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
            int pageNumber = (page ?? 1);
            IPagedList<LOAISP> pagedProducts = result.ToPagedList(pageNumber, pageSize);
            return View(pagedProducts);
        }
        public ActionResult ShowFindAdUser(int? page)
        {
            // Lấy tất cả sản phẩm từ cơ sở dữ liệu
            string NameAdmin = Session["USERACCOUNT"].ToString();
            var allProducts = DB.KHACHes.ToList();
            int pageSize = 10;
            string FindX = Session["NameAdminUser"].ToString();
            // Tìm kiếm trong danh sách sản phẩm đã tải lên bộ nhớ
            var result = allProducts.Where(x => x.TENTK.IndexOf(FindX, StringComparison.OrdinalIgnoreCase) >= 0 && x.TENTK != NameAdmin).ToList();
            int pageNumber = (page ?? 1);
            IPagedList<KHACH> pagedProducts = result.ToPagedList(pageNumber, pageSize);
            return View(pagedProducts);
        }
        [HttpPost]
        public ActionResult FindAdminUser(int? page, string FindX)
        {
            // Lấy tất cả sản phẩm từ cơ sở dữ liệu
            string NameAdmin = Session["USERACCOUNT"].ToString();
            var allProducts = DB.KHACHes.ToList();
            int pageSize = 10;
            Session["NameAdminUser"] = FindX;
            // Tìm kiếm trong danh sách sản phẩm đã tải lên bộ nhớ
            var result = allProducts.Where(x => x.TENTK.IndexOf(FindX, StringComparison.OrdinalIgnoreCase) >= 0 && x.TENTK != NameAdmin).ToList();
            int pageNumber = (page ?? 1);
            IPagedList<KHACH> pagedProducts = result.ToPagedList(pageNumber, pageSize);
            return View(pagedProducts);
        }
        public ActionResult ShowFindAdOrder(int? page)
        {
            // Lấy tất cả sản phẩm từ cơ sở dữ liệu
            string NameAdmin = Session["USERACCOUNT"].ToString();
            var allProducts = DB.DONHANGs.ToList();
            int pageSize = 10;
            int FindX = Convert.ToInt32(Session["NameAdminOrder"].ToString());
            // Tìm kiếm trong danh sách sản phẩm đã tải lên bộ nhớ
            var result = allProducts.Where(x => x.MADH.Equals(FindX) && x.KHACH.TENTK != NameAdmin).ToList();
            int pageNumber = (page ?? 1);
            IPagedList<DONHANG> pagedProducts = result.ToPagedList(pageNumber, pageSize);
            return View(pagedProducts);
        }
        [HttpPost]
        public ActionResult FindAdminOrder(int? page, string FindX)
        {
            // Lấy tất cả sản phẩm từ cơ sở dữ liệu
            string NameAdmin = Session["USERACCOUNT"].ToString();
            var allProducts = DB.DONHANGs.ToList();
            int pageSize = 10;
            if(FindX.IsEmpty())
            {
                FindX = "0";
            }
            Session["NameAdminOrder"] = FindX;
            int CovertFindX = Convert.ToInt32(FindX);
            // Tìm kiếm trong danh sách sản phẩm đã tải lên bộ nhớ
            var result = allProducts.Where(x => x.MADH.Equals(CovertFindX) && x.KHACH.TENTK != NameAdmin).ToList();
            int pageNumber = (page ?? 1);
            IPagedList<DONHANG> pagedProducts = result.ToPagedList(pageNumber, pageSize);
            return View(pagedProducts);
        }
    }
}