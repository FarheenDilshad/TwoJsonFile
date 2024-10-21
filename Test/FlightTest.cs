using SpeedyAirly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Test
{
    public class FlightTests
    {
        [Fact]
        public void HasCapacity_ShouldReturnTrue_WhenOrdersAreLessThanCapacity()
        {
            // Arrange
            var flight = new Flight
            {
                FlightNumber = 1,
                Departure = "YUL",
                Arrival = "YYZ",
                Day = 1,
                Capacity = 5,
                Orders = new List<string> { "ORD001", "ORD002" } // 2 orders, capacity is 5
            };

            // Act
            var hasCapacity = flight.HasCapacity();

            // Assert
            Assert.True(hasCapacity); // There is still capacity
        }

        [Fact]
        public void HasCapacity_ShouldReturnFalse_WhenOrdersAreEqualToCapacity()
        {
            // Arrange
            var flight = new Flight
            {
                FlightNumber = 1,
                Departure = "YUL",
                Arrival = "YYZ",
                Day = 1,
                Capacity = 3,
                Orders = new List<string> { "ORD001", "ORD002", "ORD003" } // 3 orders, capacity is 3
            };

            // Act
            var hasCapacity = flight.HasCapacity();

            // Assert
            Assert.False(hasCapacity); // No capacity left
        }

        [Fact]
        public void AddOrder_ShouldAddOrderSuccessfully_WhenCapacityIsAvailable()
        {
            // Arrange
            var flight = new Flight
            {
                FlightNumber = 1,
                Departure = "YUL",
                Arrival = "YYZ",
                Day = 1,
                Capacity = 3,
                Orders = new List<string> { "ORD001" } // 1 order, capacity is 3
            };

            // Act
            flight.AddFlightOrder("ORD002");

            // Assert
            Assert.Contains("ORD002", flight.Orders); // The new order should be added
        }

        [Fact]
        public void AddOrder_ShouldThrowInvalidOperationException_WhenCapacityIsReached()
        {
            // Arrange
            var flight = new Flight
            {
                FlightNumber = 1,
                Departure = "YUL",
                Arrival = "YYZ",
                Day = 1,
                Capacity = 2,
                Orders = new List<string> { "ORD001", "ORD002" } // Capacity reached
            };

            // Act & Assert
            var exception = Assert.Throws<InvalidOperationException>(() => flight.AddFlightOrder("ORD003"));
            Assert.Equal("Flight capacity has been reached.", exception.Message);
        }

        [Fact]
        public void AddOrder_ShouldThrowArgumentException_WhenOrderIdIsNullOrEmpty()
        {
            // Arrange
            var flight = new Flight
            {
                FlightNumber = 1,
                Departure = "YUL",
                Arrival = "YYZ",
                Day = 1,
                Capacity = 3
            };

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => flight.AddFlightOrder(null));
            Assert.Equal("Order ID cannot be null or empty.", exception.Message);
        }

        [Fact]
        public void AddOrder_ShouldThrowArgumentException_WhenOrderIdIsWhitespace()
        {
            // Arrange
            var flight = new Flight
            {
                FlightNumber = 1,
                Departure = "YUL",
                Arrival = "YYZ",
                Day = 1,
                Capacity = 3
            };

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => flight.AddFlightOrder("   "));
            Assert.Equal("Order ID cannot be null or empty.", exception.Message);
        }

        [Fact]
        public void OrdersList_ShouldBeEmptyInitially_WhenFlightIsCreated()
        {
            // Arrange
            var flight = new Flight
            {
                FlightNumber = 1,
                Departure = "YUL",
                Arrival = "YYZ",
                Day = 1
            };

            // Act
            var initialOrders = flight.Orders;

            // Assert
            Assert.Empty(initialOrders); // Orders list should be empty initially
        }

        [Fact]
        public void Capacity_ShouldDefaultTo20_WhenNotExplicitlySet()
        {
            // Arrange
            var flight = new Flight
            {
                FlightNumber = 1,
                Departure = "YUL",
                Arrival = "YYZ",
                Day = 1
            };

            // Act
            var capacity = flight.Capacity;

            // Assert
            Assert.Equal(20, capacity); // Default capacity is 20
        }
    }

}
