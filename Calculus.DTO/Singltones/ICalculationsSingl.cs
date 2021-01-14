using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Calculus.DTO.Singltones
{
    /// <summary>
    /// Синглтон. Набор поступивших данных для расчётов.
    /// </summary>  
    public interface ICalculationsSingl
    {
        /// <summary>
        /// Очередь на выполнение. (Действия которые предстоит выполнить)
        /// </summary>
        List<Action_Data> Items { get; set; }

        /// <summary>
        /// Последнее выполненное действие и Total
        /// </summary>
        Action_Data_Total Current { get; set; }

        /// <summary>
        /// Необходимо ли пересчитать Current (завёл отдельный флажок, чтобы тестировать на больших очередях Items)
        /// </summary>
        bool ShouldCalculate { get; set; }

        /// <summary>
        /// Завершена ли загрузка Current из файла (загрузка при старте приложения: в синглтон из файла)
        /// </summary>
        bool IsCurrentUploaded { get; set; }
    }
}
