using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using SeasonsService.Helper;

namespace SeasonsService.Services.MicroServices
{
    public class SpringService : Spring.SpringBase
    {
        private static Operations operations;

        public override Task<AddSpringResponse> AddSpring(AddSpringRequest request, ServerCallContext context)
        {
            operations = new Operations();
            var date = request;
            var currentDate = request.SpringDate.Split("/");
            var currentDay = int.Parse(currentDate[1]);
            var currentMonth = int.Parse(currentDate[0]);

            var springList = operations.getSpringZodiac();
            Console.Write("Sign: ");
            string sign = "Invalid";

            foreach (var value in springList)
            {
                var beginDate = operations.getBeginDate(value);
                var endDate = operations.getEndDate(value);
                var currentDateTime = operations.getCurrentDate(currentDay, currentMonth);
                if (currentDateTime >= beginDate && currentDateTime <= endDate)
                {
                    sign = value.Item3;
                    break;
                }
            }
            Console.Write(sign + "\n");
            return Task.FromResult(new AddSpringResponse()
            {
                Status = AddSpringResponse.Types.Status.Succes,
                Sign = sign,
                Season = getSeason()
            });
        }

        public string getSeason()
        {
            return "Spring";
        }
    }
}
