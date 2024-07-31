using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace UIDuAn1.Models
{
    public partial class MayTinh
    {
        public MayTinh()
        {
            HoaDon = new HashSet<HoaDon>();
        }

        public string MaMay { get; set; }
        public string Cpu { get; set; }
        public string Gpu { get; set; }
        public string Ram { get; set; }
        public decimal GiaTien { get; set; }
        public bool TinhTrang { get; set; }
        public string MaNhanVien { get; set; }

        public virtual NhanVien MaNhanVienNavigation { get; set; }
        public virtual ICollection<HoaDon> HoaDon { get; set; }
    }
}
