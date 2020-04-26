using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using System.Runtime.Serialization;

namespace iVotingSystem.XMLSerializer
{
    public abstract class XMLSet<T> where T : class
    {
        protected XMLSet()
        {

        }

        public virtual T ReadXmlSerialize(string P_sPath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (FileStream stream = new FileStream(P_sPath, FileMode.Open))
            {
                return (T)serializer.Deserialize(stream);
            }
        }

        public virtual void UpdateXmlSerialize(T P_oXML, string P_sPath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (TextWriter writer = new StreamWriter(P_sPath))
            {
                serializer.Serialize(writer, P_oXML);
            }
        }
       
    }
}
