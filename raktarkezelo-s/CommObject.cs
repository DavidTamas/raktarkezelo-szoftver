using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Communication
{
    [Serializable]
    public class CommObject
    {
        public char Type { get; set; }
        public Object Obj { get; set; }
        public string Message { get; set; }
        public DateTime Date { get; set; }

        public CommObject() { }
        public CommObject(char type, Object obj, string msg)
        {
            Type = type;
            Obj = obj;
            Message = msg;
            Date = DateTime.Now;
        }

        public override string ToString()
        {
            return Message + " [" + Date.ToString() + "]";
        }
    }
}
