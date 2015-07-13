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

        public ChatMessage( string message )
        {
            Raw = message;
            message = message.ToLower();

            //string newData = Regex.Replace(message, @"!.+ :", ":");

            Regex nameExpr1 = new Regex(@":.*!.*@.*\."); // TODO: fix bug where if there is a colon in the message, messes up name detection and cuts out bits of the message.
            Regex nameExpr2 = new Regex(@":.*!");

            Regex MessageExpr1 = new Regex(@".tv PRIVMSG #" + Config.ConnectionName + " :.+");

            string messageLengthPadding = ".tv PRIVMSG #" + Config.ConnectionName + " :";

            Match nameMatch1 = nameExpr1.Match(Raw);

            Match messageMatch = MessageExpr1.Match(Raw);
            Match nameMatch2;

            string FormattedData;

            if (message.ToLower().Contains("the modera"))
            {

            }

            if (nameMatch1.Success)
            {
                nameMatch2 = nameExpr2.Match(nameMatch1.Groups[0].ToString());
                //Console.WriteLine("namematch2 : " + nameMatch2.Groups[0].ToString());

                if (nameMatch2.Success)
                {
                    //Console.WriteLine("match: " + nameMatch2.Groups[0]);

                    Name = (nameMatch2.Groups[0].ToString().Substring(1, nameMatch2.Groups[0].Length - 2));
                    //Console.WriteLine("name: " + Name);

                    if (messageMatch.Success)
                    {
                        Message = messageMatch.Groups[0].ToString().Substring(messageLengthPadding.Length);
                    }


                    /*
                    Console.WriteLine(messageMatch.Groups[0].ToString());
                    FormattedData = (message.Substring(nameMatch2.Groups[0].Value.Length)); //Give output of "name :message"

                    messageMatch = MessageExpr.Match(FormattedData);
                    //*/


                    //Message = (messageMatch.Groups[0].Value.Substring(1));
                    //Name = (FormattedData.Substring(0, FormattedData.Length - Message.Length - 2));
                    //Name = (Name.Substring(0, 1).ToUpper() + Name.Substring(1));

                    //Console.WriteLine(Command);
                }

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
                messageMatch = ComExpr.Match(Message);

                if (messageMatch.Success || Message.Contains("the moderators of this room are:"))
                {
                    Command = messageMatch.Groups[0].Value;
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
