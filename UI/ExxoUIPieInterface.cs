using System.Linq;
using Microsoft.Xna.Framework;
using Terraria.UI;

namespace AvalonTesting.UI;

public class ExxoUIPieInterface : ExxoUIElement
{
    public readonly ExxoUIPieChart PieChart;
    private readonly ExxoUICircle backingCircle;

    public ExxoUIPieInterface()
    {
        backingCircle = new ExxoUICircle();
        backingCircle.Width.Set(0, 1);
        backingCircle.Height.Set(0, 1);
        Append(backingCircle);
        PieChart = new ExxoUIPieChart();
        PieChart.Width.Set(-10, 1);
        PieChart.Height.Set(-10, 1);
        PieChart.VAlign = UIAlign.Center;
        PieChart.HAlign = UIAlign.Center;
        backingCircle.Append(PieChart);
    }

    public override bool IsDynamicallySized => false;

    public override bool ContainsPoint(Vector2 point) => Children.Any(child => child.ContainsPoint(point));
}
