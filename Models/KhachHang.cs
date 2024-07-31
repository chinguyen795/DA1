using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace UIDuAn1.Models
{
    public partial class KhachHang
    {
        public KhachHang()
        {
            HoaDon = new HashSet<HoaDon>();
        }

        public string MaKhachHang { get; set; }
        public string TaiKhoan { get; set; }
        public string MatKhau { get; set; }
        public int SoGioSuDung { get; set; }
        public string MaNhanVien { get; set; }

        public virtual NhanVien MaNhanVienNavigation { get; set; }
        public virtual ICollection<HoaDon> HoaDon { get; set; }
    }
}
