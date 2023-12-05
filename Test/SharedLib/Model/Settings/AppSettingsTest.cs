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
    public class AppSettingsTest
    {
        [Fact]
        public void CurrentVersionPTest()
        {
            // Arrange
            var testString = "1.2.3";
            IAppSettings rs = new AppSettings();

            // Act
            rs.CurrentVersionP = testString;

            // Assert
            Assert.Equal(testString, rs.CurrentVersionP);
        }

        [Fact]
        public void LastDirNamePTest()
        {
            // Arrange
            var testString = "last";
            IAppSettings rs = new AppSettings();

            // Act
            rs.LastDirNameP = testString;

            // Assert
            Assert.Equal(testString, rs.LastDirNameP);
        }


        [Fact]
        public void LastVersionDirPathPTest()
        {
            // Arrange
            var testString = "C:\\Program Files\\Test";
            IAppSettings rs = new AppSettings();

            // Act
            rs.LastVersionDirPathP = testString;

            // Assert
            Assert.Equal(testString, rs.LastVersionDirPathP);
        }

        [Fact]
        public void LastVersionExeNamePTest()
        {
            // Arrange
            var testString = "Test.exe";
            IAppSettings rs = new AppSettings();

            // Act
            rs.LastVersionExeNameP = testString;

            // Assert
            Assert.Equal(testString, rs.LastVersionExeNameP);
        }

        [Fact]
        public void LastVersionExePathPTest()
        {
            // Arrange
            var testString = "C:\\Program Files\\Test\\Test.exe";
            IAppSettings rs = new AppSettings();

            // Act
            rs.LastVersionExePathP = testString;

            // Assert
            Assert.Equal(testString, rs.LastVersionExePathP);
        }

        [Fact]
        public void LatestDirNamePTest()
        {
            // Arrange
            var testString = "latest";
            IAppSettings rs = new AppSettings();

            // Act
            rs.LatestDirNameP = testString;

            // Assert
            Assert.Equal(testString, rs.LatestDirNameP);
        }

        [Fact]
        public void LatestVersionDirPathPTest()
        {
            // Arrange
            var testString = "C:\\Program Files\\Test";
            IAppSettings rs = new AppSettings();

            // Act
            rs.LatestVersionDirPathP = testString;

            // Assert
            Assert.Equal(testString, rs.LatestVersionDirPathP);
        }

        [Fact]
        public void LatestVersionExeNamePTest()
        {
            // Arrange
            var testString = "Test.exe";
            IAppSettings rs = new AppSettings();

            // Act
            rs.LatestVersionExeNameP = testString;

            // Assert
            Assert.Equal(testString, rs.LatestVersionExeNameP);
        }

        [Fact]
        public void LatestVersionExePathPTest()
        {
            // Arrange
            var testString = "C:\\Program Files\\Test\\Test.exe";
            IAppSettings rs = new AppSettings();

            // Act
            rs.LatestVersionExePathP = testString;

            // Assert
            Assert.Equal(testString, rs.LatestVersionExePathP);
        }

        [Fact]
        public void LatestVersionStatusPTest()
        {
            // Arrange
            var testString = "ok";
            IAppSettings rs = new AppSettings();

            // Act
            rs.LatestVersionStatusP = testString;

            // Assert
            Assert.Equal(testString, rs.LatestVersionStatusP);
        }

        [Fact]
        public void UpdatingChannelPTest()
        {
            // Arrange
            var testString = "stable";
            IAppSettings rs = new AppSettings();

            // Act
            rs.UpdatingChannelP = testString;

            // Assert
            Assert.Equal(testString, rs.UpdatingChannelP);
        }

        [Fact]
        public void SetLatestVersionStatusOkPTest()
        {
            // Arrange
            IAppSettings rs = new AppSettings();

            // Act
            rs.SetLatestVersionStatusOkP();

            // Assert
            Assert.True(rs.IsLatestVersionStatusOkP());
            Assert.False(rs.IsLatestVersionStatusDownloadedP());
            Assert.False(rs.IsLatestVersionStatusUnlaunchableP());
        }

        [Fact]
        public void SetLatestVersionStatusDownloadedPTest()
        {
            // Arrange
            IAppSettings rs = new AppSettings();

            // Act
            rs.SetLatestVersionStatusDownloadedP();

            // Assert
            Assert.False(rs.IsLatestVersionStatusOkP());
            Assert.True(rs.IsLatestVersionStatusDownloadedP());
            Assert.False(rs.IsLatestVersionStatusUnlaunchableP());
        }

        [Fact]
        public void SetLatestVersionStatusUnlaunchablePTest()
        {
            // Arrange
            IAppSettings rs = new AppSettings();

            // Act
            rs.SetLatestVersionStatusUnlaunchableP();

            // Assert
            Assert.False(rs.IsLatestVersionStatusOkP());
            Assert.False(rs.IsLatestVersionStatusDownloadedP());
            Assert.True(rs.IsLatestVersionStatusUnlaunchableP());
        }

    }
}
