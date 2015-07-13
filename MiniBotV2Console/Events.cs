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
        public static void Init(ChatMessage Data, StreamWriter Writer)
        {
            //Config.EventList.Add(EVENTNAME.Main); // Trigger for Event Template
            //Config.EventList.Add(TestEvent.Main);
            Config.EventList.Add(GetTimeEvent.Main);
            Config.EventList.Add(IsFailEvent.Main);
            Config.EventList.Add(IsWinEvent.Main);
            Config.EventList.Add(VoteEvent.Main);
            Config.EventList.Add(NameEvent.Main);
            //Config.EventList.Add(GetModsEvent.Main);
            Config.EventList.Add(ConfigureEvent.Main);
            Config.EventList.Add(GreetEvent.Main);
            Config.EventList.Add(DeathThreatEvent.Main);
            //GetModsEvent.Main(Data, Writer);

        }
    }

/*public static class EVENTNAME // Event Template
{
    public static bool Main(ChatMessage Data, StreamWriter Writer)
    {

    }
}//*/

    public static class DeathThreatEvent // Event Template
    {
        public static string[] deathThreat = { "Go jump off a bridge, {0}!", "I don't like you, {0}", "Your adopted {0}", ""};
        public static bool Main(ChatMessage Data, StreamWriter Writer)
        {
            
            if (Data.Command == "!insult" /*&& Data.isAdmin*/ && Data.ListArgs.Count == 1)
            {
                Writer.WriteLine(Config.Prefix + string.Format(deathThreat[AppCode.GetRandomInt(0, deathThreat.Length)], Data.ListArgs[0]));
                Writer.Flush();
            }

            /*if (Data.Name.Contains("nightbot"))
            {
                Writer.WriteLine(Config.Prefix + string.Format(deathThreat[AppCode.GetRandomInt(0, deathThreat.Length)], Data.Name));
                Writer.Flush();
            }//*/            
            return false;
        }
    }//*/

    public static class ConfigureEvent
    {
        public static bool Main(ChatMessage Data, StreamWriter Writer)
        {
            if (Data.Command == "!conf")
            {
                if (Data.Args[0] == "greeting")
                {
                    if (Data.Args[1] == "on" && !GreetEvent.ShouldGreet)
                    {
                        GreetEvent.ShouldGreet = true;
                        Writer.WriteLine(Config.Prefix + "Greeting turned on");
                    }
                    else if (Data.Args[1] == "off" && GreetEvent.ShouldGreet)
                    {
                        GreetEvent.ShouldGreet = false;
                        Writer.WriteLine(Config.Prefix + "Greeting turned off");
                    }
                    Writer.Flush();
                }
            }
            return false;
        }

    }
    public static class GreetEvent 
    {
        public static string[] backTrigger = { "back", "im back", "i'm back", "i am back", "back :d", "and back" };
        public static string[] greetTrigger = { "g'day", "good evening", "hai", "yo", "hey chat", "sup all", "salut", "herro", "hellu", "hi there", "hey guys", "moi", "hi", "hello", "hay", "hey", "whats up", "hiya", "sup", "haya", "hola", "ola", "buna", "aloha", "what up", "heyo", "hayo" };
        public static string[] greetings = { "Hi", "Hello", "Hay", "Hey", "Hiya", "Sup", "Haya", "'Ello"};
        
        public static bool ShouldGreet = true;
        private static string LastGreet;
        private static bool Merlyin = false;
        private static bool x;
        public static bool Main(ChatMessage Data, StreamWriter Writer)
        {
            if (Data.Message != null)
            if ((AppCode.isIn(Data.Message.ToLower(), GreetEvent.greetTrigger) || AppCode.isIn(Data.Message.ToLower(), greetTrigger,"!") || AppCode.isIn(Data.Message.ToLower(), greetTrigger,"*")) && ShouldGreet && Data.Name != LastGreet)
            {

                Writer.WriteLine(Config.Prefix + greetings[AppCode.GetRandomInt(0, greetings.Length)] + " " + Data.Name);
                Writer.Flush();
                LastGreet = Data.Name;
                return true;
            }
            else if ((AppCode.isIn(Data.Message.ToLower(), backTrigger) || AppCode.isIn(Data.Message.ToLower(), backTrigger, "!") || AppCode.isIn(Data.Message.ToLower(), backTrigger, "*")) && ShouldGreet)
            {
                Writer.WriteLine(Config.Prefix + "Welcome back " + Data.Name + "  MrDestructoid /");
                Writer.Flush();
                return true;
            }
            
            if (Data.Command == "!greet" && Data.isAdmin && Data.ListArgs.ToArray().Length == 1)
            {
                Writer.WriteLine(Config.Prefix + greetings[AppCode.GetRandomInt(0, greetings.Length)] + " " + Data.ListArgs[0]);
                Writer.Flush();
                return true;
            }
            if (Data.Name == "bidaman98caty" && Data.Message.Contains("mini") && Data.Message.Contains("bot"));
            {
                //Writer.WriteLine(Config.Prefix + "<3");
                Writer.Flush();
            }


            return false;
        }
    }
    public static class GetModsEvent
    {
        static string x = "";
        static bool GettingMods = false;
        public static bool Main( ChatMessage Data, StreamWriter Writer )
        {



            if (Data.Command != null)
            {
                if ((Data.Command == "!getmods" || Data.Name.ToLower() == "getmods") && Data.Args.Count() == 0)
                {
                    Writer.WriteLine(Config.Prefix.Substring(0, Config.Prefix.Length - 1) + "/mods");
                    Writer.Flush();
                    GettingMods = true;
                }
                else
                if (Data.Command == "!getmods" && Data.Args[0] == "list")
                {
                    x = "Mods: ";
                    foreach (var i in Config.Mods)
                    {
                        x += i + ", ";
                    }
                    Writer.WriteLine(Config.Prefix + x.Substring(0, x.Length - 2));
                    Writer.Flush();
                }
                else
                if (Data.Message.Contains("the moderators of this room are:") && GettingMods)
                {

                    if (Data.Args.Length > 0)
                    {
                        Config.Mods.Clear();

                        Config.Mods.Add(Config.ConnectionName);

                        for (var i = 5; i < Data.Args.Count(); i++)
                        {
                            x = Data.Args[i].Replace(",", "");
                            if (i != Data.Args.Length - 1)
                            {
                                x = Data.Args[i].Substring(0, Data.Args[i].Length - 1);
                            }
                            Config.Mods.Add(x);
                        }
                    }



                    if (!Config.Mods.Contains("minijackb"))
                        Config.Mods.Add("minijackb");
                    if (!Config.Mods.Contains("crhybrid"))
                        Config.Mods.Add("crhybrid");

                    GettingMods = false;

                }
                return true;
            }
            return false;
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
            if (Data.Command == "!vote" && Data.Args.Length != 0)
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
                        double percent;
                        for (var x = 0; x < VoteNames.Count; x++)
                        {
                            percent = ((double)VoteAmount[x] / (double)VoteAmount.Count) * (double)100;
                            game += ", " + VoteNames[x] + ": " + VoteAmount[x] + " ("+ percent + "%)";
                        }

                        Writer.WriteLine(Config.Prefix + "Votes: " + game.Substring(2) + ". Total votes: " + VoteAmount.Count() );
                        Writer.Flush();
                    }
                    else if (Data.isAdmin)
                    { 
                        Writer.Flush();
                        Writer.Write(Config.Prefix + "Incorrect Syntax. Please use !vote open [game1] [game2]... ");
                        Writer.Flush();
                        Writer.Flush();
                    }
                    else
                    {
                        Writer.WriteLine(Config.Prefix + "Incorrect syntax or you do not have access to this command.");
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
            if (Data.Command == "!link")
            {
                if (!SubmitNames)
                {
                    if (Data.Args[0] == "open" && Data.isAdmin)
                    {
                        Writer.WriteLine(Config.Prefix + "You can now submit links using \"!link [link]\"");
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
                        Writer.WriteLine(Config.Prefix + "List of links submitted: " + Message.Substring(2));
                        Writer.Flush();
                    }
                }
                else
                {
                    if (Data.Args[0] == "close" && Data.isAdmin)
                    {
                        SubmitNames = false;
                        Writer.WriteLine(Config.Prefix + "You can no longer submit links!");
                        Writer.Flush();
                        var Message = "";
                        foreach (var i in Names)
                        {
                            Message += ", " + i + " (" + Users[Names.IndexOf(i)] + ")";
                        }
                        Writer.WriteLine(Config.Prefix + "List of links submitted: " + Message.Substring(2));
                        Writer.Flush();
                    }
                    else if (Data.Args[0] == "remove" && Data.isAdmin)
                    {
                        if (Data.Args[1] != null)
                        if (Names.Remove(Data.Args[1]))
                        {
                            Writer.WriteLine(Config.Prefix + "That entry was removed");
                            Writer.Flush();
                        }
                        else
                        {
                            Writer.WriteLine(Config.Prefix + "I could not find that entry, ", Data.Name);
                            Writer.Flush();
                        }

                    }
                    else
                    {

                        var temp = Data.Args[0].ToString();
                        if (AppCode.isIn(temp, Names.ToArray()))
                        {
                            if (!Users[Names.IndexOf(temp)].Contains(Data.Name)) { Users[Names.IndexOf(temp)] += ", " + Data.Name; }
                            else
                            {
                                Writer.WriteLine(Config.Prefix + Data.Name + ", you have already submitted that link!");
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
