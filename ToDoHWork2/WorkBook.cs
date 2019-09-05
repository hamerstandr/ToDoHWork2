using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ToDoHWork2
{
    public class WorkBook
    {
        [XmlArray("Works")]
        private List<Tasks> works = new List<Tasks>();

        public List<Tasks> Works { get => works; set => works = value; }

        public static void Save(object obj, string path)
        {
            try
            {
                XmlSerializer s = new XmlSerializer(obj.GetType());
                using (StreamWriter writer = new StreamWriter(path))
                {
                    s.Serialize(writer, obj);
                }
            }
            catch
            { }

        }

        public static T Load<T>(string path)
        {
            try
            {
                XmlSerializer s = new XmlSerializer(typeof(T));
                using (StreamReader reader = new StreamReader(path))
                {
                    object obj = s.Deserialize(reader);
                    return (T)obj;
                }
            }
            catch { return default; }


        }
        public void Add(Tasks _Task)
        {
            Works.Add(_Task);
        }
        public Tasks Add(string _Title)
        {
            Tasks _Task = new Tasks() { Items=new List<Task>(), Title = _Title, Date = DateTime.Now };
            Works.Add(_Task);
            return _Task;
        }
    }
}
