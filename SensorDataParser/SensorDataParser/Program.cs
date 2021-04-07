using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensorDataParser
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("Run as \"SensorDataParser <data file path>\"");
                return;
            }

            String filename = args[0];

            if (!File.Exists(filename))
            {
                Console.WriteLine("Bad path.");
                return;
            }

            using (StreamReader reader = new StreamReader(filename))
            {
                double temperature = 0;
                double humidity = 0;

                int day = 0;
                int month = 0;
                int year = 0;

                int hour = 0;
                int minute = 0;
                int seconds = 0;
                int timeInSeconds = 0;

                string fn = null;
                StreamWriter writer = null;

                while (!reader.EndOfStream) 
                { 
                    string line = reader.ReadLine();
                    string[] split = line.Split(';');
                    string[] date = split[0].Split(',');
                    string[] time = split[1].Split(':');
                
                    try
                    {
                        temperature = double.Parse(split[2]);
                        humidity = double.Parse(split[3]);

                        month = int.Parse(date[0]);
                        day = int.Parse(date[1]);
                        year = int.Parse(date[2]);

                        hour = int.Parse(time[0]);
                        minute = int.Parse(time[1]);
                        seconds = int.Parse(time[2]);
                    } 
                    catch
                    {
                        Console.WriteLine("Parse error, continuing...");
                        continue;
                    }

                    string newfn = $"{day}_{month}_{year}.csv";

                    if (fn == null || !string.Equals(fn, newfn))
                    {
                        fn = newfn;
                        if (writer != null) writer.Close();
                        writer = new StreamWriter(newfn);
                        writer.WriteLine($"datum;cas;index;teplota;vlhkost;");
                    }

                    timeInSeconds = 3600 * hour + 60 * minute + seconds;

                    writer.WriteLine($"{day}. {month}. {year};{hour}:{minute}:{seconds};{timeInSeconds};{temperature};{humidity}");
                }

                if (writer != null) writer.Close();
            }

            Console.WriteLine("Done.");
        }
    }
}
