using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using System.Data;
using System.Drawing;
using System.IO;
namespace MiniBotV2Console
{
    static class Config
    {

        private static List<Del> _EventList = new List<Del>();

        public static readonly ConcurrentQueue<string> _queue = new ConcurrentQueue<string>();

        private static string _BotName = "Mini_B0t";
        private static string _authCode = "oauth:0r02okasmxn5j9ushgw2pi1w8ffhiv"; // auth key
        private static string _ConnectionName = "Twitchingtophats".ToLower();

        private static bool _debugMode = false;
        private static bool _shouldRun = true;

        private static List<string> _Mods = new List<string>();

        private static string _Prefix;
        private static ChatMessage _Data;

        private static bool _EventsEnabled = true;

        public static bool EventsEnabled
        {
            get { return _EventsEnabled; }
            set
            {
                if (value.GetType() != _EventsEnabled.GetType())
                    throw new OverflowException();
                else
                    _EventsEnabled = value;
            }
        }

        public static List<string> Mods
        {
            get { return _Mods; }
            set
            {
                if (value.GetType() != new List<string>().GetType())
                    throw new OverflowException();
                else
                    _Mods = value;
            }
        }

        public static ChatMessage Data
        {
            get { return _Data; }
            set
            {
                if (value.GetType() != new ChatMessage("").GetType())
                    throw new OverflowException();
                else
                    _Data = value;
            }
        }


        public static List<Del> EventList
        {
            get { return _EventList; }
            set
            {
                if (value.GetType() != new List<Action>().GetType())
                    throw new OverflowException();
                else
                    _EventList = value;
            }
        }

        public static string Prefix
        {
            get { return _Prefix; }
            set
            {
                if (value.GetType() != "".GetType())
                    throw new OverflowException();
                else
                    _Prefix = value;
            }
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

    }
}
