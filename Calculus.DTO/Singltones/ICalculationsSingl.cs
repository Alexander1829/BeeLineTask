using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Calculus.DTO.Singltones
{
    /// <summary>
    /// Синглтон. Набор поступивших данных для расчёта Total
    /// </summary>  
    public interface ICalculationsSingl
    {
        /// <summary>
        /// Действия которые предстоит выполнить.
        /// </summary>
        List<Action_Data> Items { get; set; }

        /// <summary>
        /// Последнее выполненное действие. И Total
        /// </summary>
        Action_Data_Total Current { get; set; }

        /// <summary>
        /// Необходимо ли пересчитать Current
        /// </summary>
        bool ShouldCalculate { get; set; }
    }
}
