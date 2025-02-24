using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Valieva41size
{
    /// <summary>
    /// Логика взаимодействия для AuthPage.xaml
    /// </summary>
    public partial class AuthPage : Page
    {
        int count = 0;  
        public AuthPage()
        {
            InitializeComponent();

            captchaBox.Visibility = Visibility.Hidden;

            //if(count!=0)
            //    GenerateCaptcha();
        }



        private void BtnGuest_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new ProductPage());

        }

        private void GenerateCaptcha()
        {
            // Список возможных символов (буквы и цифры)
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new Random();
            string captcha = new string(Enumerable.Range(0, 4).Select(_ => chars[random.Next(chars.Length)]).ToArray());

            // Разделяем капчу на отдельные символы
            captchaOneWord.Text = captcha[0].ToString();
            captchaTwoWord.Text = captcha[1].ToString();
            captchaThreeWord.Text = captcha[2].ToString();
            captchaFourWord.Text = captcha[3].ToString();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            string login = LoginTB.Text;
            string password = PasswordTB.Text;
            if(login == "" || password == "")
            {
                MessageBox.Show("Есть пустые поля");
                return;
            }

            User user = Valieva41Entities.GetContext().User.ToList().Find(p => p.UserLogin == login && p.UserPassword == password);
            if(user != null)
            {
                Manager.MainFrame.Navigate(new ProductPage(user));
                LoginTB.Text = "";
                PasswordTB.Text = "";

            }
            else
            {
                MessageBox.Show("Введены неверные данные");
                BtnLogin.IsEnabled = false;
                BtnLogin.IsEnabled = true;
                captcha.Text = "Введите капчу: ";
                captchaBox.Visibility = Visibility.Visible;
                GenerateCaptcha();

            }

        }


    }
}
