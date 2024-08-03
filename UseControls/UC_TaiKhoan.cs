using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ui_DuAn;
using UIDuAn1.Models;
using static Ui_DuAn.LoginForm;

namespace UIDuAn1
{
    public partial class UC_TaiKhoan : UserControl
    {

        public UC_TaiKhoan()
        {
            InitializeComponent();
            loadData();
        }
        private void loadData()
        {
           using (var context = new QUANLYQUANNETContext())
            {
                var nhanVien = context.NhanVien
                    .FirstOrDefault(nv => nv.Gmail == CurrentUser.Instance.Email);

                if (nhanVien != null)
                {
                    txtEmail.Text = nhanVien.Gmail;
                    txtMaVaTenNV.Text = $"{nhanVien.MaNhanVien} | {nhanVien.HoTen}";
                    txtDiaChi.Text = nhanVien.DiaChi;
                    txtVaiTro.Text = nhanVien.TenVaiTro;
                }
                else
                {
                    MessageBox.Show("Không tìm thấy thông tin nhân viên!");
                }
            }
       }
        private bool isPasswordShown = false;
        private void btnDoiMatKhau_Click(object sender, EventArgs e)
        {
            string matkhaucu = txtMatKhauCu.Text;
            string matkhaumoi = txtMatKhauMoi.Text;
            string nhaplaiMK = txtNhapLaiMatKhau.Text;

            if (string.IsNullOrWhiteSpace(matkhaucu) || string.IsNullOrWhiteSpace(matkhaumoi) || string.IsNullOrWhiteSpace(nhaplaiMK))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
                return;
            }
            if (matkhaumoi.Length < 8)
            {
                MessageBox.Show("Mật khẩu phải có ít nhất 8 ký tự!");
                return;
            }
            using (var context = new QUANLYQUANNETContext())
            {
                var query = from NhanVien in context.NhanVien
                            where NhanVien.Gmail == CurrentUser.Instance.Email && NhanVien.MatKhau == matkhaucu
                            select NhanVien;
                var nhanVien = query.FirstOrDefault();

                if (nhanVien != null)
                {
                    if (matkhaucu == matkhaumoi)
                    {
                        MessageBox.Show("Mật khẩu mới không được trùng mật khẩu cũ");
                    }
                    else if (matkhaumoi == nhaplaiMK)
                    {
                        nhanVien.MatKhau = matkhaumoi;
                        context.SaveChanges();

                        MessageBox.Show("Đổi mật khẩu thành công!");

                        CurrentUser.Instance.MatKhau = matkhaumoi;

                        // Tạo và hiển thị form đăng nhập
                        LoginForm loginform = new LoginForm();
                        loginform.Show();

                        // Đóng form cha của user control
                        Form parentForm = this.FindForm();
                        if (parentForm != null)
                        {
                            parentForm.Close();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Nhập lại mật khẩu mới không khớp!");
                    }
                }
                else
                {
                    MessageBox.Show("Mật khẩu không chính xác!");
                }
            }
        }

        private void btnShowMkCu_Click(object sender, EventArgs e)
        {
            if (isPasswordShown)
            {
                txtMatKhauCu.PasswordChar = '\0';
                isPasswordShown = false;
            }
            else
            {
                txtMatKhauCu.PasswordChar = '●';
                isPasswordShown = true;
            }
        }

        private void btnShowMkMoi_Click(object sender, EventArgs e)
        {
            if (isPasswordShown)
            {
                txtMatKhauMoi.PasswordChar = '\0';
                isPasswordShown = false;
            }
            else
            {
                txtMatKhauMoi.PasswordChar = '●';
                isPasswordShown = true;
            }
        }

        private void btnShowMkNhapLai_Click(object sender, EventArgs e)
        {
            if (isPasswordShown)
            {
                txtNhapLaiMatKhau.PasswordChar = '\0';
                isPasswordShown = false;
            }
            else
            {
                txtNhapLaiMatKhau.PasswordChar = '●';
                isPasswordShown = true;
            }
        }
    }
}
