using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MovieDatabase.Models
{
    public class Database 
    {
        private List<Movie> db; // list of movies in the database
        private int _index; // position of current movie in the database 

        // initialise the database properties
        public Database()
        {
            
        }

        // A property to Return number of movies in the database
        public int Count
        {
            
        }

        // A property to return  current _index position which should be either
        // -1 if database is empty
        // 0 - db.Count-1 if database is not empty
        public int Index
        {

        }

        // Add a movie to current position in database
        public void Add(Movie m)
        {

        }

        // Return current movie or null if database empty
        public Movie Get()
        {

        }

        // Delete current movie at index if there is a movie and update index 
        public void Delete()
        {

        }

        // Update the current movie at index if there is a movie and update index
        public void Update(Movie m)
        {

        }

        // Delete all movies from the database and reset index
        public void clear()
        {

        }

        // Move index position to first movie (0)
        // return true if index update was possible, false otherwise
        public bool First()
        {

        }

        // Move index position to last movie
        // true if index update was possible, false otherwise</returns>
        public bool Last()
        {

        }

        // Move index position to next movie
        // true if index update was possible, false otherwise<
        public bool Next()
        {

        }

        // Move index position to previous movie
        // true if index update was possible, false otherwise
        public bool Prev()
        {

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

        }

        // order the database by title of movie (ascending)
        public void OrderByTitle()
        {

        }

        // order the database by budget of movie (ascending)
        public void OrderByBudget()
        {

        }

    }
}
