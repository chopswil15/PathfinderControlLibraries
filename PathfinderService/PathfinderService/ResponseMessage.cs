﻿namespace PathfinderService
{
    public class ResponseMessage
    {
        public string Message { get; set; }
        public string Detail { get; set; }
        public ResponseMessageType MessageType { get; set; }
    }

    public enum ResponseMessageType
    {
        Info = 0,
        Warn,
        Error
    }
}