using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace UIDuAn1.Models
{
    public partial class ThucDon
    {
        public ThucDon()
        {
            HoaDonChiTiet = new HashSet<HoaDonChiTiet>();
        }

        public string MaMonAn { get; set; }
        public string TenMonAn { get; set; }
        public int SoLuong { get; set; }
        public decimal Gia { get; set; }
        public bool TinhTrang { get; set; }
        public byte[] HinhAnh { get; set; }
        public string MaNhanVien { get; set; }

        public virtual NhanVien MaNhanVienNavigation { get; set; }
        public virtual ICollection<HoaDonChiTiet> HoaDonChiTiet { get; set; }
    }
}
