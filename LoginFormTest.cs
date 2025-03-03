using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using NUnit.Framework;
using Ui_DuAn;

namespace UIDuAn1.Tests
{
	[TestFixture]
	public class LoginFormTests
	{
		private LoginForm loginForm;
		private string actualMessage = "";
		private StringWriter stringWriter;
		private TextWriter originalOutput;

		[SetUp]
		public void SetUp()
		{
			originalOutput = Console.Out;
			stringWriter = new StringWriter();
			Console.SetOut(stringWriter);

			loginForm = new LoginForm();
			loginForm.Show();
		}

		[TearDown]
		public void TearDown()
		{
			Console.SetOut(originalOutput);
			stringWriter.Dispose();

			if (loginForm != null && !loginForm.IsDisposed)
			{
				loginForm.Close();
				loginForm.Dispose();
			}
		}
        private void AutoClickMessageBox()
        {
            Task.Run(() =>
            {
                Thread.Sleep(5000); // Đợi 5 giây trước khi nhấn Enter
                SendKeys.SendWait("{ENTER}"); // Nhấn Enter
            });
        }

        [Test]
		[TestCase("ggg12345627z@gmail.com", "password123", "")]
		[TestCase("nguyenchinhnguyen7925", "password001", "Định dạng email phải là {username}@gmail.com")]
		[TestCase("nguyenchinhnguyen7925@gmail.com", "pass01", "Mật khẩu phải có ít nhất 8 ký tự!")]
		[TestCase("abcdef@gmail.com", "pass01", "Tài khoản hoặc mật khẩu không chính xác!")]
		[TestCase("abcdef@gmail.com", "password001", "Tài khoản hoặc mật khẩu không chính xác!")]
		[TestCase("", "", "Vui lòng nhập Email!")]
		[TestCase("", "password001", "Vui lòng nhập Email!")]
		[TestCase("nguyenchinguyen7925@gmail.com", "", "Vui lòng nhập mật khẩu!")]
		public void TestLoginFunctionality(string email, string password, string expectedMessage)
		{
			actualMessage = "";

			loginForm.Invoke(new Action(() =>
			{
				loginForm.Controls["txtEmail"].Text = email;
				loginForm.Controls["txtMatKhau"].Text = password;
			}));

			Thread.Sleep(500);

			loginForm.Invoke(new Action(() =>
			{
				((Guna.UI2.WinForms.Guna2GradientButton)loginForm.Controls["btnDangNhap"]).PerformClick();
			}));

			Thread.Sleep(2000);
			AutoClickMessageBox();
			string consoleOutput = stringWriter.ToString().Trim();

			if (string.IsNullOrEmpty(expectedMessage))
			{
				bool isLoginSuccess = false;
				loginForm.Invoke(new Action(() => isLoginSuccess = !loginForm.Visible));

				Assert.That(isLoginSuccess, "Form đăng nhập phải ẩn đi khi đăng nhập thành công.");
			}
			else
			{
				Assert.That(consoleOutput, Is.EqualTo(expectedMessage), "Console thông báo không đúng!");
			}
		}
	}
}
