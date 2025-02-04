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
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;


namespace Covid_19
{
    public partial class MainWindow : Window
    {
        public ViewModel ViewModel { get; set; }
        private Random _random = new Random();
        private List<Ellipse> _points = new List<Ellipse>();
        private int chislo = 2;
        private const int PointCount = 500; // ���������� �����
        private const int Speed = 2; // �������� �������� �����
        private int radius = 10;

        public MainWindow()
        {
            InitializeComponent();
            ViewModel = new ViewModel();
            this.DataContext = ViewModel; // ������������� �������� ������ ��� ��������
            this.AttachDevTools();
            string colorHuman = "Blue";
            // ��������� ��������� �����
            int randomHuman = _random.Next(0, PointCount);
            for (int i = 0; i < PointCount; i++)
            {
                if (i == randomHuman)
                {
                    colorHuman = "Red";
                }
                else
                {
                    colorHuman = "Blue";
                }
                double x = _random.Next(0, 350); // ��������� X ����������
                double y = _random.Next(0, 350);
                var point = new Ellipse
                {
                    Width = 10,
                    Height = 10,
                    Fill = Brush.Parse(colorHuman)
                };
                _points.Add(point);
                Canvas.SetLeft(point, x);
                Canvas.SetTop(point, y);
                canvas.Children.Add(point);
            }

            StartMovingPoints();
        }

        private void StartMovingPoints()
        {
            // ������ ��� ���������� ������� �����
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
                // ��������� ����������� ��� ������ �����
                var direction = _random.Next(0, 4); // 0 - ������, 1 - �����, 2 - ����, 3 - �����

                double newX = Canvas.GetLeft(point);
                double newY = Canvas.GetTop(point);

                switch (direction)
                {
                    case 0: // ������
                        if (newX < 350) newX += Speed;
                        break;
                    case 1: // �����
                        if (newX > 0) newX -= Speed;
                        break;
                    case 2: // ����
                        if (newY < 350) newY += Speed;
                        break;
                    case 3: // �����
                        if (newY > 0) newY -= Speed;
                        break;
                }

                // ������� ���������� �������
                var animationX = new Avalonia.Animation.KeyFrame
                {
                    KeyTime = TimeSpan.FromMilliseconds(500),
                    Setters = { new Setter(WidthProperty, 600)}

                };

                var animationY = new Avalonia.Animation.KeyFrame
                {
                    KeyTime = TimeSpan.FromMilliseconds(500),
                    Setters = { new Setter(HeightProperty, 500) }

                };

                Canvas.SetLeft(point, newX);
                Canvas.SetTop(point, newY);
            } 
        }
        
    }
}