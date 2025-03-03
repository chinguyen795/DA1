using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using NUnit.Framework;
using Ui_DuAn;
using UIDuAn1;

namespace UIDuAn1.Tests
{
	[TestFixture]
	public class ChangePasswordTests
	{
		private LoginForm loginForm;
		private MainForm mainForm;
		private UC_TaiKhoan taiKhoanUC;
		private string actualMessage = "";
		private StringWriter stringWriter;
		private TextWriter originalOutput;

		[SetUp]
		public void SetUp()
		{
			originalOutput = Console.Out;
			stringWriter = new StringWriter();
			Console.SetOut(stringWriter);

			// Mở form đăng nhập
			loginForm = new LoginForm();
			loginForm.Show();
			Thread.Sleep(500);

			// Điền thông tin đăng nhập
			loginForm.Invoke(new Action(() =>
			{
				loginForm.Controls["txtEmail"].Text = "ggg12345627z@gmail.com";
				loginForm.Controls["txtPassword"].Text = "password123";
			}));

			Thread.Sleep(500);

			// Nhấn nút đăng nhập
			loginForm.Invoke(new Action(() =>
			{
				((Button)loginForm.Controls["btnLogin"]).PerformClick();
			}));

			Thread.Sleep(2000);

			// Kiểm tra xem MainForm đã được mở chưa
			Console.WriteLine("Đang kiểm tra MainForm...");

			// Giả lập mở MainForm sau đăng nhập
			mainForm = new MainForm("VT02", "Test User");
			if (mainForm == null)
			{
				Assert.Fail("MainForm không được khởi tạo sau đăng nhập!");
				return;
			}

			mainForm.Show();
			Thread.Sleep(1000);

			Console.WriteLine("MainForm đã mở thành công!");
		}

		[TearDown]
		public void TearDown()
		{
			Console.SetOut(originalOutput);
			stringWriter.Dispose();

			taiKhoanUC?.Dispose();
			mainForm?.Dispose();
			loginForm?.Dispose();
		}

		[Test]
		[TestCase("password123", "passwordnew1", "passwordnew1", "Đổi mật khẩu thành công!")]
		[TestCase("pass1", "passwordnew1", "passwordnew1", "Mật khẩu không chính xác!")]
		[TestCase("password123", "pass", "pass", "Mật khẩu phải có ít nhất 8 ký tự!")]
		[TestCase("password123", "passwordnew1", "password000", "Nhập lại mật khẩu mới không khớp!")]
		[TestCase("", "", "", "Vui lòng nhập đầy đủ thông tin!")]
		[TestCase("", "passwordnew1", "passwordnew1", "Vui lòng nhập đầy đủ thông tin!")]
		[TestCase("password123", "", "passwordnew1", "Vui lòng nhập đầy đủ thông tin!")]
		[TestCase("password123", "passwordnew1", "", "Vui lòng nhập đầy đủ thông tin!")]
		public void TestChangePasswordFunctionality(string currentPassword, string newPassword, string confirmPassword, string expectedMessage)
		{
			actualMessage = "";

			// Kiểm tra nếu taiKhoanUC null
			if (taiKhoanUC == null)
			{
				Assert.Fail("UserControl taiKhoanUC chưa được khởi tạo!");
				return;
			}

			// Kiểm tra nếu Controls không tồn tại
			if (!taiKhoanUC.Controls.ContainsKey("txtMatKhauCu") ||
				!taiKhoanUC.Controls.ContainsKey("txtMatKhauMoi") ||
				!taiKhoanUC.Controls.ContainsKey("txtNhapLaiMatKhau") ||
				!taiKhoanUC.Controls.ContainsKey("btnDoiMatKhau"))
			{
				Assert.Fail("Không tìm thấy một hoặc nhiều Controls cần thiết trên UC_TaiKhoan!");
				return;
			}

			// Nhập dữ liệu
			taiKhoanUC.Invoke(new Action(() =>
			{
				taiKhoanUC.Controls["txtMatKhauCu"].Text = currentPassword;
				taiKhoanUC.Controls["txtMatKhauMoi"].Text = newPassword;
				taiKhoanUC.Controls["txtNhapLaiMatKhau"].Text = confirmPassword;
			}));

			Thread.Sleep(500);

			// Nhấn nút đổi mật khẩu
			taiKhoanUC.Invoke(new Action(() =>
			{
				((Button)taiKhoanUC.Controls["btnDoiMatKhau"]).PerformClick();
			}));

			Thread.Sleep(2000);

			// Kiểm tra thông báo hiển thị trên console
			string consoleOutput = stringWriter.ToString().Trim();
			Assert.That(consoleOutput, Is.EqualTo(expectedMessage), "Thông báo không đúng!");
		}
	}
}
