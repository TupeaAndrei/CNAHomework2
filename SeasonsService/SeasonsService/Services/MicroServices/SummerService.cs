using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using SeasonsService.Helper;

namespace SeasonsService.Services.MicroServices
{
    public class SummerService : Summer.SummerBase
    {
        private Operations operations;

        public override Task<AddSummerResponse> AddSummer(AddSummerRequest request, ServerCallContext context)
        {
            operations = new Operations();
            var currentDate = request.SummerDate.Split("/");

            var currentDay = int.Parse(currentDate[1]);
            var currentMonth = int.Parse(currentDate[0]);

            var springList = operations.getSummerZodiac();
            Console.Write("Sign: ");
            string sign = "Invaild";

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
            return Task.FromResult(new AddSummerResponse()
            {
                Status = AddSummerResponse.Types.Status.Succes,
                Sign = sign,
                Season = getSeason()
            });
        }

        private string getSeason()
        {
            return "Summer";
        }
    }
}
