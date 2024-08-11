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
    public partial class InHD : Form
    {
        public InHD()
        {

            InitializeComponent();
            cboMaHD.SelectedIndexChanged += cboMaHD_SelectedIndexChanged; // Add event handler for selection change
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LoadData()
        {
            using (var context = new QUANLYQUANNETContext())
            {
                var hoadons = context.HoaDon.Select(hoadon => new
                {
                    mahd = hoadon.MaHoaDon,
                }).ToList();

                cboMaHD.DataSource = hoadons;
                cboMaHD.DisplayMember = "mahd";
                cboMaHD.ValueMember = "mahd";
            }
        }

        private void LoadData2(string selectedMaHD)
        {
            using (var context = new QUANLYQUANNETContext())
            {
                var query1 = from hd in context.HoaDon
                             join kh in context.KhachHang on hd.MaKhachHang equals kh.MaKhachHang
                             join nv in context.NhanVien on hd.MaNhanVien equals nv.MaNhanVien
                             where hd.MaHoaDon == selectedMaHD
                             select new
                             {
                                 hd.MaHoaDon,
                                 kh.TaiKhoan,
                                 nv.HoTen
                             };

                dtgHD.DataSource = query1.ToList();

                dtgHD.Columns[0].HeaderText = "Mã hóa đơn";
                dtgHD.Columns[1].HeaderText = "Khách hàng";
                dtgHD.Columns[2].HeaderText = "Tên nhân viên";


                var query2 = from hdct in context.HoaDonChiTiet
                             join td in context.ThucDon on hdct.MaMonAn equals td.MaMonAn
                             where hdct.MaHoaDon == selectedMaHD
                             orderby hdct.MaHoaDon
                             select new
                             {
                                 hdct.MaHoaDon,
                                 hdct.MaMonAn,
                                 td.TenMonAn,
                                 hdct.SoLuongMon,
                                 hdct.TriGia,
                                 hdct.MaHoaDonChiTiet,
                             };
                dtgHDCT.DataSource = query2.ToList();

                dtgHDCT.Columns[0].HeaderText = "Mã hóa đơn";
                dtgHDCT.Columns[1].Visible = false;
                dtgHDCT.Columns[2].HeaderText = "Món ăn";
                dtgHDCT.Columns[3].HeaderText = "Số lượng món";
                dtgHDCT.Columns[4].HeaderText = "Số tiền";
                dtgHDCT.Columns[5].Visible = false;

            }
        }

        private void cboMaHD_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboMaHD.SelectedValue != null)
            {
                string selectedMaHD = cboMaHD.SelectedValue.ToString();
                LoadData2(selectedMaHD);
                CalculateTotalAllThePrice();

            }
        }

        private void InHD_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void dtgHDCT_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void dtgHD_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }
        private void CalculateTotalAllThePrice()
        {
            if (cboMaHD.SelectedIndex == -1)
            {
                lblTongTien.Refresh();
                return;
            }

            using (var context = new QUANLYQUANNETContext())
            {
                string maHoaDon = cboMaHD.SelectedValue.ToString();

                var tongTien = context.HoaDonChiTiet
                                      .Where(hdct => hdct.MaHoaDon == maHoaDon)
                                      .Sum(hdct => hdct.TriGia);

                lblTongTien.Text = $"Tổng tiền: {tongTien.ToString("F3")} VND";
                lblTongTien.Refresh(); // Đảm bảo rằng TextBox được làm mới và hiển thị giá trị
            }
        }
        private void lblTongTien_Click(object sender, EventArgs e)
        {
        }

        private void btnInHoaDon_Click(object sender, EventArgs e)
        {

        }
    }
}
