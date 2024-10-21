using Newtonsoft.Json;
using SpeedyAirly.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpeedyAirly.Shared;

namespace SpeedyAirly
{
    // Concrete implementation for loading orders
    public class OrderLoader : IOrderLoader
    {
        private readonly IFileReader _fileReader;

        public OrderLoader(IFileReader fileReader)

        {
            _fileReader = fileReader;
        }

        public Dictionary<string, Order> LoadOrders()
        {
            // Use the constant path from Constants
            string json = _fileReader.ReadAllText(Constants.OrdersJsonFilePath);
            return JsonConvert.DeserializeObject<Dictionary<string, Order>>(json);
        }
    }
}
