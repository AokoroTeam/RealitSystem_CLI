using System.CommandLine;
using CommandLine;

namespace RealitSystem_CLI
{
    internal class Program
    {
        static void Main(string[] args)
        {
           
        }

        static void ShowOutput(double temperature, string unit = "c")
        {
            // C to F
            if (unit == "C")
            {
                temperature = (temperature * 9) / 5 + 32;
                unit = "F";
            }

            // F to C
            if (unit == "F")
            {
                temperature = (temperature - 32) * 5 / 9;
                unit = "C";
            }

            Console.WriteLine($"Result : { temperature} \x00B0{ unit}");
}
    }
}