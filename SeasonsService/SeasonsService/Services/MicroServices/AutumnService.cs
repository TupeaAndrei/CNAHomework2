using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using SeasonsService.Helper;

namespace SeasonsService.Services.MicroServices
{
    public class AutumnService : Autumn.AutumnBase
    {
        private Operations operations;

        public override Task<AddAutumnResponse> AddAutumn(AddAutumnRequest request, ServerCallContext context)
        {
            operations = new Operations();
            var currentDate = request.AutumnDate.Split("/");

            var currentDay = int.Parse(currentDate[1]);
            var currentMonth = int.Parse(currentDate[0]);

            var springList = operations.getAutumnZodiac();
            Console.Write("Sign: ");
            string sign = default;

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
            return Task.FromResult(new AddAutumnResponse()
            {
                Status = AddAutumnResponse.Types.Status.Succes,
                Sign = sign,
                Season = getSeason()
            });
        }

        private string getSeason()
        {
            return "Autumn";
        }
    }
}
