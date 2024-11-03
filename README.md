**AirTek-SpeedyAir-ly Project:**
**Introduction**
Provide an overview of the console-based .NET application. Briefly describe the purpose of the application and its main functionalities.
•	Application Name:AirTek-SpeedAir-ly Project
•	Description: To Add the order to the schedule flight for the respective destination
•	Technology: .NET Core/.NET Framework
•	Purpose: To develop Console based application to display Flight as per the story#1 and Add the order on specific day to the respective flight and if the capacity Exceeds need to schedule it for next day.
**Tech Details:**
Dependency Injection has been added.
Singleton Design Pattern has been used for logging
Customise Exception has been used
Exception Handling is being handled well to debug the issue.
Data Validation is added.
Added Unit Test case with proper Code Coverage to ensure test cases execution helps in future enhancement.


**Scenario **
SpeedyAir.ly is a brand-new company that aims to provide efficient and fast air freight services; they currently 
have 3 
planes the planes are scheduled to fly daily at noon. For this exercise, there are only 2 days of flights scheduled. 
Day 1: 
Flight 1: Montreal airport (YUL) to Toronto (YYZ) 
Flight 2: Montreal (YUL) to Calgary (YYC) 
Flight 3: Montreal (YUL) to Vancouver (YVR) 
Day 2: 
Flight 4: Montreal airport (YUL) to Toronto (YYZ) 
Flight 5: Montreal (YUL) to Calgary (YYC) 
Flight 6: Montreal (YUL) to Vancouver (YVR) 
With each flight returning to the YUL at midnight. 
Each plane has a capacity of 20 boxes each. 
The company’s sales department has been able to sell 99 orders that are sending boxes to Toronto, Calgary, and 
Vancouver, these orders are found in the attached json file. Each box represents 1 order. 
As a member of the software engineering department you are asked to develop an application that can automate 
the 
process of determining which boxes to load on each flight. 
User Stories - Take-Home 
USER STORY #1 
As an inventory management user, I can load a flight schedule similar to the one listed in the Scenario above. For 
this story you do not yet need to load the orders. I can also list out the loaded flight schedule on the console. 
 
Expected output: 
Flight: 1, departure: YUL, arrival: YYZ, day: 1 
... 
Flight: 6, departure: <departure_city>, arrival: <arrival_city>, day: x 
USER STORY #2 
As an inventory management user, I can generate flight itineraries by scheduling a batch of orders. These flights 
can be used to determine shipping capacity. 
 Use the json file attached to load the given orders. 
 The orders listed in the json file are listed in priority order ie. 1..N 
Expected output: 
order: order-001, flightNumber: 1, departure: <departure_city>, arrival: <arrival_city>, day: x 
... 
order: order-099, flightNumber: 1, departure: <departure_city>, arrival: <arrival_city>, day: x 
if an order has not yet been scheduled, output:
order: order-X, flightNumber: not scheduled


**Future Enhancements:**

Dynamic Data Source: You can now load flight data from a JSON file (or any other source) instead of hardcoding the flight details. This makes it more scalable as adding flights doesn't require changes to the code.

Dependency Injection: By injecting IFlightDataSource, the FlightLoader can load flight data from various sources (JSON, database, etc.) without altering its logic. This makes the class more modular and extensible.

Readability and Maintainability: The class is easier to maintain, as adding or changing flights involves simply updating the data source (e.g., the JSON file) without needing to modify the code.

Scalability: This approach can easily scale by adding new types of flight data sources (e.g., from a web API or a database) or updating the flight schedule without touching the core logic.

Optimise the performance:

UpdateIFlightLoader to have an async method: LoadFlightsAsync.
Update FlightLoader to implement the async method.
Modify FlightScheduler to use async flight and order loading.
Update the Program class to use async calls.

**To Run the application :**
 Update the filepath of Order JSON and Add the prepoer logging path.
[coding-assigment-orders.json](https://github.com/user-attachments/files/17612111/coding-assigment-orders.json)
[coding-assigment-order2.json](https://github.com/user-attachments/files/17612110/coding-assigment-order2.json)
