using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Communication
{
    [Serializable]
    public class CommObject
    {
        public char UserType { get; set; }
        public char Type { get; set; }
        public string Message { get; set; }
        public DateTime Date { get; set; }

        public CommObject() { }
        public CommObject(char usertype, char type, string msg)
        {
            UserType = usertype;
            Type = type;
            Message = msg;
            Date = DateTime.Now.ToLocalTime();
        }

        public override string ToString()
        {
            return Message + " [" + Date.ToString() + "]";
        }
    }
}
