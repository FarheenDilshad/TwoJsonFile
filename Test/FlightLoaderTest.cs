using SpeedyAirly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Test
{
    public class FlightLoaderTests
    {
        [Fact]
        public void LoadFlights_ShouldReturnListOfFlights()
        {
            // Arrange
            var flightLoader = new FlightLoader();

            // Act
            var flights = flightLoader.LoadFlights();

            // Assert
            Assert.NotNull(flights); // Ensure that the flight list is not null
            Assert.IsType<List<Flight>>(flights); // Ensure that the returned object is a list of Flight
        }

        [Fact]
        public void LoadFlights_ShouldReturnCorrectNumberOfFlights()
        {
            // Arrange
            var flightLoader = new FlightLoader();

            // Act
            var flights = flightLoader.LoadFlights();

            // Assert
            Assert.Equal(6, flights.Count); // Ensure that the correct number of flights are loaded
        }

        [Fact]
        public void LoadFlights_ShouldReturnCorrectFlightDetails()
        {
            // Arrange
            var flightLoader = new FlightLoader();

            // Act
            var flights = flightLoader.LoadFlights();

            // Assert
            Assert.Collection(flights,
                flight => AssertFlightDetails(flight, 1, "YUL", "YYZ", 1),
                flight => AssertFlightDetails(flight, 2, "YUL", "YYC", 1),
                flight => AssertFlightDetails(flight, 3, "YUL", "YVR", 1),
                flight => AssertFlightDetails(flight, 4, "YUL", "YYZ", 2),
                flight => AssertFlightDetails(flight, 5, "YUL", "YYC", 2),
                flight => AssertFlightDetails(flight, 6, "YUL", "YVR", 2)
            );
        }

        
        // Helper method to verify flight details
        private void AssertFlightDetails(Flight flight, int expectedFlightNumber, string expectedDeparture, string expectedArrival, int expectedDay)
        {
            Assert.Equal(expectedFlightNumber, flight.FlightNumber);
            Assert.Equal(expectedDeparture, flight.Departure);
            Assert.Equal(expectedArrival, flight.Arrival);
            Assert.Equal(expectedDay, flight.Day);
        }
    }

}
