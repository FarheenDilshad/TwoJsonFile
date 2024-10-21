using SpeedyAirly.CustomException;
using SpeedyAirly.Interfaces;
using SpeedyAirly.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeedyAirly
{
    public class Order
    {
        private readonly ILogger _logger;

        public string OrderId { get; set; }
        public string Destination { get; set; }

        // Constructor accepting an ILogger, with a default to the singleton Logger instance
        public Order(ILogger logger = null)
        {
            _logger = logger ?? Logger.Instance;
        }

        public void Validate()
        {
            if (string.IsNullOrWhiteSpace(Destination))
            {
                _logger.Log("Order validation failed: Destination is null or empty.");
                throw new OrderValidationException("Destination cannot be null or empty.");
            }

            _logger.Log($"Order {OrderId} validated successfully.");
        }
    }
}