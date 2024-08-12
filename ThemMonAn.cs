using System;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Windows.Forms;
using UIDuAn1.Models;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;

namespace UIDuAn1
{
    public partial class ThemMonAn : Form
    {
        public ThemMonAn()
        {
            InitializeComponent();
        }

        private void LoadData()
        {
            using (var context = new QUANLYQUANNETContext())
            {
                var monAnList = context.ThucDon.Select(ma => new
                {
                    MaMonAn = ma.MaMonAn,
                    TenMonAn = ma.TenMonAn,
                    DisplayText = $"{ma.MaMonAn} | {ma.TenMonAn}"
                }).ToList();

                cboMonAn.DataSource = monAnList;
                cboMonAn.DisplayMember = "DisplayText";
                cboMonAn.ValueMember = "MaMonAn";

                var hoaDonList = context.HoaDon.Select(hd => new
                {
                    MaHoaDon = hd.MaHoaDon,
                    DisplayText = $"{hd.MaHoaDon}"
                }).ToList();

                cboMaHD.DataSource = hoaDonList;
                cboMaHD.DisplayMember = "DisplayText";
                cboMaHD.ValueMember = "MaHoaDon";

                var query = from hdct in context.HoaDonChiTiet
                            join td in context.ThucDon on hdct.MaMonAn equals td.MaMonAn
                            orderby hdct.MaHoaDonChiTiet
                            select new
                            {
                                hdct.MaHoaDon,
                                hdct.MaMonAn,
                                td.TenMonAn,
                                hdct.SoLuongMon,
                                hdct.TriGia,
                                hdct.MaHoaDonChiTiet,
                            };

                dtgHDCT.DataSource = query.ToList();

                dtgHDCT.Columns[0].HeaderText = "Mã hóa đơn";
                dtgHDCT.Columns[1].Visible = false;
                dtgHDCT.Columns[2].HeaderText = "Món ăn";
                dtgHDCT.Columns[3].HeaderText = "Số lượng món";
                dtgHDCT.Columns[4].HeaderText = "Số tiền";
                dtgHDCT.Columns[5].Visible = false;
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void reset()
        {
            txtSoLuong.Clear();
            txtTongGia.Clear();
            cboMaHD.SelectedIndex = -1;
            cboMonAn.SelectedIndex = -1;
            txtTongTien.Clear();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            using (var context = new QUANLYQUANNETContext())
            {
                try
                {
                    CalculateTotalPrice(); // Tính tổng giá trước khi thêm

                    // Kiểm tra nếu chưa chọn món ăn hoặc mã hóa đơn
                    if (cboMonAn.SelectedIndex == -1 || cboMaHD.SelectedIndex == -1)
                    {
                        MessageBox.Show("Vui lòng chọn món ăn và mã hóa đơn.");
                        return;
                    }

                    // Kiểm tra đầu vào của số lượng món và tổng giá
                    if (!int.TryParse(txtSoLuong.Text, out int soLuongMon) || soLuongMon <= 0 ||
                        !decimal.TryParse(txtTongGia.Text, out decimal triGia) || triGia <= 0)
                    {
                        MessageBox.Show("Vui lòng nhập đầy đủ và đúng định dạng thông tin.");
                        return;
                    }

                    // Kiểm tra nếu mã hóa đơn đã đủ chi tiết chưa
                    string maHoaDon = cboMaHD.SelectedValue.ToString();
                    int existingCount = context.HoaDonChiTiet.Count(hdct => hdct.MaHoaDon == maHoaDon);

                    if (existingCount >= 10) // Ví dụ: Giới hạn 10 món cho mỗi hóa đơn
                    {
                        MessageBox.Show("Hóa đơn đã đủ món ăn, không thể thêm món mới.");
                        return;
                    }

                    // Tìm món ăn trong thực đơn
                    var monAn = context.ThucDon.FirstOrDefault(ma => ma.MaMonAn == cboMonAn.SelectedValue.ToString());

                    if (monAn != null)
                    {
                        // Kiểm tra số lượng món ăn còn đủ không
                        if (monAn.SoLuong < soLuongMon)
                        {
                            MessageBox.Show("Số lượng món ăn không đủ.");
                            return; // Thoát khỏi phương thức nếu số lượng không đủ
                        }

                        // Tạo mã hóa đơn chi tiết mới
                        var maxMaHDCT = context.HoaDonChiTiet
                            .OrderByDescending(hdct => hdct.MaHoaDonChiTiet)
                            .Select(hdct => hdct.MaHoaDonChiTiet)
                            .FirstOrDefault();


                        int newIndex = 1;
                        // Tạo mã hóa đơn chi tiết mới
                        if (maxMaHDCT != null)
                        {
                            // Tách số thứ tự từ mã HDCT hiện tại và tăng lên 1
                            newIndex = int.Parse(maxMaHDCT.Substring(4)) + 1;
                        }

                        string newMaHDCT = $"HDCT{newIndex.ToString("D3")}";

                        // Tạo mới chi tiết hóa đơn
                        HoaDonChiTiet newHDCT = new HoaDonChiTiet
                        {
                            MaHoaDonChiTiet = newMaHDCT,
                            MaHoaDon = maHoaDon,
                            MaMonAn = cboMonAn.SelectedValue.ToString(),
                            SoLuongMon = soLuongMon,
                            TriGia = triGia
                        };

                        // Thêm chi tiết hóa đơn vào cơ sở dữ liệu
                        context.HoaDonChiTiet.Add(newHDCT);
                        context.SaveChanges();
                        MessageBox.Show("Thêm thành công");

                        // Trừ số lượng món ăn trong thực đơn
                        monAn.SoLuong -= soLuongMon;
                        context.SaveChanges();

                        LoadHDCTByMaHD(maHoaDon); // Tải lại dữ liệu sau khi thêm thành công
                        CalculateTotalAllThePrice(); // Cập nhật tổng tiền
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy món ăn trong thực đơn.");
                    }
                }
                catch (Exception ex)
                {
                    // Hiển thị lỗi nếu có
                    MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}\n\n{ex.InnerException?.Message}");
                }
            }
        }



        private void btnXoa_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn chắc chắn muốn xóa?", "Thông báo",
     MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                if (dtgHDCT.SelectedRows.Count > 0)
                {
                    string maHoaDonChiTiet = dtgHDCT.SelectedRows[0].Cells["MaHoaDonChiTiet"].Value.ToString();

                    using (var context = new QUANLYQUANNETContext())
                    {
                        // Tìm chi tiết hóa đơn cần xóa
                        HoaDonChiTiet deleteHDCT = context.HoaDonChiTiet
                            .FirstOrDefault(hdct => hdct.MaHoaDonChiTiet == maHoaDonChiTiet);

                        if (deleteHDCT != null)
                        {
                            // Tìm món ăn trong thực đơn
                            var monAn = context.ThucDon.FirstOrDefault(ma => ma.MaMonAn == deleteHDCT.MaMonAn);

                            if (monAn != null)
                            {
                                // Cộng lại số lượng món ăn trong thực đơn
                                monAn.SoLuong += deleteHDCT.SoLuongMon;
                            }

                            // Xóa chi tiết hóa đơn
                            string maHoaDon = cboMaHD.SelectedValue.ToString();
                            context.HoaDonChiTiet.Remove(deleteHDCT);
                            context.SaveChanges();
                            MessageBox.Show("Xóa thành công");
                            LoadHDCTByMaHD(maHoaDon);
                            CalculateTotalAllThePrice();
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy chi tiết hóa đơn.");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn chi tiết hóa đơn cần xóa.");
                }
            }
            
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            reset();
        }

        private void ThemMonAn_Load(object sender, EventArgs e)
        {
            LoadData();
            if (cboMaHD.Items.Count > 0)
            {
                cboMaHD.SelectedIndex = 0;
                string maHoaDon = cboMaHD.SelectedValue.ToString();
                LoadHDCTByMaHD(maHoaDon);
                CalculateTotalAllThePrice(); // Cập nhật tổng tiền cho mã hóa đơn được chọn
            }
            else
            {
                dtgHDCT.DataSource = null; // Xóa dữ liệu khi không có mã hóa đơn
                txtTongTien.Clear(); // Xóa tổng tiền
            }
        }

        private void CalculateTotalPrice()
        {
            if (cboMonAn.SelectedIndex == -1 || string.IsNullOrWhiteSpace(txtSoLuong.Text))
            {
                txtTongGia.Clear();
                return;
            }

            if (int.TryParse(txtSoLuong.Text, out int soLuongMon) && soLuongMon > 0)
            {
                using (var context = new QUANLYQUANNETContext())
                {
                    string maMonAn = cboMonAn.SelectedValue.ToString();
                    var monAn = context.ThucDon.FirstOrDefault(ma => ma.MaMonAn == maMonAn);

                    if (monAn != null)
                    {
                        decimal triGia = soLuongMon * monAn.Gia;
                        txtTongGia.Text = triGia.ToString("F2");
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy món ăn trong cơ sở dữ liệu.");
                        txtTongGia.Clear();
                    }
                }
            }
            else
            {
                txtTongGia.Clear();
            }
        }

        private void CalculateTotalAllThePrice()
        {
            if (cboMaHD.SelectedIndex == -1)
            {
                txtTongTien.Clear();
                return;
            }

            using (var context = new QUANLYQUANNETContext())
            {
                string maHoaDon = cboMaHD.SelectedValue.ToString();

                var tongTien = context.HoaDonChiTiet
                                      .Where(hdct => hdct.MaHoaDon == maHoaDon)
                                      .Sum(hdct => hdct.TriGia);

                txtTongTien.Text = tongTien.ToString("F2");
                txtTongTien.Refresh(); // Đảm bảo rằng TextBox được làm mới và hiển thị giá trị
            }
        }

        private void LoadHDCTByMaHD(string maHoaDon)
        {
            using (var context = new QUANLYQUANNETContext())
            {
                var query = from hdct in context.HoaDonChiTiet
                            join td in context.ThucDon on hdct.MaMonAn equals td.MaMonAn
                            where hdct.MaHoaDon == maHoaDon
                            select new
                            {
                                hdct.MaHoaDon,
                                hdct.MaMonAn,
                                td.TenMonAn,
                                hdct.SoLuongMon,
                                hdct.TriGia,
                                hdct.MaHoaDonChiTiet,
                            };

                dtgHDCT.DataSource = query.ToList();

                dtgHDCT.Columns[0].HeaderText = "Mã hóa đơn";
                dtgHDCT.Columns[1].Visible = false;
                dtgHDCT.Columns[2].HeaderText = "Món ăn";
                dtgHDCT.Columns[3].HeaderText = "Số lượng món";
                dtgHDCT.Columns[4].HeaderText = "Số tiền";
                dtgHDCT.Columns[5].Visible = false;
            }
        }

        private void dtgHDCT_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = dtgHDCT.Rows[e.RowIndex];

                string maHoaDon = selectedRow.Cells["MaHoaDon"].Value.ToString();
                string maMonAn = selectedRow.Cells["MaMonAn"].Value.ToString();
                string tenMonAn = selectedRow.Cells["TenMonAn"].Value.ToString();
                int soLuongMon = int.Parse(selectedRow.Cells["SoLuongMon"].Value.ToString());
                decimal triGia = decimal.Parse(selectedRow.Cells["TriGia"].Value.ToString());

                cboMaHD.SelectedValue = maHoaDon;
                cboMonAn.SelectedValue = maMonAn;
                txtSoLuong.Text = soLuongMon.ToString();
                txtTongGia.Text = triGia.ToString("F2");
            }
        }

        private void txtSoLuong_TextChanged(object sender, EventArgs e)
        {
            CalculateTotalPrice();
        }

        private void cboMonAn_SelectedIndexChanged(object sender, EventArgs e)
        {
            CalculateTotalPrice();
        }

        private void cboMaHD_SelectedIndexChanged(object sender, EventArgs e)
        {
            CalculateTotalAllThePrice();

            if (cboMaHD.SelectedIndex != -1)
            {
                string maHoaDon = cboMaHD.SelectedValue.ToString();
                LoadHDCTByMaHD(maHoaDon);
                CalculateTotalAllThePrice(); // Cập nhật tổng tiền cho mã hóa đơn được chọn
            }
            else
            {
                dtgHDCT.DataSource = null; // Xóa dữ liệu khi không có mã hóa đơn được chọn
                txtTongTien.Clear(); // Xóa tổng tiền
            }
        }
    }
}