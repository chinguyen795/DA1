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
    public partial class UC_HoaDon : UserControl
    {
        private string currentUserRole;
        public UC_HoaDon(string userRole)
        {
            currentUserRole = userRole;
            InitializeComponent();
            checkVaiTro();
            dtbNgayLap.Value = DateTime.Now;

            this.cboMaMonAn.SelectedIndexChanged += new System.EventHandler(this.cboMaMonAn_SelectedIndexChanged);
            this.txtSoLuongmonAn.TextChanged += new System.EventHandler(this.txtSoLuongmonAn_TextChanged);
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
        private void LoadComboboxData(QUANLYQUANNETContext db)
        {
            var nvQuery = from nv in db.NhanVien
                          select nv;

            cbMaNV.DataSource = nvQuery.ToList();
            cbMaNV.DisplayMember = "MaNhanVien";
            cbMaNV.ValueMember = "MaNhanVien";

            var khQuery = from kh in db.KhachHang
                          select kh;

            cbMaKhachHang.DataSource = khQuery.ToList();
            cbMaKhachHang.DisplayMember = "MaKhachHang";
            cbMaKhachHang.ValueMember = "MaKhachHang";

            var maQuery = from ma in db.ThucDon
                          select ma;

            cboMaMonAn.DataSource = maQuery.ToList();
            cboMaMonAn.DisplayMember = "MaMonAn";
            cboMaMonAn.ValueMember = "MaMonAn";
        }
        private void LoadData()
        {
            using (var context = new QUANLYQUANNETContext())
            {
                // Load data for comboboxes
                var nhanViens = context.NhanVien.Select(nv => new {
                    MaNV = nv.MaNhanVien,
                    DisplayText = $"{nv.MaNhanVien} | {nv.HoTen}"
                }).ToList();

                var khachHangs = context.KhachHang.Select(kh => new {
                    MaKH = kh.MaKhachHang,
                    DisplayText = $"{kh.MaKhachHang} | {kh.TaiKhoan}"
                }).ToList();

                var monAns = context.ThucDon.Select(ma => new {
                    MaMonAn = ma.MaMonAn,
                    TenMonAn = ma.TenMonAn
                }).ToList();

                cbMaNV.DataSource = nhanViens;
                cbMaNV.DisplayMember = "DisplayText";
                cbMaNV.ValueMember = "MaNV";

                cbMaKhachHang.DataSource = khachHangs;
                cbMaKhachHang.DisplayMember = "DisplayText";
                cbMaKhachHang.ValueMember = "MaKH";

                cboMaMonAn.DataSource = monAns;
                cboMaMonAn.DisplayMember = "TenMonAn";
                cboMaMonAn.ValueMember = "MaMonAn";

                var query = from hd in context.HoaDon
                            join nv in context.NhanVien on hd.MaNhanVien equals nv.MaNhanVien
                            join hdc in context.HoaDonChiTiet on hd.MaHoaDon equals hdc.MaHoaDon
                            join kh in context.KhachHang on hdc.MaKhachHang equals kh.MaKhachHang
                            join td in context.ThucDon on hdc.MaMonAn equals td.MaMonAn
                            select new
                            {
                                hd.MaHoaDon,
                                td.TenMonAn,
                                kh.TaiKhoan,
                                hdc.SoLuongMon,
                                hd.TriGia,
                                nv.HoTen,
                                hd.NgayLap,
                                td.MaNhanVien,
                                kh.MaKhachHang,
                            };

                dtgHoaDon.DataSource = query.ToList();

                int count = context.HoaDon.Count();
                string newMaHoaDon = $"HD{(count + 1).ToString("D3")}";
                txtMaHoaDon.Text = newMaHoaDon;

                dtgHoaDon.Columns[0].HeaderText = "Mã Hóa Đơn";
                dtgHoaDon.Columns[1].HeaderText = "Tên Món Ăn";
                dtgHoaDon.Columns[2].HeaderText = "Khách hàng";
                dtgHoaDon.Columns[3].HeaderText = "Số Lượng Món";
                dtgHoaDon.Columns[4].HeaderText = "Trị Giá";
                dtgHoaDon.Columns[5].HeaderText = "Nhân Viên";
                dtgHoaDon.Columns[6].HeaderText = "Ngày Lập";
                dtgHoaDon.Columns[7].Visible = true;
                dtgHoaDon.Columns[8].Visible = true;

            }
        }
        private void dtgHoaDon_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }
        private void ResetForm()
        {
            
            dtbNgayLap.Value = DateTime.Now;
            txtTriGia.Clear();
            cbMaNV.SelectedIndex = -1;
            cbMaKhachHang.SelectedIndex = -1;
            cboMaMonAn.SelectedIndex = -1;
            txtSoLuongmonAn.Clear();

            using (var context = new QUANLYQUANNETContext())
            {
                int count = context.HoaDon.Count();
                string newMaHoaDon = $"HD{(count + 1).ToString("D3")}";
                txtMaHoaDon.Text = newMaHoaDon;
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            using (var context = new QUANLYQUANNETContext())
            {
                if (string.IsNullOrWhiteSpace(txtTriGia.Text) ||
                    cbMaNV.SelectedIndex == -1 ||
                    cbMaKhachHang.SelectedIndex == -1 ||
                    cboMaMonAn.SelectedIndex == -1 ||
                    string.IsNullOrWhiteSpace(txtSoLuongmonAn.Text))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin.");
                    return;
                }

                // Tự động tạo mã hóa đơn mới
                int count = context.HoaDon.Count();
                string newMaHoaDon = $"HD{(count + 1).ToString("D3")}";

                HoaDon newHoaDon = new HoaDon
                {
                    MaHoaDon = newMaHoaDon,
                    NgayLap = dtbNgayLap.Value,
                    TriGia = decimal.Parse(txtTriGia.Text),
                    MaNhanVien = cbMaNV.SelectedValue.ToString()
                };

                context.HoaDon.Add(newHoaDon);

                HoaDonChiTiet newHoaDonChiTiet = new HoaDonChiTiet
                {
                    MaHoaDon = newMaHoaDon,
                    MaMonAn = cboMaMonAn.SelectedValue.ToString(),
                    SoLuongMon = int.Parse(txtSoLuongmonAn.Text),
                    MaKhachHang = cbMaKhachHang.SelectedValue.ToString()
                };

                context.HoaDonChiTiet.Add(newHoaDonChiTiet);

                context.SaveChanges();
                MessageBox.Show("Thêm thành công");
                LoadData();
                ResetForm();
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dtgHoaDon.SelectedRows.Count > 0)
            {
                string maHoaDonSelected = dtgHoaDon.SelectedRows[0].Cells["MaHoaDon"].Value.ToString();

                using (var context = new QUANLYQUANNETContext())
                {
                    HoaDon suaHoaDon = context.HoaDon.FirstOrDefault(hd => hd.MaHoaDon == maHoaDonSelected);
                    if (suaHoaDon == null)
                    {
                        MessageBox.Show("Mã hóa đơn không tồn tại");
                        return;
                    }

                    if (string.IsNullOrWhiteSpace(txtTriGia.Text) ||
                        cbMaNV.SelectedIndex == -1 ||
                        cbMaKhachHang.SelectedIndex == -1 ||
                        cboMaMonAn.SelectedIndex == -1 ||
                        string.IsNullOrWhiteSpace(txtSoLuongmonAn.Text))
                    {
                        MessageBox.Show("Vui lòng nhập đầy đủ thông tin.");
                        return;
                    }

                    suaHoaDon.NgayLap = dtbNgayLap.Value;
                    suaHoaDon.TriGia = decimal.Parse(txtTriGia.Text);
                    suaHoaDon.MaNhanVien = cbMaNV.SelectedValue.ToString();

                    var suaHoaDonChiTiet = context.HoaDonChiTiet.FirstOrDefault(hdc => hdc.MaHoaDon == maHoaDonSelected);
                    if (suaHoaDonChiTiet != null)
                    {
                        suaHoaDonChiTiet.MaMonAn = cboMaMonAn.SelectedValue.ToString();
                        suaHoaDonChiTiet.SoLuongMon = int.Parse(txtSoLuongmonAn.Text);
                        suaHoaDonChiTiet.MaKhachHang = cbMaKhachHang.SelectedValue.ToString();
                    }

                    context.SaveChanges();
                    MessageBox.Show("Cập nhật thành công");
                    LoadData();
                    ResetForm();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn hóa đơn cần cập nhật");
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn chắc chắn muốn xóa?", "Thông báo",
    MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                if (dtgHoaDon.SelectedRows.Count > 0)
                {
                    string maHoaDon = dtgHoaDon.SelectedRows[0].Cells["MaHoaDon"].Value.ToString();

                    using (var context = new QUANLYQUANNETContext())
                    {
                        HoaDon deleteHoaDon = context.HoaDon.FirstOrDefault(hd => hd.MaHoaDon == maHoaDon);

                        if (deleteHoaDon != null)
                        {
                            var deleteHoaDonChiTiet = context.HoaDonChiTiet.Where(hdc => hdc.MaHoaDon == maHoaDon).ToList();
                            context.HoaDonChiTiet.RemoveRange(deleteHoaDonChiTiet);

                            context.HoaDon.Remove(deleteHoaDon);

                            // Cập nhật lại các khóa chính sau khi xóa
                            var subsequentRecords = context.HoaDon
                                .Where(hd => string.Compare(hd.MaHoaDon, maHoaDon) > 0)
                                .OrderBy(hd => hd.MaHoaDon)
                                .ToList();

                            foreach (var record in subsequentRecords)
                            {
                                var oldRecord = new HoaDon
                                {
                                    NgayLap = record.NgayLap,
                                    TriGia = record.TriGia,
                                    MaNhanVien = record.MaNhanVien
                                };

                                var oldRecordChiTiet = context.HoaDonChiTiet
                                    .Where(hdc => hdc.MaHoaDon == record.MaHoaDon)
                                    .ToList()
                                    .Select(hdc => new HoaDonChiTiet
                                    {
                                        MaMonAn = hdc.MaMonAn,
                                        SoLuongMon = hdc.SoLuongMon,
                                        MaKhachHang = hdc.MaKhachHang
                                    })
                                    .ToList();

                                // Xóa bản ghi cũ
                                context.HoaDonChiTiet.RemoveRange(context.HoaDonChiTiet.Where(hdc => hdc.MaHoaDon == record.MaHoaDon));
                                context.HoaDon.Remove(record);
                                context.SaveChanges();

                                // Tạo bản ghi mới với khóa chính mới
                                oldRecord.MaHoaDon = "HD" + (int.Parse(record.MaHoaDon.Substring(2)) - 1).ToString("D3");
                                context.HoaDon.Add(oldRecord);
                                context.SaveChanges();

                                foreach (var chiTiet in oldRecordChiTiet)
                                {
                                    chiTiet.MaHoaDon = oldRecord.MaHoaDon;
                                    context.HoaDonChiTiet.Add(chiTiet);
                                }

                                context.SaveChanges();
                            }

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
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn hóa đơn cần xóa.");
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
                string timKiem = txtTimKiem.Text.Trim();

                var query = from hd in context.HoaDon
                            join nv in context.NhanVien on hd.MaNhanVien equals nv.MaNhanVien
                            join hdc in context.HoaDonChiTiet on hd.MaHoaDon equals hdc.MaHoaDon
                            join kh in context.KhachHang on hdc.MaKhachHang equals kh.MaKhachHang
                            where hd.MaHoaDon.Contains(timKiem) ||
                                  hd.NgayLap.ToString().Contains(timKiem) ||
                                  hd.TriGia.ToString().Contains(timKiem) ||
                                  nv.HoTen.Contains(timKiem) ||
                                  kh.TaiKhoan.Contains(timKiem)
                            select new
                            {
                                hd.MaHoaDon,
                                hd.NgayLap,
                                hd.TriGia,
                                nv.HoTen,
                                kh.TaiKhoan
                            };

                dtgHoaDon.DataSource = query.ToList();
                ResetForm();
            }
        }

        private void dtgHoaDon_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Lấy hàng được chọn
                var selectedRow = dtgHoaDon.Rows[e.RowIndex];

                // Lấy dữ liệu từ hàng được chọn
                string maHoaDon = selectedRow.Cells["MaHoaDon"].Value.ToString();
                DateTime ngayLap = Convert.ToDateTime(selectedRow.Cells["NgayLap"].Value);
                decimal triGia = Convert.ToDecimal(selectedRow.Cells["TriGia"].Value);
                string tenNhanVien = selectedRow.Cells["HoTen"].Value.ToString();
                string taiKhoanKhachHang = selectedRow.Cells["TaiKhoan"].Value.ToString();

                // Điền dữ liệu vào các trường trên form
                txtMaHoaDon.Text = maHoaDon;
                dtbNgayLap.Value = ngayLap;
                txtTriGia.Text = triGia.ToString();

                // Chọn giá trị trong các ComboBox
                cbMaNV.SelectedValue = selectedRow.Cells["MaNhanVien"].Value.ToString();
                cbMaKhachHang.SelectedValue = selectedRow.Cells["MaKhachHang"].Value.ToString();

                // Lấy chi tiết của hóa đơn đã chọn
                using (var context = new QUANLYQUANNETContext())
                {
                    var hoaDonChiTiet = context.HoaDonChiTiet.FirstOrDefault(hdc => hdc.MaHoaDon == maHoaDon);
                    if (hoaDonChiTiet != null)
                    {
                        cboMaMonAn.SelectedValue = hoaDonChiTiet.MaMonAn;
                        txtSoLuongmonAn.Text = hoaDonChiTiet.SoLuongMon.ToString();
                    }
                }
                if (cboMaMonAn.SelectedIndex != -1)
                {
                    string maMonAn = cboMaMonAn.SelectedValue.ToString();

                    using (var context = new QUANLYQUANNETContext())
                    {
                        var monAn = context.ThucDon.FirstOrDefault(ma => ma.MaMonAn == maMonAn);
                        if (monAn != null)
                        {
                            giaMonAnHienTai = monAn.Gia;
                            TinhTriGiaHoaDon();
                        }
                    }
                }
            }
        }

        private void UC_HoaDon_Load(object sender, EventArgs e)
        {
            LoadData();
            TinhTriGiaHoaDon();
            
        }
        private decimal giaMonAnHienTai = 0;
        private void cboMaMonAn_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboMaMonAn.SelectedIndex != -1)
            {
                string maMonAn = cboMaMonAn.SelectedValue.ToString();

                using (var context = new QUANLYQUANNETContext())
                {
                    var monAn = context.ThucDon.FirstOrDefault(ma => ma.MaMonAn == maMonAn);
                    if (monAn != null)
                    {
                        giaMonAnHienTai = monAn.Gia;
                        TinhTriGiaHoaDon();
                    }
                }
            }
        }
        private void TinhTriGiaHoaDon()
        {
            if (int.TryParse(txtSoLuongmonAn.Text, out int soLuong) && giaMonAnHienTai > 0)
            {
                decimal triGia = giaMonAnHienTai * soLuong;
                txtTriGia.Text = triGia.ToString();
            }
            else
            {
                txtTriGia.Clear();
            }
        }

        private void txtSoLuongmonAn_TextChanged(object sender, EventArgs e)
        {
            TinhTriGiaHoaDon();
        }
    }
}
