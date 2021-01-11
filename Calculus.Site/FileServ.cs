using System;
using System.Collections.Generic;
using System.Text;

namespace Calculus.Servs
{
    public class FileServ //: IDisposable
    {
        /// <summary>
        /// Очищает файл и записывает в него новую строку
        /// </summary>
        /// <param name="fileText"></param>
        /// <param name="filePath"></param>
        public void WriteInFile(string fileText, string filePath)
        {
            using (var file = new System.IO.StreamWriter(filePath, false))
            {
                file.Write(fileText);
            }
        }

        //public void Dispose()
        //{
        //    throw new NotImplementedException();
        //}
    }
}
