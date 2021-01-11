using Calculus.DTO.Config;
using Calculus.DTO.Xml;
using Calculus.SystServs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Linq;
using System.Threading;

namespace Calculus.DTO.Singltones
{
    /// <summary>
    /// Синглтон. Набор поступивших данных для расчёта Total.
    /// </summary>    
    public class CalculationsSingl : ICalculationsSingl
    {
        /// <summary>
        /// Действия которые предстоит выполнить.
        /// </summary>
        public List<Action_Data> Items { get; set; }

        /// <summary>
        /// Последнее выполненное действие. И Total
        /// </summary>
        public Action_Data_Total Current { get; set; }

        /// <summary>
        /// Необходимо ли пересчитать Current
        /// </summary>
        public bool ShouldCalculate { get; set; }


        public CalculationsSingl()
        {
            Current = new Action_Data_Total();
            Items = new List<Action_Data>();
            ShouldCalculate = false;

            Task.Run(() =>
            {
                bool success = false;
                while (success == false)
                {
                    Thread.Sleep(1);
                    try 
                    {
                        string text = File.ReadAllText(AppConfig.FilePath_Current);
                        var result2 = text.DeserializeStrxml<XmlCalcCurrent>();
                        if (result2.Current != null)
                            Current = result2.Current;
                        success = true;
                    }
                    catch 
                    {
                        //При создании файла, он какое-то время недоступен для чтения. 
                        //По-этому сделал такой цикл в параллельном потоке
                    }
                }
            });
        }
    }
}
