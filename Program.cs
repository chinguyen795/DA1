using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ui_DuAn;
using UIDuAn1.Models;

namespace UIDuAn1
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new CongViecForm()) ;
        }

    }
    public class CongViecForm : Form
    {
        public CongViecForm()
        {
            this.Text = "Công Việc";
            this.Size = new System.Drawing.Size(1000, 800); // Đặt kích thước Form lớn hơn

            string userRole = "VT01";
            UC_CongViec ucCongViec = new UC_CongViec(userRole);
            ucCongViec.Dock = DockStyle.Fill; // Đảm bảo UserControl chiếm toàn bộ Form

            this.Controls.Add(ucCongViec);
        }
    }

    public class HoaDonForm : Form
    {
        public HoaDonForm()
        {
            this.Text = "Máy Tính";
            this.Size = new System.Drawing.Size(1700, 1700);

            string userRole = "VT01";
            UC_HoaDon ucMayTinh = new UC_HoaDon(userRole);
            ucMayTinh.Dock = DockStyle.Fill;
            this.Controls.Add(ucMayTinh);
        }
    }


}
