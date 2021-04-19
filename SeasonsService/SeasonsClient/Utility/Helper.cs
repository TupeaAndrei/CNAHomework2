using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeasonsClient.Utility
{
    public class Helper
    {
        public static bool ValidateDate(string date)
        {
            if (hasInvalidChars(date))
            {
                Console.WriteLine("Date contains invalid chars!");
                return false;
            }
            if (date[2] != '/' || date[5] != '/')
            {
                Console.WriteLine("Bad separator! Please use / for the data format!");
                return false;
            }
            try
            {
                var auxDate = date.Split("/");
                var dateTime = new DateTime(int.Parse(auxDate[2]), int.Parse(auxDate[0]), int.Parse(auxDate[1]));
            }
            catch(FormatException e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            catch(IndexOutOfRangeException e)
            {
                Console.Write(e.Message + "or invalid string!");
                return false;
            }
            catch(ArgumentOutOfRangeException e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            return true;
        }

        public static bool hasInvalidChars(string date)
        {
            for (int index=0;index<date.Length;index++)
            {
                if (index != 2 && index != 5)
                {
                    if (!Char.IsNumber(date[index]))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public static void giveAnswer(AddZodiacResponse response)
        {
            switch(response.Status)
            {
                case AddZodiacResponse.Types.Status.Succes:
                    Console.WriteLine("Response Status: " + response.Status);
                    Console.WriteLine("Sign: " + response.Sign);
                    break;
                case AddZodiacResponse.Types.Status.Error:
                    Console.WriteLine("Response Status: " + response.Status);
                    Console.WriteLine("Error encountered!");
                    break;
                default:
                    Console.WriteLine("Unexpected error encountered!");
                    break;
            }
        }
    }
}
