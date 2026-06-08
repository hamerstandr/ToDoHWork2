using System;
using System.IO;

namespace ToDoHWork2
{
    class Database
    {
        public WorkBook Data { get; } = new();
        private readonly string _pathFileData;

        public Database()
        {
            _pathFileData = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                typeof(App).Assembly.GetName().Name);

            if (!Directory.Exists(_pathFileData))
                Directory.CreateDirectory(_pathFileData);

            _pathFileData = Path.Combine(_pathFileData, "Data.xml");

            var loadedData = WorkBook.Load<WorkBook>(_pathFileData);
            if (loadedData != null)
            {
                Data.Works = loadedData.Works;
            }
            else
            {
                Save();
            }
        }

        internal void Save(string path = "")
        {
            if (string.IsNullOrEmpty(path))
            {
                path = _pathFileData;
            }
            WorkBook.Save(Data, path);
        }
    }
}
