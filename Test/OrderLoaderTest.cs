using Moq;
using Newtonsoft.Json;
using SpeedyAirly.Interfaces;
using SpeedyAirly;
using SpeedyAirly.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Test
{
    public class OrderLoaderTest
    {
        private readonly Mock<IFileReader> _mockFileReader;

        public OrderLoaderTest()
        {
            _mockFileReader = new Mock<IFileReader>();
        }

        [Fact]
        public void LoadOrders_ShouldUseConstantsFilePath()
        {
            // Arrange: Define a mock JSON response
            string mockJson = @"{
                ""ORD001"": { ""OrderId"": ""ORD001"", ""Destination"": ""YYZ"" },
                ""ORD002"": { ""OrderId"": ""ORD002"", ""Destination"": ""YVR"" }
            }";

            // Mock the file reader to return the mock JSON using the constant path
            _mockFileReader.Setup(fr => fr.ReadAllText(Constants.OrdersJsonFilePath)).Returns(mockJson);

            var orderLoader = new OrderLoader(_mockFileReader.Object);

            // Act: Load the orders
            var result = orderLoader.LoadOrders();

            // Assert: Check that the result is correct
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal("YYZ", result["ORD001"].Destination);
            Assert.Equal("YVR", result["ORD002"].Destination);
        }
    }


}
