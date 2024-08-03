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
    public partial class UC_MayTinh : UserControl
    {
        private string currentUserRole;
        public UC_MayTinh(string userRole)
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
                            btnThem.Enabled = false;
                            btnSua.Enabled = true;
                            btnXoa.Enabled = false;
                            btnLamMoi.Enabled = true;
                            break;
                        case "VT03": // Vai trò VT03
                            btnThem.Enabled = false;
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

        private void dtgMayTinh_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectRow = dtgMayTinh.Rows[e.RowIndex];

                string MaMay = selectRow.Cells["MaMay"].Value.ToString();
                string CPU = selectRow.Cells["CPU"].Value.ToString();
                string GPU = selectRow.Cells["GPU"].Value.ToString();
                string RAM = selectRow.Cells["RAM"].Value.ToString();
                string Giatien = selectRow.Cells["GiaTien"].Value.ToString();
                bool tinhTrang = (bool)selectRow.Cells["TinhTrang"].Value;
                string manv = selectRow.Cells["MaNhanVien"].Value.ToString();

                txtMaMayTinh.Text = MaMay;
                txtCPU.Text = CPU;
                txtGPU.Text = GPU;
                txtRAM.Text = RAM;
                txtGiaTien.Text = Giatien;
                if (tinhTrang)
                {
                    rdoHoatDong.Checked = true;
                }
                else
                {
                    rdoKhongHoatDong.Checked = true;
                }

                cbMaNV.Text = manv;

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

                var query = from MT in context.MayTinh
                            join NhanVien in context.NhanVien on MT.MaNhanVien
                            equals NhanVien.MaNhanVien

                            select new
                            {
                                MT.MaMay,
                                MT.Cpu,
                                MT.Gpu,
                                MT.Ram,
                                MT.GiaTien,
                                MT.TinhTrang,
                                NhanVien.HoTen,
                                NhanVien.MaNhanVien
                            };

                dtgMayTinh.DataSource = query.ToList();

                dtgMayTinh.Columns[0].HeaderText = "Mã Máy";
                dtgMayTinh.Columns[1].HeaderText = "CPU";
                dtgMayTinh.Columns[2].HeaderText = "GPU";
                dtgMayTinh.Columns[3].HeaderText = "RAM";
                dtgMayTinh.Columns[4].HeaderText = "Gía tiền";
                dtgMayTinh.Columns[5].HeaderText = "Tình trạng";
                dtgMayTinh.Columns[6].Visible = false;
                dtgMayTinh.Columns[7].HeaderText = "Mã nhân viên";

                int mayTinhCount = context.MayTinh.Count();
                string newMayTinhID = $"MT{(mayTinhCount + 1).ToString("D3")}";

                txtMaMayTinh.Text = newMayTinhID;

            }
        }
        private void reset()
        {
            txtTimKiem.Clear();
            txtCPU.Clear();
            txtGiaTien.Clear();
            txtGPU.Clear();
            txtRAM.Clear();
            rdoHoatDong.Checked = false;
            rdoKhongHoatDong.Checked = false;
            cbMaNV.SelectedIndex = -1;

            using (var context = new QUANLYQUANNETContext())
            {
                int count = context.CaLam.Count();
                string newMaCa = $"MT{(count + 1).ToString("D3")}";
                txtMaMayTinh.Text = newMaCa;
            }
        }



        private bool ContainsLetter(string input)
        {
            return input.Any(char.IsLetter);
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            using (var context = new QUANLYQUANNETContext())
            {
                int soLuong;
                decimal giaTien;

                // Kiểm tra các trường bắt buộc
                if (string.IsNullOrWhiteSpace(txtMaMayTinh.Text) ||
                    string.IsNullOrWhiteSpace(txtCPU.Text) ||
                    string.IsNullOrWhiteSpace(txtGPU.Text) ||
                    string.IsNullOrWhiteSpace(txtRAM.Text) ||
                    string.IsNullOrWhiteSpace(txtGiaTien.Text) ||
                    !ContainsLetter(txtCPU.Text) ||
                    !ContainsLetter(txtGPU.Text) ||
                    !ContainsLetter(txtRAM.Text) ||
                    !decimal.TryParse(txtGiaTien.Text, out giaTien) ||
                    giaTien <= 0)
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ và đúng định dạng thông tin.");
                    return;
                }

                // Tự động tạo mã máy tính mới
                int mayTinhCount = context.MayTinh.Count();
                string newMayTinhID = $"MT{(mayTinhCount + 1).ToString("D3")}";

                // Xác định trạng thái
                bool tinhTrang = rdoHoatDong.Checked;

                // Tạo đối tượng MayTinh mới
                MayTinh newMayTinh = new MayTinh
                {
                    MaMay = newMayTinhID,
                    Cpu = txtCPU.Text,
                    Gpu = txtGPU.Text,
                    Ram = txtRAM.Text,
                    GiaTien = giaTien,
                    TinhTrang = tinhTrang,
                    MaNhanVien = cbMaNV.SelectedValue.ToString()
                };

                try
                {
                    context.MayTinh.Add(newMayTinh);
                    context.SaveChanges();
                    MessageBox.Show("Thêm thành công");
                    LoadData();
                    reset();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}");
                }
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dtgMayTinh.SelectedRows.Count > 0)
            {
                int selectedRowIndex = dtgMayTinh.SelectedRows[0].Index;
                string maMaySelected = dtgMayTinh.Rows[selectedRowIndex].Cells["MaMay"].Value.ToString();

                using (var context = new QUANLYQUANNETContext())
                {
                    decimal giaTien;

                    MayTinh suaMayTinh = context.MayTinh.FirstOrDefault(c => c.MaMay == maMaySelected);
                    if (suaMayTinh == null)
                    {
                        MessageBox.Show("Mã máy tính không tồn tại");
                        return;
                    }
                    int soLuong;
                    // Kiểm tra các trường thông tin bắt buộc
                    if (string.IsNullOrWhiteSpace(txtMaMayTinh.Text) ||
                    string.IsNullOrWhiteSpace(txtCPU.Text) ||
                    string.IsNullOrWhiteSpace(txtGPU.Text) ||
                    string.IsNullOrWhiteSpace(txtRAM.Text) ||
                    string.IsNullOrWhiteSpace(txtGiaTien.Text) ||
                    !ContainsLetter(txtCPU.Text) ||
                    !ContainsLetter(txtGPU.Text) ||
                    !ContainsLetter(txtRAM.Text) ||
                    !decimal.TryParse(txtGiaTien.Text, out giaTien) ||
                    giaTien <= 0)
                    {
                        MessageBox.Show("Vui lòng nhập đầy đủ và đúng định dạng thông tin.");
                        return;
                    }

                    // Cập nhật thông tin máy tính
                    suaMayTinh.Cpu = txtCPU.Text;
                    suaMayTinh.Gpu = txtGPU.Text;
                    suaMayTinh.Ram = txtRAM.Text;
                    suaMayTinh.GiaTien = giaTien;
                    suaMayTinh.TinhTrang = rdoHoatDong.Checked;
                    suaMayTinh.MaNhanVien = cbMaNV.SelectedValue.ToString();

                    context.SaveChanges();
                    MessageBox.Show("Cập nhật thành công");

                    reset();
                    LoadData();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn máy tính cần cập nhật");
            }
        }


        private void btnXoa_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn chắc chắn muốn xóa?", "Thông báo",
            MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                if (dtgMayTinh.SelectedRows.Count > 0)
                {
                    string maMay = dtgMayTinh.SelectedRows[0].Cells["MaMay"].Value.ToString();

                    using (var context = new QUANLYQUANNETContext())
                    {
                        MayTinh deleteMayTinh = context.MayTinh.FirstOrDefault(c => c.MaMay == maMay);

                        if (deleteMayTinh != null)
                        {
                            context.MayTinh.Remove(deleteMayTinh);

                            // Cập nhật lại các khóa chính sau khi xóa
                            var subsequentRecords = context.MayTinh
                                .Where(c => string.Compare(c.MaMay, maMay) > 0)
                                .OrderBy(c => c.MaMay)
                                .ToList();

                            foreach (var record in subsequentRecords)
                            {
                                var oldRecord = new MayTinh
                                {
                                      // Nếu lớp MayTinh có thuộc tính TenMay
                                    Cpu = record.Cpu,
                                    Gpu = record.Gpu,
                                    Ram = record.Ram,
                                    GiaTien = record.GiaTien,
                                    TinhTrang = record.TinhTrang, // Nếu lớp MayTinh có thuộc tính TinhTrang
                                    MaNhanVien = record.MaNhanVien
                                };

                                // Xóa bản ghi cũ
                                context.MayTinh.Remove(record);
                                context.SaveChanges();

                                // Tạo bản ghi mới với khóa chính mới
                                oldRecord.MaMay = "MT" + (int.Parse(record.MaMay.Substring(2)) - 1).ToString("D3");
                                context.MayTinh.Add(oldRecord);
                            }

                            context.SaveChanges();
                            MessageBox.Show("Xóa thành công");
                            LoadData();
                            reset();
                        }
                        else
                        {
                            MessageBox.Show("Mã máy tính không tồn tại.");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn máy tính cần xóa.");
                }
                reset();
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            reset();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            using (var context = new QUANLYQUANNETContext())
            {
                string CTimKiem = txtTimKiem.Text.Trim();

                var query = from MT in context.MayTinh
                            join NhanVien in context.NhanVien on MT.MaNhanVien equals NhanVien.MaNhanVien
                            where MT.MaMay.Contains(CTimKiem) ||
                                  MT.Cpu.Contains(CTimKiem) ||
                                  MT.Gpu.Contains(CTimKiem) ||
                                  MT.Ram.Contains(CTimKiem) ||
                                  MT.GiaTien.ToString().Contains(CTimKiem) ||
                                  NhanVien.HoTen.Contains(CTimKiem) ||
                                  NhanVien.MaNhanVien.Contains(CTimKiem)
                            select new
                            {
                                MT.MaMay,
                                MT.Cpu,
                                MT.Gpu,
                                MT.Ram,
                                MT.GiaTien,
                                MT.TinhTrang,
                                NhanVien.HoTen,
                                NhanVien.MaNhanVien
                            };

                dtgMayTinh.DataSource = query.ToList();
                reset();
            }
        }
        private void UC_MayTinh_Load(object sender, EventArgs e)
        {
            LoadData();
            
        }
    }
}