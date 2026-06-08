using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace ToDoHWork2
{
    [XmlRoot("Tasks", Namespace = "http://www.cpandl.com", IsNullable = false)]
    public class Tasks
    {
        [XmlAttribute]
        public string Title { get; set; } = string.Empty;

        [XmlArray("Items")]
        public List<Task> Items { get; set; } = new();

        [XmlAttribute]
        public DateTime Date { get; set; }

        public void Add(string title)
        {
            Items.Add(new Task
            {
                Complte = false,
                Title = title,
                Date = DateTime.Now
            });
        }
    }
}
