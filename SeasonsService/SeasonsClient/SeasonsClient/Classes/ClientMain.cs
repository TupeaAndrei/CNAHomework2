using Grpc.Net.Client;
using SeasonsClient.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SeasonsClient.Classes
{
    class ClientMain
    {
        static void Main(string[] args)
        {
            using var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new Zodiac.ZodiacClient(channel);

            var cancellationToken = new CancellationTokenSource(Timeout.Infinite);

            while (!cancellationToken.IsCancellationRequested)
            {
                string date = null;

                do
                {
                    Console.WriteLine("Enter date: ");
                    date = Console.ReadLine();
                } while (!Helper.ValidateDate(date));

                var zodiac = new ZodiacObj() { Date = date != null && date.Trim().Length > 0 ? date : "Invalid!" };
                var answer = client.AddZodiac(new AddZodiacRequest { ZodiacObj = zodiac });
                Helper.giveAnswer(answer);
            }
        }
    }
}
