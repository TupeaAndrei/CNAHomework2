using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Net.Client;
using Microsoft.Extensions.Logging;
using SeasonsService.Helper;
using SeasonsService.Services.MicroServices;

namespace SeasonsService.Services
{
    public class ZodiacService :Zodiac.ZodiacBase
    {
        private readonly Operations operations = new Operations();
        private readonly ILogger<ZodiacService> _logger;
        public ZodiacService(ILogger<ZodiacService> logger)
        {
            _logger = logger;
        }

        public override Task<AddZodiacResponse> AddZodiac(AddZodiacRequest request, ServerCallContext context)
        {
            if (request.Zodiac.Date.Equals("Invalid"))
            {
                Console.WriteLine("Data is blank");
                return Task.FromResult(new AddZodiacResponse
                {
                    Status = AddZodiacResponse.Types.Status.Error,
                    Sign = "INVALID",
                });
            }
            using var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var date = request.Zodiac;
            var stringDate = date.Date.Split("/");

            Console.Write("Sign: ");
            string sign = default;

            var year = int.Parse(stringDate[2]);
            var month = int.Parse(stringDate[0]);
            var day = int.Parse(stringDate[1]);

            var dateTime = new DateTime(year, month, day);


            switch (dateTime)
            {
                case { } when (dateTime >= new DateTime(year, 3, 21) && dateTime <= new DateTime(year, 6, 21)):
                    //spring
                    var clientSpring = new Spring.SpringClient(channel);
                    var response = clientSpring.AddSpring(new AddSpringRequest { SpringDate = request.Zodiac.Date });//await clientSpring.AddSpringAsync(new AddSpringRequest { SpringDate = request.Zodiac.Date });
                    sign = response.Sign;
                    break;
                case { } when (dateTime >= new DateTime(year, 6, 22) && dateTime <= new DateTime(year, 9, 22)):
                    //summer
                    var clientSummer = new Summer.SummerClient(channel);
                    var response2 = clientSummer.AddSummer(new AddSummerRequest { SummerDate = request.Zodiac.Date });
                    sign = response2.Sign;
                    break;
                case { } when (dateTime >= new DateTime(year, 9, 23) && dateTime <= new DateTime(year, 12, 21)):
                    //autumn
                    var clientAutumn = new Autumn.AutumnClient(channel);
                    var response3 = clientAutumn.AddAutumn(new AddAutumnRequest { AutumnDate = request.Zodiac.Date });
                    sign = response3.Sign;
                    break;
                default:
                    //winter
                    var clientWinter = new Winter.WinterClient(channel);
                    var response4 = clientWinter.AddWinter(new AddWinterRequest { WinterDate = request.Zodiac.Date });
                    sign = response4.Sign;
                    break;
                
            }
            Console.WriteLine(sign);
            return Task.FromResult(new AddZodiacResponse
            {
                Status = AddZodiacResponse.Types.Status.Succes,
                Sign = sign,
            });
        }


    }
}
