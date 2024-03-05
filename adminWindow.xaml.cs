using System;
using System.Collections;
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
using static Tema1.adminView;

namespace Tema1
{
    public partial class adminWindow : Window
    {

        Dictionary<string, Admin> admins;
        string fileName = @"C:\MAP\ADMINS.txt";

        public adminWindow()
        {
            InitializeComponent();
            admins = new Dictionary<string, Admin>();
            LoadAdminsFromFile();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            Admin admin = new Admin
            {
                Name = txtUsername.Text,
                Password = txtPassword.Password
            };

            Login(admin);
        }

        public void Login(Admin admin)
        {
            bool loggedIn = false;

            foreach (var pair in admins)
            {
                Admin a = pair.Value;
                if (a.Name == admin.Name && a.Password == admin.Password)
                {
                    loggedIn = true;
                    break;
                }
            }

            if (loggedIn)
            {
                MessageBox.Show("Autentificare reușită!", "Succes", MessageBoxButton.OK, MessageBoxImage.Information);
                adminView adminView = new adminView();
                adminView.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Nume de utilizator sau parolă incorectă!", "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadAdminsFromFile()
        {
            try
            {
                using (StreamReader reader = new StreamReader(fileName))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] parts = line.Split(',');

                        if (parts.Length == 2)
                        {
                            Admin admin = new Admin
                            {
                                Name = parts[0].Trim(),
                                Password = parts[1].Trim()
                            };

                            admins[admin.Name] = admin;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"A apărut o eroare în timpul citirii din fișier: {ex.Message}", "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
