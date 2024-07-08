using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GameplayTimeTracker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            AddTilesToCanvas(5, 200, 100, 10); // Add 5 tiles with width 200, height 100, and corner radius 10
        }

        private void AddTilesToCanvas(int tileCount, double tileWidth, double tileHeight, double cornerRadius)
        {
            double offset = 10;

            for (int i = 0; i < tileCount; i++)
            {
                Tile tile = new Tile(tileWidth, tileHeight, cornerRadius);

                // Position the tile on the canvas
                Canvas.SetLeft(tile, offset); // Fixed horizontal position with a margin of 10
                Canvas.SetTop(tile, offset + i * (tileHeight + offset)); // 10 is the gap between tiles

                // Add the tile to the canvas
                mainCanvas.Children.Add(tile);
            }
        }
    }
}