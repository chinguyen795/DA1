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
using static System.ComponentModel.Design.ObjectSelectorEditor;

namespace UIDuAn1
{
    public partial class UC_HoaDon : UserControl
    {
        private string currentUserRole;
        public UC_HoaDon(string userRole)
        {
            currentUserRole = userRole;
            InitializeComponent();
            checkVaiTro();
            dtbNgayLap.Value = DateTime.Now;
            ResetForm();
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
                            btnXoa.Enabled = true;
                            btnLamMoi.Enabled = true;
                            break;
                        case "VT03": // Vai trò VT03
                            btnThem.Enabled = true;
                            btnSua.Enabled = true;
                            btnXoa.Enabled = true;
                            btnLamMoi.Enabled = true;
                            break;
                        default:
                            // Vô hiệu hóa tất cả các nút nếu vai trò không xác định
                            break;
                    }
                }
            }
        }
        private void LoadData()
        {
            using (var context = new QUANLYQUANNETContext())
            {
                // Load danh sách nhân viên
                var nhanVienList = context.NhanVien.Select(nv => new
                {
                    MaNhanVien = nv.MaNhanVien,
                    TenNhanVien = nv.HoTen,
                    DisplayText = $"{nv.MaNhanVien} | {nv.HoTen}"
                }).ToList();

                cbMaNV.DataSource = nhanVienList;
                cbMaNV.DisplayMember = "DisplayText";
                cbMaNV.ValueMember = "MaNhanVien";

                // Load danh sách khách hàng
                var khachHangList = context.KhachHang.Select(kh => new
                {
                    MaKhachHang = kh.MaKhachHang,
                    TenKhachHang = kh.TaiKhoan,
                    DisplayText = $"{kh.MaKhachHang} | {kh.TaiKhoan}"
                }).ToList();

                cboMaKH.DataSource = khachHangList;
                cboMaKH.DisplayMember = "DisplayText";
                cboMaKH.ValueMember = "MaKhachHang";

                // Load danh sách hóa đơn
                var query = from hd in context.HoaDon
                            join nv in context.NhanVien on hd.MaNhanVien equals nv.MaNhanVien
                            join kh in context.KhachHang on hd.MaKhachHang equals kh.MaKhachHang
                            orderby hd.MaHoaDon
                            select new
                            {
                                hd.MaHoaDon,
                                hd.NgayLap,
                                hd.MaNhanVien,
                                nv.HoTen,
                                hd.MaKhachHang,
                                kh.TaiKhoan
                            };

                dtgHoaDon.DataSource = query.ToList();

                dtgHoaDon.Columns[0].HeaderText = "Mã hóa đơn";
                dtgHoaDon.Columns[1].HeaderText = "Ngày lập";
                dtgHoaDon.Columns[2].Visible = false;
                dtgHoaDon.Columns[3].HeaderText = "Nhân viên";
                dtgHoaDon.Columns[4].Visible = false;
                dtgHoaDon.Columns[5].HeaderText = "Khách hàng";

                int mayTinhCount = context.HoaDon.Count();
                string newMayTinhID = $"HD{(mayTinhCount + 1).ToString("D3")}";

                txtMaHoaDon.Text = newMayTinhID;
            }
        }
        private void dtgHoaDon_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void ResetForm()
        {

            txtMaHoaDon.Clear();
            cbMaNV.SelectedIndex = -1;
            cboMaKH.SelectedIndex = -1;
            dtbNgayLap.Value = DateTime.Now;

            using (var context = new QUANLYQUANNETContext())
            {
                int mayTinhCount = context.HoaDon.Count();
                string newMaMayTinh = $"HD{(mayTinhCount + 1).ToString("D3")}";
                txtMaHoaDon.Text = newMaMayTinh;
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (cbMaNV.SelectedIndex == -1 || cboMaKH.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn nhân viên và khách hàng.");
                return;
            }

            using (var context = new QUANLYQUANNETContext())
            {
                int mayTinhCount = context.HoaDon.Count();
                string newMayTinhID = $"HD{(mayTinhCount + 1).ToString("D3")}";


                HoaDon newHoaDon = new HoaDon
                {
                    MaHoaDon = newMayTinhID,
                    NgayLap = dtbNgayLap.Value,
                    MaNhanVien = cbMaNV.SelectedValue.ToString(),
                    MaKhachHang = cboMaKH.SelectedValue.ToString()
                };

                try
                {
                    context.HoaDon.Add(newHoaDon);
                    context.SaveChanges();
                    MessageBox.Show("Thêm thành công");
                    LoadData();
                    ResetForm();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}");
                }
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dtgHoaDon.SelectedRows.Count > 0)
            {
                string maHoaDonSelected = dtgHoaDon.SelectedRows[0].Cells["MaHoaDon"].Value.ToString();

                using (var context = new QUANLYQUANNETContext())
                {
                    if (cbMaNV.SelectedIndex == -1 || cboMaKH.SelectedIndex == -1)
                    {
                        MessageBox.Show("Vui lòng thử lại.");
                        return;
                    }

                    HoaDon suaHoaDon = context.HoaDon.FirstOrDefault(hd => hd.MaHoaDon == maHoaDonSelected);
                    if (suaHoaDon == null)
                    {
                        MessageBox.Show("Mã hóa đơn không tồn tại");
                        return;
                    }

                    suaHoaDon.NgayLap = dtbNgayLap.Value;
                    suaHoaDon.MaNhanVien = cbMaNV.SelectedValue.ToString();
                    suaHoaDon.MaKhachHang = cboMaKH.SelectedValue.ToString();

                    try
                    {
                        context.SaveChanges();
                        MessageBox.Show("Cập nhật thành công");
                        LoadData();
                        ResetForm();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}");
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn hóa đơn cần cập nhật");
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dtgHoaDon.SelectedRows.Count > 0)
            {
                string maHoaDon = dtgHoaDon.SelectedRows[0].Cells["MaHoaDon"].Value.ToString();

                DialogResult result = MessageBox.Show("Bạn chắc chắn muốn xóa?", "Thông báo",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    using (var context = new QUANLYQUANNETContext())
                    {
                        HoaDon deleteHoaDon = context.HoaDon.FirstOrDefault(hd => hd.MaHoaDon == maHoaDon);

                        try
                        {
                            if (deleteHoaDon != null)
                            {
                                context.HoaDon.Remove(deleteHoaDon);
                                context.SaveChanges();
                                MessageBox.Show("Xóa thành công");
                                LoadData();
                                ResetForm();
                            }
                            else
                            {
                                MessageBox.Show("Mã hóa đơn không tồn tại.");
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Không thể xoá vì còn liên kết với dữ liệu khác (hoá đơn chi tiết).");
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn hóa đơn cần xóa.");
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            ResetForm();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            using (var context = new QUANLYQUANNETContext())
            {
                string searchValue = txtTimKiem.Text.Trim();

                var query = from hd in context.HoaDon
                            join nv in context.NhanVien on hd.MaNhanVien equals nv.MaNhanVien
                            join kh in context.KhachHang on hd.MaKhachHang equals kh.MaKhachHang
                            where hd.MaHoaDon.Contains(searchValue) ||
                                  nv.HoTen.Contains(searchValue) ||
                                  kh.TaiKhoan.Contains(searchValue)
                            select new
                            {
                                hd.MaHoaDon,
                                hd.NgayLap,
                                hd.MaNhanVien,
                                nv.HoTen,
                                hd.MaKhachHang,
                                kh.TaiKhoan
                            };

                dtgHoaDon.DataSource = query.ToList();
            }
        }
        private void UC_HoaDon_Load(object sender, EventArgs e)
        {
            LoadData();
            

        }
        private decimal giaMonAnHienTai = 0;
        private void cboMaMonAn_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }
       

        private void txtSoLuongmonAn_TextChanged(object sender, EventArgs e)
        {
            /*TinhTriGiaHoaDon();*/
        }

        private void dtgHoaDon_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = dtgHoaDon.Rows[e.RowIndex];

                txtMaHoaDon.Text = selectedRow.Cells["MaHoaDon"].Value.ToString();
                dtbNgayLap.Value = Convert.ToDateTime(selectedRow.Cells["NgayLap"].Value);
                cbMaNV.SelectedValue = selectedRow.Cells["MaNhanVien"].Value.ToString();
                cboMaKH.SelectedValue = selectedRow.Cells["MaKhachHang"].Value.ToString();

            }

        }

        private void btnThemMon_Click(object sender, EventArgs e)
        {
            ThemMonAn themMonAn = new ThemMonAn();
            themMonAn.Show();
        }

        private void btnInHD_Click(object sender, EventArgs e)
        {
            InHD inhd = new InHD();
            inhd.Show();
        }
    }
}
