using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Project_62133199.Models;

namespace Project_62133199.Controllers
{
    public class GioHang_62133199Controller : Controller
    {
        // GET: GioHang
        dbShopGiayDataContext data = new dbShopGiayDataContext();
        public List<GioHang_62133199> LayGioHang()
        {
            List<GioHang_62133199> listGioHang = Session["GioHang_62133199"] as List<GioHang_62133199>;
            if (listGioHang == null)
            {
                listGioHang = new List<GioHang_62133199>();
                Session["GioHang_62133199"] = listGioHang;
            }
            return listGioHang;
        }
        public ActionResult ThemGioHang(int iMaGiay, string strURL)
        {
            List<GioHang_62133199> listGioHang = LayGioHang();
            GioHang_62133199 sanpham = listGioHang.Find(n => n.iMaGiay == iMaGiay);
            if (sanpham == null)
            {
                sanpham = new GioHang_62133199(iMaGiay);
                listGioHang.Add(sanpham);
                return Redirect(strURL);
            }
            else
            {
                sanpham.iSoLuong++;
                return Redirect(strURL);
            }
        }
        private int TongSoLuong()
        {
            int iTongSoLuong = 0;
            List<GioHang_62133199> listGioHang = Session["GioHang_62133199"] as List<GioHang_62133199>;
            if (listGioHang != null)
            {
                iTongSoLuong = listGioHang.Sum(n => n.iSoLuong);
            }
            return iTongSoLuong;
        }
        private double TongTien()
        {
            double iTongTien = 0;
            List<GioHang_62133199> listGioHang = Session["GioHang_62133199"] as List<GioHang_62133199>;
            if (listGioHang != null)
            {
                iTongTien = listGioHang.Sum(n => n.dThanhTien);
            }
            return iTongTien;
        }
        public ActionResult GioHang()
        {
            List<GioHang_62133199> listGioHang = LayGioHang();
            if (listGioHang.Count == 0)
            {
                return RedirectToAction("Index", "Home_62133199");
            }
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = TongTien();
            return View(listGioHang);
        }

        public ActionResult GiohangPartial()
        {
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = TongTien();
            return PartialView();
        }
        //Xoa gio hang 
        public ActionResult XoaGioHang(int iMaSp)
        {
            //lay gio hang tu sesstion
            List<GioHang_62133199> listGioHang = LayGioHang();
            //Kiem tra giay da co trong sesstion 
            GioHang_62133199 sanpham = listGioHang.SingleOrDefault(n => n.iMaGiay == iMaSp);
            //neu ton tai thi cho sua so luong
            if (sanpham != null)
            {
                listGioHang.RemoveAll(n => n.iMaGiay == iMaSp);
                return RedirectToAction("GioHang");
            }
            if (listGioHang.Count == 0)
            {
                return RedirectToAction("Index", "Home_62133199");
            }
            return RedirectToAction("GioHang");
        }
        //cap nhap gio hang 
        public ActionResult CapnhatGiohang(int iMaSp, FormCollection f)
        {
            //Lay gio hang tu sesstion
            List<GioHang_62133199> listGioHang = LayGioHang();
            //kiem tra giay da co trong sesstion
            GioHang_62133199 sanpham = listGioHang.SingleOrDefault(n => n.iMaGiay == iMaSp);
            //neu ton tai thi cho sua so luong
            if (sanpham != null)
            {
                sanpham.iSoLuong = int.Parse(f["txtSoluong"].ToString());
            }
            return RedirectToAction("GioHang");
        }
        //xoa tat ca gio hang 
        public ActionResult XoaTatcaGiohang()
        {
            //Lay gio hang tu sesstion
            List<GioHang_62133199> listGioHang = LayGioHang();
            listGioHang.Clear();
            return RedirectToAction("Index", "Home_62133199");
        }
        //hien thi view dathang de cap nhap cac thong tin cho don hang
        [HttpGet]
        public ActionResult DatHang()
        {
            //Kiem tra dang nhap
            if (Session["Taikhoan"] == null || Session["Taikhoan"].ToString() == "")
            {
                return RedirectToAction("Dangnhap", "User_62133199");
            }
            if (Session["GioHang_62133199"] == null)
            {
                return RedirectToAction("Index", "Home_62133199");
            }

            //Lya gio hang tu sesion
            List<GioHang_62133199> lstGioHang = LayGioHang();
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = TongTien();

            return View(lstGioHang);
        }
        [HttpPost]
        public ActionResult DatHang(FormCollection collection)
        {
            //them don hang
            DONHANG dh = new DONHANG();
            KHACHHANG kh = (KHACHHANG)Session["Taikhoan"];
            List<GioHang_62133199> gh = LayGioHang();
            dh.MaKH = kh.MaKH;
            dh.NgayDat = DateTime.Now;
            var ngaygiao = String.Format("{0:MM/dd/yyyy}", DateTime.Now.AddDays(7));
            dh.NgayGiao = DateTime.Parse(ngaygiao);
            dh.TinhTrangGiaoHang = false;
            dh.TongTien = (decimal)TongTien();
            data.DONHANGs.InsertOnSubmit(dh);
            data.SubmitChanges();
            //Them chi tiet don hang
            foreach (var item in gh)
            {
                CT_DONHANG ctdh = new CT_DONHANG();
                ctdh.MaDonHang = dh.MaDonHang;
                ctdh.MaGiay = item.iMaGiay;
                ctdh.SoLuong = item.iSoLuong;
                ctdh.GiaLucBan = (decimal)item.dGiaBan;
                ctdh.ThanhTien = (decimal)item.dThanhTien;
                data.CT_DONHANGs.InsertOnSubmit(ctdh);
            }
            data.SubmitChanges();
            Session["GioHang_62133199"] = null;
            return RedirectToAction("Xacnhandonhang", "GioHang_62133199");
        }
        public ActionResult Xacnhandonhang()
        {
            return View();
        }
    }
}