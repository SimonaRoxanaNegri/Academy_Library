using Avanade.Library.BusinessLogic;
using Avanade.Library.DAL;
using Avanade.Library.Entities;
using System.Web.Http;


namespace WebApi_Library.Controllers
{
    
    public class UserController : ApiController
    {
        readonly IBusinessLogics logic = new BusinessLogics(new DbDal());

  
    [HttpPost]
        public IUser GetUser([FromBody] User user)
        {
            return logic.RequestLogin(user.Username, user.Password);
        }


    }
}