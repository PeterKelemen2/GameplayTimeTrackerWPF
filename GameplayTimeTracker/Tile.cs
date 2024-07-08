using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Media.Imaging;

namespace GameplayTimeTracker;

using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

public class Tile : UserControl
{
    private const int TextMargin = 10;
    private const int TitleFontSize = 17;
    private const int TextFontSize = 10;
    private const string SampleImagePath = "/assets/fallout3.png";
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
        Text = "Fallout 3";

        Stopwatch stopwatch = Stopwatch.StartNew();
        InitializeTile();
        stopwatch.Stop();
        Console.WriteLine($"Tile initialization time: {stopwatch.Elapsed.TotalNanoseconds / 1000}");
    }

    private void InitializeTile()
    {
        // Create a Grid to hold the Rectangle and TextBlock
        Grid grid = new Grid();

        // Create a Rectangle with rounded corners
        Rectangle container = new Rectangle
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
        TextBlock titleTextBlock = new TextBlock
        {
            Text = Text, // Bind to the Text property of the UserControl
            FontWeight = FontWeights.Bold,
            FontSize = TitleFontSize,
            HorizontalAlignment = HorizontalAlignment.Left,
            VerticalAlignment = VerticalAlignment.Top,
            Margin = new Thickness(TextMargin, TextMargin / 2, 0, 0)
        };

        Image image = new Image
        {
            Source = new BitmapImage(new Uri(SampleImagePath, UriKind.Relative)),
            Stretch = Stretch.UniformToFill,
            Width = TileHeight / 2,
            Height = TileHeight / 2,
            HorizontalAlignment = HorizontalAlignment.Left,
            VerticalAlignment = VerticalAlignment.Center,
            Margin = new Thickness(10, 20, 0, 0),
        };
        RenderOptions.SetBitmapScalingMode(image, BitmapScalingMode.HighQuality);

        // Add the Rectangle and TextBlock to the Grid
        grid.Children.Add(container);
        grid.Children.Add(titleTextBlock);
        grid.Children.Add(image);

        // Set the Grid as the content of the UserControl
        Content = grid;
    }
}