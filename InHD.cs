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
                var khachHangs = context.KhachHang.Select(kh => new
                {
                    MaKH = kh.MaKhachHang,
                    DisplayText = $"{kh.MaKhachHang} | {kh.TaiKhoan}"
                }).ToList();
                var nhanviens = context.NhanVien.Select(nv => new
                {
                    MaNV = nv.MaNhanVien,
                    DisplayText = $"{nv.MaNhanVien} | {nv.HoTen}"
                }).ToList();



                cboMaHD.DataSource = hoadons;
                cboMaHD.DisplayMember = "DisplayText";
                cboMaHD.ValueMember = "MaHoaDon";

                cboMaHD.DataSource = nhanviens;
                cboMaHD.DisplayMember = "DisplayText";
                cboMaHD.ValueMember = "MaNhanVien";

                cboMaHD.DataSource = khachHangs;
                cboMaHD.DisplayMember = "DisplayText";
                cboMaHD.ValueMember = "MaKhachHang";

                var query = from hd in context.HoaDon
                            join kh in context.KhachHang on hd.MaHoaDon equals kh.MaKhachHang
                            join nv in context.NhanVien on hd.MaNhanVien equals nv.MaNhanVien

                            select new
                            {
                                hd.MaHoaDon,
                                kh.TaiKhoan,
                                nv.HoTen
                            };
                dtgHD.DataSource = query.ToList();

                dtgHD.Columns[0].HeaderText = "Mã hóa đơn";
                dtgHD.Columns[1].HeaderText = "Tên nhân viên";
                dtgHD.Columns[2].HeaderText = "Khách hàng";


            }
        }
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void InHD_Load(object sender, EventArgs e)
        {

        }
    }
}
