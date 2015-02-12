using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace MiniBotV2
{
    class MessageInfo
    {
        string Name;
        string Message;
        string Command;
        string[] args;

        public MessageInfo(string Message)
        {
            string newData = Regex.Replace(Message, @"!.+ :", ":");
            Regex nameExpr = new Regex(@":.*:");
            Match match = nameExpr.Match(newData);

            if (match.Success)
            {
                Name = Regex.Replace(match.Groups[0].Value, ":", "");
            }

            Message = Regex.Replace(newData,":.*:","");

            Regex ComExpr = new Regex(@"!\w*");
            match = ComExpr.Match(Message);

            if (match.Success)
            {
                Command = match.Groups[0].Value;
            }

            Command = Regex.Replace(Command, " ", "");

            args = Message.Split(' ');
            List<string> tempArgs = new List<string>();
            for (var i = 1; i < args.Length; i++)
            {
                tempArgs.Add(args[i]);
            }
            args = tempArgs.ToArray();

        }
    }
}
