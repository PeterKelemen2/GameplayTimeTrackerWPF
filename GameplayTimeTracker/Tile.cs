using System.Windows;

namespace GameplayTimeTracker;

using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

public class Tile : UserControl
{
    private const int TextMargin = 10;
    private const int TitleFontSize = 15;
    private const int TextFontSize = 10;
    public double TileWidth { get; set; }
    public double TileHeight { get; set; }
    public double CornerRadius { get; set; }

    public string Text
    {
        get { return (string)GetValue(TextProperty); }
        set { SetValue(TextProperty, value); }
    }

    public static readonly DependencyProperty TextProperty =
        DependencyProperty.Register("Text", typeof(string), typeof(Tile), new PropertyMetadata(""));

    public Tile(double width, double height, double cornerRadius)
    {
        TileWidth = width;
        TileHeight = height;
        CornerRadius = cornerRadius;
        Text = "Sample text";

        InitializeTile();
    }

    private void InitializeTile()
    {
        // Create a Grid to hold the Rectangle and TextBlock
        Grid grid = new Grid();

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


        // Create a TextBlock for displaying text
        TextBlock textBlock = new TextBlock
        {
            Text = Text, // Bind to the Text property of the UserControl
            FontWeight = FontWeights.Bold,
            FontSize = TitleFontSize,
            HorizontalAlignment = HorizontalAlignment.Left,
            VerticalAlignment = VerticalAlignment.Top,
            Margin = new Thickness(TextMargin)
        };

        // Add the Rectangle and TextBlock to the Grid
        grid.Children.Add(rectangle);
        grid.Children.Add(textBlock);

        // Set the Grid as the content of the UserControl
        Content = grid;
    }
}