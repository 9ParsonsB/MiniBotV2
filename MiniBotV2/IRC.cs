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
using System.Windows.Forms;

namespace MiniBotV2
{
    class IRC
    {
        bool test = true;
        public bool HasConnected = false;
        string data;
        string prefix;
        StreamWriter Writer;
        StreamReader Reader;
        NetworkStream NwStream;
        TcpClient Client;
        public void Connect(string channel, BackgroundWorker bg)
        {
            
            Client = new TcpClient("irc.twitch.tv", 6667); // irc server url + port
            NwStream = Client.GetStream(); //Get The TCP/Network Stream And Assign It To NwStream
            Reader = new StreamReader(NwStream, Encoding.GetEncoding("iso8859-1")); //Read The Data From The IRC Server
            Writer = new StreamWriter(NwStream, Encoding.GetEncoding("iso8859-1")); //Write To The Server
            Writer.WriteLine("USER " + Config.BotName + " tmi twitch " + Config.BotName); //Gives The Server The Required USER Information.
            Writer.Flush();// must flush after writing
            Writer.WriteLine("PASS " + Config.authCode); // tell server the auth key
            Writer.Flush();
            Writer.WriteLine("NICK " + Config.BotName); // tell server your display name (must be same as username)
            prefix = "PRIVMSG #" + channel + " :";
            Writer.Flush();
            Writer.WriteLine("JOIN #" + channel); //Joins The Channel Specified.

            Writer.Flush();
            HasConnected = true;
            
        }

        public List<string> Loop()
        {

            List<string> toWrite = new List<string>();
            data = Reader.ReadLine();
            if (data != null)
            {
                MessageBox.Show(data);
                toWrite.Add(data);
                if (test)
                {
                    Writer.WriteLine(prefix + "TEST COMPLETE!");
                    Writer.Flush();
                    test = false;
                }
            }

            return toWrite;
        }
    }
}
