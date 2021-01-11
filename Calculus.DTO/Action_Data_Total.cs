//using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace Calculus.DTO
{
    [Serializable]
    public class Action_Data_Total
    {
        /// <summary>
        /// Действие
        /// </summary>
        [XmlElement(Order = 1)]        
        public string Action { get; set; }

        /// <summary>
        /// Слагаемое
        /// </summary>
        [XmlElement(Order = 2)]
        public double Data { get; set; }

        /// <summary>
        /// Сумма
        /// </summary>
        [XmlElement(Order = 0)]
        public double Total { get; set; }        
    }
}