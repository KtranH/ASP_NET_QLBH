using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using GTX_STORE_WEB.Models;
using PagedList;
using Common;
using System.Net.Http;
using System.Threading.Tasks;

namespace GTX_STORE_WEB.Controllers
{
    public class HomeController : Controller
    {
        DB_GTXSTORE db = new DB_GTXSTORE();
        public ActionResult Index(int? page)
        {
            List<HANGSX> listNSX = db.HANGSXes.ToList();
            List<LOAISP> listLoai = db.LOAISPs.ToList();
            Session["NSX"] = listNSX;
            Session["LOAI"] = listLoai;
            int pageSize = 10; // Số sản phẩm trên mỗi trang
            List<SANPHAM> products = db.SANPHAMs.ToList();
            int pageNumber = (page ?? 1); // Trang mặc định là 1 nếu không có trang được chỉ định
            IPagedList<SANPHAM> pagedProducts = products.ToPagedList(pageNumber, pageSize);
            return View(pagedProducts);
        }

        public ActionResult About()
        {
            return View();
        }
        public async Task<ActionResult> Address()
        {
            string apiKey = "0b151c45f779c1e67b220970472775d5";
            string city = "hanoi";
            string apiUrl = $"http://api.openweathermap.org/data/2.5/weather?q={city}&appid={apiKey}&units=metric";
            List<string> ThongTinThoiTiet = new List<string>();
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl);
                    if (response.IsSuccessStatusCode)
                    {
                        string jsonResponse = await response.Content.ReadAsStringAsync();
                        dynamic weatherData = Newtonsoft.Json.JsonConvert.DeserializeObject(jsonResponse);

                        string weatherDescription = weatherData.weather[0].description;
                        double temperature = weatherData.main.temp;
                        int humidity = weatherData.main.humidity;
                        double windSpeed = weatherData.wind.speed;
                        //Thêm vào list để hiển thị
                        ThongTinThoiTiet.Add("Thời tiết: "+ weatherDescription);
                        ThongTinThoiTiet.Add("Nhiệt độ: "+ temperature.ToString()+ "°C");
                        ThongTinThoiTiet.Add("Độ ẩm: "+humidity.ToString() + "%");
                        ThongTinThoiTiet.Add("Tốc độ gió: "+windSpeed + "m/s");


                       
                    }
                    else
                    {
                       
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Đã xảy ra lỗi: {ex.Message}");
                }
            }
            ViewBag.ThoiTiet=ThongTinThoiTiet;
            return View(ViewBag.ThoiTiet);
        }
        public ActionResult ThankYou()
        {
            return View();
        }
        public ActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Contact(string Name, string Email, string Subject, string Content)
        {
            
                string template = System.IO.File.ReadAllText(Server.MapPath("~/client/template/networder.html"));


                template = template.Replace("{{Name}}", Name);
                template = template.Replace("{{Email}}", Email);
                template = template.Replace("{{Subject}}", Subject);
                template = template.Replace("{{Content}}", Content);
                // gửi mail cho shop//
                var toEmail = ConfigurationManager.AppSettings["ToEmailAddress"].ToString();
                new MailHelper().SendMail(toEmail, "Phản hồi từ khách hàng: " + Name, template);
                // gửi mail thông báo phản hồi thành công//
                new MailHelper().SendMail(Email, "Xác nhận phản hồi", "Cảm ơn bạn đã phản hồi cho shop chúng tôi! ");
                return RedirectToAction("ThankYou");
        }
        public ActionResult Error404()
        {
            return View();
        }
        public JsonResult Search(string search)
        {
            var timkiem = db.SANPHAMs
                .Where(x => x.TENSP.Contains(search))
                .Select(x => new
                {
                    MASP = x.MASP,
                    TENSP = x.TENSP
                })
                .ToList();

            return Json(timkiem, JsonRequestBehavior.AllowGet);
        }

    }

}