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
	public class ForgotPasswordFormTests
	{
		private FormQuenMatKhau forgotPasswordForm;
		private string actualMessage = "";
		private StringWriter stringWriter;
		private TextWriter originalOutput;

		[SetUp]
		public void SetUp()
		{
			originalOutput = Console.Out;
			stringWriter = new StringWriter();
			Console.SetOut(stringWriter);

			forgotPasswordForm = new FormQuenMatKhau();
			forgotPasswordForm.Show();
		}

		[TearDown]
		public void TearDown()
		{
			Console.SetOut(originalOutput);
			stringWriter.Dispose();

			if (forgotPasswordForm != null && !forgotPasswordForm.IsDisposed)
			{
				forgotPasswordForm.Close();
				forgotPasswordForm.Dispose();
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
		[TestCase("nguyenchnguyen7925@gmail.com", "Mật khẩu đã được gửi đến email của bạn.")]
		[TestCase("nguyenchnguyen7925", "Định dạng email phải là {username}@gmail.com")]
		[TestCase("abcdef@gmail.com", "Email không tồn tại.")]
		[TestCase("", "Vui lòng nhập email.")]


		public void TestForgotPasswordFunctionality(string email, string expectedMessage)
		{
			actualMessage = "";

			forgotPasswordForm.Invoke(new Action(() =>
			{
				forgotPasswordForm.Controls["txtEmail"].Text = email;
			}));

			Thread.Sleep(500);

			forgotPasswordForm.Invoke(new Action(() =>
			{
				((Guna.UI2.WinForms.Guna2GradientButton)forgotPasswordForm.Controls["btnXacNhan"]).PerformClick();
			}));

			Thread.Sleep(2000);

      
			AutoClickMessageBox();

            string consoleOutput = stringWriter.ToString().Trim();

			Assert.That(consoleOutput, Is.EqualTo(expectedMessage), "Console thông báo không đúng!");
		}
	}
}
