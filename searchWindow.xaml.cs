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

namespace Tema1
{
    public partial class searchWindow : Window
    {
        private List<Word> wordsFromFile;
        private List<Word> currentCategoryWords;
        private Functions functions=new Functions();
        public searchWindow()
        {
            InitializeComponent();
            PopulateCategoryDropDown();
            InitializeWordsFromFile();
        }

        private List<Word> FilterWordsByCategory(List<Word> words, string category)
        {
            return words.Where(word => word.Category.Name.Equals(category)).ToList();
        }

        private void cboCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboCategory.SelectedItem != null)
            {
                string selectedCategory = ((ComboBoxItem)cboCategory.SelectedItem).Content.ToString();
                List<Word> words = functions.GetWordsFromFile(@"C:\MAP\WORDS.txt");
                currentCategoryWords = FilterWordsByCategory(words, selectedCategory); // Actualizează lista de cuvinte pentru categoria selectată

            }
        }

        private void PopulateCategoryDropDown()
        {
            string fileName = @"C:\MAP\WORDS.txt";
            List<string> categories = functions.GetCategoriesFromFile(fileName);

            if (categories != null && categories.Count > 0)
            {
                foreach (string category in categories)
                {
                    ComboBoxItem item = new ComboBoxItem();
                    item.Content = category;
                    cboCategory.Items.Add(item);
                }
            }
            cboCategory.SelectionChanged += cboCategory_SelectionChanged;
        }

        private void InitializeWordsFromFile()
        {
            string fileName = @"C:\MAP\WORDS.txt";
            wordsFromFile = new List<Word>();

            try
            {
                using (StreamReader reader = new StreamReader(fileName))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] parts = line.Split(',');
                        if (parts.Length >= 4)
                        {
                            Category category = new Category { Name = parts[3].Trim() };
                            Word word = new Word
                            {
                                Name = parts[0].Trim(),
                                Description = parts[1].Trim(),
                                ImagePath = parts[2].Trim(),
                                Category = category
                            };
                            wordsFromFile.Add(word);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"A apărut o eroare în timpul citirii din fișier: {ex.Message}", "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void autocompleteBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = autocompleteBox.Text.ToLower();
            suggestionsListBox.Items.Clear();

            autocompleteBox.TextChanged -= autocompleteBox_TextChanged;

            IEnumerable<Word> wordsToFilter = cboCategory.SelectedItem != null ? currentCategoryWords : wordsFromFile;
            foreach (Word word in wordsToFilter)
            {
                if (word.Name.ToLower().StartsWith(searchText))
                {
                    suggestionsListBox.Items.Add(word.Name);
                }
            }

            autocompleteBox.TextChanged += autocompleteBox_TextChanged;

            suggestionsListBox.Visibility = suggestionsListBox.Items.Count > 0 ? Visibility.Visible : Visibility.Collapsed;
        }

        private void suggestionsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (suggestionsListBox.SelectedItem != null)
            {
                string selectedWord = suggestionsListBox.SelectedItem.ToString();

                Word selectedWordObject = wordsFromFile.FirstOrDefault(w => w.Name.Equals(selectedWord));

                wordListBox.ItemsSource = new List<Word> { selectedWordObject };

                wordListBox.Visibility = Visibility.Visible;

                suggestionsListBox.Visibility = Visibility.Collapsed;
            }
        }

    }
}
