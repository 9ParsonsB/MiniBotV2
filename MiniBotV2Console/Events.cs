﻿using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Xml.Linq;
using System.ComponentModel;
using System.Data;
using System.Drawing;

namespace MiniBotV2Console
{
    public delegate bool Del(ChatMessage Data, StreamWriter Writer);
    static class Event
    {
        public static void Init()
        {
            //Config.EventList.Add(EVENTNAME.Main); // Trigger for Event Template
            //Config.EventList.Add(TestEvent.Main);
            Config.EventList.Add(GetTimeEvent.Main);
            Config.EventList.Add(IsFailEvent.Main);
            Config.EventList.Add(VoteEvent.Main);
            Config.EventList.Add(NameEvent.Main);

        }
    }

/*public static class EVENTNAME // Event Template
{
    public static bool Main(ChatMessage Data, StreamWriter Writer)
    {

    }
}//*/

    public static class TestEvent
    {
        public static bool Main(ChatMessage Data, StreamWriter Writer)
        {
            if (Data.Name != null)
                if (Data.Name.ToLower() == "minijackb")
                    Writer.WriteLine(Config.Prefix + "STILL ALIVE: " + Data.Name);
            Writer.Flush();
            return true;
        }
    }

    public static class GetTimeEvent
    {
        public static bool Main(ChatMessage Data, StreamWriter Writer)
        {
            if (Data.Command == "!time")
            {
                Writer.WriteLine(Config.Prefix + "The Current Time (GMT 0): " + DateTime.Now.ToString("h:mm:ss tt"));
                Writer.Flush();
            }
            return true;
        }
    }

    public static class IsFailEvent
    {
        static int rnd;
        public static bool Main(ChatMessage Data, StreamWriter Writer)
        {
            if (Data.Command == "!fail")
            {
                rnd = AppCode.GetRandomInt();
                if (rnd == 1)
                {
                    Writer.WriteLine(Config.Prefix + "FAIL!");
                }
                else
                {
                    Writer.WriteLine(Config.Prefix + " ...No.");
                }
                Writer.Flush();
                return true;
            }
            return false;
        }
    }

    public static class IsWinEvent
    {
        static int rnd;
        public static bool Main(ChatMessage Data, StreamWriter Writer)
        {
            if (Data.Command == "!win")
            {
                rnd = AppCode.GetRandomInt();
                if (rnd == 1)
                {
                    Writer.WriteLine(Config.Prefix + "WIN!");
                }
                else
                {
                    Writer.WriteLine(Config.Prefix + " ...No.");
                }
                Writer.Flush();
                return true;
            }
            return false;
        }
    }//*/

    public static class VoteEvent // Event Template
    {
        static bool SubmitGames = false;
        static List<string> VoteNames = new List<string>();
        static List<int> VoteAmount = new List<int>();
        static List<string> Users = new List<string>();
        static List<string> Games = new List<string>();
        static string Message = "";

        public static bool Main(ChatMessage Data, StreamWriter Writer)
        {
            if (Data.Command == "!vote")
            {
                if (!SubmitGames) // if the poll is not open
                {
                    if (Data.Args[0] == "open" && Data.isAdmin)
                    {
                        if (Data.Args.Length > 1)
                        {
                            SubmitGames = true; //users can now submit there choice of game
                            VoteNames.Clear(); // clears the previous game names, amount and users
                            VoteAmount.Clear();
                            Users.Clear();

                            List<string> games = new List<string>();
                            for (var i = 1; i < Data.Args.Length; i++) // loops through the paramaters (except the first) and adds them to the list of games the user can choose
                            {
                                games.Add(Data.Args[i]);
                            }

                            if (Data.Args.Length >= 1)
                            {
                                Message = "";

                                foreach (var i in games)
                                {
                                    VoteNames.Add(i);
                                    VoteAmount.Add(0);

                                    if (i != "open" && i != "close") // failsafe incase the open or close oporator are in the games array
                                    {
                                        Message += ", \"" + i + "\"";
                                    }
                                }

                                Writer.WriteLine(Config.Prefix + "You can now submit votes using: \"!vote [choice]\"");
                                Writer.Flush();
                                Writer.WriteLine(Config.Prefix + "You can vote for: " + Message.Substring(2) + ". (IS CASE-SENSITIVE)");
                                Writer.Flush();
                            }
                        }
                        else
                        {
                            Writer.Write(Config.Prefix + "Incorrect Syntax. Please use !vote open [game1] [game2]... ");
                            Writer.Flush();
                        }
                    }
                    else if (Data.Args[0] == "list")
                    {
                        var game = "";

                        for (var x = 0; x < VoteNames.Count; x++)
                        {
                            game += ", " + VoteNames[x] + ": " + VoteAmount[x];
                        }

                        Writer.WriteLine(Config.Prefix + "Votes: " + game.Substring(2));
                        Writer.Flush();
                    }
                    else
                    {
                        Writer.Flush();
                        Writer.Write(Config.Prefix + "Incorrect Syntax. Please use !vote open [game1] [game2]... ");
                        Writer.Flush();
                        Writer.Flush();
                    }
                }
                else // if the poll is open
                {
                    if (Data.Args[0] == "close" && Data.isAdmin)
                    {
                        SubmitGames = false;
                        Writer.WriteLine(Config.Prefix + "Voting has closed!! Please dont vote anymore!");
                        Writer.Flush();

                        var game = "";

                        for (var x = 0; x < VoteNames.Count; x++)
                        {
                            game += ", " + VoteNames[x] + ": " + VoteAmount[x];
                        }

                        Writer.WriteLine(Config.Prefix + "Votes: " + game.Substring(2));
                        Writer.Flush();
                    }
                    else if (!AppCode.isIn(Data.Name, Users.ToArray())) // adds users choice
                    {
                        var exist = false;

                        for (var i = 0; i < VoteNames.Count; i++) // loops through games avaliable
                        {
                            if (VoteNames[i] == Data.Args[0]) // if the user choice is in the list of games they are allowed to choose from
                            {
                                VoteAmount[i] += 1; // add to the amount of times that game has been chosen
                                exist = true; // record that we have found a match
                                break; // exit from the loop (this is good practice incase the array of games is huge and taking up tick cycles)
                            }
                        }
                        if (!exist) // if what the user said is not a valid choice
                        {
                            Writer.WriteLine(Config.Prefix + "Sorry, " + Data.Name + ", what you said is not a valid choice"); // tell them what they did wrong
                            Writer.Flush();
                        }
                        else // if what the user said is vaild
                        {
                            Users.Add(Data.Name); // add them to the list of users so that they cannot vote again
                            Writer.WriteLine(Config.Prefix + "vote added; " + Data.Name + "!" + " You chose: " + Data.Args[0]);
                            Writer.Flush();
                        }
                    }
                    else if (AppCode.isIn(Data.Name, Users.ToArray()))
                    {
                        Writer.WriteLine(Config.Prefix + "Sorry, " + Data.Name + " you have already voted!");
                        Writer.Flush();
                    }
                    else
                    {
                        Writer.WriteLine(Config.Prefix + "Incorrect Syntax, " + Data.Name);
                        Writer.Flush();
                    }
                }
            }
            Writer.Flush();
            return true;
        }
    }//*/

    public static class NameEvent
    {
        private static bool SubmitNames = false;
        public static List<string> Names = new List<string>();
        public static List<string> Users = new List<string>();

        public static bool Main(ChatMessage Data, StreamWriter Writer)
        {
            if (Data.Command == "!name")
            {
                if (!SubmitNames)
                {
                    if (Data.Args[0] == "open" && Data.isAdmin)
                    {
                        Writer.WriteLine(Config.Prefix + "You can now submit names using \"!name [Name]\"");
                        Writer.Flush();
                        SubmitNames = true;
                        Users.Clear();
                        Names.Clear();
                    }
                    else if (Data.Args[0] == "list")
                    {
                        var Message = "";
                        foreach (var i in Names)
                        {
                            Message += ", " + i + " (" + Users[Names.IndexOf(i)] + ")";
                        }
                        Writer.WriteLine(Config.Prefix + "List of names submitted: " + Message.Substring(2));
                        Writer.Flush();
                    }
                }
                else
                {
                    if (Data.Args[0] == "close" && Data.isAdmin)
                    {
                        SubmitNames = false;
                        Writer.WriteLine(Config.Prefix + "You can no longer submit names!");
                        Writer.Flush();
                        var Message = "";
                        foreach (var i in Names)
                        {
                            Message += ", " + i + " (" + Users[Names.IndexOf(i)] + ")";
                        }
                        Writer.WriteLine(Config.Prefix + "List of names submitted: " + Message.Substring(2));
                        Writer.Flush();
                    }
                    else
                    {

                        var temp = Data.Args[0].ToString();
                        if (AppCode.isIn(temp, Names.ToArray()))
                        {
                            if (!Users[Names.IndexOf(temp)].Contains(Data.Name)) { Users[Names.IndexOf(temp)] += ", " + Data.Name; }
                            else
                            {
                                Writer.WriteLine(Config.Prefix + Data.Name + ", you have already submitted that name!");
                                Writer.Flush();
                            }
                        }
                        else
                        {
                            Names.Add(temp);
                            Users.Add(Data.Name);
                        }

                    }
                }
            }
            return true;
        }
    }



}
