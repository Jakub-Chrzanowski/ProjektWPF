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
    public class Person { 

    public int m_nID { get; set; }
    public string m_strName { get; set; }
    public string m_strSname { get; set; }
    public string m_strPESEL { get; set; }
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

            
            private void Button_Click(object sender, RoutedEventArgs e)
            {
               
                Random random = new Random();
                Person newPerson = new Person
                {
                    m_nID = random.Next(1000, 9999), 
                    m_strName = "Imie" + random.Next(1, 100), 
                    m_strSname = "Nazwisko" + random.Next(1, 100), 
                    m_strPESEL = random.Next(1000000, 99999999).ToString() 
                };

              
                People.Add(newPerson);
            }
        }
    }
