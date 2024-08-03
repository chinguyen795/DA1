using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using UIDuAn1.Models;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace UIDuAn1
{
    public partial class UC_ThongKe : UserControl
    {
        public UC_ThongKe()
        {
            InitializeComponent();
            cbLuaChon.Items.Add("Lương phải trả cho nhân viên theo tháng");
            cbLuaChon.Items.Add("Lương từng nhân viên theo tháng");
            cbLuaChon.Items.Add("Doanh thu của tháng");
            cbLuaChon.SelectedIndex = 0;

            cbLuaChon.SelectedIndexChanged += new EventHandler(cbLuaChon_SelectedIndexChanged);
            btnXuatThongTin.Click += new EventHandler(btnXuatThongTin_Click);

            LoadEmployeeData();
            UpdateComboBoxVisibility();
        }

        private void LoadEmployeeData()
        {
            using (var context = new QUANLYQUANNETContext())
            {
                var employees = context.NhanVien
                    .Select(nv => new { nv.MaNhanVien, nv.HoTen, DisplayText = nv.MaNhanVien + " | " + nv.HoTen })
                    .ToList();

                cbMNV.DataSource = employees;
                cbMNV.DisplayMember = "DisplayText";
                cbMNV.ValueMember = "MaNhanVien";
            }
        }

        private void cbLuaChon_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateComboBoxVisibility();
        }

        private void UpdateComboBoxVisibility()
        {
            if (cbLuaChon.SelectedItem.ToString() == "Lương từng nhân viên theo tháng")
            {
                cbMNV.Visible = true;
            }
            else
            {
                cbMNV.Visible = false;
            }
        }

        private void btnXuatThongTin_Click(object sender, EventArgs e)
        {
            string selectedOption = cbLuaChon.SelectedItem.ToString();

            if (!int.TryParse(txtThang.Text.Trim(), out int month) || month < 1 || month > 12)
            {
                MessageBox.Show("Vui lòng nhập số tháng hợp lệ (1-12).", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (var context = new QUANLYQUANNETContext())
            {
                if (selectedOption == "Lương phải trả cho nhân viên theo tháng")
                {
                    var caLam = context.CaLam
                        .Where(c => c.NgayLam.Month == month)
                        .Join(context.NhanVien,
                              c => c.MaNhanVien,
                              nv => nv.MaNhanVien,
                              (c, nv) => new { c.MaNhanVien, nv.HoTen, c.SoGioLam, c.NgayLam })
                        .ToList();

                    if (caLam.Count == 0)
                    {
                        lbThongTin2.Text = "Không có dữ liệu.";
                        lbThongTin3.Text = string.Empty;
                        return;
                    }

                    dtgKhachHang.DataSource = caLam;

                    int totalHours = caLam.Sum(c => c.SoGioLam);
                    int salary = totalHours * 20000;
                    lbThongTin2.Text = $"Lương cần trả cho tất cả nhân viên tháng {month}: " + salary.ToString();
                    lbThongTin3.Text = "Hệ số lương: 1:20000";
                }
                else if (selectedOption == "Lương từng nhân viên theo tháng")
                {
                    string employeeId = cbMNV.SelectedValue.ToString();
                    var caLam = context.CaLam
                        .Where(c => c.MaNhanVien == employeeId && c.NgayLam.Month == month)
                        .Join(context.NhanVien,
                              c => c.MaNhanVien,
                              nv => nv.MaNhanVien,
                              (c, nv) => new { c.MaNhanVien, nv.HoTen, c.SoGioLam, c.NgayLam })
                        .ToList();

                    if (caLam.Count == 0)
                    {
                        lbThongTin2.Text = "Không có dữ liệu.";
                        lbThongTin3.Text = string.Empty;
                        return;
                    }

                    dtgKhachHang.DataSource = caLam;

                    int totalHours = caLam.Sum(c => c.SoGioLam);
                    int salary = totalHours * 20000;
                    var employeeName = caLam.FirstOrDefault()?.HoTen ?? "Không có dữ liệu";
                    lbThongTin2.Text = $"Lương cần trả cho {employeeName} trong tháng {month}: " + salary.ToString();
                    lbThongTin3.Text = "Hệ số lương: 1:20000";
                }
                else if (selectedOption == "Doanh thu của tháng")
                {
                    var hoaDons = context.HoaDon
                        .Where(hd => hd.NgayLap.Month == month)
                        .ToList();

                    if (hoaDons.Count == 0)
                    {
                        lbThongTin2.Text = "Không có dữ liệu.";
                        lbThongTin3.Text = string.Empty;
                        return;
                    }

                    dtgKhachHang.DataSource = hoaDons;

                    decimal totalRevenue = hoaDons.Sum(hd => hd.TriGia);

                    lbThongTin2.Text = $"Doanh thu tháng {month}: {totalRevenue.ToString()}";
                    lbThongTin3.Text = "Hệ số lương: 1:10000";
                }
            }
        }

    }
}