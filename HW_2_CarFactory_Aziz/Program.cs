using System;
using System.Collections.Generic;

namespace CarFactoryApp
{
    public enum CarType
    {
        Tesla,
        BMW,
        Toyota,
        NissanLeaf,
        Audi,
        Renault,
        Peugeot
    }

    public interface ICar
    {
        string Brand { get; }
        int Seats { get; }
        string OnBoardSystem { get; }
        string GetDescription();
    }

    public interface IElectric
    {
        int BatteryCapacity { get; }
        string ChargeType { get; }
    }

    public interface IFuelCar
    {
        string FuelType { get; }
        double EngineVolume { get; }
    }

    public interface IAutomaticTransmission
    {
        int AutomaticGears { get; }
    }

    public interface IManualTransmission
    {
        int ManualGears { get; }
    }

    public abstract class ACar : ICar
    {
        public string Brand { get; protected set; }
        public int Seats { get; protected set; }
        public string OnBoardSystem { get; protected set; }

        protected ACar(string brand, int seats, string onBoardSystem)
        {
            Brand = brand;
            Seats = seats;
            OnBoardSystem = onBoardSystem;
        }

        protected virtual string GetTransmissionDescription()
        {
            if (this is IAutomaticTransmission automatic)
                return $"automatic transmission ({automatic.AutomaticGears} gears)";

            if (this is IManualTransmission manual)
                return $"manual transmission ({manual.ManualGears} gears)";

            return "unknown transmission";
        }

        protected virtual string GetEngineDescription()
        {
            if (this is IElectric electric)
                return $"electric car with {electric.BatteryCapacity} kWh battery, charging type: {electric.ChargeType}";

            if (this is IFuelCar fuelCar)
                return $"fuel car running on {fuelCar.FuelType}, engine volume {fuelCar.EngineVolume:F1} L";

            return "car with unknown engine type";
        }

        public virtual string GetDescription()
        {
            return $"{Brand}: {GetEngineDescription()}, with {GetTransmissionDescription()}, {Seats} seats, {OnBoardSystem} on board.";
        }
    }

    public class Tesla : ACar, IElectric, IAutomaticTransmission
    {
        public int BatteryCapacity { get; } = 75;
        public string ChargeType { get; } = "Type 2 / Supercharger";
        public int AutomaticGears { get; } = 1;

        public Tesla() : base("Tesla", 5, "Android Auto")
        {
        }
    }

    public class BMW : ACar, IFuelCar, IAutomaticTransmission
    {
        public string FuelType { get; } = "petrol";
        public double EngineVolume { get; } = 2.0;
        public int AutomaticGears { get; } = 8;

        public BMW() : base("BMW", 5, "iDrive")
        {
        }
    }

    public class Toyota : ACar, IFuelCar, IManualTransmission
    {
        public string FuelType { get; } = "petrol";
        public double EngineVolume { get; } = 1.6;
        public int ManualGears { get; } = 6;

        public Toyota() : base("Toyota", 5, "Toyota Multimedia")
        {
        }
    }

    public class NissanLeaf : ACar, IElectric, IAutomaticTransmission
    {
        public int BatteryCapacity { get; } = 40;
        public string ChargeType { get; } = "CHAdeMO";
        public int AutomaticGears { get; } = 1;

        public NissanLeaf() : base("Nissan Leaf", 5, "NissanConnect")
        {
        }
    }

    public class Audi : ACar, IFuelCar, IAutomaticTransmission
    {
        public string FuelType { get; } = "diesel";
        public double EngineVolume { get; } = 3.0;
        public int AutomaticGears { get; } = 8;

        public Audi() : base("Audi", 5, "MMI")
        {
        }
    }

    public class Renault : ACar, IFuelCar, IManualTransmission
    {
        public string FuelType { get; } = "petrol";
        public double EngineVolume { get; } = 1.5;
        public int ManualGears { get; } = 5;

        public Renault() : base("Renault", 5, "Easy Link")
        {
        }
    }

    public class Peugeot : ACar, IFuelCar, IAutomaticTransmission
    {
        public string FuelType { get; } = "diesel";
        public double EngineVolume { get; } = 1.6;
        public int AutomaticGears { get; } = 6;

        public Peugeot() : base("Peugeot", 5, "i-Cockpit")
        {
        }
    }

    public static class CarFactory
    {
        public static ICar CreateCar(CarType carType)
        {
            return carType switch
            {
                CarType.Tesla => new Tesla(),
                CarType.BMW => new BMW(),
                CarType.Toyota => new Toyota(),
                CarType.NissanLeaf => new NissanLeaf(),
                CarType.Audi => new Audi(),
                CarType.Renault => new Renault(),
                CarType.Peugeot => new Peugeot(),
                _ => throw new ArgumentException("Unknown car type")
            };
        }
    }

    class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Dictionary<string, CarType> carMap = new Dictionary<string, CarType>(StringComparer.OrdinalIgnoreCase)
            {
                { "tesla", CarType.Tesla },
                { "bmw", CarType.BMW },
                { "toyota", CarType.Toyota },
                { "nissanleaf", CarType.NissanLeaf },
                { "nissan leaf", CarType.NissanLeaf },
                { "audi", CarType.Audi },
                { "renault", CarType.Renault },
                { "peugeot", CarType.Peugeot }
            };

            Console.WriteLine("Car description program.");
            Console.WriteLine("Available brands: Tesla, BMW, Toyota, Nissan Leaf, Audi, Renault, Peugeot");
            Console.WriteLine("Enter done to stop.");
            Console.WriteLine();

            while (true)
            {
                Console.Write("Enter car brand or done to stop: ");
                string? input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Empty input. Try again.");
                    Console.WriteLine();
                    continue;
                }

                input = input.Trim();

                if (input.Equals("done", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("Program ended.");
                    break;
                }

                if (carMap.TryGetValue(input, out CarType carType))
                {
                    ICar car = CarFactory.CreateCar(carType);
                    Console.WriteLine(car.GetDescription());
                }
                else
                {
                    Console.WriteLine("Unknown car brand. Try: Tesla, BMW, Toyota, Nissan Leaf, Audi, Renault, Peugeot.");
                }

                Console.WriteLine();
            }
        }
    }
}