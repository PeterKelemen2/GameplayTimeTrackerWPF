namespace GameplayTimeTracker;

using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

public class Tile : UserControl
{
    public double TileWidth { get; set; }
    public double TileHeight { get; set; }
    public double CornerRadius { get; set; }

    public Tile(double width, double height, double cornerRadius)
    {
        TileWidth = width;
        TileHeight = height;
        CornerRadius = cornerRadius;

        InitializeTile();
    }

    private void InitializeTile()
    {
        // Create a Rectangle with rounded corners
        Rectangle rectangle = new Rectangle
        {
            Width = TileWidth,
            Height = TileHeight,
            RadiusX = CornerRadius,
            RadiusY = CornerRadius,
            Fill = new SolidColorBrush(Colors.LightGray), // Set the fill color
            Stroke = new SolidColorBrush(Colors.Black), // Optional: set the border color
            StrokeThickness = 1 // Optional: set the border thickness
        };

        // Add the rectangle to the UserControl's content
        Content = rectangle;
    }
}