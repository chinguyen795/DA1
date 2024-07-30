using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UIDuAn1;

namespace Ui_DuAn
{
    public partial class MainForm : Form
    {
        bool slideExpand;
        bool NhanVienCollapsed;
        public MainForm()
        {
            InitializeComponent();
            UC_ThongKe uC_ThongKe = new UC_ThongKe();
        }
        private void addUC(UserControl usercontrol)
        {
            usercontrol.Dock = DockStyle.Fill;
            pnMain.Controls.Clear();
            pnMain.Controls.Add(usercontrol);
            usercontrol.BringToFront();
        }
        private void btnMenu_Click(object sender, EventArgs e)
        {
            slideTimer.Start();
        }
        private void btnThongKe_Click(object sender, EventArgs e)
        {
            UC_ThongKe uC_ThongKe = new UC_ThongKe();
            addUC(uC_ThongKe);
        }

        private void btnThucDon_Click(object sender, EventArgs e)
        {
            UC_ThucDon uC_ThucDon = new UC_ThucDon();
            addUC(uC_ThucDon);
        }

        private void btnMayTinh_Click(object sender, EventArgs e)
        {
            UC_MayTinh uC_MayTinh = new UC_MayTinh();
            addUC(uC_MayTinh);
        }

        private void btnKhachHang_Click(object sender, EventArgs e)
        {
            UC_KhachHang uC_KhachHang = new UC_KhachHang();
            addUC(uC_KhachHang);
        }

        private void btnHoaDon_Click(object sender, EventArgs e)
        {
            UC_HoaDon uC_HoaDon = new UC_HoaDon();
            addUC(uC_HoaDon);
        }
        private void btnThongTin_Click(object sender, EventArgs e)
        {
            UC_ThongTin uC_ThongTin = new UC_ThongTin();
            addUC(uC_ThongTin);
        }

        private void btnConViec_Click(object sender, EventArgs e)
        {
            UC_CongViec uC_CongViec = new UC_CongViec();
            addUC(uC_CongViec);
        }

        private void btnTaiKhoan_Click(object sender, EventArgs e)
        {
            UC_TaiKhoan uC_TaiKhoan = new UC_TaiKhoan();
            addUC(uC_TaiKhoan);
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            guna2ShadowForm1.SetShadowForm(this);
        }
        private void pnSlideContainer_tick(object sender, EventArgs e)
        {
            if(slideExpand)
            {
                pnSlideContainer.Width -=10;
                if(pnSlideContainer.Width == pnSlideContainer.MinimumSize.Width)
                {
                    slideExpand = false;
                    slideTimer.Stop();
                }
            }
            else
            {
                pnSlideContainer.Width += 10;
                if (pnSlideContainer.Width == pnSlideContainer.MaximumSize.Width)
                {
                    slideExpand = true;
                    slideTimer.Stop();
                }
            }
        }
        private void tmNhanVien_Tick(object sender, EventArgs e)
        {
            if (NhanVienCollapsed)
            {
                pnContainer.Height += 10;
                if (pnContainer.Height == pnContainer.MaximumSize.Height)
                {
                    NhanVienCollapsed = false;
                    tmNhanVien.Stop();
                }
            }
            else
            {
                pnContainer.Height -= 10;
                if (pnContainer.Height == pnContainer.MinimumSize.Height)
                {
                    NhanVienCollapsed = true;
                    tmNhanVien.Stop();
                }
            }
        }
        private void btnNhanVien_Click(object sender, EventArgs e)
        {
            tmNhanVien.Start();
        }

        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
            this.Close();
        }
    }
}
