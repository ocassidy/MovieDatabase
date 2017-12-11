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
using MahApps.Metro;
using MahApps.Metro.Controls;

namespace MovieDatabase
{
    /// <summary>
    /// Interaction logic for MovieView.xaml
    /// </summary>
    public partial class MovieView : MetroWindow
    {
        enum WindowMode { View, Create, Edit };
        private WindowMode mode;
        private Database db;
        private bool EditSaveClicked = false;
        private bool EditCancelClicked = false;

        public MovieView()
        {
            InitializeComponent();
            db = new Database();
            mode = WindowMode.View;
        }

        private void UpdateUIFromModel()
        {
            //Updates the UI from the model
            tbTitle.Text = db.Get().Title;
            tbYear.Text = db.Get().Year.ToString();
            tbDirector.Text = db.Get().Director;
            tbDuration.Text = db.Get().Duration.ToString();
            tbBudget.Text = db.Get().Budget.ToString();

            if (Uri.IsWellFormedUriString(db.Get().Poster, UriKind.Absolute))
            {
                var uri = new Uri(db.Get().Poster, UriKind.Absolute);
                iPoster.Source = new BitmapImage(uri);
            }
            else
            {
                // the path is not a valid URI
                iPoster.Source = null;
                MessageBox.Show("Invalid Poster URL", "Error");
            }

            tbPosterURL.Text = db.Get().Poster;

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
            if (db.Get().Genre.Contains(Genres.War))
            {
                cbWar.IsChecked = true;
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

            try
            {
                // create the url and add as a string to the iPoster
                var uri = new Uri(tbPosterURL.Text, UriKind.Absolute);
                db.Get().Poster = (uri.ToString());
                iPoster.Source = new BitmapImage(uri);
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
            if (cbWar.IsChecked.Value)
            {
                db.Get().Genre.Add(Genres.War);
            }

            //
            foreach (string actors in lbCast.Items)
            {
                db.Get().Actors.ToString();
            }
        }

        //Dockpanel menu items start
        private void New_Click(object sender, RoutedEventArgs e)
        {
            db = new Database();
            SetToEditMode();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            db.Save("");
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            db.Load("");
            UpdateUIFromModel();

            //After load enable buttons
            bFirst.IsEnabled = true;
            bLast.IsEnabled = true;
            bPrevious.IsEnabled = true;
            bNext.IsEnabled = true;
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {


        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (db.Count() < 1)
            {
                db.Clear();
                SetToEditMode();
                mode = WindowMode.Edit;
            }
            else
            {
                MessageBox.Show("To edit a blank record use create!", "Error");
            }
        }

        private void View_Click(object sender, RoutedEventArgs e)
        {
            SetToViewMode();
            mode = WindowMode.View;
        }

        private void Help_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(mode.ToString());
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            SetToCreateMode();
            mode = WindowMode.Create;
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            db.Delete();
            Clear();
            UpdateUIFromModel();
        }

        private void OrderbyTitle_Click(object sender, RoutedEventArgs e)
        {
            db.OrderByTitle();
            Clear();
            db.First();
            UpdateUIFromModel();
        }

        private void OrderbyYear_Click(object sender, RoutedEventArgs e)
        {
            db.OrderByTitle();
            Clear();
            db.First();
            UpdateUIFromModel();
        }

        private void OrderbyDuration_Click(object sender, RoutedEventArgs e)
        {
            db.OrderByDuration();
            Clear();
            db.First();
            UpdateUIFromModel();
        }
        //Dockpanel menu items end

        //Navigation buttons start 
        private void First_Click(object sender, RoutedEventArgs e)
        {
            db.First();
            Clear();
            UpdateUIFromModel();
        }

        private void Previous_Click(object sender, RoutedEventArgs e)
        {
            db.Prev();
            Clear();
            UpdateUIFromModel();
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            db.Next();
            Clear();
            UpdateUIFromModel();
        }

        private void Last_Click(object sender, RoutedEventArgs e)
        {
            db.Last();
            Clear();
            UpdateUIFromModel();
        }
        //Navigation buttons end

        //Navigational Clears
        private void Clear()
        {
            lbCast.Items.Clear();

            rbRate1.IsChecked = false;
            rbRate2.IsChecked = false;
            rbRate3.IsChecked = false;
            rbRate4.IsChecked = false;
            rbRate5.IsChecked = false;

            cbComedy.IsChecked = false;
            cbAction.IsChecked = false;
            cbThriller.IsChecked = false;
            cbHorror.IsChecked = false;
            cbRomance.IsChecked = false;
            cbFamily.IsChecked = false;
            cbSciFi.IsChecked = false;
            cbWestern.IsChecked = false;
            cbWar.IsChecked = false;
        }

        private void AddCast_Click(object sender, RoutedEventArgs e)
        {
            if (tbCast.Text.Length < 1)
            {
                MessageBox.Show("Cannot add empty cast member!", "Error!");
            }
            else if (tbCast.Text.Length > 0)
            {
                lbCast.Items.Add(tbCast.Text);
            }
        }

        private void DeleteCast_Click(object sender, RoutedEventArgs e)
        {
            if (lbCast.Items.Count == 0)
            {
                MessageBox.Show("The cast list is empty, cannot delete!", "Error!");
            }
            else
            {
                lbCast.Items.RemoveAt(0);
            }
        }
        private void EditSave_Click(object sender, RoutedEventArgs e)
        {
            EditSaveClicked = true;
            SetToViewMode();
        }
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            EditCancelClicked = true;
            SetToViewMode();
           
        }
        private void SetToEditMode()
        {
            db.Clear();

            tbTitle.IsEnabled = true;
            tbDuration.IsEnabled = true;
            tbBudget.IsEnabled = true;
            tbDirector.IsEnabled = true;
            gbGenre.IsEnabled = true;
            lbCast.IsEnabled = true;
            tbCast.Visibility = Visibility.Visible;
            tbYear.IsEnabled = true;
            gbRating.IsEnabled = true;

            bAdd.IsEnabled = true;
            bAdd.Visibility = Visibility.Visible;
            bDelete.IsEnabled = true;
            bDelete.Visibility = Visibility.Visible;

            bFirst.Visibility = Visibility.Collapsed;
            bPrevious.Visibility = Visibility.Collapsed;
            bLast.Visibility = Visibility.Collapsed;
            bNext.Visibility = Visibility.Collapsed;

            lPoster.Visibility = Visibility.Collapsed;
            iPoster.Visibility = Visibility.Collapsed;
            lPosterURL.Visibility = Visibility.Visible;
            lPosterURL.IsEnabled = true;
            tbPosterURL.Visibility = Visibility.Visible;
            tbPosterURL.IsEnabled = true;

            bCancel.Visibility = Visibility.Visible;
            bCancel.IsEnabled = true;
            bSave.Visibility = Visibility.Visible;
            bSave.IsEnabled = true;

            db.Last();
        }
        private void SetToViewMode()
        {
            db.Clear();

            tbTitle.IsEnabled = false;
            tbDuration.IsEnabled = false;
            tbBudget.IsEnabled = false;
            tbDirector.IsEnabled = false;
            gbGenre.IsEnabled = false;
            lbCast.IsEnabled = false;
            tbCast.Visibility = Visibility.Collapsed;
            tbYear.IsEnabled = false;
            gbRating.IsEnabled = false;

            bAdd.IsEnabled = false;
            bAdd.Visibility = Visibility.Collapsed;
            bDelete.IsEnabled = false;
            bDelete.Visibility = Visibility.Collapsed;

            bFirst.Visibility = Visibility.Visible;
            bPrevious.Visibility = Visibility.Visible;
            bLast.Visibility = Visibility.Visible;
            bNext.Visibility = Visibility.Visible;

            lPoster.Visibility = Visibility.Visible;
            iPoster.Visibility = Visibility.Visible;
            lPosterURL.Visibility = Visibility.Collapsed;
            lPosterURL.IsEnabled = false;
            tbPosterURL.Visibility = Visibility.Collapsed;
            tbPosterURL.IsEnabled = false;

            bCancel.Visibility = Visibility.Collapsed;
            bCancel.IsEnabled = false; ;
            bSave.Visibility = Visibility.Collapsed;
            bSave.IsEnabled = false;

            db.Last();
        }
        private void SetToCreateMode()
        {
            //Instantiate new movie and set all fields to blank
            Movie movie = new Movie();

            tbTitle.Text = string.Empty;
            tbYear.Text = string.Empty;
            tbDirector.Text = string.Empty;
            tbDuration.Text = string.Empty;
            tbBudget.Text = string.Empty;
            tbPosterURL.Text = string.Empty;
            iPoster.Source = null;

            rbRate1.IsChecked = false;
            rbRate2.IsChecked = false;
            rbRate2.IsChecked = false;
            rbRate4.IsChecked = false;
            rbRate5.IsChecked = false;

            cbComedy.IsChecked = false;
            cbAction.IsChecked = false;
            cbThriller.IsChecked = false;
            cbHorror.IsChecked = false;
            cbRomance.IsChecked = false;
            cbSciFi.IsChecked = false;
            cbWestern.IsChecked = false;
            cbFamily.IsChecked = false;
            cbWar.IsChecked = false;
            
            tbTitle.IsEnabled = true;
            tbDuration.IsEnabled = true;
            tbBudget.IsEnabled = true;
            tbDirector.IsEnabled = true;
            gbGenre.IsEnabled = true;
            lbCast.IsEnabled = true;
            tbCast.Visibility = Visibility.Visible;
            tbYear.IsEnabled = true;
            gbRating.IsEnabled = true;

            bAdd.IsEnabled = true;
            bAdd.Visibility = Visibility.Visible;
            bDelete.IsEnabled = true;
            bDelete.Visibility = Visibility.Visible;

            bFirst.Visibility = Visibility.Collapsed;
            bPrevious.Visibility = Visibility.Collapsed;
            bLast.Visibility = Visibility.Collapsed;
            bNext.Visibility = Visibility.Collapsed;

            lPoster.Visibility = Visibility.Collapsed;
            iPoster.Visibility = Visibility.Collapsed;
            lPosterURL.Visibility = Visibility.Visible;
            lPosterURL.IsEnabled = true;
            tbPosterURL.Visibility = Visibility.Visible;
            tbPosterURL.IsEnabled = true;

            bCancel.Visibility = Visibility.Visible;
            bCancel.IsEnabled = true;
            bSave.Visibility = Visibility.Visible;
            bSave.IsEnabled = true;

            Clear();

            if (EditSaveClicked == true)
            {
                db.Add(movie);
                UpdateModelFromUI();
                db.Last();
                UpdateUIFromModel();
            }
            else if (EditCancelClicked == true)
            {
                MessageBox.Show("Are you sure you want to cancel and return to view mode? You will lose any unsaved data", "Warning");
            }

            EditSaveClicked = false;
            EditCancelClicked = false;
           
        }
    }
}