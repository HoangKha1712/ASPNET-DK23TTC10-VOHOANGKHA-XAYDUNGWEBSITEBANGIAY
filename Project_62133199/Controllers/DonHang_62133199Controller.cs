using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Project_62133199.Models;

namespace Project_62133199.Controllers
{
    public class DonHang_62133199Controller : Controller
    {
        dbShopGiayDataContext data = new dbShopGiayDataContext();

        // GET: DonHang
        public ActionResult LichSuDonHang()
        {
            if (Session["Taikhoan"] == null)
            {
                return RedirectToAction("Dangnhap", "User_62133199");
            }
            else
            {
                KHACHHANG kh = (KHACHHANG)Session["Taikhoan"];
                var listLichSuDonHang = data.DONHANGs.Where(p => p.MaKH == kh.MaKH).ToList();
                return View(listLichSuDonHang);
            }

        }
        public ActionResult ChiTietDonHang(int? maDonHang, decimal? tongTien)
        {
            if (Session["Taikhoan"] == null) RedirectToAction("Index", "Home_62133199");
            KHACHHANG kh = (KHACHHANG)Session["Taikhoan"];
            var ListChiTietDonHang = data.CT_DONHANGs.Where(p => p.MaDonHang == maDonHang).ToList();  // danh sách các sản phẩm trong chi tiết đơn hàng

            List<CTDH_62133199> listCTDH = new List<CTDH_62133199>();

            foreach (var item in ListChiTietDonHang)
            {
                listCTDH.Add(new CTDH_62133199(item.MaGiay, maDonHang));
            }
            ViewBag.TongTien = tongTien;
            return View(listCTDH);
        }
    }
}