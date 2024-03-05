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

    public partial class ModifyWindow : Window
    {
        public ModifyWindow()
        {
            InitializeComponent();
        }

        private void Modify_Click(object sender, RoutedEventArgs e)
        {
            string wordToModify = txtName.Text;

            string newDescription = txtDescription.Text;
            string newImagePath = txtImagePath.Text;
            string newCategory = txtCategory.Text;

            try
            {
                string fileName = @"C:\MAP\WORDS.txt";
                List<string> lines = File.ReadAllLines(fileName).ToList();

                bool wordFound = false;
                for (int i = 0; i < lines.Count; i++)
                {
                    if (lines[i].StartsWith(wordToModify + ","))
                    {
                        string[] parts = lines[i].Split(',');

                        if (!string.IsNullOrEmpty(newDescription)) parts[1] = newDescription;
                        if (!string.IsNullOrEmpty(newImagePath)) parts[2] = newImagePath;
                        if (!string.IsNullOrEmpty(newCategory)) parts[3] = newCategory;

                        lines[i] = string.Join(",", parts);

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
                    MessageBox.Show("Detaliile cuvântului au fost actualizate cu succes.", "Mesaj", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"A apărut o eroare în timpul actualizării detaliilor cuvântului: {ex.Message}", "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            this.Close();
        }
    }
}
