using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace UIDuAn1.Models
{
    public partial class HoaDon
    {
        public HoaDon()
        {
            HoaDonChiTiet = new HashSet<HoaDonChiTiet>();
        }

        public string MaHoaDon { get; set; }
        public DateTime NgayLap { get; set; }
        public string MaNhanVien { get; set; }
        public string MaKhachHang { get; set; }
        public string MaMay { get; set; }
        public decimal TriGia { get; set; }

        public virtual KhachHang MaKhachHangNavigation { get; set; }
        public virtual MayTinh MaMayNavigation { get; set; }
        public virtual NhanVien MaNhanVienNavigation { get; set; }
        public virtual ICollection<HoaDonChiTiet> HoaDonChiTiet { get; set; }
    }
}
