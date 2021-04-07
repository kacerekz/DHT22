using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Ports;
using System.Threading;

namespace ReadSerial
{
    class Program
    {

        static void Main(string[] args)
        {
            List<double> temperatures = new List<double>();
            List<double> humidities = new List<double>();
            List<DateTime> times = new List<DateTime>();

            SerialPort port = new SerialPort(args[0]);
            port.Open();

            using (StreamWriter sw = new StreamWriter("../../tmp_hum.csv"))
            {
                sw.WriteLine("date;time;temperature;humidity");

                while (true)
                {
                    // Get values
                    double tmp = double.Parse(port.ReadLine());
                    double hmd = double.Parse(port.ReadLine());
                    DateTime time = DateTime.Now;

                    // Save values
                    temperatures.Add(tmp);
                    humidities.Add(hmd);
                    times.Add(time);

                    // Write to file
                    sw.WriteLine($"{time:MM/dd/yyyy;HH:mm:ss};{tmp};{hmd};");
                    Console.WriteLine(($"{time:MM/dd/yyyy;HH:mm:ss};{tmp};{hmd};"));
                    sw.Flush();

                    // Back up every 450 values (approx 15min)
                    if (temperatures.Count >= 450)
                    {
                        Console.WriteLine("Writing backup...");
                        using (StreamWriter sw2 = new StreamWriter("../../tmp_hum_backup.csv", true))
                        {
                            for (int i = 0; i < temperatures.Count; i++)
                            {
                                sw2.WriteLine($"{times[i]:MM/dd/yyyy;HH:mm:ss};{temperatures[i]};{humidities[i]};");
                            }
                            sw2.Flush();
                        }
                        Console.WriteLine("Backed up.");
                        
                        // Clear lists
                        temperatures.Clear();
                        humidities.Clear();
                        times.Clear();
                    }

                    // Rest
                    Thread.Sleep(500);
                } //!while{}
            } //!using{}
        } //!Main()
    }
}

