using System.Xml.Serialization;

namespace Avanade.Library.Entities
{

    [XmlRoot(ElementName = "User")]
    public class User : IUser
    {

        [XmlAttribute(AttributeName = "UserId")]
        public int UserId { get ; set ; }

        [XmlAttribute(AttributeName = "Username")]
        public string Username { get ;  set ;  }

        [XmlAttribute(AttributeName = "Password")]
        public string Password { get; set ; }

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
