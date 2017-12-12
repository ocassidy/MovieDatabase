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
using Newtonsoft.Json;

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

        public MovieView()
        {
            InitializeComponent();
            db = new Database();
            mode = WindowMode.View;
        }

        private void UpdateUIFromModel(Movie m)
        {
            //Updates the UI from the model
            tbTitle.Text = m != null ? m.Title : "";
            tbYear.Text = m != null ? m.Year.ToString() : "0";
            tbDirector.Text = m != null ? m.Director : "";
            tbDuration.Text = m != null ? m.Duration.ToString() : "0";
            tbBudget.Text = m != null ? m.Budget.ToString() : "0";
            tbPosterURL.Text = m != null ? m.Poster : "";
            //iPoster.Source = new BitmapImage(new Uri(m.Poster));

            if (Uri.IsWellFormedUriString(m.Poster, UriKind.Absolute))
            {
                var uri = new Uri(m.Poster, UriKind.Absolute);
                iPoster.Source = new BitmapImage(uri);
            }

            if (m.Rating == 1)
            {
                rbRate1.IsChecked = true;
            }
            else if (m.Rating == 2)
            {
                rbRate2.IsChecked = true;
            }
            else if (m.Rating == 3)
            {
                rbRate3.IsChecked = true;
            }
            else if (m.Rating == 4)
            {
                rbRate4.IsChecked = true;
            }
            else if (m.Rating == 5)
            {
                rbRate5.IsChecked = true;
            }

            if (m.Genre.Contains(Genres.Comedy))
            {
                cbComedy.IsChecked = true;
            }
            if (m.Genre.Contains(Genres.Action))
            {
                cbAction.IsChecked = true;
            }
            if (m.Genre.Contains(Genres.Thriller))
            {
                cbThriller.IsChecked = true;
            }
            if (m.Genre.Contains(Genres.Horror))
            {
                cbHorror.IsChecked = true;
            }
            if (m.Genre.Contains(Genres.Romance))
            {
                cbRomance.IsChecked = true;
            }
            if (m.Genre.Contains(Genres.SciFi))
            {
                cbSciFi.IsChecked = true;
            }
            if (m.Genre.Contains(Genres.Western))
            {
                cbWestern.IsChecked = true;
            }
            if (m.Genre.Contains(Genres.Family))
            {
                cbFamily.IsChecked = true;
            }
            if (m.Genre.Contains(Genres.War))
            {
                cbWar.IsChecked = true;
            }

            foreach (string actors in m.Actors)
            {
                lbCast.Items.Add(actors);
            }

            NavChecks();
        }

        private Movie UpdateModelFromUI(Movie m)
        {
            //Updates the model from the UI 
            m.Title = tbTitle.Text;
            m.Year = Convert.ToInt32(tbYear.Text);
            m.Director = tbDirector.Text;
            m.Duration = Convert.ToInt32(tbDuration.Text);
            m.Budget = Convert.ToInt32(tbBudget.Text);
            m.Rating = rbRate1.IsChecked.Value ? m.Rating = 1 :
                              rbRate2.IsChecked.Value ? m.Rating = 2 :
                              rbRate3.IsChecked.Value ? m.Rating = 3 :
                              rbRate4.IsChecked.Value ? m.Rating = 4 :
                              rbRate5.IsChecked.Value ? m.Rating = 5 : m.Rating;

            //if (Uri.IsWellFormedUriString(tbPosterURL.Text, UriKind.Absolute))
            //{
            // create the url and add as a string to the iPoster
            var uri = new Uri(tbPosterURL.Text, UriKind.Absolute);
            m.Poster = (uri.ToString());
            iPoster.Source = new BitmapImage(uri);
            //}
            //else
            //{
            //    // the path is not a valid URI
            //    MessageBox.Show("Invalid Poster URL", "Error");
            //}

            if (cbComedy.IsChecked.Value)
            {
                m.Genre.Add(Genres.Comedy);
            }
            if (cbAction.IsChecked.Value)
            {
                m.Genre.Add(Genres.Action);
            }
            if (cbThriller.IsChecked.Value)
            {
                m.Genre.Add(Genres.Thriller);
            }
            if (cbHorror.IsChecked.Value)
            {
                m.Genre.Add(Genres.Horror);
            }
            if (cbRomance.IsChecked.Value)
            {
                m.Genre.Add(Genres.Romance);
            }
            if (cbSciFi.IsChecked.Value)
            {
                m.Genre.Add(Genres.SciFi);
            }
            if (cbWestern.IsChecked.Value)
            {
                m.Genre.Add(Genres.Western);
            }
            if (cbFamily.IsChecked.Value)
            {
                m.Genre.Add(Genres.Family);
            }
            if (cbWar.IsChecked.Value)
            {
                m.Genre.Add(Genres.War);
            }

            //
            foreach (string actors in lbCast.Items)
            {
                m.Actors.ToString();
            }
            return m;
        }

        //Dockpanel menu items start
        private void New_Click(object sender, RoutedEventArgs e)
        {
            db = new Database();
            tbTitle.Text = null;
            tbYear.Text = null;
            tbDirector.Text = null;
            tbDuration.Text = null;
            tbBudget.Text = null;
            iPoster.Source = null;
            NavClear();
            NavChecks();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new SaveFileDialog()
            {
                Filter = "json files|*.json",
                Title = "Save"
            };
            // if the user enters a filename and clicks save
            if (dialog.ShowDialog() == true)
            {
                db.Save(dialog.FileName);
            }
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog()
            {
                Filter = "json files|*.json",
                Title = "Load"
            };
            // if the user enters a filename and clicks save
            if (dialog.ShowDialog() == true)
            {
                db.Load(dialog.FileName);
                db.First();
                UpdateUIFromModel(db.Get());
                // update navigation
                bNext.IsEnabled = true;
                bPrev.IsEnabled = true;
                bLast.IsEnabled = true;
                bFirst.IsEnabled = true;
                NavChecks();
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (db.Count > 0)
            {
                SetToEditMode();
                NavClear();
                UpdateUIFromModel(db.Get());
                mode = WindowMode.Edit;
            }
        }

        private void View_Click(object sender, RoutedEventArgs e)
        {
            SetToViewMode();
            NavClear();
            mode = WindowMode.View;
        }

        private void Help_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(mode.ToString());
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            UpdateUIFromModel(new Movie());
            SetToEditMode();
            mode = WindowMode.Create;
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            db.Delete();

            UpdateUIFromModel(db.Get());
            // update navigation
        }

        private void OrderbyTitle_Click(object sender, RoutedEventArgs e)
        {
            db.OrderByTitle();          
            NavClear();
            UpdateUIFromModel(db.Get());
            db.First();
        }

        private void OrderbyYear_Click(object sender, RoutedEventArgs e)
        {
            db.OrderByTitle();
            NavClear();
            UpdateUIFromModel(db.Get());
        }

        private void OrderbyDuration_Click(object sender, RoutedEventArgs e)
        {
            db.OrderByDuration();
            NavClear();
            UpdateUIFromModel(db.Get());
            db.First();

        }
        //Dockpanel menu items end

        //Navigation buttons start 
        private void First_Click(object sender, RoutedEventArgs e)
        {
            if (db.First())
            {
                NavClear();
                UpdateUIFromModel(db.Get());
            }
        }

        private void Previous_Click(object sender, RoutedEventArgs e)
        {
            if (db.Prev())
            {
                NavClear();
                UpdateUIFromModel(db.Get());
            }
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            if (db.Next())
            {
                NavClear();
                UpdateUIFromModel(db.Get());
            }
        }

        private void Last_Click(object sender, RoutedEventArgs e)
        {
            if (db.Last())
            {
                NavClear();
                UpdateUIFromModel(db.Get());
            }
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
            if (mode == WindowMode.Create)
            {
                Movie m = UpdateModelFromUI(new Movie());
                db.Add(m);
            }
            else
            {
                Movie m = UpdateModelFromUI(new Movie());
                db.Update(m);

            }
            SetToViewMode();
            UpdateUIFromModel(db.Get());
            mode = WindowMode.View;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            SetToViewMode();
            UpdateUIFromModel(db.Get());
            mode = WindowMode.View;
        }
        private void SetToEditMode()
        {
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
            bPrev.Visibility = Visibility.Collapsed;
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
        }

        private void SetToViewMode()
        {
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
            bPrev.Visibility = Visibility.Visible;
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
        }
        private void NavChecks()
        {
            if (db.Index == db.Count - 1)
            {
                bNext.IsEnabled = false;
                bLast.IsEnabled = false;
            }
            else if (db.Index == 0)
            {
                bFirst.IsEnabled = false;
                bPrev.IsEnabled = false;
            }
            else
            {
                bNext.IsEnabled = true;
                bLast.IsEnabled = true;
                bFirst.IsEnabled = true;
                bPrev.IsEnabled = true;
            }
        }

        //Navigational Clears
        private void NavClear()
        {
            tbCast.Text = null;
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
    }
}