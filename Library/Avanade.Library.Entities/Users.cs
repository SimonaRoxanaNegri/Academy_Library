using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Avanade.Library.Entities
{ 
    [XmlRoot("Users")]


        public class Users : IUsers
        {
        
        [XmlElement("User")]
        public List<User> listOf { get; set; }


            public Users(List<User> users)
            {
                this.listOf = users;
            }


            public Users()
            {
            }
           

        }

    }
