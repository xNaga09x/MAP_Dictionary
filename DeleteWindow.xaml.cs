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
using System.Xml.Linq;

namespace Tema1
{
    public partial class DeleteWindow : Window
    {
        public DeleteWindow()
        {
            InitializeComponent();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            string wordToDelete = txtName.Text;

            try
            {
                string fileName = @"C:\MAP\WORDS.txt";
                List<string> lines = File.ReadAllLines(fileName).ToList();

                bool wordFound = false;
                for (int i = 0; i < lines.Count; i++)
                {
                    if (lines[i].StartsWith(wordToDelete + ","))
                    {
                        lines.RemoveAt(i);
                        wordFound = true;
                        break;
                    }
                }

                if (!wordFound)
                {
                    MessageBox.Show("Cuvântul introdus nu a fost găsit.", "Mesaj", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    File.WriteAllLines(fileName, lines);
                    MessageBox.Show("Cuvânt șters!", "Mesaj", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"A apărut o eroare în timpul ștergerii: {ex.Message}", "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            this.Close();
        }
    }
}
