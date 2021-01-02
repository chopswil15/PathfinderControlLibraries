using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace StatBlockChecker
{
    public enum MessageType
    {
        Pass = 1,
        Fail,
        Info
    }
    public class MessageInfo
    {      
        public MessageType MessageType { get; set; }
        public string MessageTypeValue
        {
            get { return MessageType.ToString(); }
        }
        public string Message { get; set; }
        public string Formula { get; set; }
        public string Calculation { get; set; }

        public string MessageDisplay
        {
            get
            {
                var returnMessage = Message.Trim();
                if (!string.IsNullOrEmpty(Formula))
                {
                    returnMessage += " " + Formula.Trim();
                }
                if (!string.IsNullOrEmpty(Calculation) && Calculation.Trim() != ":")
                {
                    returnMessage += " " + Calculation.Trim();
                }

                return returnMessage;
            }
        }

        public void AddFail(string message)
        {
            MessageType = MessageType.Fail;
            Message = message;
        }

        public void AddFail(string message, string formula)
        {
            MessageType = MessageType.Fail;
            Message = message;
            Formula = formula;
        }

        public void AddFail(string message, string formula, string calculation)
        {
            MessageType = MessageType.Fail;
            Message = message;
            Formula = formula;
            Calculation = calculation;
        }

        public void AddInfo(string message)
        {
            MessageType = MessageType.Info;
            Message = message;
        }

        public void AddPass(string message)
        {
            MessageType = MessageType.Pass;
            Message = message;
        }

        public void AddPass(string message, string formula)
        {
            MessageType = MessageType.Pass;
            Message = message;
            Formula = formula;
        }       
    }
}
