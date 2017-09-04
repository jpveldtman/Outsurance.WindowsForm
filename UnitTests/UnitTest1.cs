using Microsoft.VisualStudio.TestTools.UnitTesting;
using Outsurance.WindowsForm.Classes;
using Outsurance.WindowsForm.Helpers;
using System;
using System.Collections.Generic;

namespace UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestResults()
        {
            string file = System.IO.Path.GetFullPath(@"..\..\..\Data\\data.csv");

            if (FileAccess.ValidateFormat(file))
            {
                string error = null;
                People_Class.ReadAllPeople(file, out error);

                List<string> expectedFirstNameList = new List<string>();
                expectedFirstNameList.AddRange(new string[] { "Iago Tieman", "Cordie Bleibaum", "Shandie Rowles", "Genny Seys", "Renault Millichap", "Shandie Rowles", "Iago Tieman", "Genny Seys", "Iago Tieman" });
                List<string> expectedAddressList = new List<string>();
                expectedAddressList.AddRange(new string[] { "42 Basil Park", "74584 Moose Parkway", "81075 Killdeer Road", "2 Nova Circle", "32 Butterfield Drive" });

                List<string> actualFirstNameList = People_Class.GetFullNameList();
                List<string> actualAddressList = People_Class.GetAddressList();

                CollectionAssert.AreEqual(expectedFirstNameList, actualFirstNameList);
                CollectionAssert.AreEqual(expectedAddressList, actualAddressList);
            }
            
        }

        [TestMethod]
        public void TestIncorrectHeaders()
        {
            string file = System.IO.Path.GetFullPath(@"..\..\..\Data\\IncorrectHeaders.csv");
            bool hasAccess = FileAccess.ValidateFormat(file);
            Assert.AreEqual(false, hasAccess);
        }

        [TestMethod]
        public void TestMissingData()
        {
            string file = System.IO.Path.GetFullPath(@"..\..\..\Data\\MissingData.csv");
            if (FileAccess.ValidateFormat(file))
            {
                string error = null;
                if (FileAccess.ValidateFormat(file))
                    People_Class.ReadAllPeople(file, out error);
                Assert.AreEqual("Index was outside the bounds of the array.", error);
            }
        }
    }
}
