using Newtonsoft.Json;
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
    public class FlightScheduler
    {
        private List<Flight> flights;
        private Dictionary<string, Order> orders;
        private readonly Logger logger = Logger.Instance; // Singleton logger instance

        public FlightScheduler(IFlightLoader flightLoader, IOrderLoader orderLoader)
        {
            if (flightLoader == null) throw new ArgumentNullException(nameof(flightLoader));
            if (orderLoader == null) throw new ArgumentNullException(nameof(orderLoader));

            flights = flightLoader.LoadFlights();
            if (flights == null || flights.Count == 0)
            {
                logger.Log("No flights available.");
                throw new FlightSchedulingException("No flights available.");
            }

            orders = orderLoader.LoadOrders();
            if (orders == null || orders.Count == 0)
            {
                logger.Log("No orders available.");
                throw new FlightSchedulingException("No orders available.");
            }

            logger.Log("FlightScheduler initialized successfully.");
        }

        public void DisplayFlightScheduleDetails()
          
        {
            if (flights == null || flights.Count == 0)
            {
                logger.Log("No flights scheduled.");
                Console.WriteLine("No flights scheduled.");
                return;
            }

            foreach (var flight in flights)
            {
                string message = $"Flight: {flight.FlightNumber}, departure: {flight.Departure}, arrival: {flight.Arrival}, day: {flight.Day}";
                Console.WriteLine(message);
                logger.Log(message);
            }
        }

    
       public void ScheduleFlightOrders()
        {
            if (orders == null || orders.Count == 0)
            {
                logger.Log("No orders to schedule.");
                Console.WriteLine("No orders to schedule.");
                return;
            }

            foreach (var orderPair in orders)
            {
                var order = orderPair.Value;
                try
                {
                    order?.Validate(); // Null check and validation
                    var flight = FindFlightForOrder(order);

                    if (flight != null)
                    {
                        flight.AddFlightOrder(orderPair.Key);
                        string message = $"order: {orderPair.Key}, flightNumber: {flight.FlightNumber}, departure: {flight.Departure}, arrival: {flight.Arrival}, day: {flight.Day}";
                        Console.WriteLine(message);
                        logger.Log(message);
                    }
                    else
                    {
                        string message = $"Order: {orderPair.Key}, FlightNumber: not scheduled (no available flight or capacity reached).";
                        Console.WriteLine(message);
                        logger.Log(message);
                    }
                }
                catch (OrderValidationException ex)
                {
                    logger.Log($"Order validation error for {orderPair.Key}: {ex.Message}");
                    Console.WriteLine($"Order validation error for {orderPair.Key}: {ex.Message}");
                }
                catch (FlightSchedulingException ex)
                {
                    logger.Log($"Flight scheduling error: {ex.Message}");
                    Console.WriteLine($"Flight scheduling error: {ex.Message}");
                }
                catch (Exception ex)
                {
                    logger.Log($"An unexpected error occurred while scheduling order {orderPair.Key}: {ex.Message}");
                    Console.WriteLine($"An unexpected error occurred while scheduling order {orderPair.Key}: {ex.Message}");
                }
            }
        }

        public Flight FindFlightForOrder(Order order)
        {
            if (order == null) return null;

            foreach (var flight in flights)
            {
                if (flight.Arrival == order.Destination && flight.HasCapacity())
                {
                    return flight;
                }
            }
            return null; // No suitable flight found
        }

    }
}