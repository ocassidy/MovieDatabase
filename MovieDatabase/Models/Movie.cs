using System;
using System.Collections.Generic;

namespace MovieDatabase.Models
{
    //public enum Movies {Title, Year, Director, Duration, Budget, Rating, Poster, Actor};
    public enum Genres { Comedy, Drama, Action, Fiction, Horror, Romance, SciFi, Western, Family };

    public class Movie
    {
        private List<string> Actor { get; set; } // list of the actors in the movies
        private List<Genres> Genre { get; set; } // list of the genres movies can occupy

        public string Title { get; set; }
        public int Year { get; set; }
        public string Director { get; set; }
        public int Duration { get; set; }
        public double Budget { get; set; }
        public int Rating { get; set; }
        public string Poster { get; set; }
        // public string Actor { get; set; }

        public int GetYear
        {
            get
            {
                return Year;
            }
        
        }
        public string GetTitle
        {
            get
            {
                return Title;
            }
        }
        public double GetBudget
        {
            get
            {
                return Budget;
            }
        }
    }
}