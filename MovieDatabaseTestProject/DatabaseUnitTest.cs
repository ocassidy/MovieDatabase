using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MovieDatabase.Models;

namespace MovieDatabaseTestProject
{
    [TestClass]
    public class DatabaseUnitTest
    {
        private Movie m1;
        private Movie m2;
        private Movie m3;
        private Database db;

        [TestInitialize]
        public void SetUp()
        {
            // create test database and populate with sample movies
            db = new Database();
            m1 = new Movie() { Title = "Movie 1", Year = 2017, Duration = 150 };
            m2 = new Movie() { Title = "Movie 2", Year = 2013, Duration = 90 };
            m3 = new Movie() { Title = "Movie 3", Year = 2015, Duration = 120 };

            db.Add(m1);
            db.Add(m2);
            db.Add(m3);
        }

        [TestMethod]
        public void TestStoreAndLoad()
        {
            int numMoviesBeforeSave = db.Count;
            db.Save("movies.json");
            db.clear();
            db.Load("movies.json");
            db.First();
            // verify same number of movies loaded
            Assert.AreEqual(numMoviesBeforeSave, db.Count);

            // verify first movie still found
            Assert.AreEqual(m1.Title, db.Get().Title);
        }

        [TestMethod]
        public void TestAddAndIsCurrent()
        {
            Movie m4 = new Movie { Title = "Movie 4", Year = 2017, Duration = 50 };
            db.Add(m4);
            Assert.AreEqual(m4.Title, db.Get().Title);
        }


        [TestMethod]
        public void TestClearThenAdd()
        {
            db.clear();
            db.Add(m1);
            Assert.AreEqual(1, db.Count);
            Assert.AreEqual(m1.Title, db.Get().Title);
        }

        [TestMethod]
        public void TestClear()
        {
            db.clear();
            Assert.AreEqual(null, db.Get());
        }

        [TestMethod]
        public void TestNavigateToFirst()
        {
            db.First();
            Assert.AreEqual(m1.Title, db.Get().Title);
        }

        [TestMethod]
        public void TestNavigateToLast()
        {
            db.Last();
            Assert.AreEqual(m3.Title, db.Get().Title);
        }

        [TestMethod]
        public void TestNavigateToPrev()
        {
            db.Last();
            db.Prev();
            Assert.AreEqual(m2.Title, db.Get().Title);
        }

        [TestMethod]
        public void TestNavigateToNext()
        {
            db.First();
            db.Next();
            Assert.AreEqual(m2.Title, db.Get().Title);
        }

        [TestMethod]
        public void TestNavigateToNextFailsWhenAtLast()
        {
            db.Last();
            Assert.AreEqual(false, db.Next());
        }

        [TestMethod]
        public void TestNavigateToPrevFailsWhenAtFirst()
        {
            db.First();
            Assert.AreEqual(false, db.Prev());
        }

        [TestMethod]
        public void TestOrderByTitle()
        {
            db.OrderByTitle();
            db.First();
            Assert.AreEqual("Movie 1", db.Get().Title);
        }

        [TestMethod]
        public void TestOrderByYear()
        {
            db.OrderByYear();
            db.First();
            Assert.AreEqual(2013, db.Get().Year);
        }

        [TestMethod]
        public void TestOrderByDuration()
        {
            db.OrderByDuration();
            db.First();
            Assert.AreEqual(90, db.Get().Duration);
        }

    }
}
