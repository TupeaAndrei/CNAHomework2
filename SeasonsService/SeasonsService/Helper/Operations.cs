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
        public static List<Tuple<ZodiacObj,ZodiacObj,string>> GetAllZodiacs()
        {
            var zodiacList = new List<Tuple<ZodiacObj, ZodiacObj, string>>();

            try
            {
                var streamReader = new StreamReader(FILE_PATH);
                var line = streamReader.ReadLine()?.Split("|");
                while (line != null)
                {
                    zodiacList.Add(new Tuple<ZodiacObj, ZodiacObj, string>(
                        new ZodiacObj() { Date = line[0] },
                        new ZodiacObj() { Date = line[1] },
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
    }
}
