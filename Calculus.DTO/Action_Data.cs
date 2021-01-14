using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Calculus.DTO
{
    [Serializable]
    public class Action_Data 
    {
        /// <summary>
        /// Действие
        /// </summary>
        [XmlElement(Order = 0)]
        public string Action { get; set; }

        /// <summary>
        /// Слагаемое
        /// </summary>
        [XmlElement(Order = 1)]
        public double Data { get; set; }       
    }
}
