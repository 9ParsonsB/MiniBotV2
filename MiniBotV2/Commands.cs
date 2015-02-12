using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniBotV2
{
    static class Commands
    {
        static bool isMod(string user)
        {
            foreach (var x in Config.Mods)
            {
                if (user.ToLower() == x.ToLower())
                {
                    //Console.WriteLine("user is mod");
                    return true;
                }
            }
            //Console.WriteLine("user is not mod");
            return false;
        }

        static bool isIn(string match, string[] list, string looking = "")
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
    }
}
