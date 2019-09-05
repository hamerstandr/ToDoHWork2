using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace ToDoHWork2
{
    [XmlRoot("Tasks", Namespace = "http://www.cpandl.com",
   IsNullable = false)]
    public class Tasks
    {
        [XmlAttribute]
        public string Title { get; set; }
        [XmlArray("Items")]
        private List<Task> items = new List<Task>();

        public List<Task> Items { get => items; set => items = value; }
        [XmlAttribute]
        public DateTime Date { get; set; }
        public void Add(Task _Task)
        {
            Items.Add(_Task);
        }
        public void Add(string _Title)
        {
            Task _Task = new Task() { Complte = false, Title = _Title, Date = DateTime.Now };
            Items.Add(_Task);
        }
    }

}
