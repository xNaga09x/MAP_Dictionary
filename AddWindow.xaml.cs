using System;
using System.Collections.Generic;
using System.IO;
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
using static Tema1.adminWindow;

namespace Tema1
{
    public partial class AddWindow : Window
    {
        private List<string> categories = new List<string>();

        Functions functions = new Functions();

        public AddWindow()
        {
            InitializeComponent();
            categories = functions.GetCategoriesFromFile(@"C:\MAP\WORDS.txt");
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            string fileName = @"C:\MAP\WORDS.txt";

            string name = txtName.Text;
            string description = txtDescription.Text;
            string imagePath = txtImagePath.Text;
            string category = autocompleteBox.Text;

            if (string.IsNullOrEmpty(imagePath)) imagePath = @"C:\MAP\IMAGES\default.jpg";

            try
            {
                using (StreamWriter writer = new StreamWriter(fileName, true))
                {

                    writer.WriteLine($"{name}, {description}, {imagePath},{category}");
                    MessageBox.Show("Cuvânt adăugat!", "Succes", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"A apărut o eroare în timpul scrierii: {ex.Message}", "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            this.Close();
        }

        private void autocompleteBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = autocompleteBox.Text.ToLower();
            suggestionsListBox.Items.Clear();

            foreach (string category in categories)
            {
                if (category.ToLower().StartsWith(searchText))
                {
                    suggestionsListBox.Items.Add(category);
                }
            }

            suggestionsListBox.Visibility = suggestionsListBox.Items.Count > 0 ? Visibility.Visible : Visibility.Collapsed;
        }

        private void suggestionsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (suggestionsListBox.SelectedItem != null)
            {
                string selectedCategory = suggestionsListBox.SelectedItem.ToString();
                autocompleteBox.Text = selectedCategory;
                suggestionsListBox.Visibility = Visibility.Collapsed;
            }
        }

    }
}
