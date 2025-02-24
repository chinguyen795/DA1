using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UIDuAn1.Models;

namespace UIDuAn1
{
    public partial class UC_CongViec : UserControl
    {
        public bool IsBtnThemEnabled => btnThem.Enabled;
        public bool IsBtnSuaEnabled => btnSua.Enabled;
        public bool IsBtnXoaEnabled => btnXoa.Enabled;
        public bool IsBtnLamMoiEnabled => btnLamMoi.Enabled;
        private string currentUserRole;
        public UC_CongViec(string userRole)
        {
            currentUserRole = userRole;
            InitializeComponent();
            checkVaiTro();
            cbCaLam.Items.AddRange(new string[] {
                "Ca 1 ",
                "Ca 2 ",
                "Ca 3 ",
                "Ca 4 "
            });

            cbCaLam.SelectedIndexChanged += cbCaLam_SelectedIndexChanged;

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
        private void LoadData()
        {
            using (var context = new QUANLYQUANNETContext())
            {
                var nhanViens = context.NhanVien.Select(nv => new {
                    MaNv = nv.MaNhanVien,
                    TenNv = nv.HoTen,
                    DisplayText = $"{nv.MaNhanVien} | {nv.HoTen}"
                }).ToList();

                cbMaNV.DataSource = nhanViens;
                cbMaNV.DisplayMember = "DisplayText";
                cbMaNV.ValueMember = "MaNv";

                var query = from CL in context.CaLam
                            join NhanVien in context.NhanVien on CL.MaNhanVien equals NhanVien.MaNhanVien
                            select new
                            {
                                CL.MaCa,
                                CL.CaLam1,
                                CL.SoGioLam,
                                CL.ViPham,
                                CL.NgayLam,
                                NhanVien.HoTen,
                                
                            };

                dtgCongViec.DataSource = query.ToList();

                dtgCongViec.Columns[0].HeaderText = "Mã Ca";
                dtgCongViec.Columns[1].HeaderText = "Ca làm";
                dtgCongViec.Columns[2].HeaderText = "Số giờ làm";
                dtgCongViec.Columns[3].HeaderText = "Vi phạm";
                dtgCongViec.Columns[4].HeaderText = "Ngày đăng ký";
                dtgCongViec.Columns[5].HeaderText = "Tên nhân viên";
                

                int count = context.CaLam.Count();
                string newMaCa = $"CA{(count + 1).ToString("D3")}"; // Tạo mã ca làm mới dựa trên số lượng hiện tại

                // Hiển thị mã ca làm mới trên form
                txtMaCaLam.Text = newMaCa;
            }
            
        }

        private void ResetForm()
        {
             
            txtThoiGianLam.Clear();
            txtViPham.Clear();
            cbMaNV.SelectedIndex = -1; // Clear selection in ComboBox for employees
            cbCaLam.SelectedIndex = -1; // Clear selection in ComboBox for shifts

            using (var context = new QUANLYQUANNETContext())
            {
                int count = context.CaLam.Count();
                string newMaCa = $"CA{(count + 1).ToString("D3")}";
                txtMaCaLam.Text = newMaCa;
            }

        }

        private void cbCaLam_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cbCaLam.SelectedIndex)
            {
                case 0:
                    txtThoiGianLam.Text = "6 giờ đến 10 giờ sáng";
                    break;
                case 1:
                    txtThoiGianLam.Text = "10 giờ đến 14 giờ";
                    break;
                case 2:
                    txtThoiGianLam.Text = "14 giờ đến 18 giờ";
                    break;
                case 3:
                    txtThoiGianLam.Text = "18 giờ đến 22 giờ tối";
                    break;
                default:
                    txtThoiGianLam.Clear();
                    break;
            }
        }

        private bool ContainsLetter(string input)
        {
            return input.Any(char.IsLetter);
        }
        private void UC_CongViec_Load_1(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnThem_Click_1(object sender, EventArgs e)
        {
            using (var context = new QUANLYQUANNETContext())
            {
                // Kiểm tra các trường thông tin bắt buộc
                if (string.IsNullOrWhiteSpace(txtMaCaLam.Text) ||
                    cbCaLam.SelectedIndex == -1 ||
                    string.IsNullOrWhiteSpace(txtViPham.Text) ||
                    !ContainsLetter(txtViPham.Text)) // Kiểm tra ít nhất một chữ cái trong ViPham
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ và đúng định dạng thông tin.");
                    return;
                }

                // Kiểm tra giá trị SelectedValue của ComboBox
                if (cbMaNV.SelectedValue == null)
                {
                    MessageBox.Show("Vui lòng chọn nhân viên.");
                    return;
                }
                int customerCount = context.ThucDon.Count();
                string newCustomerID = $"CA{(customerCount + 1).ToString("D3")}";
                // Đặt SoGioLam luôn bằng 4
                int soGioLam = 4;

                // Tạo đối tượng CaLam mới
                CaLam newCaLam = new CaLam
                {
                    MaCa = txtMaCaLam.Text,
                    CaLam1 = cbCaLam.SelectedItem.ToString(),
                    SoGioLam = soGioLam,
                    ViPham = txtViPham.Text,
                    MaNhanVien = cbMaNV.SelectedValue.ToString(),
                    NgayLam = DateTime.Now,
                    
                };

                context.CaLam.Add(newCaLam);
                try
                {
                    context.SaveChanges();
                    MessageBox.Show("Thêm thành công");
                    LoadData();
                    ResetForm();
                }
                catch (DbUpdateException ex)
                {
                    // Hiển thị thông tin chi tiết của inner exception
                    MessageBox.Show($"An error occurred while updating the entries. See the inner exception for details.\n{ex.InnerException?.Message}");
                }
            }
        }

        private void btnSua_Click_1(object sender, EventArgs e)
        {
            if (dtgCongViec.SelectedRows.Count > 0)
            {
                string maCaSelected = dtgCongViec.SelectedRows[0].Cells["MaCa"].Value.ToString();

                using (var context = new QUANLYQUANNETContext())
                {
                    CaLam suaCaLam = context.CaLam.FirstOrDefault(c => c.MaCa == maCaSelected);
                    if (suaCaLam == null)
                    {
                        MessageBox.Show("Mã ca làm không tồn tại");
                        return;
                    }

                    // Kiểm tra các trường thông tin bắt buộc
                    if (cbCaLam.SelectedIndex == -1 ||
                        string.IsNullOrWhiteSpace(txtViPham.Text) ||
                        !ContainsLetter(txtViPham.Text)) // Kiểm tra ít nhất một chữ cái trong ViPham
                    {
                        MessageBox.Show("Vui lòng nhập đầy đủ và đúng định dạng thông tin.");
                        return;
                    }

                    // Đặt SoGioLam luôn bằng 4
                    int soGioLam = 4;

                    // Cập nhật thông tin ca làm
                    suaCaLam.CaLam1 = cbCaLam.SelectedItem.ToString();
                    suaCaLam.SoGioLam = soGioLam;
                    suaCaLam.ViPham = txtViPham.Text;
                    suaCaLam.MaNhanVien = cbMaNV.SelectedValue.ToString();
                    suaCaLam.NgayLam = DateTime.Now;

                    context.SaveChanges();
                    MessageBox.Show("Cập nhật thành công");
                    LoadData();
                    ResetForm();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn ca làm cần cập nhật");
            }
        }

        private void btnXoa_Click_1(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn chắc chắn muốn xóa?", "Thông báo",
        MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                if (dtgCongViec.SelectedRows.Count > 0)
                {
                    string maCa = dtgCongViec.SelectedRows[0].Cells["MaCa"].Value.ToString();

                    using (var context = new QUANLYQUANNETContext())
                    {
                        CaLam deleteCaLam = context.CaLam.FirstOrDefault(c => c.MaCa == maCa);

                        if (deleteCaLam != null)
                        {
                            context.CaLam.Remove(deleteCaLam);

                            // Cập nhật lại các khóa chính sau khi xóa
                            var subsequentRecords = context.CaLam
                                .Where(c => string.Compare(c.MaCa, maCa) > 0)
                                .OrderBy(c => c.MaCa)
                                .ToList();

                            foreach (var record in subsequentRecords)
                            {
                                // Lưu thông tin cũ
                                var oldRecord = new CaLam
                                {
                                    CaLam1 = record.CaLam1,
                                    SoGioLam = record.SoGioLam,
                                    ViPham = record.ViPham,
                                    MaNhanVien = record.MaNhanVien,
                                    NgayLam = record.NgayLam
                                };

                                // Xóa bản ghi cũ
                                context.CaLam.Remove(record);
                                context.SaveChanges();

                                // Tạo bản ghi mới với khóa chính mới
                                oldRecord.MaCa = "CA" + (int.Parse(record.MaCa.Substring(2)) - 1).ToString("D3");
                                context.CaLam.Add(oldRecord);
                            }

                            context.SaveChanges();
                            MessageBox.Show("Xóa thành công");
                            LoadData();
                            ResetForm();
                        }
                        else
                        {
                            MessageBox.Show("Mã ca làm không tồn tại.");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn ca làm cần xóa.");
                }
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
                string searchTerm = txtTimKiem.Text.Trim();

                var query = from ca in context.CaLam
                            join nhanVien in context.NhanVien on ca.MaNhanVien equals nhanVien.MaNhanVien
                            where ca.MaCa.Contains(searchTerm) ||
                                  ca.CaLam1.Contains(searchTerm) ||
                                  ca.SoGioLam.ToString().Contains(searchTerm) ||
                                  ca.ViPham.Contains(searchTerm) ||
                                  nhanVien.HoTen.Contains(searchTerm) ||
                                  nhanVien.MaNhanVien.Contains(searchTerm)
                            select new
                            {
                                ca.MaCa,
                                ca.CaLam1,
                                ca.SoGioLam,
                                ca.ViPham,
                                ca.NgayLam, // Thêm NgayLam vào kết quả
                                nhanVien.HoTen
                            };

                dtgCongViec.DataSource = query.ToList();
                ResetForm();
            }
        }

        private void dtgCongViec_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = dtgCongViec.Rows[e.RowIndex];

                string maCa = selectedRow.Cells["MaCa"].Value.ToString();
                string caLam = selectedRow.Cells["CaLam1"].Value.ToString();

                string viPham = selectedRow.Cells["ViPham"].Value.ToString();



                // Điền dữ liệu vào các điều khiển trên form
                txtMaCaLam.Text = maCa;
                cbCaLam.SelectedItem = caLam; // Nếu ComboBox đã được điền với các giá trị của CaLam

                txtViPham.Text = viPham;


            }
        }
    }
}
