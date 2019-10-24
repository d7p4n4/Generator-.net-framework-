using CSAc4yClass.Class;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Generator_.net_framework_
{
    class DeserialiseMethod
    {
        public static Ac4yClass deser(string path)     
        {
            Ac4yClass ac4y = null;

            XmlSerializer serializer = new XmlSerializer(typeof(Ac4yClass));

            StreamReader reader = new StreamReader(path);
            ac4y = (Ac4yClass)serializer.Deserialize(reader);
            reader.Close();

            return ac4y;
        }
    }
}
