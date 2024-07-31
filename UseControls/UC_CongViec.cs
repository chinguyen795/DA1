using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UIDuAn1.Models;

namespace UIDuAn1
{
    public partial class UC_CongViec : UserControl
    {
        private string currentUserRole;
        public UC_CongViec(string userRole)
        {
            currentUserRole = userRole;
            InitializeComponent();
            checkVaiTro();
        }
        private void checkVaiTro()
        {
            using (var context = new QUANLYQUANNETContext())
            {
                var vaiTro = context.VaiTro.SingleOrDefault(vt => vt.MaVaiTro == currentUserRole);
                if (vaiTro != null)
                {
                    btnThem.Enabled = false;
                    btnSua.Enabled = false;
                    btnXoa.Enabled = false;
                    btnLamMoi.Enabled = false;
                    switch (vaiTro.MaVaiTro)
                    {
                        case "VT01": // Vai trò VT01
                            btnThem.Enabled = true;
                            btnSua.Enabled = true;
                            btnXoa.Enabled = true;
                            btnLamMoi.Enabled = true;
                            break;
                        case "VT02": // Vai trò VT02
                            btnThem.Enabled = true;
                            btnSua.Enabled = true;
                            btnXoa.Enabled = false;
                            btnLamMoi.Enabled = true;
                            break;
                        case "VT03": // Vai trò VT03
                            btnThem.Enabled = false;
                            btnSua.Enabled = false;
                            btnXoa.Enabled = false;
                            btnLamMoi.Enabled = false;
                            break;
                        default:
                            // Vô hiệu hóa tất cả các nút nếu vai trò không xác định
                            break;
                    }
                }
            }
        }
    }
}
