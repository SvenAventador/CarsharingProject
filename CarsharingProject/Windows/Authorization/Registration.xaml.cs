using CarsharingLibrary.Entities;
using CarsharingLibrary.Functions;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace CarsharingProject.Windows.Authorization
{
    public partial class Registration : Window
    {
        public Registration()
        {
            InitializeComponent();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void GoToAuth_Click(object sender, RoutedEventArgs e)
        {
            new Authorization().Show();
            Close();
        }

        private void AuthButton_Click(object sender, RoutedEventArgs e)
        {
            var errors = new StringBuilder();

            if (UserFIO.Text.Split(" ").Length < 1 &&
                UserFIO.Text.Split(" ").Length > 2)
                errors.AppendLine("Пожалуйста, введите корректное ФИО!");
            if (string.IsNullOrEmpty(UserEmail.Text) ||
                !Validation.ValidEmail(UserEmail.Text))
                errors.AppendLine("Пожалуйста, введите корректную почту!");
            if (string.IsNullOrEmpty(UserPassword.Password) ||
                (UserPassword.Password.Length < 8))
                errors.AppendLine("Пожалуйста, введите корректный пароль! Он должен быть не менее 8 символов");
            if (string.IsNullOrEmpty(UserPhone.Text) ||
                !Validation.ValidPhone(UserPhone.Text))
                errors.AppendLine("Пожалуйста, введите корректный номер телефона!");
            if (string.IsNullOrEmpty(UserDLicense.Text) ||
                (UserDLicense.Text.Length != 10))
                errors.AppendLine("Пожалуйста, введите корректные серию и номер водительского удостоверения!");
            if (string.IsNullOrEmpty(UserDReg.Text) || 
                DateTime.Parse(UserDReg.Text) > DateTime.Now)
                errors.AppendLine("Пожалуйста, введите корректную дату регистрации водительского удостоверения!");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString(),
                                "Внимание",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
                return;
            }

            var user = new User
            {
                FullName = UserFIO.Text,
                Email = UserEmail.Text,
                Password = Validation.GetHashString(UserPassword.Password),
                Phone = UserPhone.Text,
                DriverLicense = UserDLicense.Text,
                RegistrationDate = DateOnly.Parse(UserDReg.Text)
            };

            try
            {
                CarsharingDbContext.GetContext().Users.Add(user);
                CarsharingDbContext.GetContext().SaveChanges();
                MessageBox.Show("Регистрация прошла успешно!",
                                "Внимание!",
                                MessageBoxButton.OK,
                                MessageBoxImage.Information);
                new Authorization().Show();
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при сохранении данных: " + ex.Message,
                                "Внимание",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
            }
        }
    }
}
