using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.IO;
using Microsoft.Win32;
using System.Windows;

namespace MovieDatabase.Models
{
    public class Database
    {
        private List<Movie> db; // list of movies in the database
        private int _index; // position of current movie in the database 

        // initialise the database properties
        public Database()
        {
            db = new List<Movie>();
            _index = -1;
        }

        // A function to Return number of movies in the database
        public int Count
        {
            get
            {
                return db.Count;
            }
        }

        //A function to return  current _index position which should be either
        //-1 if database is empty
        //0 - db.Count if database is not empty
        public int Index
        {
            get
            {
                return _index;
            }
            set
            {
                if (Count == 0)
                {
                    _index = -1;
                }
                else
                {
                    _index = 0;
                }
            }
        }

        //Add a movie to current position in database
        public void Add(Movie m)
        {
            if (Index < Count - 1)
            {
                db.Insert(Index++, m);
            }
            else
            {
                db.Add(m);
                Last();
            }
        }

        //Return current movie or null if database empty
        public Movie Get()
        {
            if (Count == 0)
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
            if (Count != 0)
            {
                db.RemoveAt(_index);
                _index--;
            }
            if (Index == -1 && Count > 0)
            {
                _index = 0;
            }
        }

        //Update the current movie at index if there is a movie and update index
        public void Update(Movie m)
        {
            if (Get() != null)
            {
                db[_index] = m;
            }
        }

        //Delete all movies from the database and reset index
        public void Clear()
        {
            db.Clear();
            _index = -1;
        }

        //Move index position to first movie (0)
        //return true if index update was possible, false otherwise
        public bool First()
        {
            if (Count > 0)
            {
                _index = 0;
                return true;
            }
            else
            {
                _index = -1;
                return false;
            }
        }

        //Move index position to last movie
        //true if index update was possible, false otherwise</returns>
        public bool Last()
        {         
            if (Count > 0)
            {
                _index = Count - 1;
                return true;
            }
            else
            {
                return false;
            }
        }

        //Move index position to next movie
        //true if index update was possible, false otherwise<
        public bool Next()
        {
            if (Index < Count - 1 && Count > 0)
            {
                _index++;
                return true;
            }
            else
            {
                //MessageBox.Show("No movies exist to move, cannot goto next!", "Error!");
                return false;
            }
        }

        //Move index position to previous movie
        //true if index update was possible, false otherwise
        public bool Prev()
        {
            if (Index > 0)
            {
                _index--;
                return true;
            }
            else
            {
                return false;
            }
        }

        //Load movies from a json file and set index to first record
        public void Load(string file)
        {
            string loadlist = File.ReadAllText(file);
            db = JsonConvert.DeserializeObject<List<Movie>>(loadlist);
            First();
        }

        //Save movies to a Json file
        public void Save(string file)
        {
            var savelist = JsonConvert.SerializeObject(db);
            File.WriteAllText(file, savelist);
        }

        //Following methods update the List of movies (db) to the specified order
        //order the database by year of movie
        public void OrderByYear()
        {
            db = (from e in db orderby e.Year select e).ToList();
        }

        //order the database by title of movie (ascending)
        public void OrderByTitle()
        {
            db = (from e in db orderby e.Title select e).ToList();
        }

        //order the database by duration of movie (ascending)
        public void OrderByDuration()
        {
            db = (from e in db orderby e.Duration select e).ToList();
        }
    }
}