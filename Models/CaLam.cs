using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace UIDuAn1.Models
{
    public partial class CaLam
    {
        public string MaCa { get; set; }
        public string CaLam1 { get; set; }
        public int SoGioLam { get; set; }
        public string ViPham { get; set; }
        public string MaNhanVien { get; set; }

        public virtual NhanVien MaNhanVienNavigation { get; set; }
    }
}
