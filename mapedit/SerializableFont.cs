using System.ComponentModel;
using System.Drawing;
using System.Xml.Serialization;

namespace mapedit
{
    public class SerializableFont
    {
        public SerializableFont()
        {
            FontValue = SystemFonts.CaptionFont; 
        }

        public SerializableFont(Font font)
        {
            FontValue = font;
        }

        [XmlIgnore]
        public Font FontValue { get; set; } 
        [Browsable(false)]
        [XmlElement("FontValue")]
        public string SerializeFontAttribute
        {
            get
            {
                return FontXmlConverter.ConvertToString(FontValue);
            }
            set
            {
                FontValue = FontXmlConverter.ConvertToFont(value);
            }
        }

        public static implicit operator Font(SerializableFont serializeableFont)
        {
            if (serializeableFont == null)
                return null;
            return serializeableFont.FontValue;
        }

        public static implicit operator SerializableFont(Font font)
        {
            return new SerializableFont(font);
        }
    }

    public static class FontXmlConverter
    {
        public static string ConvertToString(Font font)
        {
            try
            {
                if (font != null)
                {
                    TypeConverter converter = TypeDescriptor.GetConverter(typeof(Font));
                    return converter.ConvertToString(font);
                }
                else
                    return null;
            }
            catch { System.Diagnostics.Debug.WriteLine("Unable to convert"); }
            return null;
        }
        public static Font ConvertToFont(string fontString)
        {
            try
            {
                TypeConverter converter = TypeDescriptor.GetConverter(typeof(Font));
                return (Font)converter.ConvertFromString(fontString);
            }
            catch { System.Diagnostics.Debug.WriteLine("Unable to convert"); }
            return null;
        }
    }
}
