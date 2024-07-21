using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Media.Animation;


namespace GameplayTimeTracker;

using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

public class GradientBar : UserControl
{
    public double Width { get; set; }
    public double Height { get; set; }
    public double Percent { get; set; }
    public Color Color1 { get; set; }
    public Color Color2 { get; set; }
    public Color BgColor { get; set; }
    public double Padding { get; set; }
    public double Radius { get; set; }


    public GradientBar(double width, double height, double percent, Color color1, Color color2, Color bgColor,
        double padding = 5,
        double radius = 10)
    {
        // Width = width;
        Width = width;
        Height = height;
        Percent = percent;
        Color1 = color1;
        Color2 = color2;
        BgColor = bgColor;
        Padding = padding;
        Radius = radius;
        InitializeBar();
    }

    private void InitializeBar()
    {
        Grid grid = new Grid();

        LinearGradientBrush gradientBrush = new LinearGradientBrush();
        gradientBrush.StartPoint = new Point(0, 0);
        gradientBrush.EndPoint = new Point(1, 0);
        gradientBrush.GradientStops.Add(new GradientStop(Color1, 0.0));
        gradientBrush.GradientStops.Add(new GradientStop(Color2, 1.0));

        Rectangle barBackground = new Rectangle
        {
            Width = Width,
            Height = Height,
            RadiusX = Radius,
            RadiusY = Radius,
            Fill = new SolidColorBrush(BgColor)
        };

        Rectangle barForeground = new Rectangle
        {
            Width = (Width - 2 * Padding) * Percent,
            Height = Height - 2 * Padding,
            RadiusX = Radius - Padding / 2,
            RadiusY = Radius - Padding / 2,
            Fill = gradientBrush,
            HorizontalAlignment = HorizontalAlignment.Left,
            Margin = new Thickness(Padding, 0, 0, 0)
        };

        grid.Children.Add(barBackground);
        grid.Children.Add(barForeground);

        Content = grid;
    }
}

public class Tile : UserControl
{
    private const int TextMargin = 10;
    private const int TitleFontSize = 17;
    private const int TextFontSize = 14;
    private const double Height = 150;
    private const double BorderRadius = 10;
    private const int MenuTopMargin = -20;
    private bool isMenuOpen = true;

    private Rectangle menuRectangle;

    private const string SampleImagePath = "/assets/fallout3.png";

    Color DarkColor = (Color)ColorConverter.ConvertFromString("#1E2030");
    Color LightColor = (Color)ColorConverter.ConvertFromString("#2E324A");
    Color FontColor = (Color)ColorConverter.ConvertFromString("#C1C9FF");
    Color LeftColor = (Color)ColorConverter.ConvertFromString("#89ACF2");
    Color RightColor = (Color)ColorConverter.ConvertFromString("#B7BDF8");

    public int Id { get; set; }
    public double TileWidth { get; set; }
    public double TileHeight { get; set; }
    public double CornerRadius { get; set; }

    public double TotalPlaytime { get; set; }
    public double TotalPlaytimePercent { get; set; }
    public double LastPlaytime { get; set; }
    public double LastPlaytimePercent { get; set; }

    private double CalculateTileWidth()
    {
        // double scrollbarWidth = SystemParameters.VerticalScrollBarWidth;
        // double width = ActualWidth - (2 * Offset + 2 * scrollbarWidth);

        return ActualWidth - (2 * 10 + 2 * SystemParameters.VerticalScrollBarWidth); // Change 10 to var
    }

    public bool getMenuOpen()
    {
        return isMenuOpen;
    }

    private void ToggleEdit(object sender, RoutedEventArgs e)
    {
        isMenuOpen = !isMenuOpen;
        double animationDuration = 0.15;
        DoubleAnimation heightAnimation = new DoubleAnimation
        {
            From = isMenuOpen ? 0 : TileHeight,
            To = isMenuOpen ? TileHeight : 0,
            Duration = new Duration(TimeSpan.FromSeconds(animationDuration))
        };

        DoubleAnimation opacityAnimation = new DoubleAnimation
        {
            From = isMenuOpen ? 0 : 1,
            To = isMenuOpen ? 1 : 0,
            Duration = new Duration(TimeSpan.FromSeconds(animationDuration))
        };

        heightAnimation.Completed += (s, a) =>
        {
            if (!isMenuOpen)
            {
                menuRectangle.Visibility = Visibility.Collapsed;
            }
            else
            {
                menuRectangle.Visibility = Visibility.Visible;
            }
        };

        // Set the visibility to visible before starting the animation if we are opening the menu
        if (isMenuOpen)
        {
            menuRectangle.Visibility = Visibility.Visible;
        }

        // Apply the animations to the menuRectangle
        menuRectangle.BeginAnimation(Rectangle.HeightProperty, heightAnimation);
        menuRectangle.BeginAnimation(Rectangle.OpacityProperty, opacityAnimation);

        Console.WriteLine(isMenuOpen);
    }

    public double GetTotalHeight()
    {
        if (isMenuOpen)
        {
            return TileHeight + TileHeight - MenuTopMargin;
        }

        return TileHeight;
    }

    public string Text
    {
        get { return (string)GetValue(TextProperty); }
        set { SetValue(TextProperty, value); }
    }


    public static readonly DependencyProperty TextProperty =
        DependencyProperty.Register("Text", typeof(string), typeof(Tile), new PropertyMetadata(""));

    public Tile(string text, double totalTime = 20, double lastPlayedTime = 10, double width = 740)
    {
        TileWidth = width;
        TileHeight = Height;
        CornerRadius = BorderRadius;
        TotalPlaytime = totalTime;
        LastPlaytime = lastPlayedTime;
        LastPlaytimePercent = Math.Round(LastPlaytime / TotalPlaytime, 2);
        Text = text;

        Stopwatch stopwatch = Stopwatch.StartNew();
        InitializeTile();
        stopwatch.Stop();
        // Console.WriteLine($"Tile initialization time: {stopwatch.Elapsed.TotalNanoseconds / 1000}");
    }

    public void InitializeTile()
    {
        // Create a Grid to hold the Rectangle and TextBlock
        Grid grid = new Grid();

        // Define the grid rows
        RowDefinition row1 = new RowDefinition();
        RowDefinition row2 = new RowDefinition();
        grid.RowDefinitions.Add(row1);
        grid.RowDefinitions.Add(row2);

        // Create the first Rectangle
        menuRectangle = new Rectangle
        {
            Width = TileWidth - 30,
            Height = TileHeight,
            RadiusX = CornerRadius,
            RadiusY = CornerRadius,
            Fill = new SolidColorBrush(RightColor),
            Margin = new Thickness(0, MenuTopMargin, 0, 0)
        };

        // Create the second Rectangle
        Rectangle container = new Rectangle
        {
            Width = TileWidth,
            Height = TileHeight,
            RadiusX = CornerRadius,
            RadiusY = CornerRadius,
            Fill = new SolidColorBrush(LightColor),
        };

        Button editButton = new Button
        {
            Style = (Style)Application.Current.FindResource("RoundedButtonEdit"),
            Height = 40,
            Width = 40,
            HorizontalAlignment = HorizontalAlignment.Right,
            VerticalAlignment = VerticalAlignment.Center,
            Margin = new Thickness(0, 0, 100, 0),
        };
        editButton.Click += ToggleEdit;

        Button removeButton = new Button
        {
            Style = (Style)Application.Current.FindResource("RoundedButtonRemove"),
            Height = 40,
            Width = 40,
            HorizontalAlignment = HorizontalAlignment.Right,
            VerticalAlignment = VerticalAlignment.Center,
            Margin = new Thickness(0, 0, 50, 0),
        };

        // Place the rectangles in separate rows
        if (isMenuOpen)
        {
            Grid.SetRow(menuRectangle, 1);
            grid.Children.Add(menuRectangle);
        }

        Grid.SetRow(container, 0);
        Grid.SetRow(editButton, 0);
        Grid.SetRow(removeButton, 0);
        grid.Children.Add(container);
        grid.Children.Add(editButton);
        grid.Children.Add(removeButton);

        // Create a TextBlock for displaying text
        TextBlock titleTextBlock = new TextBlock
        {
            Text = Text,
            FontWeight = FontWeights.Bold,
            FontSize = TitleFontSize,
            Foreground = new SolidColorBrush(FontColor),
            HorizontalAlignment = HorizontalAlignment.Left,
            VerticalAlignment = VerticalAlignment.Top,
            Margin = new Thickness(TextMargin, TextMargin / 2, 0, 0)
        };

        // Add the TextBlock to the grid
        Grid.SetRow(titleTextBlock, 0); // Position it in the second row with the container
        grid.Children.Add(titleTextBlock);

        // Create the Image and other UI elements, positioning them in the second row as well
        Image image = new Image
        {
            Source = new BitmapImage(new Uri(SampleImagePath, UriKind.Relative)),
            Stretch = Stretch.UniformToFill,
            Width = TileHeight / 2,
            Height = TileHeight / 2,
            HorizontalAlignment = HorizontalAlignment.Left,
            VerticalAlignment = VerticalAlignment.Top,
            Margin = new Thickness(10, TileHeight / 2 - TitleFontSize - TextMargin, 0, 0),
        };
        RenderOptions.SetBitmapScalingMode(image, BitmapScalingMode.HighQuality);

        // Add all other elements as before, positioning them in the second row
        Grid.SetRow(image, 0);
        grid.Children.Add(image);

        // Add playtime elements
        // (similar changes, placing them in the appropriate row)
        Random random = new Random();
        TextBlock totalPlaytimeTitle = new TextBlock
        {
            Text = "Total Playtime:",
            FontWeight = FontWeights.Bold,
            FontSize = TextFontSize,
            Foreground = new SolidColorBrush(FontColor),
            HorizontalAlignment = HorizontalAlignment.Left,
            VerticalAlignment = VerticalAlignment.Top,
            Margin = new Thickness(TextMargin + TileHeight + 20, TileHeight / 2 - TitleFontSize - TextMargin - 10, 0, 0)
        };

        TextBlock totalPlaytime = new TextBlock
        {
            Text = $"{TotalPlaytime}m",
            FontWeight = FontWeights.Normal,
            FontSize = TextFontSize,
            Foreground = new SolidColorBrush(FontColor),
            HorizontalAlignment = HorizontalAlignment.Left,
            VerticalAlignment = VerticalAlignment.Top,
            Margin = new Thickness(TextMargin + TileHeight + 20, TileHeight / 2 - TitleFontSize - TextMargin + 15, 0, 0)
        };

        GradientBar totalTimeGradientBar = new GradientBar(
            width: 150,
            height: 30,
            percent: TotalPlaytimePercent,
            color1: LeftColor,
            color2: RightColor,
            bgColor: DarkColor
        )
        {
            HorizontalAlignment = HorizontalAlignment.Left,
            VerticalAlignment = VerticalAlignment.Top,
            Margin = new Thickness(TextMargin + TileHeight + 20, TileHeight / 2 - TitleFontSize - TextMargin + 40, 0, 0)
        };

        TextBlock lastPlaytimeTitle = new TextBlock
        {
            Text = "Last Playtime:",
            FontWeight = FontWeights.Bold,
            FontSize = TextFontSize,
            Foreground = new SolidColorBrush(FontColor),
            HorizontalAlignment = HorizontalAlignment.Left,
            VerticalAlignment = VerticalAlignment.Top,
            Margin = new Thickness((TextMargin + TileHeight + 20) * 2.3,
                TileHeight / 2 - TitleFontSize - TextMargin - 10, 0, 0)
        };

        TextBlock lastPlaytime = new TextBlock
        {
            Text = $"{LastPlaytime}m",
            FontWeight = FontWeights.Normal,
            FontSize = TextFontSize,
            Foreground = new SolidColorBrush(FontColor),
            HorizontalAlignment = HorizontalAlignment.Left,
            VerticalAlignment = VerticalAlignment.Top,
            Margin = new Thickness((TextMargin + TileHeight + 20) * 2.3,
                TileHeight / 2 - TitleFontSize - TextMargin + 15, 0, 0)
        };

        GradientBar lastTimeGradientBar = new GradientBar(
            width: 150,
            height: 30,
            percent: LastPlaytimePercent,
            color1: LeftColor,
            color2: RightColor,
            bgColor: DarkColor
        )
        {
            HorizontalAlignment = HorizontalAlignment.Left,
            VerticalAlignment = VerticalAlignment.Top,
            Margin = new Thickness((TextMargin + TileHeight + 20) * 2.3,
                TileHeight / 2 - TitleFontSize - TextMargin + 40, 0, 0)
        };

        Grid.SetRow(totalPlaytimeTitle, 0);
        Grid.SetRow(totalPlaytime, 0);
        Grid.SetRow(totalTimeGradientBar, 0);
        Grid.SetRow(lastPlaytimeTitle, 0);
        Grid.SetRow(lastPlaytime, 0);
        Grid.SetRow(lastTimeGradientBar, 0);

        grid.Children.Add(totalPlaytimeTitle);
        grid.Children.Add(totalPlaytime);
        grid.Children.Add(totalTimeGradientBar);
        grid.Children.Add(lastPlaytimeTitle);
        grid.Children.Add(lastPlaytime);
        grid.Children.Add(lastTimeGradientBar);

        // Set the Grid as the content of the UserControl
        Content = grid;
    }
}