using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.IO;

namespace mapedit
{
    public class Xml
    {
        public static void Save(String Path, System.Object Value, Type Type)
        {
            try
            {
                if (System.IO.File.Exists(Path))
                    System.IO.File.Delete(Path);
                System.IO.FileStream FileStream = new System.IO.FileStream(Path, System.IO.FileMode.OpenOrCreate);
                try
                {
                    XmlSerializer XmlSerializer = new XmlSerializer(Type);
                    XmlSerializer.Serialize(FileStream, Value);
                }
                catch 
                {
                }
                FileStream.Close();
                FileStream.Dispose();
            }
            catch
            {
            }
        }

        public static System.Object Load(String Path, Type Type)
        {
            System.Object Result = Activator.CreateInstance(Type);
            try
            {
                String Dir = System.IO.Path.GetDirectoryName(Path);
                if (!Directory.Exists(Dir))
                    Directory.CreateDirectory(Dir);
                System.IO.FileStream FileStream = new System.IO.FileStream(Path, System.IO.FileMode.OpenOrCreate);
                try
                {
                    XmlSerializer XmlSerializer = new XmlSerializer(Type);
                    Result = XmlSerializer.Deserialize(FileStream);
                }
                catch 
                {
                }
                FileStream.Close();
                FileStream.Dispose();
            }
            catch
            {
            }
            return Result;
        }
    }
}
