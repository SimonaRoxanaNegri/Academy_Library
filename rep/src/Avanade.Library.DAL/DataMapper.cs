using Avanade.Library.Entities;
using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;


namespace Avanade.Library.DAL
{
    class DataMapper
    {
        /*
        public string SerializeUser(User serializable)

        {
            XmlSerializer instanceObject = new XmlSerializer(serializable.GetType());
            StringWriter sw = new StringWriter();
            instanceObject.Serialize(sw, serializable);
            return sw.GetStringBuilder().ToString();
        }

        public string SerializeBook(Book serializable)

        {
            XmlSerializer instanceObject = new XmlSerializer(serializable.GetType());
            StringWriter sw = new StringWriter();
            instanceObject.Serialize(sw, serializable);
            return sw.GetStringBuilder().ToString();
        }

        public string SerializeReservation(Reservation serializable)

        {
            XmlSerializer instanceObject = new XmlSerializer(serializable.GetType());
            StringWriter sw = new StringWriter();
            instanceObject.Serialize(sw, serializable);
            return sw.GetStringBuilder().ToString();
        } */

        public object DeserializeObject(String deserializable, Type type)

        {
            XmlSerializer instanceObject = new XmlSerializer(type);
            byte[] byteArray = Encoding.UTF8.GetBytes(deserializable);
            MemoryStream stream = new MemoryStream(byteArray);
            return instanceObject.Deserialize(stream);
        }

    }
}
