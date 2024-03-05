using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Tema1
{
    internal class Functions
    {
        public List<Word> GetWordsFromFile(string fileName)
        {
            List<Word> words = new List<Word>();

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
                            words.Add(word);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"A apărut o eroare în timpul citirii din fișier: {ex.Message}", "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return words;
        }

        public List<string> GetCategoriesFromFile(string fileName)
        {
            List<string> categories = new List<string>();

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
                            string category = parts[3].Trim();
                            if (!categories.Contains(category))
                            {
                                categories.Add(category);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"A apărut o eroare în timpul citirii din fișier: {ex.Message}", "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return categories;
        }
    }
}
