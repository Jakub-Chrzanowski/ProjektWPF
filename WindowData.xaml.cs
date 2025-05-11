using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Globalization;

namespace WpfApp2
{
    public partial class WindowData : Window
    {
        public Person CurrentPerson { get; private set; }
        private bool isDataModified = false;
        private bool isEditing;

        public WindowData(Person person)
        {
            InitializeComponent();
            if (person != null)
            {
                CurrentPerson = person;
            }
            else
            {
                CurrentPerson = new Person();
            }
            isEditing = person != null;
            LoadPersonData();
            ValidatePESELField();
        }

        private void LoadPersonData()
        {
            txtPESEL.Text = CurrentPerson.m_strPESEL;
            txtName.Text = CurrentPerson.m_strName;
            txtAnotherName.Text = CurrentPerson.m_strAnotherName;
            txtSurname.Text = CurrentPerson.m_strSurname;
            
            if (DateTime.TryParseExact(CurrentPerson.m_strBirthDate, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime birthDate))
            {
                dpBirthDate.SelectedDate = birthDate;
            }
     
            txtPhoneNumber.Text = CurrentPerson.m_strPhoneNumber;
            txtAddress.Text = CurrentPerson.m_strAddress;
            txtLocality.Text = CurrentPerson.m_strLocality;
            txtZipCode.Text = CurrentPerson.m_strZipCode;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            isDataModified = true;
            if (sender == txtPESEL)
                ValidatePESELField();
        }

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            isDataModified = true;
            ValidatePESELField();
        }

        private void ValidatePESELField()
        {
            if (string.IsNullOrWhiteSpace(txtPESEL.Text) || !ValidatePESEL(txtPESEL.Text))
                txtPESEL.Background = Brushes.LightPink;
            else
                txtPESEL.Background = Brushes.White;
        }

        private void btn_ok_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateAllFields())
            {
                SavePersonData();
                DialogResult = true;
                Close();
            }
        }

        private void btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            if (isDataModified)
            {
                var result = MessageBox.Show("Czy na pewno chcesz zamknąć okno bez zapisywania zmian?", "Potwierdzenie", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.No)
                    return;
            }
            DialogResult = false;
            Close();
        }

        private bool ValidateAllFields()
        {
            bool isValid = true;

            
            if (string.IsNullOrWhiteSpace(txtPESEL.Text) || !ValidatePESEL(txtPESEL.Text))
            {
                txtPESEL.Background = Brushes.LightPink;
                isValid = false;
            }
            else
            {
                txtPESEL.Background = Brushes.White;
            }

            
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                txtName.Background = Brushes.LightPink;
                isValid = false;
            }
            else
            {
                txtName.Background = Brushes.White;
            }

           
            txtAnotherName.Background = Brushes.White;

            
            if (string.IsNullOrWhiteSpace(txtSurname.Text))
            {
                txtSurname.Background = Brushes.LightPink;
                isValid = false;
            }
            else
            {
                txtSurname.Background = Brushes.White;
            }

           
            if (!ValidateBirthDate())
            {
                dpBirthDate.Background = Brushes.LightPink;
                isValid = false;
            }
            else
            {
                dpBirthDate.Background = Brushes.White;
            }

            
            if (!string.IsNullOrWhiteSpace(txtPhoneNumber.Text) && !ValidatePhoneNumber(txtPhoneNumber.Text))
            {
                txtPhoneNumber.Background = Brushes.LightPink;
                isValid = false;
            }
            else
            {
                txtPhoneNumber.Background = Brushes.White;
            }

            
            if (string.IsNullOrWhiteSpace(txtAddress.Text))
            {
                txtAddress.Background = Brushes.LightPink;
                isValid = false;
            }
            else
            {
                txtAddress.Background = Brushes.White;
            }


            if (string.IsNullOrWhiteSpace(txtLocality.Text))
            {
                txtLocality.Background = Brushes.LightPink;
                isValid = false;
            }
            else
            {
                txtLocality.Background = Brushes.White;
            }

      
            if (string.IsNullOrWhiteSpace(txtZipCode.Text) || !IsValidZipCode(txtZipCode.Text))
            {
                txtZipCode.Background = Brushes.LightPink;
                isValid = false;
            }
            else
            {
                txtZipCode.Background = Brushes.White;
            }

            return isValid;
        }

        private bool IsValidZipCode(string zipCode)
        {
            if (zipCode.Length != 6 || zipCode[2] != '-') return false;
            string digits = zipCode.Remove(2, 1);
            return digits.All(char.IsDigit);
        }

        private bool ValidatePESEL(string pesel)
        {
            if (pesel.Length != 11 || !pesel.All(char.IsDigit))
                return false;

            
            if (!IsValidChecksum(pesel))
                return false;

           
            if (!dpBirthDate.SelectedDate.HasValue)
                return false;

            string birthDateStr = dpBirthDate.SelectedDate.Value.ToString("dd-MM-yyyy");
            return IsDateMatchingPesel(birthDateStr, pesel);
        }

        private bool IsValidChecksum(string pesel)
        {
            int[] weights = { 1, 3, 7, 9, 1, 3, 7, 9, 1, 3 };
            int sum = 0;
            for (int i = 0; i < weights.Length; i++)
            {
                sum += weights[i] * int.Parse(pesel[i].ToString());
            }
            int lastDigit = int.Parse(pesel[10].ToString());
            int checksum = (10 - (sum % 10)) % 10;
            return checksum == lastDigit;
        }

        private bool IsDateMatchingPesel(string birthDateStr, string pesel)
        {
            if (!DateTime.TryParseExact(birthDateStr, "dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime birthDate))
                return false;

            int year = birthDate.Year;
            int month = birthDate.Month;
            int day = birthDate.Day;

            int peselYear = int.Parse(pesel.Substring(0, 2));
            int peselMonth = int.Parse(pesel.Substring(2, 2));
            int peselDay = int.Parse(pesel.Substring(4, 2));

           
            int centuryOffset;
            if (peselMonth >= 1 && peselMonth <= 12)
            {
                centuryOffset = 1900;
            }
            else if (peselMonth >= 21 && peselMonth <= 32)
            {
                centuryOffset = 2000;
                peselMonth -= 20;
            }
            else if (peselMonth >= 41 && peselMonth <= 52)
            {
                centuryOffset = 2100;
                peselMonth -= 40;
            }
            else if (peselMonth >= 61 && peselMonth <= 72)
            {
                centuryOffset = 2200;
                peselMonth -= 60;
            }
            else if (peselMonth >= 81 && peselMonth <= 92)
            {
                centuryOffset = 1800;
                peselMonth -= 80;
            }
            else
            {
                return false;
            }

            int fullPeselYear = centuryOffset + peselYear;
            return fullPeselYear == year && peselMonth == month && peselDay == day;
        }

        private bool ValidatePhoneNumber(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone)) return true;
            string cleanPhone = phone.Replace(" ", "");
            if (cleanPhone.StartsWith("+")) cleanPhone = cleanPhone.Substring(1);
            return cleanPhone.All(char.IsDigit) && cleanPhone.Length >= 9 && cleanPhone.Length <= 12;
        }

        private bool ValidateBirthDate()
        {
            bool isValid = dpBirthDate.SelectedDate.HasValue;
            dpBirthDate.Background = isValid ? Brushes.White : Brushes.LightPink;
            return isValid;
        }

        private void SavePersonData()
        {
            CurrentPerson.m_strPESEL = txtPESEL.Text.Trim();
            CurrentPerson.m_strName = FormatName(txtName.Text);
            CurrentPerson.m_strAnotherName = FormatName(txtAnotherName.Text);
            CurrentPerson.m_strSurname = FormatName(txtSurname.Text);
            CurrentPerson.m_strBirthDate = dpBirthDate.SelectedDate?.ToString("yyyy-MM-dd") ?? "";
            CurrentPerson.m_strPhoneNumber = FormatPhoneNumber(txtPhoneNumber.Text);
            CurrentPerson.m_strAddress = FormatName(txtAddress.Text);
            CurrentPerson.m_strLocality = FormatName(txtLocality.Text);
            CurrentPerson.m_strZipCode = txtZipCode.Text.Trim();
        }

        private string FormatName(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return "";
            input = input.Trim().Replace("  ", " ");
            return char.ToUpper(input[0]) + input.Substring(1).ToLower();
        }

        private string FormatPhoneNumber(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone)) return "";
            phone = phone.Replace(" ", "");
            if (!phone.StartsWith("+"))
                phone = "+48" + phone;
            return phone;
        }
    }
}
