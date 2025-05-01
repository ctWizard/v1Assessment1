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
using MovieLibraryApp;
using testThurs.Models;

namespace assignment1
{
    /// <summary>
    /// Interaction logic for ReserveMovie.xaml
    /// </summary>
    public partial class ReserveMovie : Window
    {
        private MainViewModel _viewModel;
        public ReserveMovie(MainViewModel viewModel)
        {
            InitializeComponent();
            _viewModel=viewModel;
        }

        //Functions for ReserveMove window
        private void Reserve_Click(object sender, EventArgs e)
        {
            string movieId = MovieIdTextBox.Text.Trim();
            string userName = NameTextBox.Text.Trim();

            if (string.IsNullOrEmpty(movieId) || string.IsNullOrEmpty(userName))
            {
                MessageBox.Show("Please enter both Movie ID and your name.");
                return;
            }

            Movie? movie = _viewModel._movieHashTable.Get(movieId);

            if (movie == null)
            {
                MessageBox.Show("Movie not found.");
                return;
            }

            else
            {
                if (movie.Availible)
                {
                    movie.Availible = false;
                    return;
                }
                movie.AddReservation(userName);
                MessageBox.Show($"{userName} has been added to the queue!");
                return;
            }
        }
    } }


