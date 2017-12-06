using Microsoft.Win32;
using MovieDatabase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MahApps.Metro.Controls;

namespace MovieDatabase
{
    /// <summary>
    /// Interaction logic for MovieView.xaml
    /// </summary>
    public partial class MovieView : MetroWindow
    {
        private Database db;

        public MovieView()
        {
            InitializeComponent();
            db = new Database();
        }

        private void UpdateUIFromModel()
        {
            //Updates the UI from the model
            tbTitle.Text = db.Get().Title;
            tbYear.Text = db.Get().Year.ToString();
            tbDirector.Text = db.Get().Director;
            tbDuration.Text = db.Get().Duration.ToString();
            tbBudget.Text = db.Get().Budget.ToString();

            var uri = new Uri(db.Get().Poster, UriKind.Absolute);
            iPoster.Source = new BitmapImage(uri);

            if (db.Get().Rating == 1)
            {
                rbRate1.IsChecked = true;
            }
            else if (db.Get().Rating == 2)
            {
                rbRate2.IsChecked = true;
            }
            else if (db.Get().Rating == 3)
            {
                rbRate3.IsChecked = true;
            }
            else if (db.Get().Rating == 4)
            {
                rbRate4.IsChecked = true;
            }
            else if (db.Get().Rating == 5)
            {
                rbRate5.IsChecked = true;
            }
            else if (db.Get().Rating == 5)
            {
                rbRate5.IsChecked = true;
            }

            if (db.Get().Genre.Contains(Genres.Comedy))
            {
                cbComedy.IsChecked = true;
            }
            if (db.Get().Genre.Contains(Genres.Action))
            {
                cbAction.IsChecked = true;
            }
            if (db.Get().Genre.Contains(Genres.Thriller))
            {
                cbThriller.IsChecked = true;
            }
            if (db.Get().Genre.Contains(Genres.Horror))
            {
                cbHorror.IsChecked = true;
            }
            if (db.Get().Genre.Contains(Genres.Romance))
            {
                cbRomance.IsChecked = true;
            }
            if (db.Get().Genre.Contains(Genres.SciFi))
            {
                cbSciFi.IsChecked = true;
            }
            if (db.Get().Genre.Contains(Genres.Western))
            {
                cbWestern.IsChecked = true;
            }
            if (db.Get().Genre.Contains(Genres.Family))
            {
                cbFamily.IsChecked = true;
            }

            foreach (string actors in db.Get().Actors)
            {
                lbCast.Items.Add(actors);
            }
        }

        private void UpdateModelFromUI()
        {
            //Updates the model from the UI 
            db.Get().Title = tbTitle.Text;
            db.Get().Year = Convert.ToInt32(tbYear);
            db.Get().Director = tbDirector.Text;
            db.Get().Duration = Convert.ToInt32(tbDuration);
            db.Get().Budget = Convert.ToInt32(tbBudget);
            db.Get().Rating = rbRate1.IsChecked.Value ? db.Get().Rating = 1 :
                       rbRate2.IsChecked.Value ? db.Get().Rating = 2 :
                       rbRate3.IsChecked.Value ? db.Get().Rating = 3 :
                       rbRate4.IsChecked.Value ? db.Get().Rating = 4 :
                       rbRate5.IsChecked.Value ? db.Get().Rating = 5 : db.Get().Rating;

            var path = tbPosterURL.Text;
            try
            {
                // create the url and add as a string to the PosterImage
                var uri = new Uri(path, UriKind.Absolute);
                db.Get().Poster = (uri.ToString());
                // reset the Path
                tbPosterURL.Text = "";
            }
            catch
            {
                // the path is not a valid URI
                MessageBox.Show("Invalid Poster URL", "Error");
            }

            if (cbComedy.IsChecked.Value)
            {
                db.Get().Genre.Add(Genres.Comedy);
            }
            if (cbAction.IsChecked.Value)
            {
                db.Get().Genre.Add(Genres.Action);
            }
            if (cbThriller.IsChecked.Value)
            {
                db.Get().Genre.Add(Genres.Thriller);
            }
            if (cbHorror.IsChecked.Value)
            {
                db.Get().Genre.Add(Genres.Horror);
            }
            if (cbRomance.IsChecked.Value)
            {
                db.Get().Genre.Add(Genres.Romance);
            }
            if (cbSciFi.IsChecked.Value)
            {
                db.Get().Genre.Add(Genres.SciFi);
            }
            if (cbWestern.IsChecked.Value)
            {
                db.Get().Genre.Add(Genres.Western);
            }
            if (cbFamily.IsChecked.Value)
            {
                db.Get().Genre.Add(Genres.Family);
            }
            foreach (string actors in lbCast.Items)
            {
                db.Get().Actors.ToString();
            }
        }

        //Dockpanel menu items start
        private void New_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            db.Save("");

        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            db.Load("");
            UpdateUIFromModel();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            

        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void View_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Help_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
          
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void OrderbyTitle_Click(object sender, RoutedEventArgs e)
        {

        }

        private void OrderbyYear_Click(object sender, RoutedEventArgs e)
        {

        }

        private void OrderbyDuration_Click(object sender, RoutedEventArgs e)
        {

        }
        //Dockpanel menu items end


        //Navigation buttons start 
        private void First_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Previous_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Last_Click(object sender, RoutedEventArgs e)
        {

        }
        //Navigation buttons end
    }
}