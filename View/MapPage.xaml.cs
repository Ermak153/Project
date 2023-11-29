using CollegeNavigatorPC.View;
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

namespace CollegeNavigatorPC.View
{
    public partial class MapPage : Page
    {
        private bool isDragging = false;
        private Point startPoint;
        private ScaleTransform scaleTransform;
        private TranslateTransform translateTransform;


        // словарь аудиторий и их координат на карте
        Dictionary<string, Point> roomCoordinates = new Dictionary<string, Point>()
        {
            { "100", new Point(600, 2775) },
            { "101", new Point(600, 2650) },
            { "102", new Point(697, 2680) },
            { "103", new Point(600, 2530) },
            { "104", new Point(697, 2533) },
            { "105", new Point(600, 2350) },
            { "106", new Point(697, 2380) },
            { "108", new Point(697, 2225) },
            { "110", new Point(697, 2065) },

            { "201", new Point(155, 2675) },
            { "202", new Point(180, 2675) },
            { "203", new Point(155, 2520) },
            { "204", new Point(180, 2530) },
            { "205", new Point(155, 2470) },
            { "206", new Point(180, 2380) },
            { "207", new Point(155, 2410) },
            { "208", new Point(180, 2220) },
            { "209", new Point(155, 2315) },
            { "210", new Point(180, 2100) },
            { "212", new Point(180, 1980) },
            { "213", new Point(155, 1900) },
            { "214", new Point(180, 1900) },
            { "215", new Point(155, 1850) },
            { "216", new Point(180, 1850) },
            { "217", new Point(155, 1780) },
            { "218", new Point(180, 1790) },
            { "220", new Point(180, 1730) },
            { "222", new Point(180, 1670) },
            { "223", new Point(155, 1690) },
            { "224", new Point(180, 1420) },
            { "225", new Point(155, 1480) },
            { "226", new Point(180, 1360) },
            { "227", new Point(155, 1290) },
            { "228", new Point(180, 1290) },
            { "229", new Point(155, 1100) },
            { "230", new Point(180, 1230) },
            { "231", new Point(155, 920) },
            { "232", new Point(180, 1170) },
            { "233", new Point(155, 740) },
            { "234", new Point(180, 740) },
            { "235", new Point(155, 550) },
            { "236", new Point(180, 550) },

            { "301", new Point(155, 2675) },
            { "302", new Point(180, 2680) },
            { "303", new Point(155, 2530) },
            { "304", new Point(180, 2530) },
            { "305", new Point(155, 2400) },
            { "306", new Point(180, 2400) },
            { "307", new Point(155, 2220) },
            { "308", new Point(180, 2220) },
            { "309", new Point(155, 1770) },
            { "310", new Point(180, 2100) },
            { "311", new Point(155, 1580) },
            { "312", new Point(180, 1800) },
            { "312а", new Point(180, 1560) },
            { "312a", new Point(180, 1560) },
            { "313", new Point(155, 1270) },
            { "314", new Point(180, 1380) },
            { "315", new Point(155, 1150) },
            { "316", new Point(180, 1250) },
            { "317", new Point(155, 1080) },
            { "318", new Point(180, 1150) },
            { "319", new Point(155, 920) },
            { "321", new Point(155, 780) },
            { "323", new Point(155, 650) },

            { "401", new Point(155, 2650) },
            { "402", new Point(180, 2750) },
            { "405", new Point(155, 2400) },
            { "407", new Point(155, 2280) },
            { "408", new Point(180, 2375) },
            { "409", new Point(155, 1870) },
            { "410", new Point(180, 2250) },
            { "411", new Point(155, 1740) },
            { "412", new Point(180, 1780) },
            { "412a", new Point(180, 2060) },
            { "412а", new Point(180, 2060) },
            { "413", new Point(155, 1650) },
            { "414", new Point(180, 1560) },
            { "414а", new Point(180, 1900) },
            { "414a", new Point(180, 1900) },
            { "415", new Point(155, 1590) },
            { "416", new Point(180, 1400) },
            { "417", new Point(155, 1460) },
            { "418", new Point(180, 1340) },
            { "420", new Point(180, 1150) },
            { "421", new Point(155, 1310) },
            { "423", new Point(155, 1090) },
            { "424", new Point(180, 660) },
            { "425", new Point(155, 900) },
            { "426", new Point(180, 510) },
            { "427", new Point(155, 720) },
            { "429", new Point(155, 530) }
        };

        // текущий этаж
        int currentFloor = 1;

        public MapPage()
        {
            InitializeComponent();

            // Создайте экземпляры трансформаций
            scaleTransform = new ScaleTransform(1, 1);
            translateTransform = new TranslateTransform();

            // Добавьте трансформации к TransformGroup
            TransformGroup transformGroup = new TransformGroup();
            transformGroup.Children.Add(scaleTransform);
            transformGroup.Children.Add(translateTransform);
            mapCanvas.RenderTransform = new TransformGroup
            {
                Children = new TransformCollection
                {
                    new ScaleTransform(scaleTransform.ScaleX, scaleTransform.ScaleY),
                    new TranslateTransform(translateTransform.X, translateTransform.Y)
                }
            };



            // Установите TransformGroup в качестве трансформации для изображения
            mapCanvas.RenderTransform = transformGroup;

            // установка картинки
            mapImage.Source = new BitmapImage(new Uri($"pack://application:,,,/CollegeNavigatorPC;component/View/Resources/Map/Map{currentFloor}.png"));

            // Создайте маркер
            marker = new Ellipse();
            marker.Width = 5;
            marker.Height = 5;
            marker.Fill = Brushes.Red;
            marker.Visibility = Visibility.Collapsed; // Скрыть маркер по умолчанию

            //Добавьте маркер в Canvas
            mapCanvas.Children.Add(marker);

            mapCanvas.MouseWheel += mapImage_MouseWheel;
            mapCanvas.MouseDown += mapImage_MouseDown;
            mapCanvas.MouseMove += mapImage_MouseMove;
            mapCanvas.MouseUp += mapImage_MouseUp;


        }

        // обработчик кнопки "найти маршрут"
        private void FindRouteButton_Click(object sender, RoutedEventArgs e)
        {
            string roomNumber = roomNumberTextBox.Text;

            // Проверяем, есть ли координаты для данной аудитории
            if (roomCoordinates.ContainsKey(roomNumber))
            {
                switch (roomNumber[0])
                {
                    case '1':
                        currentFloor = 1;
                        mapImage.Source = new BitmapImage(new Uri($"pack://application:,,,/CollegeNavigatorPC;component/View/Resources/Map/Map{currentFloor}.png"));
                        break;
                    case '2':
                        currentFloor = 2;
                        mapImage.Source = new BitmapImage(new Uri($"pack://application:,,,/CollegeNavigatorPC;component/View/Resources/Map/Map{currentFloor}.png"));
                        break;
                    case '3':
                        currentFloor = 3;
                        mapImage.Source = new BitmapImage(new Uri($"pack://application:,,,/CollegeNavigatorPC;component/View/Resources/Map/Map{currentFloor}.png"));
                        break;
                    case '4':
                        currentFloor = 4;
                        mapImage.Source = new BitmapImage(new Uri($"pack://application:,,,/CollegeNavigatorPC;component/View/Resources/Map/Map{currentFloor}.png"));
                        break;
                }
                Point roomCoordinate = roomCoordinates[roomNumber];

                // Переводим координаты аудитории в координаты на изображении
                double scaleX = mapCanvas.ActualWidth / mapImage.Source.Width;
                double scaleY = mapCanvas.ActualHeight / mapImage.Source.Height;
                double markerPositionX = roomCoordinate.X * scaleX;
                double markerPositionY = roomCoordinate.Y * scaleY;

                // Устанавливаем новую позицию маркера
                Canvas.SetLeft(marker, markerPositionX - marker.Width / 2);
                Canvas.SetTop(marker, markerPositionY - marker.Height / 2);
                // Показываем маркер
                marker.Visibility = Visibility.Visible;
            }
            else
            {
                // Аудитория не найдена, скрываем маркер
                marker.Visibility = Visibility.Collapsed;
                // Тут можешь даже ошибку сообщить, можно использовать MessageBox
                MessageBox.Show("Аудитория не найдена");
            }
        }

        // обработчик кнопки "этаж назад"
        private void PrevFloorButton_Click(object sender, RoutedEventArgs e)
        {
            // уменьшение номера текущего этажа
            if (currentFloor > 1)
            {
                currentFloor--;
                mapImage.Source = new BitmapImage(new Uri($"pack://application:,,,/CollegeNavigatorPC;component/View/Resources/Map/Map{currentFloor}.png"));
                marker.Visibility = Visibility.Collapsed;
            }
        }

        // обработчик кнопки "этаж вперед"
        private void NextFloorButton_Click(object sender, RoutedEventArgs e)
        {
            // увеличение номера текущего этажа
            if (currentFloor < 4)
            {
                currentFloor++;
                mapImage.Source = new BitmapImage(new Uri($"pack://application:,,,/CollegeNavigatorPC;component/View/Resources/Map/Map{currentFloor}.png"));
                marker.Visibility = Visibility.Collapsed;
            }
        }

        private void mapImage_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            // Получите текущий масштаб
            double currentScaleX = this.scaleTransform.ScaleX;
            double currentScaleY = this.scaleTransform.ScaleY;

            // Определите множитель изменения масштаба
            double scaleChange = e.Delta > 0 ? 1.1 : 0.9;

            // Примените ограничения на масштаб
            double newScaleX = currentScaleX * scaleChange;
            double newScaleY = currentScaleY * scaleChange;

            // Установите минимальный и максимальный масштаб
            double minScale = 1;
            double maxScale = 5.0;

            if (newScaleX >= minScale && newScaleX <= maxScale && newScaleY >= minScale && newScaleY <= maxScale)
            {
                // Умножьте текущий масштаб на множитель изменения
                this.scaleTransform.ScaleX = newScaleX;
                this.scaleTransform.ScaleY = newScaleY;
            }
        }

        private void mapImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                isDragging = true;
                startPoint = e.GetPosition(null);
                mapCanvas.CaptureMouse();
            }
        }

        private void mapImage_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point currentPoint = e.GetPosition(null); // Изменено на null

                // Рассчитываем относительные координаты мыши
                double relativeX = currentPoint.X - startPoint.X;
                double relativeY = currentPoint.Y - startPoint.Y;

                // Учтите текущий масштаб при вычислении смещения
                double scaledOffsetX = relativeX / scaleTransform.ScaleX;
                double scaledOffsetY = relativeY / scaleTransform.ScaleY;

                // Обновите позицию трансформации для перемещения изображения
                double newTranslateX = translateTransform.X + scaledOffsetX;
                double newTranslateY = translateTransform.Y + scaledOffsetY;

                this.translateTransform.X = newTranslateX;
                this.translateTransform.Y = newTranslateY;

                startPoint = currentPoint;
            }
        }

        private void mapImage_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (isDragging)
            {
                isDragging = false;
                mapCanvas.ReleaseMouseCapture();
            }
        }
    }
}