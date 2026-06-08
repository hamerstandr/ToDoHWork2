using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace ToDoHWork2
{
    public class WorkBook
    {
        [XmlArray("Works")]
        public List<Tasks> Works { get; set; } = new();

        public static void Save(object obj, string path)
        {
            try
            {
                var serializer = new XmlSerializer(obj.GetType());
                using var writer = new StreamWriter(path);
                serializer.Serialize(writer, obj);
            }
            catch
            {
                // Silent fail for now
            }
        }

        public static T? Load<T>(string path) where T : class
        {
            try
            {
                var serializer = new XmlSerializer(typeof(T));
                using var reader = new StreamReader(path);
                return (T?)serializer.Deserialize(reader);
            }
            catch
            {
                return default;
            }
        }

        public Tasks Add(string title)
        {
            var task = new Tasks
            {
                Items = new List<Task>(),
                Title = title,
                Date = DateTime.Now
            };
            Works.Add(task);
            return task;
        }
    }
}
