using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveChartsCore.SkiaSharpView.Drawing.Geometries;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore;
namespace Covid_19
{
    public class ViewModel
    {
        public ISeries[] Series { get; set; } = [
      new StackedAreaSeries<double>([3, 2, 3, 5, 3, 4, 6]),
        new StackedAreaSeries<double>([6, 5, 6, 3, 8, 5, 2]),
        new StackedAreaSeries<double>([4, 8, 2, 8, 9, 5, 3])
  ];
    }
}
