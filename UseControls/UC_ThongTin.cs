using MimeKit;
using MailKit.Net.Smtp;
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

namespace UIDuAn1
{
    public partial class UC_ThongTin : UserControl
    {
        private string currentUserRole;
        public UC_ThongTin(string userRole)
        {
            currentUserRole = userRole;
            InitializeComponent();
            checkVaiTro();
            LoadCombo();
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
                            btnThem.Enabled = false;
                            btnSua.Enabled = false;
                            btnXoa.Enabled = false;
                            btnLamMoi.Enabled = false;
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
        void LoadCombo()
        {
            cbVaiTro.Items.Add("Admin");
            cbVaiTro.Items.Add("Nhân Viên");
            cbVaiTro.Items.Add("Trưởng Ca");
        }
        private void SendEmail(string toEmail, string subject, string body)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("nguyenncpc09256", "nguyenchinguyen7925@gmail.com"));
                message.To.Add(new MailboxAddress("", toEmail));
                message.Subject = subject;
                message.Body = new TextPart("html")
                {
                    Text = body
                };

                using (var client = new SmtpClient())
                {
                    client.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                    client.Authenticate("nguyenchinguyen7925@gmail.com", "zkfe bzvm qkam uklk"); // Thay bằng mật khẩu ứng dụng
                    client.Send(message);
                    client.Disconnect(true);
                }

                MessageBox.Show("Mật khẩu đã được gửi đến email của nhân viên vừa tạo");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }
        private string GenerateRandomPassword()
        {
            // Logic để tạo mật khẩu ngẫu nhiên, ví dụ:
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new Random();
            string password = new string(Enumerable.Repeat(chars, 8) // Độ dài mật khẩu là 8
                .Select(s => s[random.Next(s.Length)]).ToArray());
            return password;
        }

            private void LoadData()
        {
            using (var context = new QUANLYQUANNETContext())
            {
                var query = from nv in context.NhanVien
                            select new
                            {
                                nv.MaNhanVien,
                                nv.HoTen,
                                nv.Gmail,
                                nv.DiaChi,
                                nv.TenVaiTro,
                                nv.TrangThai
                            };

                dtgThongTinNV.DataSource = query.ToList();

                dtgThongTinNV.Columns[0].HeaderText = "MaNhanVien";
                dtgThongTinNV.Columns[1].HeaderText = "HoTen";
                dtgThongTinNV.Columns[2].HeaderText = "Email";
                dtgThongTinNV.Columns[3].HeaderText = "DiaChi";
                dtgThongTinNV.Columns[4].HeaderText = "VaiTro";
                dtgThongTinNV.Columns[5].HeaderText = "TrangThai";
            }
        }



        private void UC_ThongTin_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        private void Reset()
        {
            txtHoVaTen.Clear();
            txtEmail.Clear();
            txtMaNV.Clear();
            txtDiaChi.Clear();
            txtTimKiem.Clear();
            rdoHoatDong.Checked = false;
            rdoKhongHoatDong.Checked = false;
            cbVaiTro.Items.Clear();
        }
        private QUANLYQUANNETContext db;

        private void dtgThongTinNV_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectRow = dtgThongTinNV.Rows[e.RowIndex];

                string manv = selectRow.Cells["MaNhanVien"].Value.ToString();
                string hoten = selectRow.Cells["HoTen"].Value.ToString();
                string gmail = selectRow.Cells["Gmail"].Value.ToString();
                string diachi = selectRow.Cells["DiaChi"].Value.ToString();
                string tenvaitro = selectRow.Cells["TenVaiTro"].Value.ToString();
                string trangthai = selectRow.Cells["TrangThai"].Value.ToString();

                txtMaNV.Text = manv;
                txtEmail.Text = gmail;
                txtDiaChi.Text = diachi;
                txtHoVaTen.Text = hoten;

                if (trangthai == "1")
                {
                    rdoHoatDong.Checked = true;
                }
                else if (trangthai == "2")
                {
                    rdoKhongHoatDong.Checked = true;
                }
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            Reset();
        }
        private string GenerateNewEmployeeID(QUANLYQUANNETContext context)
        {
            // Truy xuất tất cả các `mNV` từ cơ sở dữ liệu
            var allIds = context.NhanVien
                                .Select(nv => nv.MaNhanVien)
                                .ToList();

            // Thực hiện xử lý trên phía client
            var maxID = allIds
                        .Select(id => Regex.Match(id, @"\d+").Value)
                        .Where(x => !string.IsNullOrEmpty(x))
                        .Select(int.Parse)
                        .DefaultIfEmpty(0).Max();
            return $"NV{(maxID + 1).ToString("D3")}";
        }


        private void btnThem_Click(object sender, EventArgs e)
        {
            using (var context = new QUANLYQUANNETContext())
            {
                // Tạo mã nhân viên mới
                string newCustomerID = GenerateNewEmployeeID(context);
                string email = txtEmail.Text;

                // Kiểm tra các trường bắt buộc
                string tennv = txtHoVaTen.Text;
                string diachi = txtDiaChi.Text;
                if (string.IsNullOrWhiteSpace(tennv) ||
                    string.IsNullOrWhiteSpace(diachi) ||
                    string.IsNullOrWhiteSpace(email))
                {
                    MessageBox.Show("Không để trống thông tin.");
                    return;
                }

                // Kiểm tra kí tự đặc biệt trong tên nhân viên
                if (!Regex.IsMatch(tennv, "^[a-zA-ZÀ-ỹ ]+$"))
                {
                    MessageBox.Show("Tên nhân viên không được chứa ký tự đặc biệt.");
                    return;
                }

                // Kiểm tra rdo
                if (!rdoHoatDong.Checked && !rdoKhongHoatDong.Checked)
                {
                    MessageBox.Show("Vui lòng chọn tình trạng.");
                    return;
                }
                bool tinhtrang = rdoHoatDong.Checked;
                // Tạo đối tượng nhân viên mới
                NhanVien newNV = new NhanVien
                {
                    MaNhanVien = newCustomerID,
                    HoTen = txtHoVaTen.Text,
                    Gmail = txtEmail.Text,
                    DiaChi = txtDiaChi.Text,
                    TrangThai = tinhtrang,
                    TenVaiTro = cbVaiTro.Text,
                    MatKhau = GenerateRandomPassword()
                };

                try
                {

                    SendEmail(newNV.Gmail, "Mật khẩu mới", $"Mật khẩu mới của bạn là: {newNV.MatKhau}");
                    // Thêm nhân viên vào cơ sở dữ liệu
                    context.NhanVien.Add(newNV);
                    context.SaveChanges();

                    // Thêm nhân viên mới vào DataGridView
                    var newEmployee = new
                    {
                        newNV.MaNhanVien,
                        newNV.HoTen,
                        newNV.Gmail,
                        newNV.DiaChi,
                        newNV.TenVaiTro,
                        newNV.TrangThai
                    };
                    dtgThongTinNV.Rows.Add(newEmployee);
                    Reset();

                    LoadData();
                }
                catch (Exception)
                {
                    LoadData();
                }
            }

        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dtgThongTinNV.SelectedRows.Count > 0)
            {
                int selectedRowIndex = dtgThongTinNV.SelectedRows[0].Index;
                string MaSelected = dtgThongTinNV.Rows[selectedRowIndex].Cells["MaNhanVien"].Value.ToString();

                using (var context = new QUANLYQUANNETContext())
                {
                    string tennv = txtHoVaTen.Text;
                    NhanVien SuaNV = context.NhanVien.FirstOrDefault(c => c.MaNhanVien == MaSelected);
                    if (SuaNV == null)
                    {
                        MessageBox.Show("Mã nhân viên không tồn tại");
                        return;
                    }

                    // Kiểm tra các trường thông tin bắt buộc
                    if (string.IsNullOrWhiteSpace(txtHoVaTen.Text) ||
                        string.IsNullOrWhiteSpace(txtDiaChi.Text) ||
                        string.IsNullOrWhiteSpace(txtEmail.Text) ||
                        !Regex.IsMatch(tennv, "^[a-zA-ZÀ-ỹ ]+$"))
                    {
                        MessageBox.Show("Vui lòng nhập đầy đủ và đúng định dạng thông tin.");
                        return;
                    }
                    bool tinhtrang = rdoHoatDong.Checked;

                    if (SuaNV != null)
                    {
                        SuaNV.Gmail = txtEmail.Text;
                        SuaNV.HoTen = txtHoVaTen.Text;
                        SuaNV.DiaChi = txtDiaChi.Text;
                        SuaNV.TrangThai = tinhtrang;
                        SuaNV.TenVaiTro = cbVaiTro.Text;
                        context.SaveChanges();
                        MessageBox.Show("Cập nhật thành công");
                        LoadData();
                        Reset();
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn nhân viên cần cập nhật");
            }

        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            {
                DialogResult result = MessageBox.Show("Bạn chắc chắn muốn xóa?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    if (dtgThongTinNV.SelectedRows.Count > 0)
                    {
                        string id = dtgThongTinNV.SelectedRows[0].Cells["MaNhanVien"].Value.ToString();

                        using (var context = new QUANLYQUANNETContext())
                        {
                            NhanVien DeleteNV = context.NhanVien.FirstOrDefault(c => c.MaNhanVien == id);

                            if (DeleteNV != null)
                            {
                                try
                                {
                                    context.NhanVien.Remove(DeleteNV);
                                    context.SaveChanges();
                                    MessageBox.Show("Xóa thành công");
                                    LoadData();
                                    Reset();
                                }
                                catch (Exception)
                                {
                                    MessageBox.Show("Không thể xóa nhân viên này vì còn liên kết với dữ liệu khác (sản phẩm, khách hàng)");
                                }
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

                // Lấy dữ liệu từ cơ sở dữ liệu
                var query = from nv in context.NhanVien
                            select new
                            {
                                nv.MaNhanVien,
                                nv.HoTen,
                                nv.Gmail,
                                nv.DiaChi,
                                nv.TenVaiTro,
                                nv.TrangThai
                            };

                // Thực hiện tìm kiếm trên phía client
                var filteredResult = query.ToList().Where(nv =>
                    nv.MaNhanVien.Contains(CTimKiem) ||
                    nv.HoTen.Contains(CTimKiem) ||
                    nv.Gmail.Contains(CTimKiem) ||
                    nv.DiaChi.Contains(CTimKiem) ||
                    nv.TenVaiTro.ToString().Contains(CTimKiem) ||
                    nv.TrangThai.ToString().Contains(CTimKiem));

                dtgThongTinNV.DataSource = filteredResult.ToList();
                Reset();
            }

        }
    }

}
    




