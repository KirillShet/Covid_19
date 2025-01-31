using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Input;
using Avalonia;
using Avalonia.Markup.Xaml;
using System;
using Avalonia.Threading;
using System.Timers;
using Avalonia.Interactivity;
using Avalonia.Media;
using System.Collections.Generic;
using Avalonia.Animation;
using System.Linq;
using Avalonia.Styling;


namespace Covid_19
{
    public partial class MainWindow : Window
    {
        private Random _random = new Random();
        private List<Ellipse> _points = new List<Ellipse>();
        private const int PointCount = 1000; // Количество точек
        private const int Speed = 2; // Скорость движения точек

        public MainWindow()
        {
            InitializeComponent();
            this.AttachDevTools();

            // Генерация случайных точек
            for (int i = 0; i < PointCount; i++)
            {
                var point = new Ellipse
                {
                    Width = 10,
                    Height = 10,
                    Fill = new SolidColorBrush(GetRandomColor())
                };
                _points.Add(point);
                canvas.Children.Add(point);
            }

            StartMovingPoints();
        }

        private void StartMovingPoints()
        {
            // Таймер для обновления позиции точек
            var timer = new Avalonia.Threading.DispatcherTimer();
            timer.Tick += (s, e) =>
            {
                MovePoints();
            };
            timer.Start();
        }

        private void MovePoints()
        {
            foreach (var point in _points)
            {
                // Случайное направление для каждой точки
                var direction = _random.Next(0, 4); // 0 - вправо, 1 - влево, 2 - вниз, 3 - вверх
                var currentPosition = point.Margin;

                double newX = currentPosition.Left;
                double newY = currentPosition.Top;

                switch (direction)
                {
                    case 0: // Вправо
                        if (newX < 290) newX += Speed;
                        break;
                    case 1: // Влево
                        if (newX > 0) newX -= Speed;
                        break;
                    case 2: // Вниз
                        if (newY < 290) newY += Speed;
                        break;
                    case 3: // Вверх
                        if (newY > 0) newY -= Speed;
                        break;
                }

                // Плавное обновление позиции
                var animationX = new Avalonia.Animation.KeyFrame
                {
                    KeyTime = TimeSpan.FromMilliseconds(1000),
                    Setters = { new Setter(WidthProperty, 200)}

                };

                var animationY = new Avalonia.Animation.KeyFrame
                {
                    KeyTime = TimeSpan.FromMilliseconds(1000),
                    Setters = { new Setter(HeightProperty, 100) }

                };

               /* var animation = new Avalonia.Animation.KeyFrameAnimation
                {
                    KeyFrames = new List<Avalonia.Animation.KeyFrame> { animationX, animationY }
                };*/

                point.Margin = new Avalonia.Thickness(newX, newY, 0, 0);
            }
        }

        private Color GetRandomColor()
        {
            return Color.FromArgb(
                255,
                (byte)_random.Next(0, 256),
                (byte)_random.Next(0, 256),
                (byte)_random.Next(0, 256)
            );
        }
    }
}