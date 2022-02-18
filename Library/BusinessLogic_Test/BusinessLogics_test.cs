using Avanade.Library.BusinessLogic;
using Avanade.Library.DAL;
using Avanade.Library.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace BusinessLogic_Test
{
    [TestClass]
    public class BusinessLogics_test
    {
        [TestMethod]
        public void TestSearchBookAvailables()
        {
            //arrange
            string title = "titolo";
            string authorName = "nome";
            string authorSurName = "cognome";
            string publishingHouse = "publisher";

            IDal xmlDal = new XmlDal();
            IBusinessLogics bookAvailable = new BusinessLogics(xmlDal);

            List<IBooksAvailables> listBookAvailables;
            listBookAvailables = xmlDal.GetBooksAvailables(title, authorName, authorSurName, publishingHouse);
            var resultExpected = listBookAvailables;

            //act
            var resultactual = bookAvailable.SearchBookAvailables(title, authorName, authorSurName, publishingHouse);
            //assert
            Assert.AreEqual(resultExpected, resultactual);
        }
    }
}
