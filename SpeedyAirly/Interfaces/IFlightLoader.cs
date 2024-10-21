using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using SpeedyAirly;

// Interface for loading flights
public interface IFlightLoader
{
    List<Flight> LoadFlights();
}