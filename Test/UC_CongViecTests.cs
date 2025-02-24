using Microsoft.VisualStudio.TestTools.UnitTesting;
using UIDuAn1;

namespace UIDuAn1.Tests
{
    [TestClass]
    public class UC_CongViecTests
    {
        [TestMethod]
        public void CheckVaiTro_VT01_EnableAllButtons()
        {
            var uc = new UC_CongViec("VT01");

            Assert.IsTrue(uc.IsBtnThemEnabled, "VT01 should enable btnThem");
            Assert.IsTrue(uc.IsBtnSuaEnabled, "VT01 should enable btnSua");
            Assert.IsTrue(uc.IsBtnXoaEnabled, "VT01 should enable btnXoa");
            Assert.IsTrue(uc.IsBtnLamMoiEnabled, "VT01 should enable btnLamMoi");
        }

        [TestMethod]
        public void CheckVaiTro_VT02_EnableSomeButtons()
        {
            var uc = new UC_CongViec("VT02");

            Assert.IsTrue(uc.IsBtnThemEnabled, "VT02 should enable btnThem");
            Assert.IsTrue(uc.IsBtnSuaEnabled, "VT02 should enable btnSua");
            Assert.IsFalse(uc.IsBtnXoaEnabled, "VT02 should disable btnXoa");
            Assert.IsTrue(uc.IsBtnLamMoiEnabled, "VT02 should enable btnLamMoi");
        }

        [TestMethod]
        public void CheckVaiTro_VT03_DisableAllButtons()
        {
            var uc = new UC_CongViec("VT03");

            Assert.IsFalse(uc.IsBtnThemEnabled, "VT03 should disable btnThem");
            Assert.IsFalse(uc.IsBtnSuaEnabled, "VT03 should disable btnSua");
            Assert.IsFalse(uc.IsBtnXoaEnabled, "VT03 should disable btnXoa");
            Assert.IsFalse(uc.IsBtnLamMoiEnabled, "VT03 should disable btnLamMoi");
        }

        [TestMethod]
        public void CheckVaiTro_Unknown_DisableAllButtons()
        {
            var uc = new UC_CongViec("UNKNOWN");

            Assert.IsFalse(uc.IsBtnThemEnabled, "Unknown role should disable btnThem");
            Assert.IsFalse(uc.IsBtnSuaEnabled, "Unknown role should disable btnSua");
            Assert.IsFalse(uc.IsBtnXoaEnabled, "Unknown role should disable btnXoa");
            Assert.IsFalse(uc.IsBtnLamMoiEnabled, "Unknown role should disable btnLamMoi");
        }
    }
}
