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
    public class ChatMessage
    {
        public string Name;
        public string Message;
        public string Command;
        public string Raw;
        public bool isAdmin;
        public string[] Args;
        public List<string> ListArgs;

        public ChatMessage(string message)
        {
            Raw = message;
            message = message.ToLower();
            string newData = Regex.Replace(message, @"!.+ :", ":");
            Regex nameExpr = new Regex(@":.*:"); // TODO: fix bug where if there is a colon in the message, messes up name detection and cuts out bits of the message.

            Regex testExpr = new Regex(@":.+privmsg");
            Regex MessageExpr = new Regex(@":.+");

            Match match = nameExpr.Match(newData);

            Match newMatch = testExpr.Match(message);

            string FormattedData;

            if (message.ToLower().Contains("the modera"))
            {

            }

            if (newMatch.Success)
            {
                FormattedData = (message.Substring(newMatch.Groups[0].Value.Length + 2)); //Give output of "name :message"

                newMatch = MessageExpr.Match(FormattedData);

               

                Message = (newMatch.Groups[0].Value.Substring(1));
                Name = (FormattedData.Substring(0, FormattedData.Length - Message.Length - 2));
                Name = (Name.Substring(0, 1).ToUpper() + Name.Substring(1));

                Console.WriteLine(Command);
            }



            /*if (match.Success)
            {
                Name = Regex.Replace(match.Groups[0].Value, ":", "");
            }

            message = Regex.Replace(newData,":.*:","");

            Message = message;//*/
            if (Message != null)
            {
                Regex ComExpr = new Regex(@"!\w+");
                match = ComExpr.Match(Message);

                if (match.Success || Message.Contains("the moderators of this room are:"))
                {
                    Command = match.Groups[0].Value;
                    Command = Regex.Replace(Command, " ", "");
                }//*/


            
                Args = Message.Split(' ');
                List<string> tempArgs = new List<string>();
                for (var i = 1; i < Args.Length; i++)
                {
                    tempArgs.Add(Args[i]);
                }
                ListArgs = tempArgs;
                Args = tempArgs.ToArray();

                this.isAdmin = AppCode.isMod(this.Name.ToLower());
            }

            

        }
    }
}
