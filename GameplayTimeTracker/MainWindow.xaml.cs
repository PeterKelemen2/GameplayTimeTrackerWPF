using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace GameplayTimeTracker
{
    public partial class MainWindow : Window
    {
        private const double Offset = 10;
        TileContainer tileContainer = new TileContainer();

        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
            // TileContainer tileContainer = new TileContainer();
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // AddTilesToCanvas(20, CalculateTileWidth(), 150, 10);
            tileContainer.AddTile(new Tile(123, 342, 10, "Game1", 120, 40));
            tileContainer.AddTile(new Tile(123, 342, 10, "Game2", 300, 20));
            tileContainer.AddTile(new Tile(123, 342, 10, "Game3", 50, 10));
            // tileContainer.AddTile(new Tile(123, 342, 10, "Game4", 50, 10));
            tileContainer.ListTiles();

            tileContainer.RemoveTileById(2);
            tileContainer.ListTiles();

            tileContainer.AddTile(new Tile(123, 342, 10, "GameX"));

            tileContainer.ListTiles();

            tileContainer.UpdateTileById(1, "Text", "New Value");
            tileContainer.ListTiles();
            ShowTilesOnCanvas();
        }

        private void ShowTilesOnCanvas()
        {
            // Console.WriteLine(mainCanvas.ActualHeight);
            var tilesList = tileContainer.GetTiles();
            for (int i = 0; i < tileContainer.GetListCount(); i++)
            {
                Canvas.SetLeft(tilesList[i], Offset); // Fixed horizontal position with a margin of 10
                Canvas.SetTop(tilesList[i],
                    Offset + i * (tilesList[i].TileHeight + Offset)); // 10 is the gap between tiles
                mainCanvas.Children.Add(tilesList[i]);
            }

            // Console.WriteLine(Offset + tileContainer.GetListCount() * (150 + 10));
            if (mainCanvas.ActualHeight < Offset + tileContainer.GetListCount() * (150 + 10))
            {
                mainCanvas.Height = Offset + tileContainer.GetListCount() * (150 + 10);
            }
        }

        // private void AddTilesToCanvas(int tileCount, double tileWidth, double tileHeight, double cornerRadius)
        // {
        //     for (int i = 0; i < tileCount; i++)
        //     {
        //         Tile tile = new Tile(tileWidth, tileHeight, cornerRadius);
        //
        //         // Position the tile on the canvas
        //         Canvas.SetLeft(tile, Offset); // Fixed horizontal position with a margin of 10
        //         Canvas.SetTop(tile, Offset + i * (tileHeight + Offset)); // 10 is the gap between tiles
        //
        //         // Add the tile to the canvas
        //         mainCanvas.Children.Add(tile);
        //     }
        //
        //     mainCanvas.Height = Offset + tileCount * (tileHeight + 10);
        // }

        private double CalculateTileWidth()
        {
            // double scrollbarWidth = SystemParameters.VerticalScrollBarWidth;
            // double width = ActualWidth - (2 * Offset + 2 * scrollbarWidth);

            return ActualWidth - (2 * Offset + 2 * SystemParameters.VerticalScrollBarWidth);
        }
    }
}