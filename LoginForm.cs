using System;
using System.Linq;
using System.Windows.Forms;
using UIDuAn1;
using UIDuAn1.Models;

namespace Ui_DuAn
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            sdf.SetShadowForm(this);
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private bool ispasswordShow = false;
        // dùng để lưu thông tin đăng nhập
        public class CurrentUser
        {
            private static CurrentUser _instance;

            public string Email { get; set; }
            public string MatKhau { get; set; }
            public string VaiTro { get; set; }
            public string HoTen { get; set; }
            private CurrentUser() { }

            public static CurrentUser Instance
            {
                get
                {
                    if (_instance == null)
                    {
                        _instance = new CurrentUser();
                    }
                    return _instance;
                }
            }
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text;
            string matKhau = txtMatKhau.Text;

            if (string.IsNullOrWhiteSpace(email))
            {
                string message = "Vui lòng nhập Email!";
                MessageBox.Show(message);
                Console.WriteLine(message);
                return;
            }
            if (!email.EndsWith("@gmail.com"))
            {
                string message = "Định dạng email phải là {username}@gmail.com";
                MessageBox.Show(message);
                Console.WriteLine(message);
                return;
            }
            if (string.IsNullOrWhiteSpace(matKhau))
            {
                string message = "Vui lòng nhập mật khẩu!";
                MessageBox.Show(message);
                Console.WriteLine(message);
                return;
            }
            if (matKhau.Length < 8)
            {
                string message = "Mật khẩu phải có ít nhất 8 ký tự!";
                MessageBox.Show(message);
                Console.WriteLine(message);
                return;
            }
            using (var context = new QUANLYQUANNETContext())
            {
                var query = from NhanVien in context.NhanVien
                            where NhanVien.Gmail == email && NhanVien.MatKhau == matKhau
                            select NhanVien;

                var nhanVien = query.FirstOrDefault();

                // kiểm tra kết quả trả về từ truy vấn
                if (nhanVien != null)
                {
                    CurrentUser.Instance.Email = nhanVien.Gmail;
                    CurrentUser.Instance.MatKhau = nhanVien.MatKhau;
                    CurrentUser.Instance.VaiTro = nhanVien.MaVaiTro;
                    CurrentUser.Instance.HoTen = nhanVien.HoTen; // Lưu tên người dùng
                    MainForm mainform = new MainForm(CurrentUser.Instance.VaiTro, CurrentUser.Instance.HoTen);
                    mainform.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Tài khoản hoặc mật khẩu không chính xác!");
                }
            }
        }

        private void lbQuenMatKhau_Click(object sender, EventArgs e)
        {
            FormQuenMatKhau formQuenMatKhau = new FormQuenMatKhau();
            formQuenMatKhau.Show();
            this.Hide();
        }

        private void btnShowMkCu_Click(object sender, EventArgs e)
        {
            if (ispasswordShow)
            {
                txtMatKhau.PasswordChar = '\0';
                ispasswordShow = false;
            }
            else
            {
                txtMatKhau.PasswordChar = '●';
                ispasswordShow = true;
            }
        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtMatKhau_TextChanged(object sender, EventArgs e)
        {

        }
    }

}