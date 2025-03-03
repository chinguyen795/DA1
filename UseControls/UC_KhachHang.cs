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
using System.Text.RegularExpressions;
using UIDuAn1.Models;

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
                                NhanVien.HoTen,
                                NhanVien.MaNhanVien
                            };
                int count = context.KhachHang.Count();
                string newKH = $"KH{(count + 1).ToString("D3")}";
                txtMaKhachHang.Text = newKH;
                dtgKhachHang.DataSource = query.ToList();

                dtgKhachHang.Columns[0].HeaderText = "Mã Khách Hàng";
                dtgKhachHang.Columns[1].HeaderText = "Tài Khoản";
                dtgKhachHang.Columns[2].HeaderText = "Mật Khẩu";
                dtgKhachHang.Columns[3].HeaderText = "Số Tiền";
                dtgKhachHang.Columns[4].HeaderText = "Tên Nhân Viên";
                dtgKhachHang.Columns[5].Visible = false;
            }
        }

        private void UC_KhachHang_Load(object sender, EventArgs e)
        {
            LoadData();
            dtgKhachHang.CellFormatting += dtgKhachHang_CellFormatting;
        }

        private void Reset()
        {
            txtMaKhachHang.Clear();
            txtTaiKhoan.Clear();
            txtMatkhau.Clear();
            txtSoTien.Clear();
            cbMaNV.SelectedIndex = -1;
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
                decimal sotien;
                // Kiểm tra các trường bắt buộc
                if (string.IsNullOrWhiteSpace(txtTaiKhoan.Text) ||
                    string.IsNullOrWhiteSpace(txtMatkhau.Text) ||
                    txtMatkhau.Text.Length < 9 || // Kiểm tra mật khẩu không dưới 9 ký tự
                    !decimal.TryParse(txtSoTien.Text, out sotien) || // Kiểm tra số tiền là số hợp lệ
                    sotien <= 0) // Kiểm tra số tiền phải lớn hơn 0
                {
                    string message = "Vui lòng nhập đầy đủ và đúng định dạng thông tin. Mật khẩu phải có ít nhất 9 ký tự.Số tiền phải là số và lớn hơn 0";
                    MessageBox.Show(message);
                    Console.WriteLine(message);
                    return;
                }

                // Kiểm tra xem tài khoản đã tồn tại chưa
                bool accountExists = context.KhachHang.Any(kh => kh.TaiKhoan == txtTaiKhoan.Text);
                if (accountExists)
                {
                    string message = "Tài khoản đã tồn tại.Vui lòng chọn tài khoản khác!";
                    MessageBox.Show(message);
                    Console.WriteLine(message);
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
                    string message = "Thêm thành công";
                    MessageBox.Show(message);
                    Console.WriteLine(message);
                    LoadData();
                    Reset();
                }
                catch (Exception ex)
                {
                    // Hiển thị thông tin lỗi cụ thể hơn
                    MessageBox.Show("Lỗi: " + ex.Message);
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
                    decimal sotien;
                    // Tìm khách hàng theo mã khách hàng đã chọn
                    KhachHang SuaKH = context.KhachHang.FirstOrDefault(c => c.MaKhachHang == MaSelected);
                    if (SuaKH == null)
                    {
                        MessageBox.Show("Mã khách hàng không tồn tại.");
                        return;
                    }

                    // Kiểm tra các trường thông tin bắt buộc
                    if (string.IsNullOrWhiteSpace(txtTaiKhoan.Text) ||
                        string.IsNullOrWhiteSpace(txtMatkhau.Text) ||
                        string.IsNullOrWhiteSpace(txtSoTien.Text) ||
                        cbMaNV.SelectedIndex == -1 ||
                        !decimal.TryParse(txtSoTien.Text, out sotien) ||
                        sotien <= 0) // Kiểm tra số tiền phải lớn hơn 0
                    {
                        string message = "Vui lòng nhập đầy đủ và đúng định dạng thông tin. Mật khẩu phải có ít nhất 9 ký tự.Số tiền phải là số và lớn hơn 0";
                        MessageBox.Show(message);
                        Console.WriteLine(message);
                        return;
                    }

                    // Kiểm tra tài khoản mới có bị trùng lặp không
                    bool accountExists = context.KhachHang
                        .Any(kh => kh.TaiKhoan == txtTaiKhoan.Text && kh.MaKhachHang != MaSelected);
                    if (accountExists)
                    {
                        string message = "Tài khoản đã tồn tại. Vui lòng chọn tài khoản khác";
                        MessageBox.Show(message);
                        Console.WriteLine(message);
                        return;
                    }

                    // Cập nhật thông tin khách hàng
                    SuaKH.TaiKhoan = txtTaiKhoan.Text;
                    SuaKH.MatKhau = txtMatkhau.Text;
                    SuaKH.SoTien = sotien;
                    SuaKH.MaNhanVien = cbMaNV.SelectedValue.ToString(); // Dùng SelectedValue thay vì Text

                    try
                    {
                        // Lưu thay đổi vào cơ sở dữ liệu
                        context.SaveChanges();
                        string message = "Cập nhật thành công";
                        MessageBox.Show(message);
                        Console.WriteLine(message);

                        // Làm mới dữ liệu trên form và reset các điều khiển
                        LoadData();
                        Reset();
                    }
                    catch (Exception ex)
                    {
                        // Hiển thị thông tin lỗi cụ thể
                        MessageBox.Show("Lỗi khi cập nhật: " + ex.Message);
                    }
                }
            }
            else
            {
                string message = "Vui lòng chọn khách hàng cần cập nhật";
                MessageBox.Show(message);
                Console.WriteLine(message);
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
                                string message = "Xóa thành công";
                                MessageBox.Show(message);
                                Console.WriteLine(message);
                                LoadData();
                                Reset();
                            }
                            catch (Exception ex)
                            {
                                string message = "Không thể xóa khách hàng này vì còn liên kết với dữ liệu bảng nhân viên";
                                MessageBox.Show(message);
                                Console.WriteLine(message);
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
                string message = "Tìm thành công";
                MessageBox.Show(message);
                Console.WriteLine(message);
                Reset();
            }
        }

        private void dtgKhachHang_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {

        }

        private void dtgKhachHang_CellMouseDoubleClick_1(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Lấy hàng được chọn
                var selectedRow = dtgKhachHang.Rows[e.RowIndex];

                // Lấy dữ liệu từ hàng được chọn
                string makhachhang = selectedRow.Cells["MaKhachHang"].Value.ToString();
                string taikhoan = selectedRow.Cells["TaiKhoan"].Value.ToString();
                string matkhau = selectedRow.Cells["MatKhau"].Value.ToString();
                decimal sotien = Convert.ToDecimal(selectedRow.Cells["SoTien"].Value);

                // Điền dữ liệu vào các trường trên form
                txtMaKhachHang.Text = makhachhang;
                txtTaiKhoan.Text = taikhoan;
                txtMatkhau.Text = matkhau;
                txtSoTien.Text = sotien.ToString();

                // Chọn giá trị trong các ComboBox
                cbMaNV.SelectedValue = selectedRow.Cells["MaNhanVien"].Value.ToString();
            }
        }

        private void dtgKhachHang_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dtgKhachHang_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectRow = dtgKhachHang.Rows[e.RowIndex];

                string MaKhachHang = selectRow.Cells["MaKhachHang"].Value.ToString();
                string TaiKhoan = selectRow.Cells["TaiKhoan"].Value.ToString();
                string MatKhau = selectRow.Cells["MatKhau"].Value.ToString();
                string SoTien = selectRow.Cells["SoTien"].Value.ToString();
                cbMaNV.SelectedValue = selectRow.Cells["MaNhanVien"].Value.ToString();

                txtMaKhachHang.Text = MaKhachHang;
                txtTaiKhoan.Text = TaiKhoan;
                txtMatkhau.Text = MatKhau;
                txtSoTien.Text = SoTien;

            }
        }
    }

}



