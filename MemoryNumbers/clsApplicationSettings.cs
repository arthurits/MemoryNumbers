using System;
using System.Drawing;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Utils
{

    // https://dzone.com/articles/how-to-serializedeserialize-a-dictionary-object-in-1
    [XmlRoot("Program MemoryNumbers")]
    public class ProgramSettings<TKey, TValue> : System.Collections.Generic.Dictionary<TKey, TValue>, IXmlSerializable
    {
        public XmlSchema GetSchema() { return null; }

        public void ReadXml(XmlReader reader)
        {
            if (reader.IsEmptyElement) { return; }

            reader.Read();

            while (reader.NodeType != XmlNodeType.EndElement)
            {
                object key = reader.GetAttribute("Property");
                object value = reader.GetAttribute("Value");
                this.Add((TKey)key, (TValue)value);
                reader.Read();
            }
        }

        public void WriteXml(XmlWriter writer)
        {
            foreach (var pair in this)
            {
                writer.WriteStartElement("Settings");
                writer.WriteAttributeString("Property", pair.Key.ToString());
                writer.WriteAttributeString("Value", pair.Value.ToString());
                writer.WriteEndElement();
            }
        }
    }


}
