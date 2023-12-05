using Rudeus.Model;
using SharedLib.Model.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.SharedLib.Model;

namespace Test.SharedLib.Model.Settings
{
    public class AppSettingsTest
    {
        [Fact]
        public void CurrentVersionPTest()
        {
            // Arrange
            var fs = new FakeSettings();
            var testString = "1.2.3";
            IAppSettings rs = new AppSettings(getFunc: fs.Get, setFunc: fs.Set, createRegFunc: (key) => null);

            // Act
            rs.CurrentVersionP = testString;

            // Assert
            Assert.Equal(testString, rs.CurrentVersionP);
        }

        [Fact]
        public void LastDirNamePTest()
        {
            // Arrange
            var fs = new FakeSettings();
            var testString = "last";
            IAppSettings rs = new AppSettings(getFunc: fs.Get, setFunc: fs.Set, createRegFunc: (key) => null);

            // Act
            rs.LastDirNameP = testString;

            // Assert
            Assert.Equal(testString, rs.LastDirNameP);
        }


        [Fact]
        public void LastVersionDirPathPTest()
        {
            // Arrange
            var fs = new FakeSettings();
            var testString = "C:\\Program Files\\Test";
            IAppSettings rs = new AppSettings(getFunc: fs.Get, setFunc: fs.Set, createRegFunc: (key) => null);

            // Act
            rs.LastVersionDirPathP = testString;

            // Assert
            Assert.Equal(testString, rs.LastVersionDirPathP);
        }

        [Fact]
        public void LastVersionExeNamePTest()
        {
            // Arrange
            var fs = new FakeSettings();
            var testString = "Test.exe";
            IAppSettings rs = new AppSettings(getFunc: fs.Get, setFunc: fs.Set, createRegFunc: (key) => null);

            // Act
            rs.LastVersionExeNameP = testString;

            // Assert
            Assert.Equal(testString, rs.LastVersionExeNameP);
        }

        [Fact]
        public void LastVersionExePathPTest()
        {
            // Arrange
            var fs = new FakeSettings();
            var testString = "C:\\Program Files\\Test";
            var testString2 = "Test.exe";
            IAppSettings rs = new AppSettings(getFunc: fs.Get, setFunc: fs.Set, createRegFunc: (key) => null);

            // Act
            rs.LastVersionDirPathP = testString;
            rs.LastVersionExeNameP = testString2;

            // Assert
            Assert.Equal($"{testString}\\{testString2}", rs.LastVersionExePathP);
        }

        [Fact]
        public void LatestDirNamePTest()
        {
            // Arrange
            var fs = new FakeSettings();
            var testString = "latest";
            IAppSettings rs = new AppSettings(getFunc: fs.Get, setFunc: fs.Set, createRegFunc: (key) => null);

            // Act
            rs.LatestDirNameP = testString;

            // Assert
            Assert.Equal(testString, rs.LatestDirNameP);
        }

        [Fact]
        public void LatestVersionDirPathPTest()
        {
            // Arrange
            var fs = new FakeSettings();
            var testString = "C:\\Program Files\\Test";
            IAppSettings rs = new AppSettings(getFunc: fs.Get, setFunc: fs.Set, createRegFunc: (key) => null);

            // Act
            rs.LatestVersionDirPathP = testString;

            // Assert
            Assert.Equal(testString, rs.LatestVersionDirPathP);
        }

        [Fact]
        public void LatestVersionExeNamePTest()
        {
            // Arrange
            var fs = new FakeSettings();
            var testString = "Test.exe";
            IAppSettings rs = new AppSettings(getFunc: fs.Get, setFunc: fs.Set, createRegFunc: (key) => null);

            // Act
            rs.LatestVersionExeNameP = testString;

            // Assert
            Assert.Equal(testString, rs.LatestVersionExeNameP);
        }

        [Fact]
        public void LatestVersionExePathPTest()
        {
            // Arrange
            var fs = new FakeSettings();
            var testString = "C:\\Program Files\\Test";
            var testString2 = "Test.exe";
            IAppSettings rs = new AppSettings(getFunc: fs.Get, setFunc: fs.Set, createRegFunc: (key) => null);

            // Act
            rs.LastVersionDirPathP = testString;
            rs.LastVersionExeNameP = testString2;

            // Assert
            Assert.Equal($"{testString}\\{testString2}", rs.LastVersionExePathP);
        }

        [Fact]
        public void LatestVersionStatusPTest()
        {
            // Arrange
            var fs = new FakeSettings();
            var testString = "ok";
            IAppSettings rs = new AppSettings(getFunc: fs.Get, setFunc: fs.Set, createRegFunc: (key) => null);

            // Act
            rs.LatestVersionStatusP = testString;

            // Assert
            Assert.Equal(testString, rs.LatestVersionStatusP);
        }

        [Fact]
        public void UpdatingChannelPTest()
        {
            // Arrange
            var fs = new FakeSettings();
            var testString = "stable";
            IAppSettings rs = new AppSettings(getFunc: fs.Get, setFunc: fs.Set, createRegFunc: (key) => null);

            // Act
            rs.UpdatingChannelP = testString;

            // Assert
            Assert.Equal(testString, rs.UpdatingChannelP);
        }

        [Fact]
        public void SetLatestVersionStatusOkPTest()
        {
            // Arrange
            var fs = new FakeSettings();
            IAppSettings rs = new AppSettings(getFunc: fs.Get, setFunc: fs.Set, createRegFunc: (key) => null);

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
            var fs = new FakeSettings();
            IAppSettings rs = new AppSettings(getFunc: fs.Get, setFunc: fs.Set, createRegFunc: (key) => null);

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
            var fs = new FakeSettings();
            IAppSettings rs = new AppSettings(getFunc: fs.Get, setFunc: fs.Set, createRegFunc: (key) => null);

            // Act
            rs.SetLatestVersionStatusUnlaunchableP();

            // Assert
            Assert.False(rs.IsLatestVersionStatusOkP());
            Assert.False(rs.IsLatestVersionStatusDownloadedP());
            Assert.True(rs.IsLatestVersionStatusUnlaunchableP());
        }

    }
}
