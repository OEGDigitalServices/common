using System;

namespace Orange.Common.Entities
{
    public class MessageDetails
    {
        public DateTime MessageDate;
        public string MessageTarget;
        public string ReferenceID;
        public string Alias;
        public string MessageOriginator;
        public string MessageStatus;
        public string MessageBody;
        public int MessageOption = 2;
    }
}
