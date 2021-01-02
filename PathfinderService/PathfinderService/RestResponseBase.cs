using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PathfinderService
{
    public class RestResponseBase
    {
        public RestResponseBase()
        {
            Messages = new List<ResponseMessage>();
        }
        public List<ResponseMessage> Messages { get; set; }
        public bool HasErrors
        {
            get
            {
                if (Messages != null && Messages.Any(x => x.MessageType == ResponseMessageType.Error)) return true;
                return false;
            }
        }
    }
}