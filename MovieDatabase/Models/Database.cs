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
            Index();
        }

        // A property to Return number of movies in the database
        public int Count()
        {
            return db.Count;
        }

        // A property to return  current _index position which should be either
        // -1 if database is empty
        // 0 - db.Count if database is not empty
        public int Index()
        {
            /*if (Count() == 0)
            {
                _index = -1;
            }
            else
            {
                //***SOMETHING IS BROKEN FROM HERE ON ?**
                //_index = (0 - db.Count());
                //First();
            }*/
            return _index;
        }

        // Add a movie to current position in database
        public void Add(Movie m)
        {
            if (Index() >= 0)
            {
                db.Add(m);
                Last(); 
            }       
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
            if (db.Count > 0)
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

        // Move index position to last movie
        // true if index update was possible, false otherwise</returns>
        public bool Last()
        {
            _index = db.Count - 1;
            if (_index == db.Count - 1) { 
                
                return true;
            } else
            {
                return false;
            }
        }

        // Move index position to next movie
        // true if index update was possible, false otherwise<
        public bool Next()
        { 
            if (_index < db.Count() - 1)
            {
                _index++;
                return true;
            }
            else
            {
                MessageBox.Show("No movies exist to move, cannot goto next!", "Error!");
                return false;
            }
        }

        // Move index position to previous movie
        // true if index update was possible, false otherwise
        public bool Prev()
        {  
            if (_index > 0)
            {
                _index--;
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
            var dialog = new OpenFileDialog()
            {
                Filter = "json files|*.json",
                Title = "Load"
            };

            // if the user enters a filename and clicks save
            if (dialog.ShowDialog() == true)
            {
                file = dialog.FileName;
                string loadlist = File.ReadAllText(file);
                db = JsonConvert.DeserializeObject<List<Movie>>(loadlist);
                First();
            }
            else if (dialog.ShowDialog() == false)
            {
                First();
            }
        }

        // Save movies to a Json file
        public void Save(string file)
        {
            if (Count() <= 0)
            {
                MessageBox.Show("Cannot Save Blank File", "Error!");
            }
            else if (Count() > 0)
            {
                var dialog = new SaveFileDialog()
                {
                    Filter = "json files|*.json",
                    Title = "Save"
                };
                // if the user enters a filename and clicks save
                if (dialog.ShowDialog() == true)
                {
                    file = dialog.FileName;
                    var savelist = JsonConvert.SerializeObject(db);
                    File.WriteAllText(file, savelist);
                }
            }
        }

        // Following methods update the List of movies (db) to the specified order
        // order the database by year of movie
        public void OrderByYear()
        {
            db = (from e in db orderby e.Year select e).ToList();
        }

        // order the database by title of movie (ascending)
        public void OrderByTitle()
        {
            db = (from e in db orderby e.Title select e).ToList();
        }

        // order the database by duration of movie (ascending)
        public void OrderByDuration()
        {
            db = (from e in db orderby e.Duration select e).ToList();
        }
    }
}