using Avanade.Library.BusinessLogic;
using Avanade.Library.DAL;
using Avanade.Library.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Configuration;
using Avanade.Library.ConsoleApp;
using System.Linq;

namespace BusinessLogic_Test
{
    [TestClass]
    public class BusinessLogics_test
    {
        [TestMethod]
        public void TestSearchBookAvailables()
        {
            //arrange
            
            string title = "";
            string authorName = "";
            string authorSurName = "";
            string publishingHouse = "";

            IDal xmlDal = new XmlDal();
            IBusinessLogics bookAvailable = new BusinessLogics(xmlDal);

            List<IBooksAvailables> listBookAvailables;
            listBookAvailables = xmlDal.GetBooksAvailables(title, authorName, authorSurName, publishingHouse);

            var resultExpected = String.Join(",\n", listBookAvailables.Select(x => x.FormatterBookAvailableString()));

            //act
            var resultactual = String.Join("",bookAvailable.SearchBookAvailables(title, authorName, authorSurName, publishingHouse));
            //assert
            Assert.AreEqual(resultExpected, resultactual);
        }
    }
}
