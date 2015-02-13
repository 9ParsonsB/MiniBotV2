using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniBotV2Console
{
    static class AppCode
    {
        static Random rnd = new Random();
        public static bool isMod(string user)
        {
            foreach (var x in Config.Mods)
            {
                if (user != null)
                if (user.ToLower() == x.ToLower())
                {
                    //Console.WriteLine("user is mod");
                    return true;
                }
            }
            //Console.WriteLine("user is not mod");
            return false;
        }

        public static bool isIn(string match, string[] list, string looking = "")
        {
            foreach (var x in list)
            {
                //Console.WriteLine("looking for: " + x + looking);
                if (match == x + looking)
                {

                    return true;
                }
            }
            return false;
        }

        public static int GetRandomInt(int min = 1, int max = 3)
        {
            return rnd.Next(min, max);
        }
    }
}

