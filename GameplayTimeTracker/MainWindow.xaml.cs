using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace GameplayTimeTracker
{
    public partial class MainWindow : Window
    {
        private const double Offset = 10;
        TileContainer tileContainer = new();

        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // AddTilesToCanvas(20, CalculateTileWidth(), 150, 10);
            tileContainer.AddTile(new Tile("Game1", 120, 40));
            tileContainer.AddTile(new Tile("Game2", 300, 20));
            tileContainer.AddTile(new Tile("Game3", 50, 25));
            // tileContainer.AddTile(new Tile("Game4", 60, 15));
            tileContainer.ListTiles();

            tileContainer.RemoveTileById(2);
            tileContainer.ListTiles();

            tileContainer.AddTile(new Tile("GameX"));

            tileContainer.ListTiles();

            tileContainer.UpdateTileById(1, "Text", "New Value");
            tileContainer.ListTiles();
            ShowTilesOnCanvas();
        }

        private void ShowTilesOnCanvas()
        {
            var tilesList = tileContainer.GetTiles();
            
            // Accounting for the appearance of the scrollbar
            if (mainCanvas.ActualHeight < Offset + tileContainer.GetListCount() * (tilesList[0].TileHeight + 10))
            {
                mainCanvas.Height = Offset + tileContainer.GetListCount() * (tilesList[0].TileHeight + 10);
                foreach (var tile in tilesList)
                {
                    tile.TileWidth = CalculateTileWidth(true);
                    tile.InitializeTile();
                }
            }
            else
            {
                foreach (var tile in tilesList)
                {
                    tile.TileWidth = CalculateTileWidth(false);
                    tile.InitializeTile();
                }
            }

            for (int i = 0; i < tileContainer.GetListCount(); i++)
            {
                // Console.WriteLine(tilesList[i].TotalPlaytimePercent);
                Canvas.SetLeft(tilesList[i], Offset); // Fixed horizontal position with a margin of 10
                Canvas.SetTop(tilesList[i],
                    Offset + i * (tilesList[i].TileHeight + Offset)); // 10 is the gap between tiles
                mainCanvas.Children.Add(tilesList[i]);
            }
        }

        private double CalculateTileWidth(bool isScrollbarPresent)
        {
            if (isScrollbarPresent)
            {
                return ActualWidth - (2 * Offset + 2 * SystemParameters.VerticalScrollBarWidth);
            }

            return ActualWidth - (2 * Offset + SystemParameters.VerticalScrollBarWidth);
        }
    }
}