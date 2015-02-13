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

        public ChatMessage(string message)
        {
            Raw = message;
            message = message.ToLower();
            string newData = Regex.Replace(message, @"!.+ :", ":");
            Regex nameExpr = new Regex(@":.*:");
            Match match = nameExpr.Match(newData);

            if (match.Success)
            {
                Name = Regex.Replace(match.Groups[0].Value, ":", "");
            }

            message = Regex.Replace(newData,":.*:","");

            Message = message;

            Regex ComExpr = new Regex(@"!\w*");
            match = ComExpr.Match(Message);

            if (match.Success)
            {
                Command = match.Groups[0].Value;
                Command = Regex.Replace(Command, " ", "");
            }

            

            Args = Message.Split(' ');
            List<string> tempArgs = new List<string>();
            for (var i = 1; i < Args.Length; i++)
            {
                tempArgs.Add(Args[i]);
            }
            Args = tempArgs.ToArray();

            this.isAdmin = AppCode.isMod(this.Name);

        }
    }
}
