using CarsharingLibrary.Entities;
using CarsharingLibrary.Functions;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace CarsharingProject.Windows.Authorization
{
    public partial class Authorization : Window
    {
        public Authorization()
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

        private void AuthButton_Click(object sender, RoutedEventArgs e)
        {
            var errors = new StringBuilder();
            if (string.IsNullOrEmpty(UserEmail.Text) ||
                !Validation.ValidEmail(UserEmail.Text))
                errors.AppendLine("Пожалуйста, введите корректную почту!");
            if (string.IsNullOrEmpty(UserPassword.Password) ||
                (UserPassword.Password.Length < 8))
                errors.AppendLine("Пожалуйста, введите корректный пароль! Он должен быть не менее 8 символов");

            var user = CarsharingDbContext.GetContext()
                                          .Users
                                          .FirstOrDefault(x => x.Email == UserEmail.Text && 
                                                          x.Password == Validation.GetHashString(UserPassword.Password));

            if (user == null) 
                errors.AppendLine("Логин или пароль введен неправильно!");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString(),
                                "Внимание",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
                return;
            }

            MessageBox.Show("Авторизация прошла успешно!",
                            "Внимание!",
                            MessageBoxButton.OK,
                            MessageBoxImage.Information);
            Manager.CurrentUser = user;
            new Admin.Admin().Show();
            Close();
        }

        private void RegWindowButton_Click(object sender, RoutedEventArgs e)
        {
            new Registration().Show();
            Close();
        }
    }
}
