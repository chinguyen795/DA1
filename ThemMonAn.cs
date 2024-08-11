using System;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Windows.Forms;
using UIDuAn1.Models;

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

                    // Tạo mã hóa đơn chi tiết mới
                    int hdctCount = context.HoaDonChiTiet.Count();
                    string newMaHDCT = $"HDCT{(hdctCount + 1).ToString("D3")}";

                    // Tạo mới chi tiết hóa đơn
                    HoaDonChiTiet newHDCT = new HoaDonChiTiet
                    {
                        MaHoaDonChiTiet = newMaHDCT,
                        MaHoaDon = cboMaHD.SelectedValue.ToString(),
                        MaMonAn = cboMonAn.SelectedValue.ToString(),
                        SoLuongMon = soLuongMon,
                        TriGia = triGia
                    };

                    // Thêm chi tiết hóa đơn vào cơ sở dữ liệu
                    context.HoaDonChiTiet.Add(newHDCT);
                    context.SaveChanges();
                    MessageBox.Show("Thêm thành công");

                    string maHoaDon = cboMaHD.SelectedValue.ToString();
                    LoadHDCTByMaHD(maHoaDon); // Tải lại dữ liệu sau khi thêm thành công

                    // Tìm món ăn trong thực đơn
                    var monAn = context.ThucDon.FirstOrDefault(ma => ma.MaMonAn == newHDCT.MaMonAn);

                    if (monAn != null)
                    {
                        // Trừ số lượng món ăn trong thực đơn
                        monAn.SoLuong -= soLuongMon;

                        if (monAn.SoLuong < 0)
                        {
                            MessageBox.Show("Số lượng món ăn không đủ.");
                            return;
                        }

                        // Lưu thay đổi vào cơ sở dữ liệu
                        context.SaveChanges();


                        CalculateTotalAllThePrice();
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
                        HoaDonChiTiet deleteHDCT = context.HoaDonChiTiet
                            .FirstOrDefault(hdct => hdct.MaHoaDonChiTiet == maHoaDonChiTiet);

                        if (deleteHDCT != null)
                        {
                            context.HoaDonChiTiet.Remove(deleteHDCT);
                            context.SaveChanges();
                            MessageBox.Show("Xóa thành công");
                            LoadData();
                            reset();
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