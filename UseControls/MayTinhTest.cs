/*using System;
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
    public class MayTinhTest
    {
        private MayTinhForm mayTinhForm;
        private string actualMessage = "";
        private StringWriter stringWriter;
        private TextWriter originalOutput;

        [SetUp]
        public void SetUp()
        {
            // Lưu đầu ra gốc của Console và thay thế bằng StringWriter để ghi lại output
            originalOutput = Console.Out;
            stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            // Khởi tạo form
            mayTinhForm = new MayTinhForm();
            mayTinhForm.Show();

            // Đợi UI load xong
            Thread.Sleep(2000);
        }

        [TearDown]
        public void TearDown()
        {
            // Đặt lại đầu ra của Console sau khi test xong
            Console.SetOut(originalOutput);
            stringWriter.Dispose();

            if (mayTinhForm != null && !mayTinhForm.IsDisposed)
            {
                mayTinhForm.Close();
                mayTinhForm.Dispose();
            }
        }

        [Test]
        // Thêm máy tính thành công
        [TestCase("100", "Intel Core i5", "NVIDIA GTX 1050", "16GB", "Thêm thành công")]
        // Thêm với CPU trống
        [TestCase("100", "NVIDIA GTX 1050", "", "16GB", "Vui lòng nhập đầy đủ và đúng định dạng thông tin")]
        // Thêm với GPU trống
        [TestCase("100", "", "Intel Core i5", "16GB", "Vui lòng nhập đầy đủ và đúng định dạng thông tin")]
        // Thêm với RAM trống
        [TestCase("100", "NVIDIA GTX 1050", "Intel Core i5", "", "Vui lòng nhập đầy đủ và đúng định dạng thông tin")]
        // Thêm với giá tiền trống
        [TestCase("", "NVIDIA GTX 1050", "Intel Core i5", "16GB", "Vui lòng nhập đầy đủ và đúng định dạng thông tin")]
        // Thêm giá tiền kiểu chữ
        [TestCase("money", "NVIDIA GTX 1050", "Intel Core i5", "16GB", "Vui lòng nhập đầy đủ và đúng định dạng thông tin")]
        // Thêm giá tiền âm
        [TestCase("-100", "NVIDIA GTX 1050", "Intel Core i5", "16GB", "Vui lòng nhập đầy đủ và đúng định dạng thông tin")]
        // Thêm với CPU không chứa chữ
        [TestCase("100", "NVIDIA GTX 1050", "123", "16GB", "Vui lòng nhập đầy đủ và đúng định dạng thông tin")]
        // Thêm với GPU không chứa chữ
        [TestCase("100", "123", "Intel Core i5", "16GB", "Vui lòng nhập đầy đủ và đúng định dạng thông tin")]
        // Thêm với RAM không chứa chữ
        [TestCase("100", "NVIDIA GTX 1050", "Intel Core i5", "***", "Vui lòng nhập đầy đủ và đúng định dạng thông tin")]
        public void TestThemMayTinh(string giaTien, string gpu, string cpu, string ram, string expectedMessage)
        {
            actualMessage = ""; 

            // Tìm các control trên form
            var txtGiaTien = FindControlRecursive(mayTinhForm, "txtGiaTien") as Guna2TextBox;
            var txtGPU = FindControlRecursive(mayTinhForm, "txtGPU") as Guna2TextBox;
            var txtCPU = FindControlRecursive(mayTinhForm, "txtCPU") as Guna2TextBox;
            var txtRAM = FindControlRecursive(mayTinhForm, "txtRAM") as Guna2TextBox;
            var btnThem = FindControlRecursive(mayTinhForm, "btnThem") as Guna2GradientButton;

            // Kiểm tra các control có tồn tại không
            Assert.Multiple(() =>
            {
                Assert.That(txtGiaTien, Is.Not.Null, "txtGiaTien không tồn tại!");
                Assert.That(txtGPU, Is.Not.Null, "txtGPU không tồn tại!");
                Assert.That(txtCPU, Is.Not.Null, "txtCPU không tồn tại!");
                Assert.That(txtRAM, Is.Not.Null, "txtRAM không tồn tại!");
                Assert.That(btnThem, Is.Not.Null, "btnThem không tồn tại!");
            });

            // Nhập dữ liệu vào form
            mayTinhForm.Invoke(new Action(() =>
            {
                txtGiaTien.Text = giaTien;
                txtGPU.Text = gpu;
                txtCPU.Text = cpu;
                txtRAM.Text = ram;
            }));

            // Đợi UI cập nhật
            Thread.Sleep(500);

            // Nhấn nút "Thêm"
            mayTinhForm.Invoke(new Action(() =>
            {
                btnThem.PerformClick();
            }));

            // Đợi xử lý
            Thread.Sleep(2000);

            // Kiểm tra thông báo từ MessageBox
            string consoleOutput = stringWriter.ToString().Trim();
            Assert.That(consoleOutput, Does.Contain(expectedMessage), "Thêm thất bại");
        }

        [Test]
        // Cập nhật thành công
        [TestCase("100", "Intel Arc A750", " Intel Core i5-12400F", "64GB", "Cập nhật thành công")]
        // Cập nhật giá tiền kiểu chữ
        [TestCase("tien", "Intel Arc A750", "Intel Core i5-12400F", "64GB", "Vui lòng nhập đầy đủ và đúng định dạng thông tin")]
        // Cập nhật với giá tiền âm
        [TestCase("-100", "Intel Arc A750", "Intel Core i5-12400F", "64GB", "Vui lòng nhập đầy đủ và đúng định dạng thông tin")]
        // Cập nhật với CPU không chứa chữ
        [TestCase("100", "Intel Arc A750", "567", "64GB", "Vui lòng nhập đầy đủ và đúng định dạng thông tin")]
        // Cập nhật với GPU không chứa chữ
        [TestCase("100", "456", "Intel Core i5-12400F", "64GB", "Vui lòng nhập đầy đủ và đúng định dạng thông tin")]
        // Cập nhật với RAM không chứa chữ
        [TestCase("100", "Intel Arc A750", "Intel Core i5-12400F", "***", "Vui lòng nhập đầy đủ và đúng định dạng thông tin")]
        public void TestSuaMayTinh(string giaTien, string gpu, string cpu, string ram, string expectedMessage)
        {
            actualMessage = ""; // Reset actual message

            // Tìm các control trên form
            var dtgMayTinh = FindControlRecursive(mayTinhForm, "dtgMayTinh") as DataGridView;
            var txtGiaTien = FindControlRecursive(mayTinhForm, "txtGiaTien") as Guna2TextBox;
            var txtGPU = FindControlRecursive(mayTinhForm, "txtGPU") as Guna2TextBox;
            var txtCPU = FindControlRecursive(mayTinhForm, "txtCPU") as Guna2TextBox;
            var txtRAM = FindControlRecursive(mayTinhForm, "txtRAM") as Guna2TextBox;
            var btnSua = FindControlRecursive(mayTinhForm, "btnSua") as Guna2GradientButton;

            // Kiểm tra các control có tồn tại không
            Assert.Multiple(() =>
            {
                Assert.That(dtgMayTinh, Is.Not.Null, "dtgMayTinh không tồn tại!");
                Assert.That(txtGiaTien, Is.Not.Null, "txtGiaTien không tồn tại!");
                Assert.That(txtGPU, Is.Not.Null, "txtGPU không tồn tại!");
                Assert.That(txtCPU, Is.Not.Null, "txtCPU không tồn tại!");
                Assert.That(txtRAM, Is.Not.Null, "txtRAM không tồn tại!");
                Assert.That(btnSua, Is.Not.Null, "btnSua không tồn tại!");
            });

            // Chọn một hàng trong DataGridView nếu có dữ liệu
            mayTinhForm.Invoke(new Action(() =>
            {
                if (dtgMayTinh.Rows.Count > 0)
                {
                    dtgMayTinh.ClearSelection();
                    dtgMayTinh.Rows[0].Selected = true;
                    dtgMayTinh.CurrentCell = dtgMayTinh.Rows[0].Cells[0]; 
                }
            }));

            // Đợi UI cập nhật sau khi chọn hàng
            Thread.Sleep(500);

            // Nhập dữ liệu vào form
            mayTinhForm.Invoke(new Action(() =>
            {
                txtGiaTien.Text = giaTien;
                txtGPU.Text = gpu;
                txtCPU.Text = cpu;
                txtRAM.Text = ram;
            }));

            // Đợi UI cập nhật
            Thread.Sleep(500);

            // Nhấn nút "Sửa"
            mayTinhForm.Invoke(new Action(() =>
            {
                btnSua.PerformClick();
            }));

            // Đợi xử lý
            Thread.Sleep(2000);

            // Kiểm tra thông báo từ MessageBox
            string consoleOutput = stringWriter.ToString().Trim();
            Assert.That(consoleOutput, Does.Contain(expectedMessage), "Cập nhật máy tính thất bại");
        }

        [Test]
        [TestCase("Vui lòng chọn máy tính cần sửa")]
        public void TestSuaMayTinhKhiChuaChon(string expectedMessage)
        {
            actualMessage = ""; // Reset actual message

            // Tìm các control trên form
            var btnSua = FindControlRecursive(mayTinhForm, "btnSua") as Guna2GradientButton;

            // Kiểm tra các control có tồn tại không
            Assert.Multiple(() =>
            {
                Assert.That(btnSua, Is.Not.Null, "btnSua không tồn tại!");
            });

            // Đợi UI cập nhật sau khi chọn hàng
            Thread.Sleep(500);

            // Gửi phím "Y" ngay sau khi hộp thoại xuất hiện (Yes)
            Task.Run(() =>
            {
                Thread.Sleep(1000); // Đợi hộp thoại hiển thị
                SendKeys.SendWait("Y"); // Gửi phím "Y" để chọn "Yes"
            });

            // Nhấn nút "Sửa"
            mayTinhForm.Invoke(new Action(() =>
            {
                btnSua.PerformClick();
            }));

            // Đợi xử lý
            Thread.Sleep(2000);

            // Kiểm tra thông báo từ MessageBox
            string consoleOutput = stringWriter.ToString().Trim();
            Assert.That(consoleOutput, Does.Contain(expectedMessage), "Cập nhật máy tính thất bại");
        }


        [Test]
        [TestCase("Không thể xóa máy tính này vì còn liên kết với dữ liệu bảng nhân viên")]
        public void TestXoaMayTinhKhiConLienKet(string expectedMessage)
        {
            actualMessage = ""; // Reset actual message

            // Tìm các control trên form
            var dtgMayTinh = FindControlRecursive(mayTinhForm, "dtgMayTinh") as DataGridView;
            var btnXoa = FindControlRecursive(mayTinhForm, "btnXoa") as Guna2GradientButton;

            // Kiểm tra các control có tồn tại không
            Assert.Multiple(() =>
            {
                Assert.That(dtgMayTinh, Is.Not.Null, "dtgMayTinh không tồn tại!");
                Assert.That(btnXoa, Is.Not.Null, "btnXoa không tồn tại!");
            });

            // Chọn một hàng trong DataGridView nếu có dữ liệu
            mayTinhForm.Invoke(new Action(() =>
            {
                if (dtgMayTinh.Rows.Count > 0)
                {
                    dtgMayTinh.ClearSelection();
                    dtgMayTinh.Rows[0].Selected = true;
                    dtgMayTinh.CurrentCell = dtgMayTinh.Rows[0].Cells[0]; 
                }
            }));

            // Đợi UI cập nhật sau khi chọn hàng
            Thread.Sleep(500);

            // Gửi phím "Y" ngay sau khi hộp thoại xuất hiện (Yes)
           Y

            // Đợi xử lý
            Thread.Sleep(2000);

            // Kiểm tra thông báo từ MessageBox
            string consoleOutput = stringWriter.ToString().Trim();
            Assert.That(consoleOutput, Does.Contain(expectedMessage), "Xóa khách hàng thất bại");
        }

        [Test]
        [TestCase("Vui lòng chọn máy tính cần xóa")]
        public void TestXoaMayTinhKhiChuaChon(string expectedMessage)
        {
            actualMessage = ""; // Reset actual message

            // Tìm các control trên form
            var btnXoa = FindControlRecursive(mayTinhForm, "btnXoa") as Guna2GradientButton;

            // Kiểm tra các control có tồn tại không
            Assert.Multiple(() =>
            {
                Assert.That(btnXoa, Is.Not.Null, "btnXoa không tồn tại!");
            });

            // Đợi UI cập nhật sau khi chọn hàng
            Thread.Sleep(500);

            // Gửi phím "Y" ngay sau khi hộp thoại xuất hiện (Yes)
            Task.Run(() =>
            {
                Thread.Sleep(1000); // Đợi hộp thoại hiển thị
                SendKeys.SendWait("Y"); // Gửi phím "Y" để chọn "Yes"
            });

            // Nhấn nút "Xóa"
            mayTinhForm.Invoke(new Action(() =>
            {
                btnXoa.PerformClick();
            }));

            // Đợi xử lý
            Thread.Sleep(2000);

            // Kiểm tra thông báo từ MessageBox
            string consoleOutput = stringWriter.ToString().Trim();
            Assert.That(consoleOutput, Does.Contain(expectedMessage), "Xóa khách hàng thất bại");
        }

        [Test]
        // Tìm theo mã máy 
        [TestCase("MT001", "Tìm thành công")]
        // Tìm theo CPU
        [TestCase("Intel i7", "Tìm thành công")]
        // Tìm theo GPU
        [TestCase("AMD Radeon RX 580", "Tìm thành công")]
        // Tìm theo RAM
        [TestCase("16GB", "Tìm thành công")]
        // Tìm theo giá tiền
        [TestCase("123", "Tìm thành công")]
        // Tìm theo mã nhân viên
        [TestCase("NV01", "Tìm thành công")]
        // Tìm theo mã máy null
        [TestCase("MT999", "Tìm thất bại. Vui lòng nhập lại thông tin")]
        // Tìm theo CPU null
        [TestCase("Intel Core i100", "Tìm thất bại. Vui lòng nhập lại thông tin")]
        // Tìm theo GPU null
        [TestCase("NVIDIA GTX 1234", "Tìm thất bại. Vui lòng nhập lại thông tin")]
        // Tìm theo RAM null
        [TestCase("1TB", "Tìm thất bại. Vui lòng nhập lại thông tin")]
        // Tìm theo giá tiền null
        [TestCase("9999", "Tìm thất bại. Vui lòng nhập lại thông tin")]
        // Tìm theo mã nhân viên null
        [TestCase("NV999", "Tìm thất bại. Vui lòng nhập lại thông tin")]
        // Không nhập thông tin
        [TestCase("", "Vui lòng nhập thông tin")]
        public void TestTimKiemMayTinh(string noidung, string expectedMessage)
        {
            actualMessage = ""; // Reset actual message

            // Tìm các control trên form
            var txtTimKiem = FindControlRecursive(mayTinhForm, "txtTimKiem") as Guna2TextBox;
            var btnTimKiem = FindControlRecursive(mayTinhForm, "btnTimKiem") as Guna2GradientButton;

            // Kiểm tra các control có tồn tại không
            Assert.Multiple(() =>
            {
                Assert.That(txtTimKiem, Is.Not.Null, "txtTimKiem không tồn tại!");
                Assert.That(btnTimKiem, Is.Not.Null, "btnTimKiem không tồn tại!");
            });

            // Đợi UI cập nhật sau khi chọn hàng
            Thread.Sleep(500);

            // Nhập dữ liệu vào form
            mayTinhForm.Invoke(new Action(() =>
            {
                txtTimKiem.Text = noidung;
            }));

            // Đợi UI cập nhật
            Thread.Sleep(500);

            // Nhấn nút "Tìm kiếm"
            mayTinhForm.Invoke(new Action(() =>
            {
                btnTimKiem.PerformClick();
            }));

            // Đợi xử lý
            Thread.Sleep(2000);

            // Kiểm tra thông báo từ MessageBox
            string consoleOutput = stringWriter.ToString().Trim();
            Assert.That(consoleOutput, Does.Contain(expectedMessage), "Tìm kiếm khách hàng thất bại");
        }

        /// <summary>
        /// Phương thức tìm control theo tên trong form
        /// </summary>
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
            return null;
        }
    }
}
*/