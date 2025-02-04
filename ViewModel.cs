using System;
using LiveChartsCore;
using LiveChartsCore.Defaults;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveChartsCore.SkiaSharpView.Drawing.Geometries;
using LiveChartsCore.SkiaSharpView;
using System.ComponentModel;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Avalonia;
using LiveChartsCore.Kernel;
using ReactiveUI;

namespace Covid_19
{

    public class ViewModel : ReactiveObject
    {
        private ObservableCollection<Human> _healthData;
        private ObservableCollection<ChartPoint> _sickData;
        private ObservableCollection<ChartPoint> _healthyData;
        private ObservableCollection<ChartPoint> _recoveredData;

        public ViewModel()
        {
            _healthData = new ObservableCollection<Human>();
            _sickData = new ObservableCollection<ChartPoint>();
            _healthyData = new ObservableCollection<ChartPoint>();
            _recoveredData = new ObservableCollection<ChartPoint>();

            // Инициализация с пустыми данными
            _healthData.Add(new Human { Date = DateTime.Now, Susceptible = 100, Infected = 900, Removed = 0 });

            UpdateChartData();

            // Таймер для имитации изменения данных
            Task.Run(() =>
            {
                while (true)
                {
                    // Симулируем изменения количества больных
                    UpdateHealthData();
                    UpdateChartData();
                    Task.Delay(1000).Wait(); // Обновление данных каждую секунду
                }
            });
        }

        public ObservableCollection<ChartPoint> SickData => _sickData;
        public ObservableCollection<ChartPoint> HealthyData => _healthyData;
        public ObservableCollection<ChartPoint> RecoveredData => _recoveredData;

        private void UpdateHealthData()
        {
            var lastData = _healthData.Last();
            var newSick = lastData.Susceptible + 5;  // Увеличиваем количество больных
            var newRecovered = lastData.Infected + 2;  // Увеличиваем количество переболевших
            var newHealthy = lastData.Removed - 5;  // Уменьшаем количество здоровых

            _healthData.Add(new Human
            {
                Date = DateTime.Now,
                Susceptible = newSick,
                Infected = newHealthy,
                Removed = newRecovered
            });
        }

        private void UpdateChartData()
        {
            // Очистим старые данные
            _sickData.Clear();
            _healthyData.Clear();
            _recoveredData.Clear();

            // Заполним новые данные
            foreach (var data in _healthData)
            {
                _sickData.Add(new ChartPoint(data.Susceptible, 0));
                _healthyData.Add(new ChartPoint(data.Infected, 0));
                _recoveredData.Add(new ChartPoint(data.Removed, 0));
            }
        }
    }

}
