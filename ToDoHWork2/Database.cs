using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoHWork2
{
    class Database
    {
        public readonly WorkBook Data = new WorkBook();
        readonly string PathFileData= Path.Combine(Environment.GetFolderPath(
    Environment.SpecialFolder.ApplicationData), typeof(App).Assembly.GetName().Name);
        public Database()
        {
            if(!Directory.Exists(PathFileData))
                Directory.CreateDirectory(PathFileData);
            PathFileData = Path.Combine(PathFileData, "Data.xml");
            Data = WorkBook.Load<WorkBook>(PathFileData);
            if (Data == null)
            {
                Data = new WorkBook();
                WorkBook.Save(Data, PathFileData);
            }
                
        }
        
        internal void Save(string Path="")
        {
            if (Path == "")
            {
                Path = PathFileData;
            }
            WorkBook.Save(Data, Path);
        }

    }
}
