using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace MovieDatabase.Models
{
    public class Database 
    {
        private List<Movie> db; // list of movies in the database
        private int _index; // position of current movie in the database 

        // initialise the database properties
        public Database()
        {
            _index = 0;
            db.Clear();
        }

        // A property to Return number of movies in the database
        public int Count()
        {
            return db.Count;
        }

        // A property to return  current _index position which should be either
        // -1 if database is empty
        // 0 - db.Count-1 if database is not empty
        public int Index()
        {
            if (Count() == 0)
            {
                _index = -1;
            }
            else
            {
                _index = (0 - (db.Count - 1));
            }
            return _index;
        }

        // Add a movie to current position in database
        public void Add(Movie m)
        {
            db.Insert(_index, m);
        }

        // Return current movie or null if database empty
        public Movie Get()
        {
            if (Count() == 0)
            {
                return null;
            }
            else
            {
                return db[_index];
            }
        }

        // Delete current movie at index if there is a movie and update index 
        public void Delete()
        {
            if (Count() != 0)
            {
                db.RemoveAt(_index);
                Index();
            }
        }

        // Update the current movie at index if there is a movie and update index
        public void Update(Movie m)
        {
            if (Count() != 0)
            {
                db[_index] = m;
                Index();
            }
        }

        // Delete all movies from the database and reset index
        public void Clear()
        {
            db.Clear();
            Index();
        }

        // Move index position to first movie (0)
        // return true if index update was possible, false otherwise
        public bool First()
        {
            _index = 0;
            if (_index == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // Move index position to last movie
        // true if index update was possible, false otherwise</returns>
        public bool Last()
        {
            _index = db.Count - 1;
            if (_index == db.Count - 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // Move index position to next movie
        // true if index update was possible, false otherwise<
        public bool Next()
        {
            _index++;
            if (_index < db.Count)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // Move index position to previous movie
        // true if index update was possible, false otherwise
        public bool Prev()
        {
            _index--;
            if (_index > -1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // Load movies from a json file and set index to first record
        public void Load(string file)
        {

        }

        // Save movies to a Json file
        public void Save(string file)
        {

        }

        // Following methods update the List of movies (db) to the specified order

        // order the database by year of movie
        public void OrderByYear()
        {
            db = (from e in db orderby e.GetYear select e).ToList();
        }

        // order the database by title of movie (ascending)
        public void OrderByTitle()
        {
            db = (from e in db orderby e.GetTitle select e).ToList();
        }

        // order the database by budget of movie (ascending)
        public void OrderByBudget()
        {
            db = (from e in db orderby e.GetBudget select e).ToList();
        }

    }
}