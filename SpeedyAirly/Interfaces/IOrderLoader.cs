using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeedyAirly.Interfaces
{
    public interface IOrderLoader
    {
        Dictionary<string, Order> LoadOrders();
    }
}
