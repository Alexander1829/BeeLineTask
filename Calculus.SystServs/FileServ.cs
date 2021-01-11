using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Calculus.SystServs
{
    public class FileServ
    {
        /// <summary>
        /// Дописывает строку в начало файла
        /// </summary>
        /// <param name="filePath">Путь к файлу</param>
        /// <param name="appendText">Строка будет дописана первой в файл</param>
        public void AppendAtBeginFile(string filePath, string appendText)
        {
            string text = File.ReadAllText(filePath);
            if (string.IsNullOrWhiteSpace(text)) //Если файл пустой, то для красоты не будем добавлять "\n"
                File.WriteAllTextAsync(path: filePath, contents: appendText);
            else
                File.WriteAllTextAsync(path: filePath, contents: string.Concat(appendText, "\n", text));
        }
    }
}
