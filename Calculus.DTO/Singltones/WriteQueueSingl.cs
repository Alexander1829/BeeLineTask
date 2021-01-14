using Calculus.DTO.Xml;
using System;
using System.Collections.Generic;
using System.Text;

namespace Calculus.DTO.Singltones
{
    /// <summary>
    /// Синглтон. Очередь на запись в файлы.
    /// </summary>
    public class WriteQueueSingl : IWriteQueueSingl
    {
        public List<CalcResult> WriteQueue { get; set; }
        public bool FilesShouldReload { get; set; }

        public WriteQueueSingl()
        {
            WriteQueue = new List<CalcResult>();
        }

        /// <summary>
        /// Преобразует Items из WriteQueue в строку для записи в файл
        /// </summary>
        /// <returns></returns>
        public string QueueToFileText()
        {
            StringBuilder s = new StringBuilder();
            for (int i = WriteQueue.Count - 1; i >= 0; i--) //в обратном порядке, сначала последние запросы
            {
                for (int ii = WriteQueue[i].Items.Count - 1; ii >= 0; ii--) //в обратном порядке, сначала последние данные из запроса
                {
                    s.Append("<Item>")
                        .Append("<Action>").Append(WriteQueue[i].Items[ii].Action).Append("</Action>")
                        .Append("<Data>").Append(WriteQueue[i].Items[ii].Data).Append("</Data>")
                    .Append("</Item>");
                }
            }
            return s.ToString();
        }
    }
}
