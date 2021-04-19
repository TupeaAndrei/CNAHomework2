using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using SeasonsService.Helper;

namespace SeasonsService.Services.MicroServices
{
    public class WinterService : Winter.WinterBase
    {
        private Operations operations;

        public override Task<AddWinterResponse> AddWinter(AddWinterRequest request, ServerCallContext context)
        {
            operations = new Operations();
            var currentDate = request.WinterDate.Split("/");

            var currentDay = int.Parse(currentDate[1]);
            var currentMonth = int.Parse(currentDate[0]);

            var springList = operations.getWinterZodiac();
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
            return Task.FromResult(new AddWinterResponse()
            {
                Status = AddWinterResponse.Types.Status.Succes,
                Sign = sign,
                Season = getSeason()
            });
        }

        private string getSeason()
        {
            return "Winter";
        }
    }
}
