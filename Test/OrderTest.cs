using Moq;
using SpeedyAirly.CustomException;
using SpeedyAirly.Shared;
using SpeedyAirly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using SpeedyAirly.Interfaces;

namespace Test
{
    public class OrderTest
    {
        private readonly Mock<ILogger> _mockLogger;

        public OrderTest()
        {
            _mockLogger = new Mock<ILogger>(); // Mock the ILogger interface
        }

        [Fact]
        public void Validate_ShouldThrowOrderValidationException_WhenDestinationIsNull()
        {
            // Arrange
            var order = new Order(_mockLogger.Object)
            {
                OrderId = "ORD001",
                Destination = null // Destination is null, validation should fail
            };

            // Act & Assert
            var exception = Assert.Throws<OrderValidationException>(() => order.Validate());
            Assert.Equal("Destination cannot be null or empty.", exception.Message);

            // Verify that the logger logged the validation failure
            _mockLogger.Verify(l => l.Log("Order validation failed: Destination is null or empty."), Times.Once);
        }

        [Fact]
        public void Validate_ShouldLogSuccess_WhenOrderIsValid()
        {
            // Arrange
            var order = new Order(_mockLogger.Object)
            {
                OrderId = "ORD002",
                Destination = "YYZ" // Valid order
            };

            // Act
            order.Validate();

            // Assert: No exception should be thrown, and validation should log success
            _mockLogger.Verify(l => l.Log($"Order {order.OrderId} validated successfully."), Times.Once);
        }

        // Additional test cases can follow the same pattern...
    }
}
