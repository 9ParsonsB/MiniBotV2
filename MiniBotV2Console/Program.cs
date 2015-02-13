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
    class Program
    {
        static TcpClient Client;
        static NetworkStream NwStream;
        static StreamWriter Writer;
        static StreamReader Reader;
        static string channel = "minijackb";
        static void Main(string[] args)
        {
            Config.ConnectionName = channel.ToLower();
            Client = new TcpClient("irc.twitch.tv", 6667); // irc server url + port
            NwStream = Client.GetStream(); //Get The TCP/Network Stream And Assign It To NwStream

            Writer = new StreamWriter(NwStream, Encoding.GetEncoding("iso8859-1")); //Write To The Server;
            Reader = new StreamReader(NwStream, Encoding.GetEncoding("iso8859-1")); //Read The Data From The IRC Server;

            Writer.WriteLine("USER " + Config.BotName + " tmi twitch " + Config.BotName); //Gives The Server The Required USER Information.
            Writer.Flush();// must flush after writing
            Writer.WriteLine("PASS " + Config.authCode); // tell server the auth key
            Writer.Flush();
            Writer.WriteLine("NICK " + Config.BotName); // tell server your display name (must be same as username)
            Config.Prefix = "PRIVMSG #" + Config.ConnectionName + " : ";
            Writer.Flush();
            Writer.WriteLine("JOIN #" + Config.ConnectionName); //Joins The Channel Specified.

            Writer.Flush();

            string rawdata;
            ChatMessage Data;

            Writer.WriteLine(Config.Prefix + "I'm Here!");
            Writer.Flush();

            Event.Init();

            while (Config.shouldRun)
            {
                rawdata = Reader.ReadLine();

                if (rawdata != null)
                {
                    Console.WriteLine(rawdata);
                    Data = new ChatMessage(rawdata);

                    if (!rawdata.Contains(":tmi.twitch.tv") && Data.Name != null)
                    {
                        foreach (var i in Config.EventList)
                        {
                            i(Data, Writer);
                        }
                    }
                }
            }
        }
    }
}

