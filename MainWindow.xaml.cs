using System;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;





namespace WpfApp1
{
    public class Person
    {
        public string? m_strPESEL { get; set; }
        public string? m_strName { get; set; }
        public string? m_strAnotherName { get; set; }
        public string? m_strSurname { get; set; }

        public string? m_strBirthDate { get; set; }

        public string? m_strPhoneNumber { get; set; }

        public string? m_strAdress { get; set; }
        public string? m_strLocality { get; set; }

        public string? m_strZipCode { get; set; }
        public Person()
        {

        }
    }

    public partial class MainWindow : Window
    {

        public ObservableCollection<Person> People { get; set; }
        WindowData window = new WindowData();
        public MainWindow()
        {
            InitializeComponent();


            People = new ObservableCollection<Person>();
            listView.ItemsSource = People;
        }

        private void New_Click(object sender, RoutedEventArgs e)
        {
            Person newPerson = new Person
            {
                m_strPESEL = window.PESEL.Text,
                m_strName = ,
                m_strAnotherName = ,
                m_strSurname = "Nazwisko" + random.Next(1, 100),
                m_strBirthDate = ,
                m_strPhoneNumber = ,
                m_strAdress = ,
                m_strLocality = ,
                m_strZipCode = ,
            };


            People.Add(newPerson);
            window.Show();

        }
        private void Open_Click(object sender, RoutedEventArgs e)
        {

        }
        private void Save_Click(object sender, RoutedEventArgs e)
        {

        }
        private void Exit_Click(object sender, RoutedEventArgs e)
        {

        }
        private void NewRecord_Click(object sender, RoutedEventArgs e)
        {

        }
        private void RemoveSel_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
