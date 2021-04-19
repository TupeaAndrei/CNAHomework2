using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SeasonsService.Helper
{
    public class Operations
    {
        private const string FILE_PATH = "./Helper/Textfiles/zodiac.txt";
        public List<Tuple<string,string,string>> GetAllZodiacs()
        {
            var zodiacList = new List<Tuple<string, string, string>>();

            try
            {
                var streamReader = new StreamReader(FILE_PATH);
                var line = streamReader.ReadLine()?.Split("|");
                while (line != null)
                {
                    zodiacList.Add(new Tuple<string, string, string>(
                        line[0],
                        line[1],
                        line[2]));
                    line = streamReader.ReadLine()?.Split("|");
                }
                streamReader.Close();
            }
            catch(Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            return zodiacList;
        }

        public List<Tuple<string,string,string>> getSpringZodiac()
        {
            var fullList = GetAllZodiacs();
            var springList = new List<Tuple<string, string, string>>();
            foreach (var value in fullList)
            {
                var month = value.Item1;
                int monthNum = int.Parse(month.Substring(0, 2));
                if (monthNum >=3 && monthNum <=5)
                {
                    springList.Add(value);
                }
            }
            return springList;
        }

        public List<Tuple<string, string, string>> getSummerZodiac()
        {
            var fullList = GetAllZodiacs();
            var summerList = new List<Tuple<string, string, string>>();
            foreach (var value in fullList)
            {
                var month = value.Item1;
                int monthNum = int.Parse(month.Substring(0, 2));
                if (monthNum >= 6 && monthNum <= 8)
                {
                    summerList.Add(value);
                }
            }
            return summerList;
        }

        public List<Tuple<string, string, string>> getAutumnZodiac()
        {
            var fullList = GetAllZodiacs();
            var autumnList = new List<Tuple<string, string, string>>();
            foreach (var value in fullList)
            {
                var month = value.Item1;
                int monthNum = int.Parse(month.Substring(0, 2));
                if (monthNum >= 9 && monthNum <= 11)
                {
                    autumnList.Add(value);
                }
            }
            return autumnList;
        }

        public  List<Tuple<string, string, string>> getWinterZodiac()
        {
            var fullList = GetAllZodiacs();
            var winterList = new List<Tuple<string, string, string>>();
            foreach (var value in fullList)
            {
                var month = value.Item1;
                int monthNum = int.Parse(month.Substring(0, 2));
                if (monthNum == 12 || monthNum == 1 || monthNum == 2)
                {
                    winterList.Add(value);
                }
            }
            return winterList;
        }

        public DateTime getBeginDate(Tuple<string,string,string> value)
        {
            var begin = value.Item1.Split("/");
            var beginDay = int.Parse(begin[1]);
            var beginMonth = int.Parse(begin[0]);
            var beginDate = new DateTime(2000, beginMonth, beginDay);
            return beginDate;
        }

        public DateTime getEndDate(Tuple<string,string,string> value)
        {
            var end = value.Item2.Split("/");
            var endDay = int.Parse(end[1]);
            var endMonth = int.Parse(end[0]);
            var endDate = new DateTime(2000, endMonth, endDay);
            return endDate;
        }

        public DateTime getCurrentDate(int currentDay,int currentMonth)
        {
            var currentDate = new DateTime(2000, currentMonth, currentDay);
            return currentDate;
        }
    }
}
