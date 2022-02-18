using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avanade.Library.Entities
{
    public interface IResponse<T>
    {
        string ResponseMessage { get; set; }
        T entity { get; set; }
    }
}
