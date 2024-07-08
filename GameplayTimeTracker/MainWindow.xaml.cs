using System.Windows;
using System.Windows.Controls;

namespace GameplayTimeTracker
{
    public partial class MainWindow : Window
    {
        private const double Offset = 10;

        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            AddTilesToCanvas(20, CalculateTileWidth(), 150, 10);
        }

        private void AddTilesToCanvas(int tileCount, double tileWidth, double tileHeight, double cornerRadius)
        {
            for (int i = 0; i < tileCount; i++)
            {
                Tile tile = new Tile(tileWidth, tileHeight, cornerRadius);

                // Position the tile on the canvas
                Canvas.SetLeft(tile, Offset); // Fixed horizontal position with a margin of 10
                Canvas.SetTop(tile, Offset + i * (tileHeight + Offset)); // 10 is the gap between tiles

                // Add the tile to the canvas
                mainCanvas.Children.Add(tile);
            }

            mainCanvas.Height = Offset + tileCount * (tileHeight + 10);
        }

        private double CalculateTileWidth()
        {
            // double scrollbarWidth = SystemParameters.VerticalScrollBarWidth;
            // double width = ActualWidth - (2 * Offset + 2 * scrollbarWidth);

            return ActualWidth - (2 * Offset + 2 * SystemParameters.VerticalScrollBarWidth);
        }
    }
}