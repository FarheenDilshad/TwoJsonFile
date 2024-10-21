using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeedyAirly
{
    public class Flight
    {
        public int FlightNumber { get; set; }
        public string Departure { get; set; }
        public string Arrival { get; set; }
        public int Day { get; set; }
        public int Capacity { get; set; } = 20; // Each flight has a capacity of 20 boxes
        public List<string> Orders { get; set; } = new List<string>();

        public bool HasCapacity()
        {
            // Check if the current number of orders is less than the capacity of 20
            return Orders != null && Orders.Count < Capacity;
        }

       
        public void AddFlightOrder(string orderId)

        {
            if (string.IsNullOrWhiteSpace(orderId))
            {
                throw new ArgumentException("Order ID cannot be null or empty.");
            }

            if (HasCapacity())
            {
                Orders.Add(orderId);
            }
            else
            {
                throw new InvalidOperationException("Flight capacity has been reached.");
            }
        }
    }

}
