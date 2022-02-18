using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using Avanade.Library.Entities;
using Avanade.Library.Proxy;


namespace LibraryProxy.Tests
{
    [TestClass()]
    
    public class UserProxyTests
    {

        [TestMethod()]
        public async Task GetUserTest()
        {

            var content = new User(0,"administrator", "admin1","Admin");
            var resultActual = await UserProxy.GetUser(content);
            var resultExpcted = new User(1,"administrator","admin1","Admin");

            Assert.AreEqual(resultExpcted.Username, resultActual.Username);
            Assert.AreEqual(resultExpcted.Password, resultActual.Password);
            Assert.AreEqual(resultExpcted.Role, resultActual.Role);

        }
    }
}