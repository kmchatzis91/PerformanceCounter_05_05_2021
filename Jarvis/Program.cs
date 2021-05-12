using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;

namespace Jarvis
{
    class Program
    {
        static void Main(string[] args)
        {
            bool thresholdReached = true;
            PerformanceCounter cpuPerformanceCounter = new PerformanceCounter("Processor Information", "% Processor Time", "_Total");
            PerformanceCounter memoryPerformanceCounter = new PerformanceCounter("Memory", "Available MBytes");
            PerformanceCounter memoryInUsePerformanceCounter = new PerformanceCounter("Memory", "% Committed Bytes In Use");

            while (thresholdReached)
            {
                float currentCpu = cpuPerformanceCounter.NextValue();
                float currentMemory = memoryPerformanceCounter.NextValue();
                float memoryInUse = memoryInUsePerformanceCounter.NextValue();

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n CPU Load: {0}%", currentCpu);
                Console.WriteLine(" Available Memory: {0} mb", currentMemory);
                Console.WriteLine(" Memory percentage in use: {0}%", memoryInUse);
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("\n - - - - - - - - - - - - - - - - - - - - - ");

                Thread.Sleep(5000);

                if (currentMemory < 1024)
                {
                    thresholdReached = false;
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n Memory in use is over 75%. Ending Application...");
                    Console.ForegroundColor = ConsoleColor.White;
                    Environment.Exit(0);
                }
            }

        } // END static void Main(string[] args)

    } // END class Program

} // END namespace Jarvis
