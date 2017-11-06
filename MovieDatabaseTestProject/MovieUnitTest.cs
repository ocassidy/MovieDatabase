using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MovieDatabase.Models;
using System.Collections.Generic;

namespace MovieDatabaseTestProject
{
    [TestClass]
    public class MovieUnitTest
    {
        // test movie
        private Movie m;

        [TestInitialize]
        public void SetUp()
        {
            m = new Movie()
            {
                // initialise your test movie attributes
            };
        }

        [TestMethod]
        public void TestCreateMovie()
        {
            // verify the movie was created by checking that each attribute value was set
            // e.g.   Assert.AreEqual(m.Title, "The title of the test movie");
  
        }

        [TestMethod]
        public void TestUpdateMovie()
        {
            // make changes to the test movie m            
          
            // Check if changes were accepted
        
        }
    }
}
