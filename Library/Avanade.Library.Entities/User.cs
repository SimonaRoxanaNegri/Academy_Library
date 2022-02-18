using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Avanade.Library.Entities
{
    [DataContract]
    [XmlRoot(ElementName = "User")]
    public class User : IUser
    {
        [DataMember]
        [XmlAttribute(AttributeName = "UserId")]
        public int UserId { get ; set ; }

        [DataMember]
        [XmlAttribute(AttributeName = "Username")]
        public string Username { get ;  set ;  }

        [DataMember]
        [XmlAttribute(AttributeName = "Password")]
        public string Password { get; set ; }

        [DataMember]
        [XmlAttribute(AttributeName = "Role")]
        public string Role { get; set; }

 

        public User(int userId, string username, string password, string role)
        {
            UserId = userId;
            Username = username;
            Password = password;
            Role = role;
        }

        //serve al deserializer per creare un utente vuoto da popolare
        public User()
        {
        }

        
    }
}
