using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace UIDuAn1.Models
{
    public partial class VaiTro
    {
        public VaiTro()
        {
            NhanVien = new HashSet<NhanVien>();
        }

        public string MaVaiTro { get; set; }
        public string TenVaiTro { get; set; }

        public virtual ICollection<NhanVien> NhanVien { get; set; }
    }
}
