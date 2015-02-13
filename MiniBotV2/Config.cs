using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
namespace MiniBotV2
{
    static class Config
    {
        public static readonly ConcurrentQueue<string> _queue = new ConcurrentQueue<string>();
        private static TextBox _Console;
        private static string _BotName = "Mini_B0t";
        private static string _authCode = "oauth:5fobcu1uc7k7c8h291rougwswzzp4eb"; // auth key
        private static string _ConnectionName = "Minijack".ToLower();
        private static bool _debugMode = false;
        private static bool _shouldRun = true;
        private static string[] _mods = { "minijackb", "test" };


        public static TextBox Console
        {
            get { return _Console; }
            set { _Console = value;}
        }
        public static string ConnectionName
        {
            get { return _ConnectionName; }
            set 
            {
                if (value.GetType() != "".GetType())
                    throw new OverflowException();
                else
                    _ConnectionName = value;
            }
        }

        public static bool shouldRun
        {
            get { return _shouldRun; }
            set
            {
                if (value.GetType() != true.GetType())
                    throw new OverflowException();
                else
                    _shouldRun = value;
            }
        }

        public static bool debugMode
        {
            get { return _debugMode; }
            set
            {
                if (value.GetType() != true.GetType())
                    throw new OverflowException();
                else
                    _debugMode = value;
            }

        }
        public static string BotName { get { return _BotName; } set { } }

        public static string authCode { get { return _authCode; } set { } }

        public static string[] Mods { get { return _mods; } set { } }
    }
}
