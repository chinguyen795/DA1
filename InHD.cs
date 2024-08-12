using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UIDuAn1.Models;

namespace UIDuAn1
{
    public partial class InHD : Form
    {
        public InHD()
        {

            InitializeComponent();
            cboMaHD.SelectedIndexChanged += cboMaHD_SelectedIndexChanged; // Add event handler for selection change
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LoadData()
        {
            using (var context = new QUANLYQUANNETContext())
            {
                var hoadons = context.HoaDon.Select(hoadon => new
                {
                    mahd = hoadon.MaHoaDon,
                }).ToList();

                cboMaHD.DataSource = hoadons;
                cboMaHD.DisplayMember = "mahd";
                cboMaHD.ValueMember = "mahd";
            }
        }

        private void LoadData2(string selectedMaHD)
        {
            using (var context = new QUANLYQUANNETContext())
            {
                var query1 = from hd in context.HoaDon
                             join kh in context.KhachHang on hd.MaKhachHang equals kh.MaKhachHang
                             join nv in context.NhanVien on hd.MaNhanVien equals nv.MaNhanVien
                             where hd.MaHoaDon == selectedMaHD
                             select new
                             {
                                 hd.MaHoaDon,
                                 kh.TaiKhoan,
                                 nv.HoTen
                             };

                dtgHD.DataSource = query1.ToList();

                dtgHD.Columns[0].HeaderText = "Mã hóa đơn";
                dtgHD.Columns[1].HeaderText = "Khách hàng";
                dtgHD.Columns[2].HeaderText = "Tên nhân viên";


                var query2 = from hdct in context.HoaDonChiTiet
                             join td in context.ThucDon on hdct.MaMonAn equals td.MaMonAn
                             where hdct.MaHoaDon == selectedMaHD
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
                dtgHDCT.DataSource = query2.ToList();

                dtgHDCT.Columns[0].HeaderText = "Mã hóa đơn";
                dtgHDCT.Columns[1].Visible = false;
                dtgHDCT.Columns[2].HeaderText = "Món ăn";
                dtgHDCT.Columns[3].HeaderText = "Số lượng món";
                dtgHDCT.Columns[4].HeaderText = "Số tiền";
                dtgHDCT.Columns[5].Visible = false;

            }
        }

        private void cboMaHD_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboMaHD.SelectedValue != null)
            {
                string selectedMaHD = cboMaHD.SelectedValue.ToString();
                LoadData2(selectedMaHD);
                CalculateTotalAllThePrice();

            }
        }

        private void InHD_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void dtgHDCT_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void dtgHD_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }
        private void CalculateTotalAllThePrice()
        {
            if (cboMaHD.SelectedIndex == -1)
            {
                lblTongTien.Refresh();
                return;
            }

            using (var context = new QUANLYQUANNETContext())
            {
                string maHoaDon = cboMaHD.SelectedValue.ToString();

                var tongTien = context.HoaDonChiTiet
                                      .Where(hdct => hdct.MaHoaDon == maHoaDon)
                                      .Sum(hdct => hdct.TriGia);

                lblTongTien.Text = $"Tổng tiền: {tongTien.ToString("F3")} VND";
                lblTongTien.Refresh(); // Đảm bảo rằng TextBox được làm mới và hiển thị giá trị
            }
        }
        private void lblTongTien_Click(object sender, EventArgs e)
        {
        }
        private string ConvertDataGridViewToString(DataGridView dataGridView)
        {
            StringBuilder sb = new StringBuilder();

            // Adjust the width for each column
            int col1Width = 20;
            int col2Width = 30;
            int col3Width = 20;
            int col4Width = 20;

            // Add header row
            foreach (DataGridViewColumn column in dataGridView.Columns)
            {
                if (column.Name != "MaHoaDonChiTiet" && column.Name != "MaMonAn") // Skip unnecessary columns
                {
                    sb.Append(column.HeaderText.PadRight(col1Width)); // Adjust the column width if needed
                }
            }
            sb.AppendLine();

            // Add separator line
            sb.AppendLine(new string('-', col1Width + col2Width + col3Width + col4Width)); // Adjust to match the total column width

            // Add data rows
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                if (!row.IsNewRow)
                {
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        if (cell.OwningColumn.Name != "MaHoaDonChiTiet" && cell.OwningColumn.Name != "MaMonAn") // Skip unnecessary columns
                        {
                            string cellValue = cell.Value?.ToString() ?? string.Empty;
                            if (cell.OwningColumn.HeaderText == "Số tiền") // Align numbers to the right
                            {
                                sb.Append(cellValue.PadLeft(col4Width));
                            }
                            else
                            {
                                sb.Append(cellValue.PadRight(col1Width)); // Adjust the column width if needed
                            }
                        }
                    }
                    sb.AppendLine();
                }
            }

            return sb.ToString();
        }

        private void btnInHoaDon_Click(object sender, EventArgs e)
        {
            // Lấy mã hóa đơn được chọn
            string maHoaDon = cboMaHD.SelectedValue?.ToString();

            // Đảm bảo rằng đã chọn một hóa đơn
            if (string.IsNullOrEmpty(maHoaDon))
            {
                MessageBox.Show("Vui lòng chọn một hóa đơn để in.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var context = new QUANLYQUANNETContext())
            {
                // Lấy thông tin chi tiết hóa đơn từ cơ sở dữ liệu
                var invoice = context.HoaDon
                                     .Where(hd => hd.MaHoaDon == maHoaDon)
                                     .Join(context.KhachHang, hd => hd.MaKhachHang, kh => kh.MaKhachHang, (hd, kh) => new { hd, kh })
                                     .Join(context.NhanVien, combined => combined.hd.MaNhanVien, nv => nv.MaNhanVien, (combined, nv) => new
                                     {
                                         combined.hd.MaHoaDon,
                                         combined.hd.NgayLap,
                                         KhachHang = combined.kh.TaiKhoan,
                                         NhanVien = nv.HoTen,
                                         TriGia = lblTongTien.Text // Tổng tiền đã được tính trước đó
                                     })
                                     .FirstOrDefault();

                // Đảm bảo rằng đã tìm thấy dữ liệu hóa đơn
                if (invoice == null)
                {
                    MessageBox.Show("Không tìm thấy hóa đơn.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Thiết lập font chữ và căn chỉnh
                Font font = new Font("Arial", 12);
                float lineHeight = font.GetHeight();

                // Tạo tài liệu in
                PrintDocument printDocument = new PrintDocument();
                printDocument.PrintPage += (s, ev) =>
                {
                    float yLineTop = ev.MarginBounds.Top;

                    // In logo
                    Image logo;
                    try
                    {
                        logo = Image.FromFile("D:\\FPT Polytechnic\\DA1_logo\\z5724904645891_664da714e9649c2b3039fce1b5b1c396.jpg");
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Không thể tải logo. Kiểm tra lại đường dẫn.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    float imgWidth = logo.Width * 0.25f;
                    float imgHeight = logo.Height * 0.25f;
                    float imgX = (ev.MarginBounds.Left + ev.MarginBounds.Right) / 2 - imgWidth / 2;
                    float imgY = yLineTop;
                    ev.Graphics.DrawImage(logo, imgX, imgY, imgWidth, imgHeight);
                    yLineTop += imgHeight + 20;

                    // In tiêu đề
                    string title = "HÓA ĐƠN";
                    Font titleFont = new Font("Arial", 18, FontStyle.Bold);
                    SizeF titleSize = ev.Graphics.MeasureString(title, titleFont);
                    float titleX = (ev.MarginBounds.Left + ev.MarginBounds.Right) / 2 - titleSize.Width / 2;
                    ev.Graphics.DrawString(title, titleFont, Brushes.Black, titleX, yLineTop);
                    yLineTop += titleSize.Height * 2;

                    // In thông tin hóa đơn
                    ev.Graphics.DrawString($"Mã Hóa Đơn: {invoice.MaHoaDon}", font, Brushes.Black, ev.MarginBounds.Left, yLineTop);
                    yLineTop += lineHeight;
                    ev.Graphics.DrawString($"Ngày Lập: {invoice.NgayLap.ToShortDateString()} {invoice.NgayLap.ToShortTimeString()}", font, Brushes.Black, ev.MarginBounds.Left, yLineTop);
                    yLineTop += lineHeight;
                    ev.Graphics.DrawString($"Khách Hàng: {invoice.KhachHang}", font, Brushes.Black, ev.MarginBounds.Left, yLineTop);
                    yLineTop += lineHeight;
                    ev.Graphics.DrawString($"Nhân Viên: {invoice.NhanVien}", font, Brushes.Black, ev.MarginBounds.Left, yLineTop);
                    yLineTop += lineHeight;

                    // In nội dung DataGridView
                    string dgvHDCTContent = ConvertDataGridViewToString(dtgHDCT);

                    // In nội dung DataGridView
                    ev.Graphics.DrawString("Chi Tiết Hóa Đơn", font, Brushes.Black, ev.MarginBounds.Left, yLineTop);
                    yLineTop += lineHeight; // Tăng yLineTop sau tiêu đề

                    // In dữ liệu DataGridView
                    ev.Graphics.DrawString(dgvHDCTContent, font, Brushes.Black, ev.MarginBounds.Left, yLineTop);
                    yLineTop += lineHeight * dtgHDCT.RowCount + 5; // Tăng yLineTop theo số lượng hàng trong DataGridView

                    // Vẽ đường gạch ngang
                    float lineY = yLineTop + lineHeight; // Vị trí y cho đường gạch ngang

                    // Cập nhật yLineTop để in tổng tiền ở vị trí dưới đường gạch ngang
                   yLineTop = lineY + lineHeight * 7; // Thay đổi lineHeight nếu cần thêm khoảng cách
            
                    // In tổng tiền
                    ev.Graphics.DrawString(invoice.TriGia, font, Brushes.Black, ev.MarginBounds.Left, yLineTop);

                    yLineTop += lineHeight * 3;
                    ev.Graphics.DrawString("Cảm ơn quý khách đã sử dụng dịch vụ!", font, Brushes.Black, ev.MarginBounds.Left, yLineTop);
                    yLineTop += lineHeight;
                };

                try
                {
                    PrintDialog printDialog = new PrintDialog();
                    printDialog.Document = printDocument;
                    if (printDialog.ShowDialog() == DialogResult.OK)
                    {
                        printDocument.Print();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi in hóa đơn: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

    }
}
