using Rudeus.Model;
using SharedLib.Model.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Model;

namespace Test.SharedLib.Model.Settings
{
    public class RootSettingsTest
    {
        [Fact]
        public void CurrentVersionPTest()
        {
            // Arrange
            var testString = "1.2.3";
            IRootSettings rs = new RootSettings();

            // Act
            rs.CurrentVersionP = testString;

            // Assert
            Assert.Equal(testString, rs.CurrentVersionP);
        }

        [Fact]
        public void UpdatingChannelPTest()
        {
            // Arrange
            var testString = "stable";
            IRootSettings rs = new RootSettings();

            // Act
            rs.UpdatingChannelP = testString;

            // Assert
            Assert.Equal(testString, rs.UpdatingChannelP);
        }

        [Fact]
        public void AccessTokenPTest()
        {
            // Arrange
            var testString = "test_access_token";
            IRootSettings rs = new RootSettings();

            // Act
            rs.AccessTokenP = testString;

            // Assert
            Assert.Equal(testString, rs.AccessTokenP);
        }

        [Fact]
        public void DeviceIdPTest()
        {
            // Arrange
            var testString = "test_device_id";
            IRootSettings rs = new RootSettings();

            // Act
            rs.DeviceIdP = testString;

            // Assert
            Assert.Equal(testString, rs.DeviceIdP);
        }

        [Fact]
        public void FirstHostnamePTest()
        {
            // Arrange
            var testString = "test_hostname";
            IRootSettings rs = new RootSettings();

            // Act
            rs.FirstHostnameP = testString;

            // Assert
            Assert.Equal(testString, rs.FirstHostnameP);
        }

        [Fact]
        public void HostnamePTest()
        {
            // Arrange
            var testString = "test_hostname";
            IRootSettings rs = new RootSettings();

            // Act
            rs.HostnameP = testString;

            // Assert
            Assert.Equal(testString, rs.HostnameP);
        }

        [Fact]
        public void UsernamePTest()
        {
            // Arrange
            var testString = "test_username";
            IRootSettings rs = new RootSettings();

            // Act
            rs.UsernameP = testString;

            // Assert
            Assert.Equal(testString, rs.UsernameP);
        }

        [Fact]
        public void LabelIdPTest()
        {
            // Arrange
            var testString = "test_label_id";
            IRootSettings rs = new RootSettings();

            // Act
            rs.LabelIdP = testString;

            // Assert
            Assert.Equal(testString, rs.LabelIdP);
        }

        [Fact]
        public void SpecPTest()
        {
            // Arrange
            var testString = "test_label_name";
            IRootSettings rs = new RootSettings();

            // Act
            rs.SpecP = testString;

            // Assert
            Assert.Equal(testString, rs.SpecP);
        }

        [Fact]
        public void CpuNamePTest()
        {
            // Arrange
            var testString = "test_cpu_name";
            IRootSettings rs = new RootSettings();

            // Act
            rs.CpuNameP = testString;

            // Assert
            Assert.Equal(testString, rs.CpuNameP);
        }

        [Fact]
        public void MemoryPTest()
        {
            // Arrange
            var testString = "test_memory";
            IRootSettings rs = new RootSettings();

            // Act
            rs.MemoryP = testString;

            // Assert
            Assert.Equal(testString, rs.MemoryP);
        }

        [Fact]
        public void CDrivePTest()
        {
            // Arrange
            var testString = "test_c_drive";
            IRootSettings rs = new RootSettings();

            // Act
            rs.CDriveP = testString;

            // Assert
            Assert.Equal(testString, rs.CDriveP);
        }

        [Fact]
        public void OSPTest()
        {
            // Arrange
            var testString = "test_os";
            IRootSettings rs = new RootSettings();

            // Act
            rs.OSP = testString;

            // Assert
            Assert.Equal(testString, rs.OSP);
        }

        [Fact]
        public void OSVersionPTest()
        {
            // Arrange
            var testString = "test_os_version";
            IRootSettings rs = new RootSettings();

            // Act
            rs.OSVersionP = testString;

            // Assert
            Assert.Equal(testString, rs.OSVersionP);
        }

        [Fact]
        public void WithSecurePTest()
        {
            // Arrange
            var testString = "test_withsecure";
            IRootSettings rs = new RootSettings();

            // Act
            rs.WithSecureP = testString;

            // Assert
            Assert.Equal(testString, rs.WithSecureP);
        }

        [Fact]
        public void IsBetaChannelPTest()
        {
            // Arrange
            IRootSettings rs = new RootSettings();
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
            IRootSettings rs = new RootSettings();
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
        public void IsDevelopChannelPTest()
        {
            // Arrange
            IRootSettings rs = new RootSettings();
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
            IRootSettings rs = new RootSettings();
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
            IRootSettings rs = new RootSettings();

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
            IRootSettings rs = new RootSettings();

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
            IRootSettings rs = new RootSettings();

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
            IRootSettings rs = new RootSettings();

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
