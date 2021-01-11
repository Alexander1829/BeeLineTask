using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Calculus.DTO.Config
{
    public class AppConfig
    {
        /// <summary>
        /// Путь к файлу-хранилищу данных, со значением Current
        /// </summary>
        public static string FilePath_Current
        {            
            get {
                var path = string.Format("{0}\\{1}", FolderPath, FileName_Current); 
                return path;
            }
        }

        /// <summary>
        /// Путь к файлу-хранилищу данных, со значением Items
        /// </summary>
        public static string FilePath_Items
        {
            get
            {
                var path = string.Format("{0}\\{1}", FolderPath, FileName_Items);
                return path;
            }
        }

        /// <summary>
        /// Путь к папке, хранящей файлы-данных.
        /// </summary>
        public static string FolderPath
        {
            get
            {
                return Directory.GetCurrentDirectory();
            }
        }

        public static string FileName_Items
        {
            get
            {
                return "DataStore_Items.xml";                
            }
        }

        public static string FileName_Current
        {
            get
            {
                return "DataStore_Current.xml";
            }
        }
    }
}
