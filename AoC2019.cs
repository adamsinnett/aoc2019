using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace AoC2019
{
    class AdventOFCode
    {
        static void Main(string[] args)
        {
            //DayOne.getFuel(getInput(1));
            //DayTwo.getCodes(getInput(2));
            //Day3.CrossedWires(getInput(3));
            //Day4.DoDay4(getInput(4));
            Day5.Execute(getInput(5));

            System.Console.WriteLine("");
            System.Console.WriteLine("Good so far!");
        }



        // TODO make abstract
        private static string getInput(int day)
        {

            var request = WebRequest.Create(@"https://adventofcode.com/2019/day/"+day.ToString()+@"/input");
            var cookie = new Cookie("session", "53616c7465645f5f2cce37a173e82fceb957550c8a2b9d5ef7de29ec23d14a58ebad77f8d483d319a263e45fd4bd36c9")
            {
                Domain = @"adventofcode.com"
            };

            request.TryAddCookie(cookie);
            var response = request.GetResponse();
            string input;
            using (Stream dataStream = response.GetResponseStream())
            {
                // Open the stream using a StreamReader for easy access.  
                StreamReader reader = new StreamReader(dataStream);
                // Read the content.  
                input = reader.ReadToEnd();
            }

            // Close the response.  
            response.Close();

            return input;
        }
    }
}
