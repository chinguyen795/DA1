using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using NUnit.Framework;
using UIDuAn1;
using Guna.UI2.WinForms;
using System.Threading.Tasks;

namespace UIDuAn1.Tests
{
    [TestFixture]
    public class CongViecTest
    {
        private CongViecForm congViecForm;
        private string actualMessage = "";
        private StringWriter stringWriter;
        private TextWriter originalOutput;

        [SetUp]
        public void SetUp()
        {
             
            originalOutput = Console.Out;
            stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
             
            congViecForm = new CongViecForm();
            congViecForm.Show();
            congViecForm.BringToFront();
            congViecForm.Refresh();
             
            Thread.Sleep(2000);
            
        }

        [TearDown]
        public void TearDown()
        { 
            Console.SetOut(originalOutput);

            if (stringWriter != null)
            {
                stringWriter.Dispose();
                stringWriter = null;
            }

            if (congViecForm != null && !congViecForm.IsDisposed)
            {
                congViecForm.Close();
                congViecForm.Dispose();
                congViecForm = null;
            }
        }

        [Test]
        [TestCase("CA001", "Ca Sáng", "8", "Không vi phạm", "Nguyễn Văn A", "Thêm thành công")]
        [TestCase("CA002", "Ca Chiều", "6", "Có vi phạm nhỏ", "Trần Thị B", "Thêm thành công")]
        [TestCase("", "Ca Tối", "4", "Không có", "Lê Văn C", "Vui lòng nhập đầy đủ và đúng định dạng thông tin")] 
        [TestCase("CA005", "Ca Sáng", "", "Không có", "Lê Văn C", "Vui lòng nhập đầy đủ và đúng định dạng thông tin")]
        [TestCase("CA006", "Ca Chiều", "6", "", "Trần Thị B", "Vui lòng nhập đầy đủ và đúng định dạng thông tin")]
        [TestCase("CA007", "Ca Tối", "5", "Không có", "Lê Văn C", "Thêm thành công")]
        [TestCase("CA008", "Ca Sáng", "4", "Không có", "Lê Văn D", "Thêm thành công")]
        [TestCase("CA009", "Ca Sáng", "abc", "Không có", "Nguyễn Văn A", "Vui lòng nhập đầy đủ và đúng định dạng thông tin")]
        [TestCase("CA010", "Ca Sáng", "-3", "Không có", "Nguyễn Văn A", "Vui lòng nhập đầy đủ và đúng định dạng thông tin")]



        public void TestThemCaLam(string maCa, string caLam, string soGioLam, string viPham, string tenNhanVien, string expectedMessage)
        {
            actualMessage = "";
             
            var txtMaCaLam = FindControlRecursive(congViecForm, "txtMaCaLam") as Guna2TextBox;
            var cbCaLam = FindControlRecursive(congViecForm, "cbCaLam") as Guna2ComboBox;
            var txtSoGioLam = FindControlRecursive(congViecForm, "txtThoiGianLam") as Guna2TextBox;
            var txtViPham = FindControlRecursive(congViecForm, "txtViPham") as Guna2TextBox;
            var cbNhanVien = FindControlRecursive(congViecForm, "cbMaNV") as Guna2ComboBox;
            var btnThem = FindControlRecursive(congViecForm, "btnThem") as Guna2GradientButton;
             
            Assert.Multiple(() =>
            {
                Assert.That(txtMaCaLam, Is.Not.Null, "txtMaCaLam không tồn tại!");
                Assert.That(cbCaLam, Is.Not.Null, "cbCaLam không tồn tại!");
                Assert.That(txtSoGioLam, Is.Not.Null, "txtThoiGianLam không tồn tại!");
                Assert.That(txtViPham, Is.Not.Null, "txtViPham không tồn tại!");
                Assert.That(cbNhanVien, Is.Not.Null, "cbMaNV không tồn tại!");
                Assert.That(btnThem, Is.Not.Null, "btnThem không tồn tại!");
            });
             
            Task.Run(() =>
            {
                Thread.Sleep(1000);
                SendKeys.SendWait("{ENTER}");
            });
             
            congViecForm.Invoke(new Action(() =>
            {
                txtMaCaLam.Text = maCa;
                 
                
                    cbCaLam.SelectedItem = caLam;
                 

                txtSoGioLam.Text = soGioLam;
                txtViPham.Text = viPham;

                if (!string.IsNullOrEmpty(tenNhanVien) && cbNhanVien.Items.Contains(tenNhanVien))
                {
                    cbNhanVien.SelectedItem = tenNhanVien;
                }

                btnThem.PerformClick();
            }));
             
            Thread.Sleep(2000);
             
            AssertMessage(expectedMessage);
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
        [TestCase("CA001", "Xóa thành công")]
        [TestCase("CA002", "Xóa thành công")]
        [TestCase("", "Vui lòng chọn ca làm cần xóa.")]
        [TestCase("CA999", "Mã ca làm không tồn tại.")]
        public void TestXoaCaLam(string maCa, string expectedMessage)
        {
            actualMessage = "";

            // Tìm các control trên form
            var dtgCongViec = FindControlRecursive(congViecForm, "dtgCongViec") as Guna2DataGridView;
            var btnXoa = FindControlRecursive(congViecForm, "btnXoa") as Guna2GradientButton;

            // Kiểm tra các control có tồn tại không
            Assert.Multiple(() =>
            {
                Assert.That(dtgCongViec, Is.Not.Null, "dtgCongViec không tồn tại!");
                Assert.That(btnXoa, Is.Not.Null, "btnXoa không tồn tại!");
            });

            // Chọn một dòng trong bảng nếu có dữ liệu
            congViecForm.Invoke(new Action(() =>
            {
                if (!string.IsNullOrEmpty(maCa))
                {
                    bool found = false;
                    foreach (DataGridViewRow row in dtgCongViec.Rows)
                    {
                        if (row.Cells["MaCa"].Value != null && row.Cells["MaCa"].Value.ToString() == maCa)
                        {
                            dtgCongViec.ClearSelection();
                            row.Selected = true;
                            dtgCongViec.CurrentCell = row.Cells[0];
                            found = true;
                            break;
                        }
                    }

                    if (!found)
                    {
                        actualMessage = "Mã ca làm không tồn tại.";
                    }
                }
            }));

            Thread.Sleep(500);
             
            if (string.IsNullOrEmpty(actualMessage))
            {
                Task.Run(() =>
                {
                    Thread.Sleep(1000);  
                    SendKeys.SendWait("Y"); 
                });

                // Nhấn nút "Xóa"
                congViecForm.Invoke(new Action(() =>
                {
                    btnXoa.PerformClick();
                    AutoClickMessageBox(); 
                }));

                Thread.Sleep(2000);
                 
                Task.Run(() =>
                {
                    Thread.Sleep(1000);
                    SendKeys.SendWait("{ENTER}");  
                });

                Thread.Sleep(1000);
            }

            // Kiểm tra thông báo từ MessageBox
            AssertMessage(expectedMessage);
        }

        [Test]
        [TestCase("CA001", "Tìm thành công")]
        [TestCase("Ca Sáng", "Tìm thành công")]
        [TestCase("8", "Tìm thành công")]
        [TestCase("Không vi phạm", "Tìm thành công")]
        [TestCase("Nguyễn Văn A", "Tìm thành công")]
        [TestCase("NV001", "Tìm thành công")]
        [TestCase("CA999", "Tìm thất bại. Không có kết quả phù hợp.")]
        [TestCase("Không tồn tại", "Tìm thất bại. Không có kết quả phù hợp.")]
        [TestCase("", "Tìm thất bại. Không có kết quả phù hợp.")]
        public void TestTimKiemCaLam(string searchTerm, string expectedMessage)
        {

            actualMessage = "";

            // Tìm các control trên form
            var txtTimKiem = FindControlRecursive(congViecForm, "txtTimKiem") as Guna2TextBox;
            var btnTimKiem = FindControlRecursive(congViecForm, "btnTimKiem") as Guna2GradientButton;
            var dtgCongViec = FindControlRecursive(congViecForm, "dtgCongViec") as Guna2DataGridView;

            // Kiểm tra các control có tồn tại không
            Assert.Multiple(() =>
            {
                Assert.That(txtTimKiem, Is.Not.Null, "txtTimKiem không tồn tại!");
                Assert.That(btnTimKiem, Is.Not.Null, "btnTimKiem không tồn tại!");
                Assert.That(dtgCongViec, Is.Not.Null, "dtgCongViec không tồn tại!");
            });

            // Nhập dữ liệu vào ô tìm kiếm
            congViecForm.Invoke(new Action(() =>
            {
                txtTimKiem.Text = searchTerm;
            }));

            // Đợi UI cập nhật
            Thread.Sleep(1500);

            // Nhấn nút "Tìm kiếm"
            congViecForm.Invoke(new Action(() =>
            {
                btnTimKiem.PerformClick();
            }));
            Task.Run(() =>
            {
                Thread.Sleep(1000);
                SendKeys.SendWait("{ENTER}");
            });
            AutoClickMessageBox();
            // Đợi xử lý
            Thread.Sleep(2000);

            // Kiểm tra thông báo từ MessageBox
            AssertMessage(expectedMessage);
        }

        [Test]
        [TestCase("CA001", "Ca Sáng", "Không vi phạm", "Nguyễn Văn A", "Cập nhật thành công")]
        [TestCase("CA002", "Ca Chiều", "Có vi phạm nhỏ", "Trần Thị B", "Cập nhật thành công")]
        [TestCase("", "Ca Tối", "Không có", "Lê Văn C", "Vui lòng nhập đầy đủ và đúng định dạng thông tin")]
        [TestCase("CA004", "", "Không có", "Lê Văn C", "Vui lòng nhập đầy đủ và đúng định dạng thông tin")]
        [TestCase("CA005", "Ca Sáng", "", "Lê Văn C", "Vui lòng nhập đầy đủ và đúng định dạng thông tin")]
        [TestCase("CA006", "Ca Chiều", "Không có", "", "Vui lòng nhập đầy đủ và đúng định dạng thông tin")]

        public void TestSuaCaLam(string maCa, string caLam, string viPham, string tenNhanVien, string expectedMessage)
        {
            actualMessage = "";

            // Tìm các control trên form
            var dtgCongViec = FindControlRecursive(congViecForm, "dtgCongViec") as Guna2DataGridView;
            var txtMaCaLam = FindControlRecursive(congViecForm, "txtMaCaLam") as Guna2TextBox;
            var cbCaLam = FindControlRecursive(congViecForm, "cbCaLam") as Guna2ComboBox;
            var txtViPham = FindControlRecursive(congViecForm, "txtViPham") as Guna2TextBox;
            var cbNhanVien = FindControlRecursive(congViecForm, "cbMaNV") as Guna2ComboBox;
            var btnSua = FindControlRecursive(congViecForm, "btnSua") as Guna2GradientButton;

            // Kiểm tra các control có tồn tại không
            Assert.Multiple(() =>
            {
                Assert.That(dtgCongViec, Is.Not.Null, "dtgCongViec không tồn tại!");
                Assert.That(txtMaCaLam, Is.Not.Null, "txtMaCaLam không tồn tại!");
                Assert.That(cbCaLam, Is.Not.Null, "cbCaLam không tồn tại!");
                Assert.That(txtViPham, Is.Not.Null, "txtViPham không tồn tại!");
                Assert.That(cbNhanVien, Is.Not.Null, "cbMaNV không tồn tại!");
                Assert.That(btnSua, Is.Not.Null, "btnSua không tồn tại!");
            });

            // Auto nhấn OK nếu có MessageBox
            Task.Run(() =>
            {
                Thread.Sleep(1000);
                SendKeys.SendWait("{ENTER}");
            });
            Task.Run(() =>
            {
                Thread.Sleep(1000);
                SendKeys.SendWait("{ENTER}");
            });

            // Chọn một dòng trong bảng nếu có dữ liệu
            congViecForm.Invoke(new Action(() =>
            {
                if (dtgCongViec.Rows.Count > 0)
                {
                    dtgCongViec.ClearSelection();
                    dtgCongViec.Rows[0].Selected = true;
                    dtgCongViec.CurrentCell = dtgCongViec.Rows[0].Cells[0];
                }
            }));

            Thread.Sleep(500);

            // Nhập dữ liệu vào form
            congViecForm.Invoke(new Action(() =>
            {
                txtMaCaLam.Text = maCa;

                if (!string.IsNullOrEmpty(caLam) && cbCaLam.Items.Contains(caLam))
                {
                    cbCaLam.SelectedItem = caLam;
                }

                txtViPham.Text = viPham;

                if (!string.IsNullOrEmpty(tenNhanVien) && cbNhanVien.Items.Contains(tenNhanVien))
                {
                    cbNhanVien.SelectedItem = tenNhanVien;
                }

                btnSua.PerformClick();
            }));

            // Đợi xử lý
            Thread.Sleep(2000);

            // Kiểm tra thông báo từ MessageBox
            AssertMessage(expectedMessage);
        }

        private void AssertControls(params Control[] controls)
        {
            Assert.Multiple(() =>
            {
                foreach (var control in controls)
                {
                    Assert.That(control, Is.Not.Null, $"{control?.Name} không tồn tại!");
                }
            });
        }

        private void AssertMessage(string expectedMessage)
        {
            string consoleOutput = stringWriter.ToString().Trim();
            Assert.That(consoleOutput, Does.Contain(expectedMessage), $"Lỗi kiểm thử: {expectedMessage} không xuất hiện.");
        }

        private Control FindControlRecursive(Control parent, string name)
        {
            if (parent.Name == name)
                return parent;
            foreach (Control child in parent.Controls)
            {
                Control result = FindControlRecursive(child, name);
                if (result != null)
                    return result;
            }
            Console.WriteLine($"⚠ Không tìm thấy control: {name}");
            return null;
        }
    }
}
