using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RudeusTest.RudeusSharedLib.API.DummyResponse;

namespace RudeusTest.RudeusSharedLib.API
{
    public class TestRemoteAPI
    {
        // TODO: Remoteに影響されないテストの作成方法
        [Theory]
        [DummyResponseString("RudeusSharedLib\\API\\DummyResponse\\OK.json")]
        public void TestRegisterDevice(string dummyResponse)
        {
            // Arrange
            // Act
            // Assert
        }
        
    }
}
