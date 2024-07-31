using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace UIDuAn1.Models
{
    public partial class HoaDonChiTiet
    {
        public string MaHoaDon { get; set; }
        public string MaMonAn { get; set; }
        public int SoLuong { get; set; }

        public virtual HoaDon MaHoaDonNavigation { get; set; }
        public virtual ThucDon MaMonAnNavigation { get; set; }
    }
}
