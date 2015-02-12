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

namespace MiniBotV2
{
    class IRC
    {
        public void Connect(string channel)
        {
            bool test = true;
            string data;
            TcpClient Client = new TcpClient("irc.twitch.tv", 6667); // irc server url + port
            NetworkStream NwStream = Client.GetStream(); //Get The TCP/Network Stream And Assign It To NwStream
            StreamReader Reader = new StreamReader(NwStream, Encoding.GetEncoding("iso8859-1")); //Read The Data From The IRC Server
            StreamWriter Writer = new StreamWriter(NwStream, Encoding.GetEncoding("iso8859-1")); //Write To The Server
            Writer.WriteLine("USER " + Config.BotName + " tmi twitch " + Config.BotName); //Gives The Server The Required USER Information.
            Writer.Flush();// must flush after writing
            Writer.WriteLine("PASS " + Config.authCode); // tell server the auth key
            Writer.Flush();
            Writer.WriteLine("NICK " + Config.BotName); // tell server your display name (must be same as username)

            Writer.Flush();
            Writer.WriteLine("JOIN #" + channel); //Joins The Channel Specified.
            string prefix = "PRIVMSG #" + channel + " :";
            Writer.Flush();

            while (Config.shouldRun)
            {
                data = Reader.ReadLine();
                if (data != null)
                {
                    Console.WriteLine(data);
                    if (test)
                    {
                        Writer.WriteLine(prefix + "TEST COMPLETE!");
                        Writer.Flush();
                        test = false;
                    }
                }
            }
        }
    }
}
