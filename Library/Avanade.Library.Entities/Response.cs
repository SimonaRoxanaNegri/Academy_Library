using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Avanade.Library.Entities
{
    [DataContract]
    public class Response<T> : IResponse<T>
    {
        [DataMember]
        public string ResponseMessage { get; set; }
        public T entity { get; set; }
    }
}
