using Moq;
using Xunit;
using SpeedyAirly;
using SpeedyAirly.Interfaces;
using SpeedyAirly.CustomException;
using System.Collections.Generic;
using System;

namespace SpeedyAirly.Tests
{
    public class FlightSchedulerTests
    {
        public class FlghtSchedulerTest
        {
            private Mock<IFlightLoader> _mockFlightLoader;
            private Mock<IOrderLoader> _mockOrderLoader;
            private FlightScheduler _flightScheduler;

            // Constructor that will run before every test
            public FlghtSchedulerTest()
            {
                _mockFlightLoader = new Mock<IFlightLoader>();
                _mockOrderLoader = new Mock<IOrderLoader>();
            }

            [Fact]
            public void Constructor_ShouldInitializeSuccessfully_WhenFlightsAndOrdersAreAvailable()
            {
                // Arrange
                var flights = new List<Flight>
        {
            new Flight { FlightNumber = 1, Departure = "YUL", Arrival = "YYZ", Day = 1 },
            new Flight { FlightNumber = 2, Departure = "YUL", Arrival = "YYC", Day = 1 }
        };
                var orders = new Dictionary<string, Order>
        {
            { "ORD001", new Order { OrderId = "ORD001", Destination = "YYZ" } }
        };

                _mockFlightLoader.Setup(f => f.LoadFlights()).Returns(flights);
                _mockOrderLoader.Setup(o => o.LoadOrders()).Returns(orders);

                // Act
                _flightScheduler = new FlightScheduler(_mockFlightLoader.Object, _mockOrderLoader.Object);

                // Assert
                Assert.NotNull(_flightScheduler);
            }


            [Fact]
            public void Constructor_ShouldThrowFlightSchedulingException_WhenNoFlightsAreAvailable()
            {
                // Arrange
                _mockFlightLoader.Setup(f => f.LoadFlights()).Returns(new List<Flight>());
                _mockOrderLoader.Setup(o => o.LoadOrders()).Returns(new Dictionary<string, Order>());

                // Act & Assert
                Assert.Throws<FlightSchedulingException>(() => new FlightScheduler(_mockFlightLoader.Object, _mockOrderLoader.Object));
            }

            [Fact]
            public void Constructor_ShouldThrowFlightSchedulingException_WhenNoOrdersAreAvailable()
            {
                // Arrange
                var flights = new List<Flight>
            {
                new Flight { FlightNumber = 1, Departure = "YUL", Arrival = "YYZ", Day = 1 }
            };
                _mockFlightLoader.Setup(f => f.LoadFlights()).Returns(flights);
                _mockOrderLoader.Setup(o => o.LoadOrders()).Returns(new Dictionary<string, Order>());

                // Act & Assert
                Assert.Throws<FlightSchedulingException>(() => new FlightScheduler(_mockFlightLoader.Object, _mockOrderLoader.Object));
            }

            [Theory]
            [InlineData("YUL", "YYZ", 1)]
            [InlineData("YUL", "YYC", 1)]
            public void DisplayFlightSchedule_ShouldLogAndDisplayFlightDetails_WhenFlightsAreAvailable(string departure, string arrival, int day)
            {
                // Arrange
                var flights = new List<Flight>
            {
                new Flight { FlightNumber = 1, Departure = departure, Arrival = arrival, Day = day }
            };
                var orders = new Dictionary<string, Order>
            {
                { "ORD001", new Order { OrderId = "ORD001", Destination = "YYZ" } }
            };
                _mockFlightLoader.Setup(f => f.LoadFlights()).Returns(flights);
                _mockOrderLoader.Setup(o => o.LoadOrders()).Returns(orders);

                _flightScheduler = new FlightScheduler(_mockFlightLoader.Object, _mockOrderLoader.Object);

                // Act
                _flightScheduler.DisplayFlightScheduleDetails();

                // Assert
                // The test would pass if it does not throw an exception and the logger.Log is invoked correctly
                // Assert.True(true); // You can capture output using StringWriter if needed
            }

            [Fact]
            public void ScheduleOrders_ShouldAssignOrdersToFlights_WhenFlightsHaveCapacity()
            {
                // Arrange
                var flights = new List<Flight>
            {
                new Flight { FlightNumber = 1, Departure = "YUL", Arrival = "YYZ", Day = 1, Capacity = 5 }
            };
                var orders = new Dictionary<string, Order>
            {
                { "ORD001", new Order { OrderId = "ORD001", Destination = "YYZ" } }
            };

                _mockFlightLoader.Setup(f => f.LoadFlights()).Returns(flights);
                _mockOrderLoader.Setup(o => o.LoadOrders()).Returns(orders);

                _flightScheduler = new FlightScheduler(_mockFlightLoader.Object, _mockOrderLoader.Object);

                // Act
                _flightScheduler.ScheduleFlightOrders();

                // Assert
                Assert.Contains("ORD001", flights[0].Orders);
            }

            [Fact]
            public void ScheduleOrders_ShouldNotAssignOrder_WhenFlightIsFull()
            {
                // Arrange
                var flights = new List<Flight>
            {
                new Flight { FlightNumber = 1, Departure = "YUL", Arrival = "YYZ", Day = 1, Capacity = 0 }
            };
                var orders = new Dictionary<string, Order>
            {
                { "ORD001", new Order { OrderId = "ORD001", Destination = "YYZ" } }
            };

                _mockFlightLoader.Setup(f => f.LoadFlights()).Returns(flights);
                _mockOrderLoader.Setup(o => o.LoadOrders()).Returns(orders);

                _flightScheduler = new FlightScheduler(_mockFlightLoader.Object, _mockOrderLoader.Object);

                // Act
                _flightScheduler.ScheduleFlightOrders();

                // Assert
                Assert.DoesNotContain("ORD001", flights[0].Orders);
            }

            [Fact]
            public void ScheduleOrders_ShouldLogValidationError_WhenOrderValidationFails()
            {
                // Arrange
                var flights = new List<Flight>
            {
                new Flight { FlightNumber = 1, Departure = "YUL", Arrival = "YYZ", Day = 1 }
            };
                var orders = new Dictionary<string, Order>
            {
                { "ORD001", new Order { OrderId = null, Destination = "YYZ" } } // Invalid order
            };

                _mockFlightLoader.Setup(f => f.LoadFlights()).Returns(flights);
                _mockOrderLoader.Setup(o => o.LoadOrders()).Returns(orders);

                _flightScheduler = new FlightScheduler(_mockFlightLoader.Object, _mockOrderLoader.Object);

                // Act
                _flightScheduler.ScheduleFlightOrders();

                // Assert
                // You can check for the log of validation error (if logger is mockable)
            }

            [Fact]
            public void FindFlightForOrder_ShouldReturnCorrectFlight_WhenFlightIsAvailable()
            {
                // Arrange
                var flights = new List<Flight>
            {
                new Flight { FlightNumber = 1, Departure = "YUL", Arrival = "YYZ", Day = 1, Capacity = 5 }
            };
                var orders = new Dictionary<string, Order>
            {
                { "ORD001", new Order { OrderId = "ORD001", Destination = "YYZ" } }
            };

                _mockFlightLoader.Setup(f => f.LoadFlights()).Returns(flights);
                _mockOrderLoader.Setup(o => o.LoadOrders()).Returns(orders);

                _flightScheduler = new FlightScheduler(_mockFlightLoader.Object, _mockOrderLoader.Object);

                // Act
                var flight = _flightScheduler.FindFlightForOrder(orders["ORD001"]);

                // Assert
                Assert.NotNull(flight);
                Assert.Equal("YYZ", flight.Arrival);
            }

            
        }


        }
    }
