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
    public partial class entertainmentWindow : Window
    {

        private List<Word> words;
        private int currentIndex = 0;
        private int correctAnswers = 0;
        private int totalWords = 0;
        private int maxWordsToShow = 5;
        private Random random = new Random();
        private List<string> userAnswers = new List<string>();
        private Functions functions = new Functions();

        public entertainmentWindow()
        {
            InitializeComponent();
            LoadWords();
            ShuffleWords();
            ShowWord(currentIndex);
        }

        private void LoadWords()
        {
            string fileName = @"C:\MAP\WORDS.txt";
            words = functions.GetWordsFromFile(fileName);
            totalWords = words.Count;
        }

        private void ShowWord(int index)
        {
            if (index >= 0 && index < maxWordsToShow)
            {
                Word currentWord = words[index];

                if (!string.IsNullOrEmpty(currentWord.ImagePath) && currentWord.ImagePath != "C:\\MAP\\IMAGES\\default.jpg")
                {
                    bool showDescription = random.Next(0, 2) == 0;

                    if (showDescription)
                    {
                        txtDescription.Text = currentWord.Description;
                        txtDescription.Visibility = Visibility.Visible;
                        imgWord.Visibility = Visibility.Collapsed;
                    }
                    else
                    {
                        imgWord.Source = new BitmapImage(new Uri(currentWord.ImagePath));
                        txtDescription.Visibility = Visibility.Collapsed;
                        imgWord.Visibility = Visibility.Visible;
                    }
                }
                else
                {
                    txtDescription.Text = currentWord.Description;
                    txtDescription.Visibility = Visibility.Visible;
                    imgWord.Visibility = Visibility.Collapsed;
                }

                txtWordCount.Text = $"Word {index + 1} of {5}";
            }
        }

        private void ShuffleWords()
        {
            for (int i = 0; i < words.Count - 1; i++)
            {
                int randomIndex = random.Next(i, words.Count);
                Word temp = words[i];
                words[i] = words[randomIndex];
                words[randomIndex] = temp;
            }
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            userAnswers.Add(txtAnswer.Text.Trim());

            currentIndex++;

            if (currentIndex >=maxWordsToShow-1)
            {
                btnNext.Content = "Finish";
            }

            if (currentIndex >= maxWordsToShow)
            {
                CheckAnswers();
                ResetGame();
                return;
            }

            txtAnswer.Text = "";

            if (currentIndex > 0)
            {
                btnPrevious.IsEnabled = true;
            }

            ShowWord(currentIndex);
        }

        private void CheckAnswers()
        {
            int correctCount = 0;
            for (int i = 0; i < maxWordsToShow; i++)
            {
                if (userAnswers[i].Equals(words[i].Name, StringComparison.OrdinalIgnoreCase))
                {
                    correctCount++;
                }
            }

            MessageBox.Show($"Ai ghicit corect {correctCount} cuvinte din 5.");
        }

        private void btnPrevious_Click(object sender, RoutedEventArgs e)
        {
            currentIndex--;
            if (currentIndex <= 0)
            {
                currentIndex = 0;
                btnPrevious.IsEnabled = false;
            }

            if (currentIndex < words.Count - 1)
            {
                btnNext.Content = "Next";
            }

            txtAnswer.Text = "";

            ShowWord(currentIndex);
        }

        private void ResetGame()
        {
            currentIndex = 0;
            correctAnswers = 0;
            userAnswers.Clear();

            totalWords = Math.Min(maxWordsToShow, words.Count);

            ShuffleWords();

            ShowWord(currentIndex);

            btnPrevious.IsEnabled = false;
            btnNext.Content = "Next";
        }
    }
}
