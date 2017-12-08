using System;
using System.Collections.Generic;

namespace MovieDatabase.Models
{
    public enum Genres {Comedy, Action, Thriller, Horror, Romance, SciFi, Western, Family, War };

    public class Movie
    {
        //Movie class member variables 
        public string Title { get; set; }
        public int Year { get; set; }
        public string Director { get; set; }
        public int Duration { get; set; }
        public double Budget { get; set; }
        public int Rating { get; set; }
        public string Poster { get; set; }
        public List<Genres> Genre { get; set; } // list of the genres movies can occupy
        public List<string> Actors { get; set; } // list of the actors in the movies
    }
}