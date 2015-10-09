﻿using System;
using Woopsa;

namespace WoopsaDemoServer
{
    public class Thermostat
    {
        public double SetPoint { get; set; }

        public Thermostat()
        {
            SetPoint = 20;
        }
    }

    public class WeatherStation
    {
        public double Temperature { get; private set; }

        public bool IsRaining { get; set; }

        public int Altitude { get; set; }

        public double Sensitivity { get; set; }

        public string City { get; set; }

        public DateTime Time { get; set; }

        public TimeSpan TimeSinceLastRain { get; set; }

        public Thermostat Thermostat { get; private set; }

        public WeatherStation()
        {
            Temperature = 24.2;
            IsRaining = false;
            Altitude = 430;
            Sensitivity = 0.5;
            City = "Geneva";
            Time = DateTime.Now;
            TimeSinceLastRain = TimeSpan.FromDays(3);
            Thermostat = new Thermostat();
        }

        public string GetWeatherAtDate(DateTime date)
        {
            switch(date.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    return "rainy";
                default:
                    return "sunny";
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            WeatherStation root = new WeatherStation();
            WoopsaServer woopsaServer = new WoopsaServer(root, 80);

            Console.WriteLine("Woopsa server listening on http://localhost:{0}{1}", woopsaServer.WebServer.Port, woopsaServer.RoutePrefix);
            Console.WriteLine("Some examples of what you can do directly from your browser:");
            Console.WriteLine(" * View the object hierarchy of the root object:");
            Console.WriteLine("   http://localhost:{0}{1}meta/", woopsaServer.WebServer.Port, woopsaServer.RoutePrefix);
            Console.WriteLine(" * Read the value of a property:");
            Console.WriteLine("   http://localhost:{0}{1}read/Temperature", woopsaServer.WebServer.Port, woopsaServer.RoutePrefix);
        }
    }
}
