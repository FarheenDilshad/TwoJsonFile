using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeedyAirly.CustomException
{
    public class OrderValidationException : Exception
    {
        public OrderValidationException() { }

        public OrderValidationException(string message) : base(message) { }

        public OrderValidationException(string message, Exception inner) : base(message, inner) { }
    }

}
