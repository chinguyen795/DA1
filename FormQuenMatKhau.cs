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
using MimeKit;
using MailKit.Net.Smtp;
using UIDuAn1.Models;

namespace UIDuAn1
{
	public partial class FormQuenMatKhau : Form
	{
		public FormQuenMatKhau()
		{
			InitializeComponent();
		}

		private void RetrievePassword(string email)
		{
			using (var context = new QUANLYQUANNETContext())
			{
				var nhanVien = context.NhanVien.FirstOrDefault(nv => nv.Gmail == email);
				if (nhanVien == null)
				{
					string message = "Email không tồn tại.";
					MessageBox.Show(message);
					Console.WriteLine(message);
					return;
				}

				try
				{
					SendEmail(nhanVien.Gmail, "Mật khẩu của bạn", $"Mật khẩu của bạn là: {nhanVien.MatKhau}");
					string message = "Mật khẩu đã được gửi đến email của bạn.";
					DialogResult result = MessageBox.Show(message);
					Console.WriteLine(message);

					if (result == DialogResult.OK)
					{
						LoginForm loginForm = new LoginForm();
						loginForm.Show();
						this.Close();
					}
				}
				catch (Exception ex)
				{
					string message = "Lỗi: " + ex.Message;
					MessageBox.Show(message);
					Console.WriteLine(message);
				}
			}
		}

		private void SendEmail(string toEmail, string subject, string body)
		{
			try
			{
				var message = new MimeMessage();
				message.From.Add(new MailboxAddress("nguyenncpc09256", "nguyenchinguyen7925@gmail.com"));
				message.To.Add(new MailboxAddress("", toEmail));
				message.Subject = subject;
				message.Body = new TextPart("html")
				{
					Text = body
				};

				using (var client = new SmtpClient())
				{
					client.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
					client.Authenticate("nguyenchinguyen7925@gmail.com", "mmzn cxki nxyd ooan"); // Thay bằng mật khẩu ứng dụng
					client.Send(message);
					client.Disconnect(true);
				}
			}
			catch (Exception ex)
			{
				string message = "Lỗi: " + ex.Message;
				MessageBox.Show(message);
				Console.WriteLine(message);
			}
		}

		private void btnXacNhan_Click(object sender, EventArgs e)
		{
			string email = txtEmail.Text.Trim();

			if (string.IsNullOrWhiteSpace(email))
			{
				string message = "Vui lòng nhập email.";
				MessageBox.Show(message);
				Console.WriteLine(message);
				return;
			}

			if (!email.EndsWith("@gmail.com")) // kt có kết thúc đúng k
			{
				string message = "Định dạng email phải là {username}@gmail.com";
				MessageBox.Show(message);
				Console.WriteLine(message);
				return;
			}

			RetrievePassword(email);
		}

		private void btnThoat_Click(object sender, EventArgs e)
		{
			LoginForm loginForm = new LoginForm();
			loginForm.Show();
			this.Close();
		}
	}
}
