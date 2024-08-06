using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UIDuAn1.Models;

namespace UIDuAn1
{
    public partial class UC_ThucDon : UserControl
    {
        private string currentUserRole;
        public UC_ThucDon(string userRole)
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
                            btnThem.Enabled = true;
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

                var query = from td in context.ThucDon
                            join NhanVien in context.NhanVien on td.MaNhanVien
                            equals NhanVien.MaNhanVien

                            select new
                            {
                                td.MaMonAn,
                                td.TenMonAn,
                                td.SoLuong,
                                td.Gia,
                                td.TinhTrang,
                                td.HinhAnh,
                                NhanVien.MaNhanVien,
                                NhanVien.HoTen
                            };

                dtgThucDon.DataSource = query.ToList();

                int count = context.ThucDon.Count();
                string newMaThucDon = $"MA{(count + 1).ToString("D3")}";
                txtMaMonAn.Text = newMaThucDon;

                dtgThucDon.Columns[0].HeaderText = "Mã Thực Đơn";
                dtgThucDon.Columns[1].HeaderText = "Tên Thực Đơn";
                dtgThucDon.Columns[2].HeaderText = "Số lượng";
                dtgThucDon.Columns[3].HeaderText = "Giá";
                dtgThucDon.Columns[4].HeaderText = "Tình Trạng";
                dtgThucDon.Columns[5].HeaderText = "Hình Ảnh";
                dtgThucDon.Columns[6].HeaderText = "Mã Nhân Viên";
                dtgThucDon.Columns[7].HeaderText = "Tên Nhân Viên";

            }
        }
        private byte[] GetImageFromFile(string filePath)
        {
            using (var ms = new MemoryStream())
            {
                using (var img = Image.FromFile(filePath))
                {
                    img.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    return ms.ToArray();
                }
            }
        }
        private void reset()
        {
            txtTenMonAn.Clear();
            txtTimKiem.Clear();
            txtGiaMonAn.Clear();
            txtMaMonAn.Clear();
            txtSoluongMon.Clear();
            cbMaNV.SelectedIndex = -1;
            pcChenAnh.Image = null;
            rdoConMonAn.Checked = false;
            rdoHetMonAn.Checked = false;
        }

        private string GenerateNewMaMonAn()
        {
            using (var context = new QUANLYQUANNETContext())
            {
                var existingMaMonAn = context.ThucDon
                    .Select(td => int.Parse(td.MaMonAn.Substring(2)))
                    .ToList();

                existingMaMonAn.Sort();

                int newMaMonAnNumber = 1; // Bắt đầu từ 1

                // Tìm số nhỏ nhất còn thiếu
                for (int i = 0; i < existingMaMonAn.Count; i++)
                {
                    if (existingMaMonAn[i] != newMaMonAnNumber)
                    {
                        break;
                    }
                    newMaMonAnNumber++;
                }

                return "MA" + newMaMonAnNumber.ToString("D3");
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            using (var context = new QUANLYQUANNETContext())
            {
                int sl, gia;

                // Kiểm tra các trường bắt buộc
                if (string.IsNullOrWhiteSpace(txtTenMonAn.Text) ||
                    string.IsNullOrWhiteSpace(txtSoluongMon.Text) ||
                    string.IsNullOrWhiteSpace(txtGiaMonAn.Text) ||
                    !int.TryParse(txtSoluongMon.Text, out sl) ||
                    !int.TryParse(txtGiaMonAn.Text, out gia) ||
                     gia <= 0 || gia > 922337203685477.5807m)
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ và đúng định dạng thông tin.");
                    return;
                }

                if (!rdoConMonAn.Checked && !rdoHetMonAn.Checked)
                {
                    MessageBox.Show("Vui lòng chọn tình trạng.");
                    return;
                }

                

                // Tạo mã món ăn mới
                string newCustomerID = GenerateNewMaMonAn();

                bool tinhtrang = rdoConMonAn.Checked;
                // Tạo đối tượng món ăn mới
                ThucDon newSP = new ThucDon
                {
                    MaMonAn = newCustomerID,
                    TenMonAn = txtTenMonAn.Text,
                    SoLuong = int.Parse(txtSoluongMon.Text),
                    Gia = int.Parse(txtGiaMonAn.Text),
                    TinhTrang = tinhtrang,
                    MaNhanVien = cbMaNV.SelectedValue.ToString()
                };

                // Lưu trữ ảnh
                if (pcChenAnh.Image != null)
                {
                    newSP.HinhAnh = GetImageFromFile(pcChenAnh.ImageLocation);
                }
                else
                {
                    newSP.HinhAnh = null;
                }

                try
                {
                    context.ThucDon.Add(newSP);
                    context.SaveChanges();
                    MessageBox.Show("Thêm thành công");
                    LoadData();
                    reset();
                }
                catch (Exception)
                {
                    MessageBox.Show("Lỗi");
                }
            }
        }


        private void UC_ThucDon_Load(object sender, EventArgs e)
        {
            LoadData();
            DataGridViewImageColumn pic = new DataGridViewImageColumn();
            pic = (DataGridViewImageColumn)dtgThucDon.Columns[5];
            pic.ImageLayout = DataGridViewImageCellLayout.Zoom;

        }

        private void btnChenAnh_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Chọn ảnh";
            openFileDialog.Filter = "Image Files(*.gif;*.jpg;*.jpeg;*.bmp;*.wmf;*.png)|*.gif;*.jpg;*.jpeg;*.bmp;.wmf;*.png";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                pcChenAnh.ImageLocation = openFileDialog.FileName;
            }

        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            reset();
        }

        private void dtgThucDon_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == dtgThucDon.Columns["HinhAnh"].Index && e.Value != null)
            {
                byte[] imageData = (byte[])e.Value;
                using (var ms = new MemoryStream(imageData))
                {
                    e.Value = Image.FromStream(ms);
                    e.FormattingApplied = true;
                }
            }

        }
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dtgThucDon.SelectedRows.Count > 0)
            {
                int selectedRowIndex = dtgThucDon.SelectedRows[0].Index;
                string MaSelected = dtgThucDon.Rows[selectedRowIndex].Cells["MaMonAn"].Value.ToString();

                using (var context = new QUANLYQUANNETContext())
                {
                    int sl, gia;
                    bool tinhtrang = rdoConMonAn.Checked;

                    ThucDon SuaTD = context.ThucDon.FirstOrDefault(c => c.MaMonAn == MaSelected);
                    if (SuaTD == null)
                    {
                        MessageBox.Show("Mã sản phẩm không tồn tại");
                        return;
                    }
                    // Kiểm tra các trường thông tin bắt buộc
                    if (
                        string.IsNullOrWhiteSpace(txtTenMonAn.Text) ||
                    string.IsNullOrWhiteSpace(txtSoluongMon.Text) ||
                    string.IsNullOrWhiteSpace(txtGiaMonAn.Text) ||
                    !int.TryParse(txtSoluongMon.Text, out sl) ||
                    !int.TryParse(txtGiaMonAn.Text, out gia)
                        )

                    {
                        MessageBox.Show("Vui lòng nhập đầy đủ và đúng định dạng thông tin.");
                        return;
                    }

                    // Cập nhật thông tin sản phẩm
                    SuaTD.TenMonAn = txtTenMonAn.Text;
                    SuaTD.SoLuong = int.Parse(txtSoluongMon.Text);
                    SuaTD.Gia = int.Parse(txtGiaMonAn.Text);
                    SuaTD.TinhTrang = tinhtrang;
                    SuaTD.MaNhanVien = cbMaNV.SelectedValue.ToString();

                    // Kiểm tra nếu người dùng đã chọn một hình ảnh mới
                    if (pcChenAnh.ImageLocation != null)
                    {
                        SuaTD.HinhAnh = GetImageFromFile(pcChenAnh.ImageLocation);
                    }

                    context.SaveChanges();
                    MessageBox.Show("Cập nhật thành công");

                    reset();
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
            DialogResult result = MessageBox.
                Show("Bạn chắc chắn muốn xóa ?", "Thông báo",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {

                if (dtgThucDon.SelectedRows.Count > 0)
                {
                    string id = dtgThucDon.SelectedRows[0].Cells["MaMonAn"].Value.ToString();

                    using (var context = new QUANLYQUANNETContext())
                    {
                        ThucDon DeleteTD = context.ThucDon.FirstOrDefault(c => c.MaMonAn == id);

                        if (DeleteTD != null)
                        {
                            try {
                            context.ThucDon.Remove(DeleteTD);
                            context.SaveChanges();
                            MessageBox.Show("Xóa thành công");
                            LoadData();
                            reset();
                            }
                            catch (Exception ex)
                            {
                            MessageBox.Show("Không thể xoá vì còn liên kết với dữ liệu khác (hoá đơn)");
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

                var query = from td in context.ThucDon
                            join NhanVien in context.NhanVien on td.MaNhanVien
                                                                                    equals NhanVien.MaNhanVien
                            where td.MaMonAn.Contains(CTimKiem) ||
                                                              td.TenMonAn.Contains(CTimKiem) ||
                                                              td.SoLuong.ToString().Contains(CTimKiem) ||
                                                              td.Gia.ToString().Contains(CTimKiem) ||
                                                              td.TinhTrang.ToString().Contains(CTimKiem) ||
                                                              NhanVien.MaNhanVien.Contains(CTimKiem) ||
                                                              NhanVien.HoTen.Contains(CTimKiem)
                            select new
                            {
                                td.MaMonAn,
                                td.TenMonAn,
                                td.SoLuong,
                                td.Gia,
                                td.TinhTrang,
                                td.HinhAnh,
                                NhanVien.MaNhanVien,
                                NhanVien.HoTen
                            };

                dtgThucDon.DataSource = query.ToList();
                reset();
            }
        }

        private void dtgThucDon_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectRow = dtgThucDon.Rows[e.RowIndex];

                string matd = selectRow.Cells["MaMonAn"].Value.ToString();
                string tentd = selectRow.Cells["TenMonAn"].Value.ToString();
                string soluong = selectRow.Cells["SoLuong"].Value.ToString();
                string gia = selectRow.Cells["Gia"].Value.ToString();
                bool tinhtrang = (bool)selectRow.Cells["TinhTrang"].Value;
                byte[] imageData = (byte[])selectRow.Cells["HinhAnh"].Value;
                cbMaNV.SelectedValue = selectRow.Cells["MaNhanVien"].Value.ToString();

                txtMaMonAn.Text = matd;
                txtTenMonAn.Text = tentd;
                txtSoluongMon.Text = soluong;
                txtGiaMonAn.Text = gia;
                if (tinhtrang)
                {
                    rdoConMonAn.Checked = true;
                }
                else
                {
                    rdoHetMonAn.Checked = true;
                }

                using (MemoryStream ms = new MemoryStream(imageData))
                {
                    pcChenAnh.Image = Image.FromStream(ms);
                }

            }
        }
    }
}

