using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Calculus.DTO.Xml
{
    /// <summary>
    /// DTO для сериализации Action_Data_Total в xml-текст
    /// </summary>
    [Serializable]
    [XmlRoot("Root", IsNullable = false)]
    public class XmlCalcCurrent
    {
        [XmlElement(ElementName = "Current")]
        public Action_Data_Total Current { get; set; }
    }
}
