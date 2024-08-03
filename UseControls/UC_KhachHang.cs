using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using UIDuAn1.Models;
using System.Text.RegularExpressions;

namespace UIDuAn1
{
    public partial class UC_KhachHang : UserControl
    {
        private string currentUserRole;
        public UC_KhachHang(string userRole)
        {
            currentUserRole = userRole;
            InitializeComponent();
            checkVaiTro();
        }

        //check vai tro
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
                            btnThem.Enabled = true;
                            btnSua.Enabled = true;
                            btnXoa.Enabled = false;
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
                var nhanViens = context.NhanVien.Select(nv => new
                {
                    MaNv = nv.MaNhanVien,
                    TenNv = nv.HoTen,
                    DisplayText = $"{nv.MaNhanVien} | {nv.HoTen}"
                }).ToList();

                cbMaNV.DataSource = nhanViens;
                cbMaNV.DisplayMember = "DisplayText";
                cbMaNV.ValueMember = "MaNv";
                var query = from kh in context.KhachHang
                            join NhanVien in context.NhanVien on kh.MaNhanVien equals NhanVien.MaNhanVien

                            select new
                            {
                                kh.MaKhachHang,
                                kh.TaiKhoan,
                                kh.MatKhau,
                                kh.SoTien,
                                NhanVien.MaNhanVien,
                            };

                dtgKhachHang.DataSource = query.ToList();

                dtgKhachHang.Columns[0].HeaderText = "Mã Khách Hàng";
                dtgKhachHang.Columns[1].HeaderText = "Tài Khoản";
                dtgKhachHang.Columns[2].HeaderText = "Mật Khẩu";
                dtgKhachHang.Columns[3].HeaderText = "Số Tiền";
                dtgKhachHang.Columns[4].HeaderText = "Mã Nhân Viên";
            }
        }

        private void UC_KhachHang_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        private void Reset()
        {
            txtMaKhachHang.Clear();
            txtTaiKhoan.Clear();
            txtMatkhau.Clear();
            txtSoTien.Clear();
            cbMaNV.SelectedIndex = -1;
        }
        private void dtgKhachHang_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow selectRow = dtgKhachHang.Rows[e.RowIndex];

                    string makh = selectRow.Cells["MaKhachHang"].Value.ToString();
                    string taikhoan = selectRow.Cells["TaiKhoan"].Value.ToString();
                    string matkhau = selectRow.Cells["MatKhau"].Value.ToString();
                    string sotien = selectRow.Cells["SoTien"].Value.ToString();
                    string manv = selectRow.Cells["MaNhanVien"].Value.ToString();

                    txtMaKhachHang.Text = makh;
                    txtTaiKhoan.Text = taikhoan;
                    txtMatkhau.Text = matkhau;
                    txtSoTien.Text = sotien;
                    cbMaNV.Text = manv;
                }
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            Reset();
        }
        private string GenerateNewMaKhachHang()
        {
            using (var context = new QUANLYQUANNETContext())
            {
                var existingMaKhachHang = context.KhachHang
                    .Select(kh => int.Parse(kh.MaKhachHang.Substring(2)))
                    .ToList();

                existingMaKhachHang.Sort();

                int newMaKhachHangNumber = 1; // Bắt đầu từ 1

                // Tìm số nhỏ nhất còn thiếu
                for (int i = 0; i < existingMaKhachHang.Count; i++)
                {
                    if (existingMaKhachHang[i] != newMaKhachHangNumber)
                    {
                        break;
                    }
                    newMaKhachHangNumber++;
                }
                return "KH" + newMaKhachHangNumber.ToString("D3");
            }
        }


        private void btnThem_Click(object sender, EventArgs e)
        {
            using (var context = new QUANLYQUANNETContext())
            {
                int sotien;
                string pattern = @"^[a-zA-Z0-9]+$"; // Chỉ cho phép chữ cái và số

                // Kiểm tra các trường bắt buộc
                if (string.IsNullOrWhiteSpace(txtTaiKhoan.Text) ||
                    !Regex.IsMatch(txtTaiKhoan.Text, pattern) || // Kiểm tra ký tự đặc biệt trong tài khoản
                    string.IsNullOrWhiteSpace(txtMatkhau.Text) ||
                    txtMatkhau.Text.Length < 9 || // Kiểm tra mật khẩu không dưới 9 ký tự
                    !int.TryParse(txtSoTien.Text, out sotien) ||
                    sotien <= 0) // Kiểm tra số tiền phải lớn hơn 0
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ và đúng định dạng thông tin. " +
                                    "Tài khoản không được chứa ký tự đặc biệt. " +
                                    "Mật khẩu phải có ít nhất 9 ký tự. Số tiền phải lớn hơn 0.");
                    return;
                }

                // Tạo mã khách hàng mới
                string newCustomerID = GenerateNewMaKhachHang();
                // Tạo đối tượng khách hàng mới
                KhachHang newKH = new KhachHang
                {
                    MaKhachHang = newCustomerID,
                    TaiKhoan = txtTaiKhoan.Text,
                    MatKhau = txtMatkhau.Text,
                    SoTien = sotien,
                    MaNhanVien = cbMaNV.SelectedValue.ToString()
                };

                try
                {
                    context.KhachHang.Add(newKH);
                    context.SaveChanges();
                    MessageBox.Show("Thêm thành công");
                    LoadData();
                    Reset();
                }
                catch (Exception)
                {
                    MessageBox.Show("Lỗi");
                }
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dtgKhachHang.SelectedRows.Count > 0)
            {
                int selectedRowIndex = dtgKhachHang.SelectedRows[0].Index;
                string MaSelected = dtgKhachHang.Rows[selectedRowIndex].Cells["MaKhachHang"].Value.ToString();

                using (var context = new QUANLYQUANNETContext())
                {
                    int sotien;
                    KhachHang SuaKH = context.KhachHang.FirstOrDefault(c => c.MaKhachHang == MaSelected);
                    if (SuaKH == null)
                    {
                        MessageBox.Show("Mã khách hàng không tồn tại");
                        return;
                    }

                    // Kiểm tra các trường thông tin bắt buộc
                    if (
                        string.IsNullOrWhiteSpace(txtMatkhau.Text) ||
                        string.IsNullOrWhiteSpace(txtTaiKhoan.Text) ||
                        string.IsNullOrWhiteSpace(txtSoTien.Text) ||
                        !int.TryParse(txtSoTien.Text, out sotien)
                        )

                    {
                        MessageBox.Show("Vui lòng nhập đầy đủ và đúng định dạng thông tin.");
                        return;
                    }
                    // Cập nhật thông tin sản phẩm
                    SuaKH.TaiKhoan = txtTaiKhoan.Text;
                    SuaKH.MatKhau = txtMatkhau.Text;
                    SuaKH.SoTien = int.Parse(txtSoTien.Text);
                    SuaKH.MaNhanVien = cbMaNV.SelectedValue.ToString();

                    context.SaveChanges();
                    MessageBox.Show("Cập nhật thành công");

                    Reset();
                    LoadData();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần cập nhật");
            }
        }
        private void btnXoa_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn chắc chắn muốn xóa?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                if (dtgKhachHang.SelectedRows.Count > 0)
                {
                    string id = dtgKhachHang.SelectedRows[0].Cells["MaKhachHang"].Value.ToString();

                    using (var context = new QUANLYQUANNETContext())
                    {
                        KhachHang DeleteKH = context.KhachHang.FirstOrDefault(c => c.MaKhachHang == id);

                        if (DeleteKH != null)
                        {
                            try
                            {
                                context.KhachHang.Remove(DeleteKH);
                                context.SaveChanges();
                                MessageBox.Show("Xóa thành công");
                                LoadData();
                                Reset();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Không thể xóa khách hàng này vì còn liên kết với dữ liệu khác nhân viên. Chi tiết lỗi: " + ex.Message);
                            }
                        }
                    }
                }
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            using (var context = new QUANLYQUANNETContext())
            {
                string CTimKiem = txtTimKiem.Text.Trim();

                var query = from kh in context.KhachHang
                            join nv in context.NhanVien on kh.MaNhanVien equals nv.MaNhanVien
                            where kh.MaKhachHang.Contains(CTimKiem) ||
                                  kh.TaiKhoan.Contains(CTimKiem) ||
                                  kh.MatKhau.Contains(CTimKiem) ||
                                  kh.SoTien.ToString().Contains(CTimKiem) ||
                                  kh.MaNhanVien.Contains(CTimKiem)
                            select new
                            {
                                kh.MaKhachHang,
                                kh.TaiKhoan,
                                kh.MatKhau,
                                kh.SoTien,
                                nv.MaNhanVien,
                            };

                dtgKhachHang.DataSource = query.ToList();
                Reset();
            }
        }
    }
}



