using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeedyAirly.CustomException
{
    public class FlightSchedulingException : Exception
    {
        public FlightSchedulingException() { }

        public FlightSchedulingException(string message) : base(message) { }

        public FlightSchedulingException(string message, Exception inner) : base(message, inner) { }
    }
}
