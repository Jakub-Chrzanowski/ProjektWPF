using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp2
{
    public class Person
    {
        public string m_strPESEL { get; set; }
        public string m_strName { get; set; }
        public string m_strAnotherName { get; set; }
        public string m_strSurname { get; set; }
        public string m_strBirthDate { get; set; }
        public string m_strPhoneNumber { get; set; }
        public string m_strAddress { get; set; }
        public string m_strLocality { get; set; }
        public string m_strZipCode { get; set; }

        public Person()
        {
            m_strPESEL = "";
            m_strName = "";
            m_strAnotherName = "";
            m_strSurname = "";
            m_strBirthDate = "";
            m_strPhoneNumber = "";
            m_strAddress = "";
            m_strLocality = "";
            m_strZipCode = "";
        }
    }

    public partial class MainWindow : Window
    {
        public ObservableCollection<Person> People { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            People = new ObservableCollection<Person>();
            listView.ItemsSource = People;
        }

        private void NewRecord_Click(object sender, RoutedEventArgs e)
        {
            WindowData window = new WindowData(null);
            window.Owner = this;
            if (window.ShowDialog() == true)
            {
                People.Add(window.CurrentPerson);
            }
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (listView.SelectedItem is Person selectedPerson)
            {
                WindowData window = new WindowData(selectedPerson);
                window.Owner = this;
                if (window.ShowDialog() == true)
                {
                    
                }
            }
        }

        private void RemoveSel_Click(object sender, RoutedEventArgs e)
        {
            while (listView.SelectedItems.Count > 0)
            {
                People.Remove((Person)listView.SelectedItems[0]);
            }
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Pliki CSV (*.csv)|*.csv",
                Title = "Otwórz plik CSV"
            };
            if (openFileDialog.ShowDialog() == true)
            {
                People.Clear();
                string filePath = openFileDialog.FileName;
                string delimiter = ";";
                Encoding encoding = Encoding.UTF8;
                if (File.Exists(filePath))
                {
                    var lines = File.ReadAllLines(filePath, encoding);
                    foreach (var line in lines)
                    {
                        string[] columns = line.Split(delimiter);
                        if (columns.Length >= 9)
                        {
                            Person person = new Person
                            {
                                m_strPESEL = columns[0],
                                m_strName = columns[1],
                                m_strAnotherName = columns[2],
                                m_strSurname = columns[3],
                                m_strBirthDate = columns[4],
                                m_strPhoneNumber = columns[5],
                                m_strAddress = columns[6],
                                m_strLocality = columns[7],
                                m_strZipCode = columns[8]
                            };
                            People.Add(person);
                        }
                    }
                }
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Pliki CSV (*.csv)|*.csv",
                Title = "Zapisz jako plik CSV"
            };
            if (saveFileDialog.ShowDialog() == true)
            {
                string filePath = saveFileDialog.FileName;
                string delimiter = ";";
                using (StreamWriter writer = new StreamWriter(filePath, false, Encoding.UTF8))
                {
                    foreach (Person item in People)
                    {
                        var row = $"{item.m_strPESEL}{delimiter}{item.m_strName}{delimiter}{item.m_strAnotherName}{delimiter}" +
                                  $"{item.m_strSurname}{delimiter}{item.m_strBirthDate}{delimiter}{item.m_strPhoneNumber}{delimiter}" +
                                  $"{item.m_strAddress}{delimiter}{item.m_strLocality}{delimiter}{item.m_strZipCode}";
                        writer.WriteLine(row);
                    }
                }
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void About_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Baza danych uczniów\nWykoane przez: Jakub Chrzanowski 3TP", "O programie", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
