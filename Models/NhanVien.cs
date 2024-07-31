using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace UIDuAn1.Models
{
    public partial class NhanVien
    {
        public NhanVien()
        {
            CaLam = new HashSet<CaLam>();
            HoaDon = new HashSet<HoaDon>();
            KhachHang = new HashSet<KhachHang>();
            MayTinh = new HashSet<MayTinh>();
            ThucDon = new HashSet<ThucDon>();
        }

        public string MaNhanVien { get; set; }
        public string HoTen { get; set; }
        public string Gmail { get; set; }
        public string DiaChi { get; set; }
        public string TenVaiTro { get; set; }
        public bool TrangThai { get; set; }
        public string MatKhau { get; set; }
        public string MaVaiTro { get; set; }

        public virtual VaiTro MaVaiTroNavigation { get; set; }
        public virtual ICollection<CaLam> CaLam { get; set; }
        public virtual ICollection<HoaDon> HoaDon { get; set; }
        public virtual ICollection<KhachHang> KhachHang { get; set; }
        public virtual ICollection<MayTinh> MayTinh { get; set; }
        public virtual ICollection<ThucDon> ThucDon { get; set; }
    }
}
