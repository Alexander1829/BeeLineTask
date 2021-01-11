using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Calculus.SystServs
{
    ////public static class XmlServStat
    //public static class XmlServStat
    //{
    //    /// <summary>
    //    /// Сериализует объект в строку-xml.
    //    /// </summary>
    //    /// <typeparam name="T"></typeparam>
    //    /// <param name="obj"></param>
    //    /// <returns></returns>
    //    public static string SerializeXmlToStr<T>(this T obj)
    //    {
    //        if (obj == null)
    //        {
    //            return string.Empty;
    //        }
    //        try
    //        {
    //            var xmlserializer = new XmlSerializer(typeof(T));
    //            var sw = new StringWriter();
    //            using (var writer = XmlWriter.Create(sw))
    //            {
    //                xmlserializer.Serialize(writer, obj);
    //                return sw.ToString();
    //            }
    //        }
    //        catch (Exception e)
    //        {
    //            throw e;
    //        }
    //    }

    //    /// <summary>
    //    /// Десериализует строку в объект
    //    /// </summary>
    //    /// <typeparam name="T"></typeparam>
    //    /// <param name="xmlString"></param>
    //    /// <returns></returns>
    //    public static T DeserializeStrxml<T>(this string xmlString) where T : class, new()
    //    {
    //        if (string.IsNullOrEmpty(xmlString))
    //        {
    //            return new T();
    //        }
    //        try
    //        {
    //            XmlSerializer serializer = new XmlSerializer(typeof(T), new XmlRootAttribute("Calculations"));
    //            var stringReader = new StringReader(xmlString);
    //            T result = (T)serializer.Deserialize(stringReader);
    //            return result;
    //        }
    //        catch (Exception e)
    //        {
    //            throw e;
    //        }
    //    }
    //}

    public static class XmlServStat
    {
        /// <summary>
        /// Сериализует объект в строку-xml.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string SerializeXmlToStr<T>(this T obj)
        {
            if (obj == null)
            {
                return string.Empty;
            }
            try
            {
                var xmlserializer = new XmlSerializer(typeof(T));
                var sw = new StringWriter();
                using (var writer = XmlWriter.Create(sw))
                {
                    xmlserializer.Serialize(writer, obj);
                    return sw.ToString();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Десериализует строку-xml в объект
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xmlString"></param>
        /// <returns></returns>
        public static T DeserializeStrxml<T>(this string xmlString) where T : class, new()
        {
            if (string.IsNullOrEmpty(xmlString))
            {
                return new T();
            }
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T), new XmlRootAttribute("Root"));
                var stringReader = new StringReader(xmlString);
                T result = (T)serializer.Deserialize(stringReader);
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
