using SpeedyAirly;
using SpeedyAirly.CustomException;
using SpeedyAirly.Interfaces;
using SpeedyAirly.Shared;
using System.Security.Cryptography.X509Certificates;

class Program
{
    public static void Main(string[] args)
    {
        var logger = Logger.Instance;

        try
        {
            logger.Log("Application started.");

            // Instantiate the FileReader to be used by OrderLoader
            IFileReader fileReader = new FileReader();

            // Instantiate OrderLoader using FileReader, and assign it to IOrderLoader
            IOrderLoader orderLoader = new OrderLoader(fileReader);

            // Instantiate FlightLoader
            IFlightLoader flightLoader = new FlightLoader();

            // Inject dependencies into FlightScheduler
            var scheduler = new FlightScheduler(flightLoader, orderLoader);

            // User Story 1: Display flight schedule
            scheduler.DisplayFlightScheduleDetails();

            // User Story 2: Schedule orders and display itineraries
            scheduler.ScheduleFlightOrders();
        }
        catch (FlightSchedulingException ex)
        {
            logger.Log($"Flight scheduling issue: {ex.Message}");
            Console.WriteLine($"Flight scheduling issue: {ex.Message}");
        }
        catch (OrderValidationException ex)
        {
            logger.Log($"Order validation issue: {ex.Message}");
            Console.WriteLine($"Order validation issue: {ex.Message}");
        }
        catch (Exception ex)
        {
            logger.Log($"An unexpected error occurred: {ex.Message}");
            Console.WriteLine($"An unexpected error occurred: {ex.Message}");
        }
        finally
        {
            logger.Log("Application ended.");
        }

    }

}
