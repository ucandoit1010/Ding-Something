using System;
using System.IO;

namespace DINGSOMETHING.Models.Helper
{
    public class FileHelper
    {
        public string GetScriptFromFile(string fileName) {

            string path = Path.Combine(
                System.Environment.CurrentDirectory,
                "Models",
                "BusinessAccess",
                "SQL",
                string.Concat(fileName,".sql"));
            
            return File.ReadAllText(path);

        }

    }


}