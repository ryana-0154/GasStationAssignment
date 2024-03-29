﻿// Author: Ryan Alderton
// SID: 1609275

using System;

namespace GasStationForms
{
    public class Pump
    {
        // Constants for the tank size in liters
        const int CAR_TANK = 40;
        const int VAN_TANK = 80;
        const int HGV_TANK = 150;

        // float var to hold the fuelling time, which is randomly generated
        public static float fuellingTime;
        // Declaring the variables
        private static float flowRate = 1.5f; // 1.5 litres / second
        // float var for the litres dispensed whenever this method is called
        public static float litresDispensedThisTransaction = 0f;
        // float var to hold the litres dispensed for each type of fuel
        public static float petrolLitresDispensed = 0f;
        public static float dieselLitresDispensed = 0f;
        public static float lpgLitresDispensed = 0f;
        // double var to hold the money earnt from sales
        public static double totalTakings = 0;
        // decimal var to hold the 1% commision earnt
        public static decimal commision = 0;

        // Encapsulating the above variables
        public static float FlowRate { get; set; }

        // Instantiating a new object of the following classes
        static Random random = new Random();
        static Vehicle vehicle = new Vehicle();
        static Log log = new Log();
        static Fuel fuel = new Fuel();

        /// <summary>
        /// Pump Constructor
        /// </summary>
        public Pump()
        {
            
        }
        
        /// <summary>
        /// Generates the time it takes to fuel, between 17 and 19 seconds.
        /// </summary>
        /// <returns>the fuellingTime in milliseconds</returns>
        public static float GenerateInterval(string vehType)
        {
            // Float to hold the amount of fuelneeded and instantiate it with 0
            float fuelNeeded = 0f;
            // Int to hold the fuelLevel that the vehicle arrived with, and instantiate it with 0
            int fuelLevel = 0;
            
            // Switch statement based on the vehType parameter
            switch (vehType)
            {
                // If vehType is Car, do this
                case "Car":
                    // set fuelLevel to be the value of the returen of vehicle.GenerateFuelLevel
                    fuelLevel = vehicle.GenerateFuelLevel(vehType);
                    // Set fuelNeeded to be the cars tank size minus the fuelLevel it already has
                    fuelNeeded = CAR_TANK - fuelLevel;
                    // Write to the console that the car needs however many litres of fuel
                    Console.WriteLine($"Car needs {fuelNeeded} litres");
                    break;
                // If the vehType is Van, do this
                case "Van":
                    // set fuelLevel to be the value of the returen of vehicle.GenerateFuelLevel
                    fuelLevel = vehicle.GenerateFuelLevel(vehType);
                    // Set the fuelNeeded to be the vans tank size minus the fuelLevel is already has
                    fuelNeeded = VAN_TANK - fuelLevel;
                    // Write to the console that the van needs however many litres of fuel
                    Console.WriteLine($"Van needs {fuelNeeded} litres");
                    break;
                // if the vehType is HGV, do this
                case "HGV":
                    // set fuelLevel to be the value of the returen of vehicle.GenerateFuelLevel
                    fuelLevel = vehicle.GenerateFuelLevel(vehType);
                    // Set the fuelNeeded to be the HGV tank size minus the fuelLevel is already has
                    fuelNeeded = HGV_TANK - fuelLevel;
                    // Write to the console that the HGV needs however many litres of fuel
                    Console.WriteLine($"HGV needs {fuelNeeded} litres");
                    break;
                // if the vehType is neither Car, Van or HGV, then do this
                default:
                    // Create an error log with the reason for the error
                    log.CreateErrorLog("GenerateInterval vehType parameter incorrect");
                    break;
            }

            // Set the fuelling time to be the amount of fuelNeeded multiplied by the flowrate(1.5)
            fuellingTime = (fuelNeeded * flowRate) * 100;
            
            // Write to the console the sum that we just did
            Console.WriteLine($"{fuelNeeded} * {flowRate} = {fuellingTime}");

            // Return the fuelling time, in milliseconds
            return fuellingTime;
        }

        /// <summary>
        /// Divides the pump timer interval by the pump flow rate
        /// </summary>
        /// <returns>totalLitresDispensed</returns>
        public static void DispenseFuel(string type, string fuel)
        {
            // Fuelling time = the return value of generate interval
            float fuellingTimeInSeconds = fuellingTime / 1000;
            
            // Litres dispensed this transaction = the fuellingtime multiplied by flowrate(1.5)
            litresDispensedThisTransaction = (fuellingTimeInSeconds * flowRate);

            // Display the litres dispensed this transaction in the console window
            Console.WriteLine($"This transaction: {litresDispensedThisTransaction}");
            
            // Switch statement based on what fuel the vehicle has
            switch (fuel)
            {
                // if the fuel is Unleaded, do this
                case "Unleaded":
                    // The total takings is the litres dispensed * cost of fuel / 100 rounded to 2 decimal places
                    totalTakings += Math.Round(((litresDispensedThisTransaction * Fuel.UNLEADED) / 100), 2);
                    // Add the litres dispensed this transaction to what we have already dispensed
                    petrolLitresDispensed += (float)Math.Round(litresDispensedThisTransaction, 2);
                    // Write the costs and fuel to the CLI
                    Console.WriteLine($"Costs: {totalTakings} at {fuel} cost {Fuel.UNLEADED}");
                    break;
                // if the fuel is Diesel, then do this
                case "Diesel":
                    // The total takings is the litres dispensed * cost of fuel / 100 rounded to 2 decimal places
                    totalTakings += Math.Round(((litresDispensedThisTransaction * Fuel.DIESEL) / 100), 2);
                    // Add the litres dispensed this transaction to what we have already dispensed
                    dieselLitresDispensed += (float)Math.Round(litresDispensedThisTransaction, 2);
                    // Write the costs and fuel to the CLI
                    Console.WriteLine($"Costs: {totalTakings} at {fuel} cost {Fuel.DIESEL}");
                    break;
                // if the fuel is LPG, do this
                case "LPG":
                    // The total takings is the litres dispensed * cost of fuel / 100 rounded to 2 decimal places
                    totalTakings += Math.Round(((litresDispensedThisTransaction * Fuel.LPG) / 100), 2);
                    // Add the litres dispensed this transaction to what we have already dispensed
                    lpgLitresDispensed += (float)Math.Round(litresDispensedThisTransaction, 2);
                    // Write the costs and fuel to the CLI
                    Console.WriteLine($"Costs: {totalTakings} at {fuel} cost {Fuel.LPG}");
                    break;
                // if the fuel is neither Unleaded, Diesel or LPG, then do this
                default:
                    // Create an error log with the error in question
                    log.CreateErrorLog("DispenseFuel error, incorrect fuel type provided");
                    break;
            }
            // The commision is the total takings divided by 100 to be 1%
            commision = (decimal)totalTakings / 100;

            // Create a transaction log
            log.CreateLog($"LITRES THIS TRANSACTION: {litresDispensedThisTransaction} | TAKINGS: {totalTakings} | COMMISSION: {commision}");

        }

    }
}
