using System;
using System.Linq;

namespace Project_62133199.Models
{
    public class GioHang_62133199
    {
        dbShopGiayDataContext data = new dbShopGiayDataContext();
        public int iMaGiay { set; get; }
        public int iSize { get; set; }
        public string sTenGiay { set; get; }
        public string sAnhBia { set; get; }
        public Double dGiaBan { set; get; }
        public int iSoLuong { set; get; }
        public Double dThanhTien
        {
            get { return iSoLuong * dGiaBan; }
        }
        public GioHang_62133199(int MaGiay)
        {
            iMaGiay = MaGiay;
            SANPHAM GIAY = data.SANPHAMs.Single(n => n.MaGiay == iMaGiay);
            sTenGiay = GIAY.TenGiay;
            sAnhBia = GIAY.AnhBia;
            iSize = GIAY.Size;
            iSoLuong = 1;
            dGiaBan = double.Parse(GIAY.GiaBan.ToString());

        }
    }
}