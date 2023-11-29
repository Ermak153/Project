using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
using CollegeNavigatorPC.Model;
using CollegeNavigatorPC.ViewModel;

namespace CollegeNavigatorPC.View
{
    /// <summary>
    /// Логика взаимодействия для AudiencesPage.xaml
    /// </summary>
    public partial class AudiencesPage : Page
    {
        public AudiencesPage()
        {
            InitializeComponent();
        }

        private void AudiencesPage_Click(object sender, RoutedEventArgs e)
        {
            // Создание новой страницы, которую вы хотите отобразить
            var mapPage = new MapPage();

            // Получение ссылки на NavigationWindow, в котором находится текущая страница
            var navigationWindow = Window.GetWindow(this) as NavigationWindow;

            // Проверка, что NavigationWindow доступен
            if (navigationWindow != null)
            {
                // Навигация на новую страницу
                navigationWindow.Navigate(mapPage);
            }
        }
    }
}
