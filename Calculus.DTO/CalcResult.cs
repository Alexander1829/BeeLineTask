using System;
using System.Collections.Generic;
using System.Text;

namespace Calculus.DTO
{
    public class CalcResult
    {
        /// <summary>
        /// Выполненные действия, без последнего
        /// </summary>
        public List<Action_Data> Items { get; set; }

        /// <summary>
        /// Последнее выполненное действие и Total
        /// </summary>
        public Action_Data_Total Current { get; set; }

        public CalcResult(){
            Current = new Action_Data_Total();
            Items = new List<Action_Data>();
        }        
    }
}
