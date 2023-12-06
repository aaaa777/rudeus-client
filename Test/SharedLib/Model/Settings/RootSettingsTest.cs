using Rudeus.Model;
using SharedLib.Model.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.SharedLib.Model;

#pragma warning disable CS8603 // Null 参照戻り値である可能性があります。
namespace Test.SharedLib.Model.Settings
{
    public class RootSettingsTest
    {
        [Fact]
        public void CurrentVersionPTest()
        {
            // Arrange
            var fs = new FakeSettings();
            var testString = "1.2.3";
            IRootSettings rs = new RootSettings(getFunc: fs.Get, setFunc: fs.Set);

            // Act
            rs.CurrentVersionP = testString;

            // Assert
            Assert.Equal(testString, rs.CurrentVersionP);
        }

        [Fact]
        public void UpdatingChannelPTest()
        {
            // Arrange
            var fs = new FakeSettings();
            var testString = "stable";
            IRootSettings rs = new RootSettings(getFunc: fs.Get, setFunc: fs.Set);

            // Act
            rs.UpdatingChannelP = testString;

            // Assert
            Assert.Equal(testString, rs.UpdatingChannelP);
        }

        [Fact]
        public void AccessTokenPTest()
        {
            // Arrange
            var fs = new FakeSettings();
            var testString = "test_access_token";
            IRootSettings rs = new RootSettings(getFunc: fs.Get, setFunc: fs.Set);

            // Act
            rs.AccessTokenP = testString;

            // Assert
            Assert.Equal(testString, rs.AccessTokenP);
        }

        [Fact]
        public void DeviceIdPTest()
        {
            // Arrange
            var fs = new FakeSettings();
            var testString = "test_device_id";
            IRootSettings rs = new RootSettings(getFunc: fs.Get, setFunc: fs.Set);

            // Act
            rs.DeviceIdP = testString;

            // Assert
            Assert.Equal(testString, rs.DeviceIdP);
        }

        [Fact]
        public void FirstHostnamePTest()
        {
            // Arrange
            var fs = new FakeSettings();
            var testString = "test_hostname";
            IRootSettings rs = new RootSettings(getFunc: fs.Get, setFunc: fs.Set);

            // Act
            rs.FirstHostnameP = testString;

            // Assert
            Assert.Equal(testString, rs.FirstHostnameP);
        }

        [Fact]
        public void HostnamePTest()
        {
            // Arrange
            var fs = new FakeSettings();
            var testString = "test_hostname";
            IRootSettings rs = new RootSettings(getFunc: fs.Get, setFunc: fs.Set);

            // Act
            rs.HostnameP = testString;

            // Assert
            Assert.Equal(testString, rs.HostnameP);
        }

        [Fact]
        public void UsernamePTest()
        {
            // Arrange
            var fs = new FakeSettings();
            var testString = "test_username";
            IRootSettings rs = new RootSettings(getFunc: fs.Get, setFunc: fs.Set);

            // Act
            rs.UsernameP = testString;

            // Assert
            Assert.Equal(testString, rs.UsernameP);
        }

        [Fact]
        public void LabelIdPTest()
        {
            // Arrange
            var fs = new FakeSettings();
            var testString = "test_label_id";
            IRootSettings rs = new RootSettings(getFunc: fs.Get, setFunc: fs.Set);

            // Act
            rs.LabelIdP = testString;

            // Assert
            Assert.Equal(testString, rs.LabelIdP);
        }

        [Fact]
        public void SpecPTest()
        {
            // Arrange
            var fs = new FakeSettings();
            var testString = "test_label_name";
            IRootSettings rs = new RootSettings(getFunc: fs.Get, setFunc: fs.Set);

            // Act
            rs.SpecP = testString;

            // Assert
            Assert.Equal(testString, rs.SpecP);
        }

        [Fact]
        public void CpuNamePTest()
        {
            // Arrange
            var fs = new FakeSettings();
            var testString = "test_cpu_name";
            IRootSettings rs = new RootSettings(getFunc: fs.Get, setFunc: fs.Set);

            // Act
            rs.CpuNameP = testString;

            // Assert
            Assert.Equal(testString, rs.CpuNameP);
        }

        [Fact]
        public void MemoryPTest()
        {
            // Arrange
            var fs = new FakeSettings();
            var testString = "test_memory";
            IRootSettings rs = new RootSettings(getFunc: fs.Get, setFunc: fs.Set);

            // Act
            rs.MemoryP = testString;

            // Assert
            Assert.Equal(testString, rs.MemoryP);
        }

        [Fact]
        public void CDrivePTest()
        {
            // Arrange
            var fs = new FakeSettings();
            var testString = "test_c_drive";
            IRootSettings rs = new RootSettings(getFunc: fs.Get, setFunc: fs.Set);

            // Act
            rs.CDriveP = testString;

            // Assert
            Assert.Equal(testString, rs.CDriveP);
        }

        [Fact]
        public void OSPTest()
        {
            // Arrange
            var fs = new FakeSettings();
            var testString = "test_os";
            IRootSettings rs = new RootSettings(getFunc: fs.Get, setFunc: fs.Set);

            // Act
            rs.OSP = testString;

            // Assert
            Assert.Equal(testString, rs.OSP);
        }

        [Fact]
        public void OSVersionPTest()
        {
            // Arrange
            var fs = new FakeSettings();
            var testString = "test_os_version";
            IRootSettings rs = new RootSettings(getFunc: fs.Get, setFunc: fs.Set);

            // Act
            rs.OSVersionP = testString;

            // Assert
            Assert.Equal(testString, rs.OSVersionP);
        }

        [Fact]
        public void WithSecurePTest()
        {
            // Arrange
            var fs = new FakeSettings();
            var testString = "test_withsecure";
            IRootSettings rs = new RootSettings(getFunc: fs.Get, setFunc: fs.Set);

            // Act
            rs.WithSecureP = testString;

            // Assert
            Assert.Equal(testString, rs.WithSecureP);
        }

        [Fact]
        public void IsBetaChannelPTest()
        {
            // Arrange
            var fs = new FakeSettings();
            IRootSettings rs = new RootSettings(getFunc: fs.Get, setFunc: fs.Set);
            rs.UpdatingChannelP = "beta";

            // Act
            rs.IsBetaChannelP();

            // Assert
            Assert.True(rs.IsBetaChannelP());
            Assert.False(rs.IsStableChannelP());
            Assert.False(rs.IsDevelopChannelP());
            Assert.False(rs.IsTestChannelP());
        }

        [Fact]
        public void IsStableChannelPTest()
        {
            // Arrange
            var fs = new FakeSettings();
            IRootSettings rs = new RootSettings(getFunc: fs.Get, setFunc: fs.Set);
            rs.UpdatingChannelP = "stable";

            // Act
            rs.IsStableChannelP();

            // Assert
            Assert.False(rs.IsBetaChannelP());
            Assert.True(rs.IsStableChannelP());
            Assert.False(rs.IsDevelopChannelP());
            Assert.False(rs.IsTestChannelP());
        }

        [Fact]
        public void IsStableChannelPTest2()
        {
            // Arrange
            var fs = new FakeSettings();
            IRootSettings rs = new RootSettings(getFunc: fs.Get, setFunc: fs.Set);
            rs.UpdatingChannelP = "<randomtext>";

            // Act
            rs.IsStableChannelP();

            // Assert
            Assert.False(rs.IsBetaChannelP());
            Assert.True(rs.IsStableChannelP());
            Assert.False(rs.IsDevelopChannelP());
            Assert.False(rs.IsTestChannelP());
        }

        [Fact]
        public void IsDevelopChannelPTest()
        {
            // Arrange
            var fs = new FakeSettings();
            IRootSettings rs = new RootSettings(getFunc: fs.Get, setFunc: fs.Set);
            rs.UpdatingChannelP = "develop";

            // Act
            rs.IsDevelopChannelP();

            // Assert
            Assert.False(rs.IsBetaChannelP());
            Assert.False(rs.IsStableChannelP());
            Assert.True(rs.IsDevelopChannelP());
            Assert.False(rs.IsTestChannelP());
        }

        [Fact]
        public void IsTestChannelPTest()
        {
            // Arrange
            var fs = new FakeSettings();
            IRootSettings rs = new RootSettings(getFunc: fs.Get, setFunc: fs.Set);
            rs.UpdatingChannelP = "test";

            // Act
            rs.IsTestChannelP();

            // Assert
            Assert.False(rs.IsBetaChannelP());
            Assert.False(rs.IsStableChannelP());
            Assert.False(rs.IsDevelopChannelP());
            Assert.True(rs.IsTestChannelP());
        }

        [Fact]
        public void SetBetaChannelPTest()
        {
            // Arrange
            var fs = new FakeSettings();
            IRootSettings rs = new RootSettings(getFunc: fs.Get, setFunc: fs.Set);

            // Act
            rs.SetBetaChannelP();

            // Assert
            Assert.True(rs.IsBetaChannelP());
            Assert.False(rs.IsStableChannelP());
            Assert.False(rs.IsDevelopChannelP());
            Assert.False(rs.IsTestChannelP());
        }

        [Fact]
        public void SetStableChannelPTest()
        {
            // Arrange
            var fs = new FakeSettings();
            IRootSettings rs = new RootSettings(getFunc: fs.Get, setFunc: fs.Set);

            // Act
            rs.SetStableChannelP();

            // Assert
            Assert.False(rs.IsBetaChannelP());
            Assert.True(rs.IsStableChannelP());
            Assert.False(rs.IsDevelopChannelP());
            Assert.False(rs.IsTestChannelP());
        }

        [Fact]
        public void SetDevelopChannelPTest()
        {
            // Arrange
            var fs = new FakeSettings();
            IRootSettings rs = new RootSettings(getFunc: fs.Get, setFunc: fs.Set);

            // Act
            rs.SetDevelopChannelP();

            // Assert
            Assert.False(rs.IsBetaChannelP());
            Assert.False(rs.IsStableChannelP());
            Assert.True(rs.IsDevelopChannelP());
            Assert.False(rs.IsTestChannelP());
        }

        [Fact]
        public void SetTestChannelPTest()
        {
            // Arrange
            var fs = new FakeSettings();
            IRootSettings rs = new RootSettings(getFunc: fs.Get, setFunc: fs.Set);

            // Act
            rs.SetTestChannelP();

            // Assert
            Assert.False(rs.IsBetaChannelP());
            Assert.False(rs.IsStableChannelP());
            Assert.False(rs.IsDevelopChannelP());
            Assert.True(rs.IsTestChannelP());
        }
    }
}
