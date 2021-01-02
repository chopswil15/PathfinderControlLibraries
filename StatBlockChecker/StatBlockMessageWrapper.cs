using System;
using System.Collections.Generic;
using Utilities;
using System.Linq;
using System.Text;

using System.Xml;
using PathfinderGlobals;

namespace StatBlockChecker
{
    public class StatBlockMessageWrapper
    {
        private MessageXML _messageXML;
        private List<MessageInfo> _messageInfos;

        public StatBlockMessageWrapper()
        {
            _messageXML = new MessageXML();
            _messageInfos = new List<MessageInfo>();
        }

        public XmlDocument MessageXML
        {
            get { return _messageXML.GenerateMessageGrouping(); }
        }

        public List<MessageInfo> MessageInfos
        {
            get { return _messageInfos; }
        }

        public bool MessageExists(string message)
        {
            return _messageInfos.Exists(x => x.Message.Contains(message));
        }

        public void AddPass(string CheckName)
        {
            _messageXML.AddPass(CheckName);
            MessageInfo messageInfo = new MessageInfo();
            messageInfo.AddPass(CheckName);
           MessageInfoAdd(messageInfo);
        }

        public void AddPass(string CheckName, string Formula)
        {
            _messageXML.AddPass(CheckName, Formula);
            MessageInfo messageInfo = new MessageInfo();
            messageInfo.AddPass(CheckName, Formula);
           MessageInfoAdd(messageInfo);
        }

        public void AddInfo(string Info)
        {
            _messageXML.AddInfo(Info);
            MessageInfo messageInfo = new MessageInfo();
            messageInfo.AddInfo(Info);
           MessageInfoAdd(messageInfo);
        }

        public void AddFail(string CheckName, string Computed, string SB)
        {
            _messageXML.AddFail(PathfinderConstants.PAREN_LEFT + CheckName + ") Computed: " + Computed + " Stat Block: " + SB);
            MessageInfo messageInfo = new MessageInfo();
            messageInfo.AddFail(PathfinderConstants.PAREN_LEFT + CheckName + ") Computed: " + Computed + " Stat Block: " + SB);
           MessageInfoAdd(messageInfo);
        }

        public void AddFail(string CheckName, string Computed, string SB, string Formula, string Calculation)
        {
            _messageXML.AddFail(PathfinderConstants.PAREN_LEFT + CheckName + ") Computed: " + Computed + " Stat Block: " + SB, Formula, Calculation);
            MessageInfo messageInfo = new MessageInfo();
            messageInfo.AddFail(PathfinderConstants.PAREN_LEFT + CheckName + ") Computed: " + Computed + " Stat Block: " + SB, Formula, Calculation);
           MessageInfoAdd(messageInfo);
        }

        public void AddFail(string CheckName, string Computed, string SB, string Formula)
        {
            _messageXML.AddFail(PathfinderConstants.PAREN_LEFT + CheckName + ") Computed: " + Computed + " Stat Block: " + SB, Formula);
            MessageInfo messageInfo = new MessageInfo();
            messageInfo.AddFail(PathfinderConstants.PAREN_LEFT + CheckName + ") Computed: " + Computed + " Stat Block: " + SB, Formula);
           MessageInfoAdd(messageInfo);
        }

        public void AddFail(string CheckName, string Message)
        {
            _messageXML.AddFail(PathfinderConstants.PAREN_LEFT + CheckName + ") " + Message);
            MessageInfo messageInfo = new MessageInfo();
            messageInfo.AddFail(PathfinderConstants.PAREN_LEFT + CheckName + ") " + Message);
           MessageInfoAdd(messageInfo);
        }

        private void MessageInfoAdd(MessageInfo messageInfo)
        {
            if(!_messageInfos.Contains(messageInfo)) _messageInfos.Add(messageInfo);
        }
    }
}
