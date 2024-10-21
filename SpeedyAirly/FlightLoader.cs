using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeedyAirly
{
    public class FlightLoader : IFlightLoader
    {
        public List<Flight> LoadFlights()
        {
            // Load the flight schedule with predefined flights
            return new List<Flight>
        {
            new Flight { FlightNumber = 1, Departure = "YUL", Arrival = "YYZ", Day = 1 },
            new Flight { FlightNumber = 2, Departure = "YUL", Arrival = "YYC", Day = 1 },
            new Flight { FlightNumber = 3, Departure = "YUL", Arrival = "YVR", Day = 1 },
            new Flight { FlightNumber = 4, Departure = "YUL", Arrival = "YYZ", Day = 2 },
            new Flight { FlightNumber = 5, Departure = "YUL", Arrival = "YYC", Day = 2 },
            new Flight { FlightNumber = 6, Departure = "YUL", Arrival = "YVR", Day = 2 }
        };
        }
    }
}
