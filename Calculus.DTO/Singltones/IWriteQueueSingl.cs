using Calculus.DTO.Xml;
using System;
using System.Collections.Generic;
using System.Text;

namespace Calculus.DTO.Singltones
{
    /// <summary>
    /// Синглтон. Набор данных для дальнейшего переноса в файлы
    /// </summary>
    public interface IWriteQueueSingl
    {
        /// <summary>
        /// Очередь данных на запись в файлы
        /// </summary>
        List<CalcResult> WriteQueue { get; set; }

        /// <summary>
        /// Надо ли записывать в файлы
        /// </summary>
        bool FilesShouldReload {get;set;}

        /// <summary>
        /// Преобразует Items из WriteQueue в строку для записи в файл
        /// </summary>
        /// <returns></returns>
        string QueueToFileText();
    }
}
